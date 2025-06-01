using Mapster;
using Microsoft.EntityFrameworkCore;
using TallerIdentity.Application.Abstractions.Messaging;
using TallerIdentity.Application.Commons.Bases;
using TallerIdentity.Application.Dtos.Users;
using TallerIdentity.Application.Interfaces.Services;
using Helper = TallerIdentity.Application.Helpers.Helpers;

namespace TallerIdentity.Application.UseCases.Users.Queries.GetAllUser;

internal sealed class GetAllUserHandler(IUnitOfWork unitOfWork, IOrderingQuery orderingQuery)
    : IQueryHandler<GetAllUserQuery, IEnumerable<UserResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IOrderingQuery _orderingQuery = orderingQuery;

    public async Task<BaseResponse<IEnumerable<UserResponseDto>>> Handle(GetAllUserQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<UserResponseDto>>();

        try
        {
            var users = _unitOfWork.User.GetAllQueryable();

            if (query.NumFilter is not null && !string.IsNullOrEmpty(query.TextFilter))
            {
                switch (query.NumFilter)
                {
                    case 1:
                        users = users.Where(u => u.FirstName.Contains(query.TextFilter));
                        break;
                    case 2:
                        users = users.Where(u => u.LastName.Contains(query.TextFilter));
                        break;
                    case 3:
                        users = users.Where(u => u.Email.Contains(query.TextFilter));
                        break;
                }
            }

            if (query.StateFilter is not null)
            {
                var stateFilter = Helper.SplitStateFilter(query.StateFilter);
                users = users.Where(x => stateFilter.Contains(x.State));
            }

            query.Sort ??= "Id";

            var items = await _orderingQuery.Ordering(query, users)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await users.CountAsync(cancellationToken);
            response.Data = items.Adapt<IEnumerable<UserResponseDto>>();
            response.Message = "Consulta exitosa";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }

        return response;
    }
}

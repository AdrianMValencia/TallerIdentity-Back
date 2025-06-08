using Mapster;
using Microsoft.EntityFrameworkCore;
using TallerIdentity.Application.Abstractions.Messaging;
using TallerIdentity.Application.Commons.Bases;
using TallerIdentity.Application.Dtos.UserRoles;
using TallerIdentity.Application.Interfaces.Services;
using Helper = TallerIdentity.Application.Helpers.Helpers;

namespace TallerIdentity.Application.UseCases.UserRoles.Queries.GetAllUserRole;

internal sealed class GetAllUserRoleHandler(IUnitOfWork unitOfWork, IOrderingQuery orderingQuery)
    : IQueryHandler<GetAllUserRoleQuery, IEnumerable<UserRoleResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IOrderingQuery _orderingQuery = orderingQuery;

    public async Task<BaseResponse<IEnumerable<UserRoleResponseDto>>> Handle
        (GetAllUserRoleQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<UserRoleResponseDto>>();

        try
        {
            var userRoles = _unitOfWork.UserRole
                .GetAllQueryable()
                .Include(x => x.User)
                .Include(x => x.Role)
                .AsQueryable();

            if (query.NumFilter is not null && !string.IsNullOrEmpty(query.TextFilter))
            {
                switch (query.NumFilter)
                {
                    case 1:
                        userRoles = userRoles.Where(x => x.User.FirstName.Contains(query.TextFilter));
                        break;
                    case 2:
                        userRoles = userRoles.Where(x => x.Role.Name.Contains(query.TextFilter));
                        break;
                }
            }

            if (query.StateFilter is not null)
            {
                var stateFilter = Helper.SplitStateFilter(query.StateFilter);
                userRoles = userRoles.Where(x => stateFilter.Contains(x.State));
            }

            query.Sort ??= "Id";

            var items = await _orderingQuery.Ordering(query, userRoles)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await userRoles.CountAsync(cancellationToken);
            response.Data = items.Adapt<IEnumerable<UserRoleResponseDto>>();
            response.Message = "Consulta exitosa.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }

        return response;
    }
}

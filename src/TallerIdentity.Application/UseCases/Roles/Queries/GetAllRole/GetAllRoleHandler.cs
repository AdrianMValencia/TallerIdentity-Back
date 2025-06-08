using Mapster;
using Microsoft.EntityFrameworkCore;
using TallerIdentity.Application.Abstractions.Messaging;
using TallerIdentity.Application.Commons.Bases;
using TallerIdentity.Application.Dtos.Roles;
using TallerIdentity.Application.Interfaces.Services;
using Helper = TallerIdentity.Application.Helpers.Helpers;

namespace TallerIdentity.Application.UseCases.Roles.Queries.GetAllRole;

internal sealed class GetAllRoleHandler(IUnitOfWork unitOfWork, IOrderingQuery orderingQuery)
    : IQueryHandler<GetAllRoleQuery, IEnumerable<RoleResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IOrderingQuery _orderingQuery = orderingQuery;

    public async Task<BaseResponse<IEnumerable<RoleResponseDto>>> Handle
        (GetAllRoleQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<RoleResponseDto>>();

        try
        {
            var roles = _unitOfWork.Role.GetAllQueryable();

            if (query.NumFilter is not null && !string.IsNullOrEmpty(query.TextFilter))
            {
                switch (query.NumFilter)
                {
                    case 1:
                        roles = roles.Where(x => x.Name.Contains(query.TextFilter));
                        break;
                    case 2:
                        roles = roles.Where(x => x.Description!.Contains(query.TextFilter));
                        break;
                }
            }

            if (query.StateFilter is not null)
            {
                var stateFilter = Helper.SplitStateFilter(query.StateFilter);
                roles = roles.Where(x => stateFilter.Contains(x.State));
            }

            query.Sort ??= "Id";

            var items = await _orderingQuery.Ordering(query, roles)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await roles.CountAsync(cancellationToken);
            response.Data = items.Adapt<IEnumerable<RoleResponseDto>>();
            response.Message = "Consulta exitosa.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Error al consultar los roles: {ex.Message}";
        }

        return response;
    }
}

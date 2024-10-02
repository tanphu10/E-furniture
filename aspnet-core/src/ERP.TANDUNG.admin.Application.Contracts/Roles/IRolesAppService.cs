using ERP.TANDUNG.Admin.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.PermissionManagement;

namespace ERP.TANDUNG.Admin.Roles
{
    public interface IRolesAppService : ICrudAppService<RoleDto, Guid, PagedResultRequestDto, CreateUpdateRoleDto, CreateUpdateRoleDto>
    {
        Task<PagedResultDto<RoleInListDto>> GetListFilterAsync(BaseListFilterDto input);
        Task<List<RoleInListDto>> GetListAllAsync();
        Task DeleteMultipleAsync(IEnumerable<Guid> ids);
        Task<GetPermissionListResultDto> GetPermissionAsync(string providerName, string providerKey);
        Task UpdatePermissionAsync(string providerName, string providerKey, UpdatePermissionsDto input);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ERP.TANDUNG.Admin.System.Users
{
    public interface IUsersAppService:ICrudAppService<UserDto,Guid,PagedResultRequestDto,CreateUserDto,UpdateUserDto>
    {
        Task DeleteMultipleAsync(IEnumerable<Guid> ids);
        Task<List<UserInListDto>> GetListAllAsync(string filterKeyword);
        Task<PagedResultDto<UserInListDto>> GetListFilterAsync(BaseListFilterDto input);

    }
}

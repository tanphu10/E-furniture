using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ERP.TANDUNG.Admin.Products
{
    public interface IProductsAppService:ICrudAppService<
        ProductDto,Guid,PagedResultRequestDto,
        CreateUpdateProductDto, 
        CreateUpdateProductDto>
    {
        Task<PagedResultDto<ProductInListDto>> GetListFilterProductAsync(BaseListFilterDto input);
        Task<List<ProductInListDto>> GetListAllProductAsync();
        Task DeleteMultipleProductAsync(IEnumerable<Guid> ids);

    }
}

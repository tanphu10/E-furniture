﻿using ERP.TANDUNG.Admin.Catalog.Products.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ERP.TANDUNG.Admin.Catalog.Products
{
    public interface IProductsAppService : ICrudAppService<
        ProductDto, Guid, PagedResultRequestDto,
        CreateUpdateProductDto,
        CreateUpdateProductDto>
    {
        Task<PagedResultDto<ProductInListDto>> GetListFilterAsync(ProductListFilterDto input);
        Task<List<ProductInListDto>> GetListAllAsync();
        Task DeleteMultipleAsync(IEnumerable<Guid> ids);
        Task<string> GetThumbnailImageAsync(string fileName);
        Task<string> GetSuggestNewCodeAsync();
        Task<ProductAttributeValueDto> AddProductAttributeAsync(AddUpdateProductAttributeDto input);
        Task<ProductAttributeValueDto> UpdateProductAttributeAsync(Guid id, AddUpdateProductAttributeDto input);
        Task RemoveProductAttributeAsync(Guid attributeId, Guid id);
        Task<List<ProductAttributeValueDto>> GetListProductAttributeAllAsync(Guid id);
        //Task<PagedResultDto<ProductAttributeValueDto>> GetListProductAttributeAsync(ProductAttributeListFilterDto productId);

    }
}

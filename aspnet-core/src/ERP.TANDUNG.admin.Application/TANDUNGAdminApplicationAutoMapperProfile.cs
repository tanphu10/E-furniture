using AutoMapper;
using ERP.TANDUNG.Admin.ProductCategories;
using ERP.TANDUNG.Admin.Products;
using ERP.TANDUNG.ProductCategories;
using ERP.TANDUNG.Products;

namespace ERP.TANDUNG.Admin;

public class TANDUNGAdminApplicationAutoMapperProfile : Profile
{
    public TANDUNGAdminApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        //ProductCategory
        CreateMap<ProductCategory, ProductCategoryDto>();
        CreateMap<ProductCategory, ProductCategoryInListDto>();
        CreateMap<CreateUpdateProductCategoryDto, ProductCategory>();
        //Product
        CreateMap<Product, ProductDto>();
        CreateMap<Product, ProductInListDto>();
        CreateMap<CreateUpdateProductDto, Product>();
    }
}

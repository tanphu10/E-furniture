using AutoMapper;
using ERP.TANDUNG.Admin.ProductAttributes;
using ERP.TANDUNG.Admin.ProductCategories;
using ERP.TANDUNG.Admin.Products;
using ERP.TANDUNG.Admin.Roles;
using ERP.TANDUNG.Manufacturers;
using ERP.TANDUNG.ProductAttributes;
using ERP.TANDUNG.ProductCategories;
using ERP.TANDUNG.Products;
using ERP.TANDUNG.Roles;
using Volo.Abp.Identity;

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
        //Manufacturer
        CreateMap<Manufacturer, ManufacturerDto>();
        CreateMap<Manufacturer, ManufacturerInListDto>();
        CreateMap<CreateUpdateManufacturerDto, Manufacturer>();
        //Product Attribute
        CreateMap<ProductAttribute, ProductAttributeDto>();
        CreateMap<ProductAttribute, ProductAttributeInListDto>();
        CreateMap<CreateUpdateProductAttributeDto, ProductAttribute>();

        //Role
        CreateMap<IdentityRole, RoleDto>().ForMember(x => x.Description, map => map.MapFrom(x => x.ExtraProperties.ContainsKey(RoleConsts.DescriptionFieldName) ? x.ExtraProperties[RoleConsts.DescriptionFieldName] : null));
        CreateMap<IdentityRole, RoleInListDto>().ForMember(x => x.Description, map => map.MapFrom(x => x.ExtraProperties.ContainsKey(RoleConsts.DescriptionFieldName) ? x.ExtraProperties[RoleConsts.DescriptionFieldName] : null));
        CreateMap<CreateUpdateRoleDto, IdentityRole>();

    }
}

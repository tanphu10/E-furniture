using ERP.TANDUNG.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace ERP.TANDUNG.Admin.Permissions;

public class TANDUNGPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {

        //Catalog
        var catalogGroup = context.AddGroup(TANDUNGPermissions.CatalogGroupName);
        //Add Manufacturer
        var manufacturerPermission = catalogGroup.AddPermission(TANDUNGPermissions.Manufacturer.Default, L("Permission:Catalog.Manufacturer"));
        manufacturerPermission.AddChild(TANDUNGPermissions.Manufacturer.Create, L("Permission:Catalog.Manufacturer.Create"));
        manufacturerPermission.AddChild(TANDUNGPermissions.Manufacturer.Update, L("Permission:Catalog.Manufacturer.Update"));
        manufacturerPermission.AddChild(TANDUNGPermissions.Manufacturer.Delete, L("Permission:Catalog.Manufacturer.Delete"));
        //Add ProductCategory
        var productCategoryPermission = catalogGroup.AddPermission(TANDUNGPermissions.ProductCategory.Default, L("Permission:Catalog.ProductCategory"));
        productCategoryPermission.AddChild(TANDUNGPermissions.ProductCategory.Create, L("Permission:Catalog.ProductCategory.Create"));
        productCategoryPermission.AddChild(TANDUNGPermissions.ProductCategory.Update, L("Permission:Catalog.ProductCategory.Update"));
        productCategoryPermission.AddChild(TANDUNGPermissions.ProductCategory.Delete, L("Permission:Catalog.ProductCategory.Delete"));
        //Add Product
        var productPermission = catalogGroup.AddPermission(TANDUNGPermissions.Product.Default, L("Permission:Catalog.Product"));
        productPermission.AddChild(TANDUNGPermissions.Product.Create, L("Permission:Catalog.Product.Create"));
        productPermission.AddChild(TANDUNGPermissions.Product.Update, L("Permission:Catalog.Product.Update"));
        productPermission.AddChild(TANDUNGPermissions.Product.Delete, L("Permission:Catalog.Product.Delete"));
        //Add Attribute
        var attributePermission = catalogGroup.AddPermission(TANDUNGPermissions.Attribute.Default, L("Permission:Catalog.Attribute"));
        attributePermission.AddChild(TANDUNGPermissions.Attribute.Create, L("Permission:Catalog.Attribute.Create"));
        attributePermission.AddChild(TANDUNGPermissions.Attribute.Update, L("Permission:Catalog.Attribute.Update"));
        attributePermission.AddChild(TANDUNGPermissions.Attribute.Delete, L("Permission:Catalog.Attribute.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TANDUNGResource>(name);
    }
}

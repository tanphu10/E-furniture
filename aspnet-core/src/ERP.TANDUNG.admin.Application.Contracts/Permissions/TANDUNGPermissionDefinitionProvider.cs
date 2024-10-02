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

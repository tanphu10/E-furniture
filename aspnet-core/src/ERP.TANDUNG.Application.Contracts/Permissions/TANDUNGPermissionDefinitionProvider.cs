using ERP.TANDUNG.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace ERP.TANDUNG.Permissions;

public class TANDUNGPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(TANDUNGPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(TANDUNGPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TANDUNGResource>(name);
    }
}

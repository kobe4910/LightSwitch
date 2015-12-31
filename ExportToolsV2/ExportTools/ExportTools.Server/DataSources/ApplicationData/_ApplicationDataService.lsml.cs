namespace LightSwitchApplication
{
    public partial class ApplicationDataService
    {
        
        partial void InfoesSet_CanRead(ref bool result)
        {
            result = Application.User.HasPermission(Permissions.CanAccessScreen);
        }

        partial void InfoesSet_CanInsert(ref bool result)
        {
            result = Application.User.HasPermission(Permissions.CanAddOfficer);
        }

        partial void InfoesSet_CanUpdate(ref bool result)
        {
            result = Application.User.HasPermission(Permissions.CanEditOfficer);
        }

        partial void InfoesSet_CanDelete(ref bool result)
        {
            result = Application.User.HasPermission(Permissions.CanDeleteOfficer);
        }
    }
}
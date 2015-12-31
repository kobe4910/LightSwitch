﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LightSwitchApplication
{
    /// <summary>
    /// Defines the names of the application permissions.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "12.1.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public static class Permissions
    {
        /// <summary>
        /// Provides the ability to manage security for the application.
        /// </summary>
        public const string SecurityAdministration = global::Microsoft.LightSwitch.Security.ApplicationPermissions.SecurityAdministration;
        /// <summary>
        /// Allows users to access the screen
        /// </summary>
        public const string CanAccessScreen = "LightSwitchApplication:CanAccessScreen";
        /// <summary>
        /// Allows users to add officers to the system
        /// </summary>
        public const string CanAddOfficer = "LightSwitchApplication:CanAddOfficer";
        /// <summary>
        /// Allows users to delete officers in the system
        /// </summary>
        public const string CanDeleteOfficer = "LightSwitchApplication:CanDeleteOfficer";
        /// <summary>
        /// Allows users to edit officers in the system
        /// </summary>
        public const string CanEditOfficer = "LightSwitchApplication:CanEditOfficer";

        /// <summary>
        /// Gets all permissions defined for the application.  This includes system and user-defined permissions.
        /// </summary>
        public static global::System.Collections.ObjectModel.ReadOnlyCollection<string> AllPermissions { get { return global::Microsoft.LightSwitch.Security.ApplicationPermissions.AllPermissions; } }
    }
}
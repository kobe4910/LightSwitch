using System;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;
using OfficeIntegration;

namespace LightSwitchApplication
{
    public partial class RelChuJi
    {
        partial void RelChuJi_InitializeDataWorkspace(global::System.Collections.Generic.List<global::Microsoft.LightSwitch.IDataService> saveChangesTo)
        {
            // Write your code here.
            this.ChuJiRelProperty = new ChuJiRel();
        }

        partial void RelChuJi_Saved()
        {
            // Write your code here.
            this.Close(false);
            Application.Current.ShowDefaultScreen(this.ChuJiRelProperty);
        }

        partial void RelChuJi_Saving(ref bool handled)
        {
            // Write your code here.
            List<ColumnMapping> mapContent2 = new List<ColumnMapping>();

            mapContent2.Add(new ColumnMapping("RelTitle1", "RelTitle1"));
        }
    }
}
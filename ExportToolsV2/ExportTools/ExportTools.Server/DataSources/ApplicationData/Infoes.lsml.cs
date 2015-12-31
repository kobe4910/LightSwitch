using Microsoft.LightSwitch;

namespace LightSwitchApplication
{
    public partial class Infoes
    {
        /*
        partial void BirthDate_Validate(EntityValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Error-Message>");
            if (this.BirthDate > DateTime.Today)
            {
                results.AddPropertyError("出生日期不能大于今天日期！");
            }
        }

        partial void WorkDate_Validate(EntityValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Error-Message>");
            if (this.WorkDate > DateTime.Today)
            {
                results.AddPropertyError("参加工作时间不能大于今天日期！");
            }
        }

        partial void PartyDate_Validate(EntityValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Error-Message>");
            if (this.PartyDate > DateTime.Today)
            {
                results.AddPropertyError("入党时间不能大于今天日期！");
            }
        }
         */

        partial void Duty_Validate(EntityValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Error-Message>");
        }

        partial void Photo_Validate(EntityValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Error-Message>");
        }

        partial void PID_Validate(EntityValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Error-Message>");
            //this.DataWorkspace.ApplicationData.InfoesSet;
            /*
            var tmp = this.DataWorkspace.ApplicationData.InfoesSet.Where(c => c.PID == this.PID).FirstOrDefault();
            if (tmp == null)
            {
                results.AddPropertyError("输入工号已存在！");
            }
             */
        }
    }
}
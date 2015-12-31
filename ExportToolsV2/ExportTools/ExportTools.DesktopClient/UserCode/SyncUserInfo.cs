using System;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using LightSwitchApplication.TeacherInfo;
using Microsoft.LightSwitch.Presentation.Extensions;

namespace LightSwitchApplication.UserCode
{
    public class SyncUserInfo
    {
        private readonly string _persNo;
        public byte[] Photo { get; set; }
        public shu_teacher_all UserInfo { get; set; }

        public SyncUserInfo(string persNo)
        {
            _persNo = persNo;
        }

        public void Process()
        {
            UpdateInfo(_persNo);
            UpdatePhoto(_persNo);
        }

        private void UpdatePhoto(string persNo)
        {
            MessageHeader shuSoapHeader = new ShuSoapHeader { UserID = "478FAFFC-05B3-4415-892A-79AEE857D681", PassWord = "zzb_pass" };

            var client = new SLoginServicesSoapClient();
            client.GetPhotoCompleted += ClientGetPhotoCompleted;
            OperationContext.Current = new OperationContext(client.InnerChannel);
            OperationContext.Current.OutgoingMessageHeaders.Add(shuSoapHeader);
            client.GetPhotoAsync(persNo, 1521);
        }

        void ClientGetPhotoCompleted(object sender, GetPhotoCompletedEventArgs e)
        {
            this.Photo = e.Result;
            //执行获取照片以后的操作 
            //e.Result 代表获取到的照片
            //throw new NotImplementedException();
        }

        void UpdateInfo(string persNo)
        {
            MessageHeader shuSoapHeader = new ShuSoapHeader { UserID = "3EC059F5-DC81-4938-83CA-5643B01EC872", PassWord = "zzb_pass" };

            var client = new SLoginServicesSoapClient();
            client.GetShuPersInfoForInfoCompleted += ClientGetShuPersInfoForFccCompleted;
            OperationContext.Current = new OperationContext(client.InnerChannel);
            OperationContext.Current.OutgoingMessageHeaders.Add(shuSoapHeader);
            //call Reference.cs->EndGetShuPersInfoForFcc
            client.GetShuPersInfoForInfoAsync(persNo, 8423);
        }

        void ClientGetShuPersInfoForFccCompleted(object sender, GetShuPersInfoForInfoCompletedEventArgs e)
        {
            this.UserInfo = e.Result;

            //执行获取人员信息以后的操作
            //throw new NotImplementedException();
        }
    }
}

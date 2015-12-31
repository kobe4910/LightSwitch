using System;
using System.Net;
using System.ServiceModel.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml;

namespace LightSwitchApplication.UserCode
{
    public class ShuSoapHeader : MessageHeader
    {
        public string NMsg { get; set; }
        public string UserID { get; set; }
        public string PassWord { get; set; }


        public override string Name
        {
            get { return "ShuSoapHeader"; }
        }

        public override string Namespace
        {
            get { return "http://passport.shu.edu.cn/"; }
        }

        protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
        {
            writer.WriteElementString("NMsg", "http://passport.shu.edu.cn/", NMsg);
            writer.WriteElementString("UserID", "http://passport.shu.edu.cn/", UserID);
            writer.WriteElementString("PassWord", "http://passport.shu.edu.cn/", PassWord);
        }
    }
}

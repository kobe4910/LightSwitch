using System;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.ComponentModel;
using LightSwitchApplication.UserCode;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;
using OfficeIntegration;
using System.Data;
using System.Runtime.InteropServices.Automation;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.LightSwitch.Theming;
using Path = System.IO.Path;

namespace LightSwitchApplication
{
    public partial class InfoesSetListDetail
    {
        partial void ExportApprove_Execute()
        {
            // Write your code here.
            Func<DateTime, string> formatDate1 = (System.DateTime x) => { return string.Format("{0:yyyy.MM}", x); };
            Func<DateTime, string> formatDate2 = (System.DateTime y) => { return string.Format("{0:yyyy.MM.dd}", y); };

            List<ColumnMapping> mapContent = new List<ColumnMapping>();
            //Word.GenerateDocument
            mapContent.Add(new ColumnMapping("Photo", "Photo"));
            mapContent.Add(new ColumnMapping("Name", "Name"));
            mapContent.Add(new ColumnMapping("Sex", "Sex"));
            mapContent.Add(new ColumnMapping("BirthDate", "BirthDate", FormatDelegate: formatDate1));
            mapContent.Add(new ColumnMapping("Nation", "Nation"));
            mapContent.Add(new ColumnMapping("Native", "Native"));
            mapContent.Add(new ColumnMapping("BirthPlace", "BirthPlace"));
            mapContent.Add(new ColumnMapping("PartyDate", "PartyDate", FormatDelegate: formatDate2));
            mapContent.Add(new ColumnMapping("WorkDate", "WorkDate", FormatDelegate: formatDate2));
            mapContent.Add(new ColumnMapping("Health", "Health"));
            mapContent.Add(new ColumnMapping("ProJob", "ProJob"));
            mapContent.Add(new ColumnMapping("Speciality", "Speciality"));
            mapContent.Add(new ColumnMapping("WorkExp", "WorkExp"));
            mapContent.Add(new ColumnMapping("RewardPunish", "RewardPunish"));
            mapContent.Add(new ColumnMapping("Exam", "Exam"));
            mapContent.Add(new ColumnMapping("Reason", "Reason"));
            mapContent.Add(new ColumnMapping("FullEdu", "FullEdu"));
            mapContent.Add(new ColumnMapping("FUniMajor", "FUniMajor"));
            mapContent.Add(new ColumnMapping("PartEdu", "PartEdu"));
            mapContent.Add(new ColumnMapping("PUniMajor", "PUniMajor"));
            mapContent.Add(new ColumnMapping("XianRen", "XianRen"));
            mapContent.Add(new ColumnMapping("NiRen", "NiRen"));
            mapContent.Add(new ColumnMapping("NiMian", "NiMian"));

            var doc = Word.GenerateDocument(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Export1.docx", this.InfoesSet.SelectedItem, mapContent);
            //var doc = Word.GenerateDocument(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\test.docx", this.InfoesSet.SelectedItem, mapContent);

            List<string> details = new List<string>(){
                "RelTitle", "RelName", "RelBirthDate", "RelParty", "RelComJob"
            };

            //pay attention to the third para, it must be mapped to word`s row correctly
            Word.Export(doc, "SocialRel", 6, false, this.InfoesSet.SelectedItem.SocialRel, details);
        }

        #region  Process Items
        private const string ITEMS_CONTROL = "InfoesList";

        //this is somewhere to store a reference to the grid control 
        private DataGrid _itemsControl = null;
        private int _SelectedRowsCount = 0;

        partial void InfoesSetListDetail_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            // Write your code here.
            //here we're adding an event handler to get a reference to the grid control, when it becomes available 
            //and we have no way of knowing when that will be 
            this.FindControl(ITEMS_CONTROL).ControlAvailable += DemoItems_ControlAvailable;
        }

        private void DemoItems_ControlAvailable(object send, ControlAvailableEventArgs e)
        {
            //we know that the control is a grid, but we use TryCast, just in case 
            _itemsControl = e.Control as DataGrid;

            //if the cast failed, just leave, there's nothing more we can do here 
            if (_itemsControl == null)
            {
                return;
            }

            //set the property on the grid that allows multiple selection 
            _itemsControl.SelectionMode = DataGridSelectionMode.Extended;
            _itemsControl.SelectionChanged += new SelectionChangedEventHandler(ItemsList_SelectionChanged);
        }

        private void ItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (_itemsControl == null)
            {
                case true:
                    _SelectedRowsCount = 0;

                    break;
                case false:
                    _SelectedRowsCount = _itemsControl.SelectedItems.Count;
                    break;
            }
        }

        private void ProcessItems_CanExecute(ref bool result)
        {
            //only enable the button if the variable has been initialised 
            //& the rows have actually been selected 
            result = (_SelectedRowsCount > 0);
        }
        #endregion

        partial void ExportNotice_Execute()
        {
            // Write your code here.
            object missingValue = System.Reflection.Missing.Value;
            dynamic wordApplication = AutomationFactory.CreateObject("Word.Application");
            wordApplication.Visible = false;

            dynamic doc = wordApplication.Documents.Add();
            wordApplication.ActiveDocument.pagesetup.topmargin = 65.4; //设置word文档的上边距
            wordApplication.ActiveDocument.pagesetup.bottommargin = 65.4;//设置word文档的下边距
            wordApplication.ActiveDocument.pagesetup.leftmargin = 51.8;//设置word文档的左边距
            wordApplication.ActiveDocument.pagesetup.rightmargin = 51.8;//设置word文档的右边距

            dynamic rng = wordApplication.Range;
            int start = doc.Characters.Count - 1; //定义文本的坐标
            int end = doc.Characters.Count - 1;
            rng = doc.content;
            rng = doc.Range(ref start, ref end);
            rng.Text = "  为在干部选拔任用工作中进一步扩大民主，广泛听取群众意见，把干部选好、选准，根据《党政领导干部选拔任用工作条例》规定，经校党委常委会研究决定，对下列同志进行任职前公示：" + "\r\n" + "\r\n";
            rng.font.size = 14;
            rng.font.name = "宋体"; //设置字体
            rng.ParagraphFormat.Alignment = 0; //设置左对齐

            dynamic table;
            int Tstart = doc.Characters.Count - 1;
            int Tend = doc.Characters.Count - 1;
            int num = _itemsControl.SelectedItems.Count;
            Object tableLocation = doc.Range(ref　Tstart, ref　Tend);
            table = doc.Tables.Add(tableLocation, num, 2, ref missingValue, ref missingValue); //在指定位置插入表格
            table.Borders.OutsideLineStyle = 0; //显示表格的边框线
            table.Borders.InsideLineStyle = 0;

            foreach (Infoes item in _itemsControl.SelectedItems)
            {
                //390*567
                byte[] tmp = item.Photo;
                FileStream aFile = new FileStream(@"C:\Users\Administrator\Desktop\Pic\photo.jpg", FileMode.Create);
                BinaryWriter sw = new BinaryWriter(aFile);
                sw.Write(tmp);
                sw.Flush();
                sw.Close();

                table.cell(num, 1).Range.InlineShapes.AddPicture(@"C:\Users\Administrator\Desktop\Pic\photo.jpg");

                /*********************************************/
                table.cell(num, 1).width = 320f;
                table.cell(num, 1).Range.font.size = 14;
                table.cell(num, 1).Range.ParagraphFormat.Alignment = 1; //设置单元格垂直的居中方式

                var cur1 = string.Format("{0:yyyy年MM月}", item.BirthDate);
                var cur2 = string.Format("{0:yyyy年MM月}", item.WorkDate);
                var cur3 = string.Format("{0:yyyy年MM月}", item.PartyDate);
                table.cell(num, 2).width = 180f;
                table.cell(num, 2).Range.Text = "\r\n" + item.Name + "\r\n"
                                              + item.Sex + "，" + cur1 + "出生，"
                                              + "民族" + item.Nation + "，" + "籍贯" + item.Native + "，"
                                              + cur2 + "参加工作，" + cur3 + "加入中国共产党。"
                                              + "现任" + item.XianRen + "。" + "拟任" + item.NiRen + "。";
                table.cell(num, 2).Range.font.size = 14;
                table.cell(num, 2).Range.font.name = "宋体";
                table.cell(num, 2).Range.ParagraphFormat.Alignment = 0;

                if (num > 1)
                {
                    num--;
                }
            }

            dynamic append = wordApplication.Range;
            int begin = doc.Characters.Count - 1; //定义文本的坐标
            int final = doc.Characters.Count - 1;
            append = doc.content;
            append = doc.Range(ref begin, ref final);
            append.Text = "\r\n" + "  上述公示对象的公示时间为：**年**月**日~**年**月**日。如对公示对象有情况反映的，可在公示期间向校党委组织部反映，联系电话：66134275、66132607；电子信箱：zuzb@mail.shu.edu.cn";
            append.font.size = 14;
            append.font.name = "宋体"; //设置字体
            append.ParagraphFormat.Alignment = 0; //设置左对齐

            string SavePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\abc.docx";
            //string SavePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\wrong.docx" + "-" + System.DateTime.Now.ToString("yyyyMMdd HHmmss");
            wordApplication.ActiveDocument.SaveAs(ref SavePath,
                    ref missingValue, ref missingValue, ref missingValue, ref missingValue,
                    ref missingValue, ref missingValue, ref missingValue, ref missingValue,
                    ref missingValue, ref missingValue, ref missingValue, ref missingValue,
                    ref missingValue, ref missingValue, ref missingValue);

            /*
            //如果想要换页，则要用到分页符，代码如下：
            dynamic para;
            para = doc.Content.Paragraphs.Add(ref　missingValue);
            object pBreak = 0;
            para.Range.InsertBreak(ref　pBreak);
             * */
        }

        partial void HRupdate_Execute()
        {
            // Write your code here.
            foreach (Infoes item in _itemsControl.SelectedItems)
            {
                var dup = (from x in this.DataWorkspace.ApplicationData.InfoesSet
                          where x.PID == item.PID select x).Execute().Count();

                if (dup != 0)
                {
                    this.ShowMessageBox("         工号重复！", caption: "输入错误", button: MessageBoxOption.Ok);
                    //item.Delete();
                    return;
                }

                //var tmp = item.PID.ToString();
                var tmp = item.PID;
                var synUserInfo = new SyncUserInfo(tmp);
                synUserInfo.Process();

                this.DataWorkspace.ApplicationData.SaveChanges();

                /*
                item.WorkExp =  synUserInfo.UserInfo.name + "\n" + synUserInfo.UserInfo.GH + "\n" + synUserInfo.UserInfo.sex + "\n" + 
                                synUserInfo.UserInfo.nation + "\n" + synUserInfo.UserInfo.birthday + "\n" + synUserInfo.UserInfo.JXRQ + "\n" + 
                                synUserInfo.UserInfo.CJGZRQ + "\n" + synUserInfo.UserInfo.poli + "\n" + synUserInfo.UserInfo.polidata + "\n" +
                                synUserInfo.UserInfo.staffgroup + "\n" + synUserInfo.UserInfo.staffsubgroup + "\n" + synUserInfo.UserInfo.department + "\n" +
                                synUserInfo.UserInfo.department2 + "\n" + synUserInfo.UserInfo.education + "\n" + synUserInfo.UserInfo.college + "\n" +
                                synUserInfo.UserInfo.xw + "\n" + synUserInfo.UserInfo.zc + "\n" + synUserInfo.UserInfo.zw + "\n" + synUserInfo.UserInfo.zj + "\n" + 
                                synUserInfo.UserInfo.xk;
                 */

                try
                {
                    item.Name = synUserInfo.UserInfo.name;
                }
                catch (Exception e)
                {
                    this.ShowMessageBox("     输入工号不存在！", caption: "输入错误", button: MessageBoxOption.Ok);
                    item.Delete();
                    return;
                }

                item.Sex = synUserInfo.UserInfo.sex;
                item.Nation = synUserInfo.UserInfo.nation;
                item.Politics = synUserInfo.UserInfo.poli;
                item.FullEdu = synUserInfo.UserInfo.education;
                item.FUniMajor = synUserInfo.UserInfo.college;
                //item.PartEdu = synUserInfo.UserInfo;
                //item.PUniMajor = synUserInfo.UserInfo;
                item.Duty = synUserInfo.UserInfo.zw;
                item.Rank = synUserInfo.UserInfo.zj;
                item.JobTitle = synUserInfo.UserInfo.zc;
                item.Major = synUserInfo.UserInfo.xk;
                
                item.Photo = synUserInfo.Photo;
                if (item.Photo == null)
                {
                    this.ShowMessageBox("没有照片，请自行上传！", caption: "照片格式", button: MessageBoxOption.Ok);
                }
                

                try
                {
                    var tmp_bth = synUserInfo.UserInfo.birthday.Insert(4, "-");
                    var tmp_bth2 = tmp_bth.Insert(7, "-");
                    item.BirthDate = Convert.ToDateTime(tmp_bth2);
                }
                catch (Exception e)
                {
                    this.ShowMessageBox("出生日期格式不正确，请手动输入！", caption: "格式错误", button: MessageBoxOption.Ok);
                    //item.BirthDate = Convert.ToDateTime("1900-1-2");
                    //this.ShowMessageBox(synUserInfo.UserInfo.birthday);
                    return;
                }

                try
                {
                    var tmp_wrk = synUserInfo.UserInfo.CJGZRQ.Insert(4, "-");
                    var tmp_wrk2 = tmp_wrk.Insert(7, "-");
                    item.WorkDate = Convert.ToDateTime(tmp_wrk2);
                }
                catch (Exception e)
                {
                    this.ShowMessageBox("参加工作日期格式不正确，请手动输入！", caption: "格式错误", button: MessageBoxOption.Ok);
                    //item.WorkDate = Convert.ToDateTime("1900-1-2");
                    //this.ShowMessageBox(synUserInfo.UserInfo.CJGZRQ);
                    return;
                }

                try
                {
                    var tmp_pty = synUserInfo.UserInfo.polidata.Insert(4, "-");
                    var tmp_pty2 = tmp_pty.Insert(7, "-");
                    item.PartyDate = Convert.ToDateTime(tmp_pty2);
                }
                catch (Exception e)
                {
                    this.ShowMessageBox("入党日期格式不正确，请手动输入！", caption: "格式错误", button: MessageBoxOption.Ok);
                    //this.ShowMessageBox(synUserInfo.UserInfo.polidata);
                    return;
                }

                this.DataWorkspace.ApplicationData.SaveChanges();
            }
        }

        partial void Exit_Cur_Execute()
        {
            // Write your code here.
            foreach (Infoes c in this.DataWorkspace.ApplicationData.InfoesSet)
            {
                c.Delete();
            }
            this.DataWorkspace.ApplicationData.SaveChanges();
        }

        partial void InfoesSetListDetail_Activated()
        {
            // Write your code here.
            //Infoes.DetailsClass.Equals() =  this.Details.Commands.Editable;
        }
    }
}



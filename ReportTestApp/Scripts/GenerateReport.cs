using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using Telerik.Reporting;
using Telerik.Reporting.Drawing;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;

namespace ReportTestApp.Scripts
{
    public class GenerateReport
    {
        public static void Generate(Telerik.ReportViewer.Html5.WebForms.ReportViewer rv, IEnumerable<object> data, string extension, SortDescriptorCollection sortDescriptors, GroupDescriptorCollection grpDescriptors)
        {
            Telerik.Reporting.Report report1 = new Telerik.Reporting.Report();
            report1.DataSource = data;
            string sortCol = "";
            string sortDir = "";

            //multi sort can be done by iterating through collection like for group
            if (sortDescriptors.Count > 0)
            {
                ColumnSortDescriptor sd = sortDescriptors[0] as ColumnSortDescriptor;
                sortCol = sd.Column.UniqueName;
                sortDir = sd.SortDirection.ToString();
            }

            //Page Header Section
            Telerik.Reporting.PageHeaderSection pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            pageHeaderSection1.Height = new Telerik.Reporting.Drawing.Unit(0.3, Telerik.Reporting.Drawing.UnitType.Inch);
            pageHeaderSection1.Style.BackgroundColor = Color.Gray;
            Telerik.Reporting.TextBox txtHead = new Telerik.Reporting.TextBox();
            txtHead.Value = "Title";
            txtHead.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3.2395832538604736D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.02083333395421505D, Telerik.Reporting.Drawing.UnitType.Inch));
            txtHead.Name = "reportTitle";
            txtHead.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(5.5603775978088379D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.20000000298023224D, Telerik.Reporting.Drawing.UnitType.Inch));
            pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { txtHead });

            IEnumerator dataColl = data.GetEnumerator();
            int count = 0;
            int first = 0;
            object obj = null;

            while (dataColl.MoveNext())
            {
                if (first == 0)
                {
                    obj = dataColl.Current;
                    foreach (PropertyInfo info in obj.GetType().GetProperties())
                    {
                        count++;
                    }
                    first++;
                }
            }

            Telerik.Reporting.Drawing.Unit x = Telerik.Reporting.Drawing.Unit.Inch(0);
            Telerik.Reporting.Drawing.Unit y = Telerik.Reporting.Drawing.Unit.Inch(0);
            Telerik.Reporting.ReportItemBase[] headColumnList = new Telerik.Reporting.ReportItem[count];
            Telerik.Reporting.ReportItemBase[] detailColumnList = new Telerik.Reporting.ReportItem[count];
            Telerik.Reporting.Group group = new Telerik.Reporting.Group();
            SizeU size = new SizeU(Telerik.Reporting.Drawing.Unit.Inch((double)(22) / count), Telerik.Reporting.Drawing.Unit.Inch(0.6));
            int column = 0;

            foreach (PropertyInfo info in obj.GetType().GetProperties())
            {
                string columnName = info.Name;
                Telerik.Reporting.HtmlTextBox headerCol = CreateTxtHeader(columnName, column);
                headerCol.Style.BackgroundColor = Color.LemonChiffon;
                headerCol.Style.BorderStyle.Default = BorderType.Solid;
                headerCol.Style.BorderWidth.Default = Unit.Pixel(1);
                headerCol.CanGrow = true;
                headerCol.Location = new Telerik.Reporting.Drawing.PointU(x, y);
                headerCol.Size = size;
                headColumnList[column] = headerCol;
                Telerik.Reporting.TextBox textBox = CreateTxtDetail(columnName, column);
                textBox.Style.BorderStyle.Default = BorderType.Solid;
                textBox.Style.BorderWidth.Default = Unit.Pixel(1);
                textBox.CanGrow = true;
                textBox.Location = new Telerik.Reporting.Drawing.PointU(x, y);
                textBox.Size = size;
                detailColumnList[column] = textBox;
                textBox.ItemDataBinding += new EventHandler(textBox_ItemDataBound);
                x += Telerik.Reporting.Drawing.Unit.Inch(headerCol.Size.Width.Value);
                column++;
            }

            Telerik.Reporting.ReportItemBase[] groupColumnList = new Telerik.Reporting.ReportItem[grpDescriptors.Count];
            int i = grpDescriptors.Count;
            if (grpDescriptors.Count > 0)
            {
                Telerik.Reporting.GroupHeaderSection groupHeaderSection1 = new Telerik.Reporting.GroupHeaderSection();
                foreach (ColumnGroupDescriptor grpDescriptor in grpDescriptors)
                {
                    string grpCol = grpDescriptor.Column.UniqueName;
                    group.Groupings.Add(new Grouping("=Fields." + grpCol));
                    if (grpDescriptor.SortDirection.ToString().ToLower() == "descending")
                    {
                        group.Sortings.Add(new Sorting("=Fields." + grpCol, SortDirection.Desc));
                    }
                    else
                    {
                        group.Sortings.Add(new Sorting("=Fields." + grpCol, SortDirection.Asc));
                    }
                    i--;
                    Telerik.Reporting.TextBox hdCol = new Telerik.Reporting.TextBox();
                    hdCol.Style.BackgroundColor = Color.Orange;
                    hdCol.Style.BorderStyle.Default = BorderType.Solid;
                    hdCol.Style.BorderWidth.Default = Unit.Pixel(1);
                    hdCol.KeepTogether = true;
                    hdCol.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(5.5603775978088379D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.20000000298023224D, Telerik.Reporting.Drawing.UnitType.Inch));
                    hdCol.Value = "=[" + grpCol + "]";
                    groupColumnList[i] = hdCol;
                    group.GroupHeader = groupHeaderSection1;
                    //to avoid extra row after group col
                    group.GroupHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(0);
                }
                groupHeaderSection1.Items.AddRange(groupColumnList);
            }
            if (sortCol.Length > 0)
            {
                group.Groupings.Add(new Grouping("=Fields." + sortCol));
                if (sortDir.ToLower() == "descending")
                {
                    group.Sortings.Add(new Sorting("=Fields." + sortCol, SortDirection.Desc));
                }
                else
                {
                    group.Sortings.Add(new Sorting("=Fields." + sortCol, SortDirection.Asc));
                }
            }
            ReportHeaderSection reportHeaderSection1 = new Telerik.Reporting.ReportHeaderSection();
            reportHeaderSection1.Height = new Telerik.Reporting.Drawing.Unit(0.3, Telerik.Reporting.Drawing.UnitType.Inch);
            reportHeaderSection1.Items.AddRange(headColumnList);
            report1.Groups.Add(group);

            //Detail Section
            Telerik.Reporting.DetailSection detailSection1 = new Telerik.Reporting.DetailSection();
            detailSection1.Height = new Telerik.Reporting.Drawing.Unit(0.3, Telerik.Reporting.Drawing.UnitType.Inch);
            detailSection1.Items.AddRange(detailColumnList);

            //Page Footer Section
            Telerik.Reporting.PageFooterSection pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            pageFooterSection1.Height = new Telerik.Reporting.Drawing.Unit(0.3, Telerik.Reporting.Drawing.UnitType.Inch);
            pageFooterSection1.Style.BackgroundColor = Color.LightGray;
            pageFooterSection1.PrintOnFirstPage = true;
            pageFooterSection1.PrintOnLastPage = true;
            Telerik.Reporting.TextBox txtFooter = new Telerik.Reporting.TextBox();
            txtFooter.Value = "='Page ' + PageNumber + ' of ' + PageCount";
            txtFooter.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(4.2395832538604736D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.02083333395421505D, Telerik.Reporting.Drawing.UnitType.Inch));
            txtFooter.Name = "pageInfoTextBox";
            txtFooter.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(5.5603775978088379D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.20000000298023224D, Telerik.Reporting.Drawing.UnitType.Inch));
            Telerik.Reporting.PictureBox picBoxFooter = new Telerik.Reporting.PictureBox();
            picBoxFooter.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.2395832538604736D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.02083333395421505D, Telerik.Reporting.Drawing.UnitType.Inch));
            picBoxFooter.Value = @"C:\CCMSGoldStandard_Local\CCMSGoldStandard\CCMSAppShell\Images\no.png";
            picBoxFooter.Style.TextAlign = Telerik.Reporting.Drawing.
            HorizontalAlign.Center;
            picBoxFooter.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1, ((Telerik.Reporting.Drawing.UnitType)(Telerik.Reporting.Drawing.UnitType.Inch))), new Telerik.Reporting.Drawing.Unit(.5D, ((Telerik.Reporting.Drawing.UnitType)(Telerik.Reporting.Drawing.UnitType.Inch))));
            picBoxFooter.Sizing = ImageSizeMode.AutoSize;
            pageFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { txtFooter, picBoxFooter });

            //add all section to report
            report1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { pageHeaderSection1, reportHeaderSection1, detailSection1, pageFooterSection1 });
            report1.PageSettings.Landscape = false;
            report1.PageSettings.Margins.Bottom = new Telerik.Reporting.Drawing.Unit(1D, Telerik.Reporting.Drawing.UnitType.Inch);
            report1.PageSettings.Margins.Left = new Telerik.Reporting.Drawing.Unit(.25, Telerik.Reporting.Drawing.UnitType.Inch);
            report1.PageSettings.Margins.Right = new Telerik.Reporting.Drawing.Unit(.25, Telerik.Reporting.Drawing.UnitType.Inch);
            report1.PageSettings.Margins.Top = new Telerik.Reporting.Drawing.Unit(1D, Telerik.Reporting.Drawing.UnitType.Inch);
            Telerik.Reporting.Drawing.SizeU paperSize = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(22, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(22, Telerik.Reporting.Drawing.UnitType.Inch));
            report1.PageSettings.PaperSize = paperSize;
            report1.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            Hashtable deviceInfo = new Hashtable();
            deviceInfo["FontEmbedding"] = "Subset";
            if (extension.ToLower() == "csv")
            {
                deviceInfo["NoHeader"] = true;
                deviceInfo["NoStaticText"] = true;
            }
            Telerik.Reporting.Processing.ReportProcessor RP = new Telerik.Reporting.Processing.ReportProcessor();
            byte[] buffer = RP.RenderReport(extension.ToUpper(), report1, deviceInfo).DocumentBytes;
            string myPath = "C:";
            string file = myPath + @"\" + DateTime.Now.ToString("HHmmss") + "." + extension;
            FileStream fs = new FileStream(file, FileMode.Create);
            fs.Write(buffer, 0, buffer.Length);
            fs.Flush();
            fs.Close();

            Process.Start(file);
        }

        static void textBox_ItemDataBound(object sender, EventArgs e)
        {
            if (((Telerik.Reporting.Processing.TextBox)sender).Value == null || (((Telerik.Reporting.Processing.TextBox)sender).Value.ToString() == ""))
            {
                ((Telerik.Reporting.Processing.TextBox)sender).Value = "";
            }
        }

        public static Telerik.Reporting.HtmlTextBox CreateTxtHeader(string FieldName, int i)
        {
            Telerik.Reporting.HtmlTextBox txtHead = new Telerik.Reporting.HtmlTextBox();
            txtHead.Value = FieldName;
            return txtHead;
        }

        public static Telerik.Reporting.HtmlTextBox CreateHTMLTxtDetail(string FieldName, int i)
        {
            Telerik.Reporting.HtmlTextBox txtHead = new Telerik.Reporting.HtmlTextBox();
            txtHead.Value = "=[" + FieldName + "]";
            //txtHead.Dock = DockStyle.Left;
            //txtHead.Name = FieldName;  
            txtHead.CanGrow = true;
            return txtHead;
        }

        public static Telerik.Reporting.TextBox CreateTxtDetail(string FieldName, int i)
        {
            Telerik.Reporting.TextBox txtHead = new Telerik.Reporting.TextBox();
            txtHead.Value = "=[" + FieldName + "]";
            txtHead.Name = FieldName;
            //txtHead.Dock = DockStyle.Left;
            //txtHead.CanGrow = true;
            return txtHead;
        }



    }
}
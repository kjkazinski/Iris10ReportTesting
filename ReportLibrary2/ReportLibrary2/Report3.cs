namespace ReportLibrary2
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    // using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using System.Diagnostics;
    using System.Text.RegularExpressions;
    using System.Collections.Generic;
    using ReportFormat.Model;
    using Newtonsoft.Json;

    /// <summary>
    /// Summary description for Report3.
    /// </summary>
    public partial class Report3 : Report
    {
        public static ReportFooterSection ReportFooter = new ReportFooterSection();
        public static DetailSection Detail = new DetailSection();
        public static String SqlConnectionString;
        public static String SQLCommandString;
        public static string CountyName;
        public static string SortOption;
        public static SortDirection MySortDirection;
        public static bool SortMe = false;
        public static SqlDataSource SqlDataSource1 = new SqlDataSource();
        public static Report Report1 = new Report();
        public static string StartDate;
        public static string EndDate;
        public static bool dateFilter = false;
        public static bool groupBy = false;
        public static string myGrouping = "";


        //TextBox Vars
        private static int count = 0;
        public static TextBox[] MyCaptionBoxes = new TextBox[50];
        public static TextBox[] MyDataBoxes = new TextBox[50];
        public static double CapLocX = 1.2666666507720947D;
        public static double CapLocY = 0.14083333395421505D;
        public static double GroupLoc = 0.14083333395421505D;
        public static double TBHeight = 0.44166669249534607D;

        //Groups
        public static bool AddReportFooter = false;
        public static GroupHeaderSection[] HeaderSections = new GroupHeaderSection[50];
        public static GroupFooterSection[] FooterSections = new GroupFooterSection[50];
        public static Telerik.Reporting.Group[] AllGroups = new Telerik.Reporting.Group[50];
        public static int GroupCount = 0;
        public static int Summing = 1;
        public static int Counting = 0;
        public static SizeU TBSize = new SizeU(Unit.Inch(CapLocX), Unit.Inch(TBHeight));
        public static int ColorR = 121;
        public static int ColorG = 167;
        public static int ColorB = 227;


        public Report3()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent2();
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        public static void GetJSON(string json)
        {
            Debug.WriteLine("JSON: " + json);
            ReportModel data = new ReportModel(json);
            GenerateHeaderFooters();
            for (int k = 0; k < data.GenerateTitleField.Length; k++)
            {
                GenerateTextField(data.GenerateTitleField.GetValue(k).ToString(), "=Fields." + data.GenerateDataField.GetValue(k).ToString());
            }
            for (int a = 0; a < data.GroupBy.Length; a++)
            {
                GroupBy(data.GroupBy.GetValue(a).ToString(), "=Fields." + data.GroupBy.GetValue(a).ToString());
            }
            for (int i = 0; i < data.Filters.Length; i++)
            {
                Debug.WriteLine("filter stuff: " + data.Filters.GetValue(i));
            }

            ChangeSqlString(data.ConnectionString);
            SQLCommandString = data.SelectCommand;
        }

        public static void TestFunction(List<string> myModel)
        {
            Debug.WriteLine("This is sparta! " + myModel[0] + " " + myModel[0]);
        }

        public static void AddDateFilter(string start,string end)
        {
            StartDate = start;
            EndDate = end;
            Debug.WriteLine(StartDate + " " + EndDate);
            dateFilter = true;
        }

       

        public static void ChangeSqlString(string sqlCon)
        {
            SqlConnectionString = sqlCon;
            Debug.WriteLine("THIS IS FROM THE REPORT " + SqlConnectionString);
        }

        public static void GenerateHeaderFooters()
        {
            for (int i = 0; i < 50; i++)
            {
                HeaderSections[i] = new GroupHeaderSection();
                FooterSections[i] = new GroupFooterSection();
                AllGroups[i] = new Telerik.Reporting.Group();
                HeaderSections[i].Height = Unit.Inch(TBHeight);
                HeaderSections[i].Style.BackgroundColor = Color.FromArgb(224, 224, 224);
                FooterSections[i].Height = Unit.Inch(TBHeight);
                AllGroups[i].GroupFooter = FooterSections[i];
                AllGroups[i].GroupHeader = HeaderSections[i];
            }
        }

        public static void AddReportFooterSection(int sumOrCount, string section, string name)
        {
            var sumCount = new TextBox();
            var totalBox = new TextBox();
            int spot = GetPosition(name);

            if (sumOrCount == 1)
            {
                sumCount = GenerateAttributes(MyCaptionBoxes[spot].Location, "= Sum(" + section + ")", name, "{0:$#,0.00}");
            }
            else
            {
                sumCount = GenerateAttributes(MyCaptionBoxes[spot].Location, "= Count(" + section + ")", name, "{0:$#,0.00}");
            }
            ReportFooter.Items.Add(sumCount);
            totalBox = GenerateAttributes(MyCaptionBoxes[0].Location, "Grand Total: ", name, "{0:$#,0.00}");
            ReportFooter.Items.Add(totalBox);
            ReportFooter.Style.BackgroundColor = Color.FromArgb(89, 220, 216);
            AddReportFooter = true;
        }

        public static void GroupBy(string name, string val)
        {
            Debug.WriteLine("GroupBy: " + val);
            var myGroupBoxName = new TextBox();
            var myGroupBoxValue = new TextBox();
            myGroupBoxName = GenerateAttributes(new PointU(Unit.Inch(GroupLoc), Unit.Inch(CapLocY)), name, name, "{0}");
          
            myGroupBoxValue = GenerateAttributes(new PointU(Unit.Inch(GroupLoc + 1.4D), Unit.Inch(CapLocY)), val, name, "{0}");
            GroupLoc += 0.2D;
            GroupCount++;

            HeaderSections[GroupCount].Items.AddRange(new ReportItemBase[] {
            myGroupBoxName,
            myGroupBoxValue});

            AllGroups[GroupCount].Groupings.Add(new Grouping(val));
        }

        public static void ChangeBandColor(int bs, int section, int r, int g, int b)
        {
            if (bs == 0)
                HeaderSections[section].Style.BackgroundColor = Color.FromArgb(r, g, b);
            else
                FooterSections[section].Style.BackgroundColor = Color.FromArgb(r, g, b);
        }

        public static void AddSortings(int groupNum, string field, SortDirection dir)
        {
            Debug.WriteLine("YATA: " + groupNum + " field: " + field);
            AllGroups[groupNum].Sortings.Add(new Sorting(field, dir));
        }

        public static void SumOrCount(string name, int typeFlag, string field)
        {
            var sumCount = new TextBox();
            int spot = GetPosition(name);
            if (typeFlag == 1)
            {

                sumCount = GenerateAttributes(MyCaptionBoxes[spot].Location, "= Sum(" + field + ")", "", "{0:$#,0.00}");
                FooterSections[GroupCount].Items.Add(sumCount);
            }
            else if (typeFlag == 0)
            {
                sumCount = GenerateAttributes(MyCaptionBoxes[spot].Location, "= Count(" + field + ")", "", "{0:#,0}");
                FooterSections[GroupCount].Items.Add(sumCount);
            }
        }

        public static void GenerateTextField(string title, string data) //Adds labels
        {
            var boxLoc = new PointU(Unit.Inch(CapLocX), Unit.Inch(CapLocY));
            MyCaptionBoxes[count] = GenerateAttributes(boxLoc, title, title, "{0}");
            Debug.WriteLine("Count: "+GroupCount);
            Debug.WriteLine("data here: " + title+" "+data);
            HeaderSections[GroupCount].Items.Add(MyCaptionBoxes[count]);
            MyDataBoxes[count] = GenerateAttributes(boxLoc, data, title, "{0:$#,0.00}");
            Detail.Items.Add(MyDataBoxes[count]);
            CapLocX += 1.3666666507720947D;
            count++;
        }


        private static TextBox GenerateAttributes(PointU loc, string value, string name, string format)
        {
            var retBox = new TextBox();
            retBox.CanGrow = true;
            retBox.Size = TBSize;
            retBox.Location = loc;
            retBox.Value = value;
            retBox.Name = name;
            if (Regex.IsMatch(name, "date", RegexOptions.IgnoreCase) == false && Regex.IsMatch(name, "last", RegexOptions.IgnoreCase) == false) //all number formats except date
            {
                retBox.Format = format;
            }
            return retBox;
        }

        private static int GetPosition(string name)
        {
            for (int i = 0; i < MyCaptionBoxes.Length; i++)
            {
                if (MyCaptionBoxes[i].Name == name)
                {
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// Refreshes the data on the telerik report so new data can be added.
        /// </summary>
        public static void ResetDefaults()
        {
            for (int i = 0; i < count; i++)
            {
                MyDataBoxes[i].Value = "";
                MyCaptionBoxes[i].Value = "";
                MyDataBoxes[i] = new TextBox();
                MyCaptionBoxes[i] = new TextBox();
                AllGroups[i] = new Telerik.Reporting.Group();
            }
            for (int i = 0; i < 50; i++)
            {
                HeaderSections[i].Items.Clear();
                FooterSections[i].Items.Clear();
                AllGroups[i].Groupings.Clear();

            }
            GroupCount = 0;
            Summing = 1;
            Counting = 0;
            GroupLoc = 0.14083333395421505D;

            Detail.Items.Clear();
            count = 0;
            CapLocX = 1.2666666507720947D;
        }


        private void InitializeComponent2()
        {
            
            ((ISupportInitialize)(this)).BeginInit();
            
            // 
            // sqlDataSource1
            // 
            SqlDataSource1.ConnectionString = SqlConnectionString;
            SqlDataSource1.Name = "sqlDataSource1";
            SqlDataSource1.SelectCommand = SQLCommandString;
            
            this.DataSource = SqlDataSource1;
            for (int i = 0; i < (GroupCount + 1); i++)
            {
                this.Groups.Add(AllGroups[i]);
            }
            this.Items.Add(Detail);
            if (AddReportFooter) { this.Items.Add(ReportFooter); }


            //group1.GroupFooter = GroupFooter;
            //group1.GroupHeader = GroupHeader;
            //group1.Name = "labelsGroup";
            //this.Groups.AddRange(new Telerik.Reporting.Group[] {
            //group1});
            //this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            //GroupHeader,
            //GroupFooter,
            //PageHeader,
            //PageFooter,
            //ReportHeader,
            //ReportFooter,
            //DetailSection});
            //this.Name = "Report3";
            //this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D));
            //this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            //styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            //new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            //new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            //styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            //styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            //styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            //new Telerik.Reporting.Drawing.StyleSelector("Title")});
            //styleRule2.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(34)))), ((int)(((byte)(77)))));
            //styleRule2.Style.Font.Name = "Calibri";
            //styleRule2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18D);
            //styleRule3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            //new Telerik.Reporting.Drawing.StyleSelector("Caption")});
            //styleRule3.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(ColorR)))), ((int)(((byte)(ColorG)))), ((int)(((byte)(ColorB)))));
            //styleRule3.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(34)))), ((int)(((byte)(77)))));
            //styleRule3.Style.Font.Name = "Calibri";
            //styleRule3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            //styleRule3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            //styleRule4.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            //new Telerik.Reporting.Drawing.StyleSelector("Data")});
            //styleRule4.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(34)))), ((int)(((byte)(77)))));
            //styleRule4.Style.Font.Name = "Calibri";
            //styleRule4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            //styleRule4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            //styleRule5.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            //new Telerik.Reporting.Drawing.StyleSelector("PageInfo")});
            //styleRule5.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(34)))), ((int)(((byte)(77)))));
            //styleRule5.Style.Font.Name = "Calibri";
            //styleRule5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            //styleRule5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            //this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            //styleRule1,
            //styleRule2,
            //styleRule3,
            //styleRule4,
            //styleRule5});
            //this.Width = Telerik.Reporting.Drawing.Unit.Inch(6.4583334922790527D);
            ((ISupportInitialize)(this)).EndInit();

        }

    }
}
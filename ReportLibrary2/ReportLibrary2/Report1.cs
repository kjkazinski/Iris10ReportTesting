namespace ReportLibrary2
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using Telerik.Reporting.Data;
    using System.Diagnostics;
    using System.Data.SqlClient;
    using System.Collections; // For ArrayList
    using System.Collections.Generic;

	/// <summary>
	/// Summary description for Report1
	/// </summary>
    public partial class Report1 : Telerik.Reporting.Report
	{
        public static GroupHeaderSection GroupHeader = new Telerik.Reporting.GroupHeaderSection();
        public static GroupFooterSection GroupFooter = new Telerik.Reporting.GroupFooterSection();
        public static PageFooterSection PageFooter = new Telerik.Reporting.PageFooterSection();
        public static ReportHeaderSection ReportHeader = new Telerik.Reporting.ReportHeaderSection();
        public static ReportFooterSection ReportFooter = new Telerik.Reporting.ReportFooterSection();
        public static DetailSection DetailSection = new Telerik.Reporting.DetailSection();
        public static PageHeaderSection PageHeader = new Telerik.Reporting.PageHeaderSection();
        public static Telerik.Reporting.TextBox CurrentTime = new Telerik.Reporting.TextBox();
        public static Telerik.Reporting.TextBox PageInfo = new Telerik.Reporting.TextBox();
        public static Telerik.Reporting.TextBox TitleText = new Telerik.Reporting.TextBox();
        public static Telerik.Reporting.TextBox ReportName = new Telerik.Reporting.TextBox();
        public static String SqlConnectionString;
        public static string SortOption;
        public static SortDirection MySortDirection;
        public static String ComText;
        public static bool SortMe = false;
        public static Filter valueFilter;
        public static bool SetFilter = false;
        public static bool SetParams = false;

        //TextBox Vars
        private static int count = 0;
        private static int count2 = 0;
        public static Telerik.Reporting.TextBox[] MyCaptionBoxes = new Telerik.Reporting.TextBox[50];
        public static Telerik.Reporting.TextBox[] MyDataBoxes = new Telerik.Reporting.TextBox[50];
        public static double CapLocX = 0.02083333395421505D;
        public static double CapLocX2 = 0.02083333395421505D;

        public static ReportParameter[] ParameterList = new ReportParameter[50];
        public static Report1 myReport = new Report1();
        public static DateTimePicker myStart;
        public static DateTimePicker myEnd;

        public static SubReport secondReport = new SubReport();

        public static string myData;

        public Report1()
		{

            
            //
            // Required for telerik Reporting designer support
            //
            //InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //


            //SQL Connection strings to connect data sources to the Report
            SqlConnection sqlConnection1 = new SqlConnection();
            SqlCommand sqlSelectCommand1 = new SqlCommand();
            SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter();

            sqlConnection1.ConnectionString = SqlConnectionString;
            sqlSelectCommand1.CommandText = "SELECT        Activity.*\r\nFROM            Activity"; //ComText;
            sqlSelectCommand1.Connection = sqlConnection1;
            sqlDataAdapter1.SelectCommand = sqlSelectCommand1;
            this.DataSource = sqlDataAdapter1;

            

            //myData = sqlDataAdapter1.TableMappings.;

           // secondReport.Report.DataSource = sqlDataAdapter1;


            //SqlConnection sqlConnection2 = new SqlConnection();
            // SqlCommand sqlSelectCommand2 = new SqlCommand();
            // SqlDataAdapter sqlDataAdapter2 = new SqlDataAdapter();


            // sqlConnection2.ConnectionString = "Data Source=devServer;Initial Catalog=Z_Baker;User ID=sa;Password=data22";
            // sqlSelectCommand2.CommandText = "SELECT Activity.* FROM Activity";
            // sqlSelectCommand2.Connection = sqlConnection2;
            // sqlDataAdapter2.SelectCommand = sqlSelectCommand2;
            // myReport.DataSource = sqlDataAdapter2;

            // secondReport.ReportSource = myReport.ItemDataBound();


            InitializeComponent2();
            
            //Grouping test Code

            // Group group = new Group();

            //Grouping groupExpression = new Grouping("DateStamp"); //Name is static field header
            //Grouping groupExpression2 = new Grouping("Perform_Standard"); //Name is static field header
            //group.Groupings.Add(groupExpression);
            //group.Groupings.Add(groupExpression2);
            //group.GroupHeader = new GroupHeaderSection();
            //group.GroupHeader.Height = new Unit(0.3, UnitType.Inch); //0.3 static size, change to dyn
            //group.GroupHeader.Style.BackgroundColor = Color.LightBlue;
            //Telerik.Reporting.TextBox tbRegion = new Telerik.Reporting.TextBox();
            //tbRegion.Value = "=Perform_Standard";
            //tbRegion.Size = new SizeU(
            //    new Unit(1, UnitType.Inch),
            //    new Unit(0.25, UnitType.Inch));
            //group.GroupHeader.Items.Add(tbRegion);
            //  group.GroupFooter = new GroupFooterSection();
            //   group.GroupFooter.Height = new Unit(0.3, UnitType.Inch);
            //  group.GroupFooter.Style.BackgroundColor = Color.LightGreen;
            // Telerik.Reporting.TextBox tbTotals = new Telerik.Reporting.TextBox();
            //  tbTotals.Value = "= Count(Fields.Name)";// + \" Count(Fields.Name)";
            // tbTotals.Docking = Telerik.Reporting.DockingStyle.Fill; //Dock is obsolete use Docking
            // tbTotals.Style.TextAlign = HorizontalAlign.Center;
            //tbTotals.Format = "{0:C2}";  //Money Format
            //  tbTotals.Style.Font.Bold = true;
            // group.GroupFooter.Items.Add(tbTotals);
            // this.Groups.Add(group);





            //Report Parameters
            /*
            ReportParameter list1 = new ReportParameter();
            list1.Name = "StartDate";
            list1.Text = "Enter Start Date:";
            list1.Type = ReportParameterType.DateTime;
            list1.AllowBlank = false;
            list1.AllowNull = false;
            list1.Visible = true;
            this.ReportParameters.Add(list1);

            ReportParameter list2 = new ReportParameter();
            list2.Name = "EndDate";
            list2.Text = "Enter End Date:";
            list2.Type = ReportParameterType.DateTime;
            list2.AllowBlank = false;
            list2.AllowNull = false;
            list2.Visible = true;
            this.ReportParameters.Add(list2);
    */
            if (SetParams == true)
            {
                Filter dateFilter1 = new Filter("=Fields.DateStamp", FilterOperator.GreaterOrEqual, myStart.Value.ToString());
                this.Filters.Add(dateFilter1);
                Filter dateFilter2 = new Filter("=Fields.DateStamp", FilterOperator.LessOrEqual, myEnd.Value.ToString());
                this.Filters.Add(dateFilter1);
            }
            
            

            //Filter Test
            // Filter reportFilter = new Filter("=Fields.Name", FilterOperator.LessThan, "10");
            //Report.Filters.Add(reportFilter);
            // Filter groupFilter = new Filter("=Fields.Description", FilterOperator.NotLike, "%C");
            // Report.Filters.Add(groupFilter);
            // Filter dateFilter1 = new Filter("=Fields.DateStamp", FilterOperator.GreaterOrEqual, "=parameters.StartDate.Value");
            // Report.Filters.Add(dateFilter1);
            // Filter dateFilter2 = new Filter("=Fields.DateStamp", FilterOperator.LessOrEqual, "=parameters.EndDate.Value");
            //Report.Filters.Add(dateFilter2);


            if (SetFilter == true)
            {
                Report.Filters.Add(valueFilter);
            }
            


            //Sorting

            if (SortMe == true)
            {
                this.Sortings.Add(
                    new Sorting(SortOption, MySortDirection));

            }
        }

       public static string GetData()
        {
            return myData;
        }

        public static void AddFilter(string s, FilterOperator t,string n)
        {
            valueFilter = new Filter(s, t, n);
            SetFilter = true;

        }


        public static void SortOptions(string s, SortDirection d,bool e)
        {
            SortOption = s;
            MySortDirection = d;
            SortMe = e;
        }

        /// <summary>
        /// Creates a new telerik textbox and adds it to the header section for the report
        /// </summary>
        public static void GenerateTextField(string val)
        {
            MyCaptionBoxes[count] = new Telerik.Reporting.TextBox();
            GroupHeader.Items.Add(MyCaptionBoxes[count]);

            MyCaptionBoxes[count].CanGrow = true;
            MyCaptionBoxes[count].Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(CapLocX), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            MyCaptionBoxes[count].Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2666666507720947D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            MyCaptionBoxes[count].Style.Visible = false;
            MyCaptionBoxes[count].StyleName = "Caption";
            MyCaptionBoxes[count].Value = val;
            MyCaptionBoxes[count].Visible = true;
            CapLocX += 1.3D;
            count++;
        }

        /// <summary>
        /// Creates a new telerik textbox and adds it to the data section of the telerik report
        /// </summary>
        public static void GenerateDataField(string val)
        {
            MyDataBoxes[count2] = new Telerik.Reporting.TextBox();
            DetailSection.Items.Add(MyDataBoxes[count2]);

            MyDataBoxes[count2].CanGrow = true;
            MyDataBoxes[count2].Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(CapLocX2), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            MyDataBoxes[count2].Style.Visible = false;
            MyDataBoxes[count2].Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2666666507720947D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            MyDataBoxes[count2].StyleName = "Data";
            MyDataBoxes[count2].Value = val;
            MyDataBoxes[count2].Visible = true;
            CapLocX2 += 1.3D;
            count2++;
        }

        /// <summary>
        /// Refreshes the data on the telerik report so new data can be added.
        /// </summary>
        public static void ResetDefaults()
        {
            
            for(int i = 0; i < count; i++)
            {
                MyDataBoxes[i].Value = "";
                MyCaptionBoxes[i].Value = "";
                MyDataBoxes[i] = new Telerik.Reporting.TextBox();
                MyCaptionBoxes[i] = new Telerik.Reporting.TextBox();
            }

            GroupHeader.Items.Clear();
            DetailSection.Items.Clear();
            count = 0;
            count2 = 0;
            CapLocX = 0.02083333395421505D;
            CapLocX2 = 0.02083333395421505D;
        }


        public static void ChangeName(string name)
        {
            TitleText.Value = name;
            ReportName.Value = name;
        }

        public static void ChangeSqlString(string sqlCon)
        {
            SqlConnectionString = sqlCon;
        }

        public void InitializeComponent2()
        {
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule5 = new Telerik.Reporting.Drawing.StyleRule();


            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // labelsGroupHeaderSection
            // 
            GroupHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(0.44166669249534607D);
            GroupHeader.Name = "labelsGroupHeaderSection";
            GroupHeader.PrintOnEveryPage = true;
            // 
            // labelsGroupFooterSection
            // 
            GroupFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(0.28125D);
            GroupFooter.Name = "labelsGroupFooterSection";
            GroupFooter.Style.Visible = false;
           
            // 
            // pageFooter
            // 
            PageFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(0.42083334922790527D);
            PageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            CurrentTime,
            PageInfo});
            PageFooter.Name = "pageFooter";
            // 
            // currentTimeTextBox
            // 
            CurrentTime.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            CurrentTime.Name = "currentTimeTextBox";
            CurrentTime.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.9791667461395264D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            CurrentTime.StyleName = "PageInfo";
            CurrentTime.Value = "=NOW()";
            // 
            // pageInfoTextBox
            // 
            PageInfo.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.0000789165496826D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            PageInfo.Name = "pageInfoTextBox";
            PageInfo.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.4374210834503174D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            PageInfo.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            PageInfo.StyleName = "PageInfo";
            PageInfo.Value = "=PageNumber + \" of \"+PageCount";
            // 
            // reportHeader
            // 
            ReportHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(0.80823493003845215D);
            ReportHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            TitleText});
            ReportHeader.Name = "reportHeader";
            // 
            // titleTextBox
            // 
            TitleText.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            TitleText.Name = "titleTextBox";
            TitleText.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.4583334922790527D), Telerik.Reporting.Drawing.Unit.Inch(0.787401556968689D));
            TitleText.StyleName = "Title";
            // 
            // reportFooter
            // 
            ReportFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(0.28125D);
            ReportFooter.Name = "reportFooter";
            // 
            // detail
            // 
            DetailSection.Height = Telerik.Reporting.Drawing.Unit.Inch(0.44166669249534607D);
            DetailSection.Name = "detail";
            
            // 
            // pageHeader
            // 
            PageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(0.44166669249534607D);
            PageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            ReportName});
            PageHeader.Name = "pageHeader";
            // 
            // reportNameTextBox
            // 
            ReportName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            ReportName.Name = "reportNameTextBox";
            ReportName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.4166665077209473D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            ReportName.StyleName = "PageInfo";
            // 
            // Report1
            // 
            group1.GroupFooter = GroupFooter;
            group1.GroupHeader = GroupHeader;
            group1.Name = "labelsGroup";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            GroupHeader,
            GroupFooter,
            PageHeader,
            PageFooter,
            ReportHeader,
            ReportFooter,
            DetailSection});
            this.Name = "Report1";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Title")});
            styleRule2.Style.Color = System.Drawing.Color.Black;
            styleRule2.Style.Font.Bold = true;
            styleRule2.Style.Font.Italic = false;
            styleRule2.Style.Font.Name = "Tahoma";
            styleRule2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18D);
            styleRule2.Style.Font.Strikeout = false;
            styleRule2.Style.Font.Underline = false;
            styleRule3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Caption")});
            styleRule3.Style.Color = System.Drawing.Color.Black;
            styleRule3.Style.Font.Name = "Tahoma";
            styleRule3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            styleRule3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule4.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Data")});
            styleRule4.Style.Font.Name = "Tahoma";
            styleRule4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            styleRule4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule5.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("PageInfo")});
            styleRule5.Style.Font.Name = "Tahoma";
            styleRule5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            styleRule5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2,
            styleRule3,
            styleRule4,
            styleRule5});
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(6.4583334922790527D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }


    }
}
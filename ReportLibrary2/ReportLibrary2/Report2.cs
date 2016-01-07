namespace ReportLibrary2
{
    using System;
    using System.ComponentModel;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using System.Diagnostics;

    /// <summary>
    /// Summary description for Report2.
    /// </summary>
    public partial class Report2 : Telerik.Reporting.Report
    {
        public static GroupHeaderSection groupHeader = new Telerik.Reporting.GroupHeaderSection();
        public static GroupFooterSection groupFooter = new Telerik.Reporting.GroupFooterSection();
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
        public static Report2 myReport = new Report2();
        public static DateTimePicker myStart;
        public static DateTimePicker myEnd;

        public static SubReport secondReport = new SubReport();

        public static string myData;
        public Report2()
        {

            //SqlConnection sqlConnection1 = new SqlConnection();
            //SqlCommand sqlSelectCommand1 = new SqlCommand();
            //SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter();

            //sqlConnection1.ConnectionString = "ReportLibrary2.Properties.Settings.ZMarion";
            //sqlSelectCommand1.CommandText = "SELECT        Activity.*\r\nFROM            Activity";
            //sqlSelectCommand1.Connection = sqlConnection1;
            //sqlDataAdapter1.SelectCommand = sqlSelectCommand1;
            //this.DataSource = sqlDataAdapter1;
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            //this.DataSource
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        public static void ChangeSqlString(string sqlCon)
        {
            SqlConnectionString = sqlCon;
            Debug.WriteLine("THIS IS FROM THE REPORT" + SqlConnectionString);
        }

        public static void GetJSON(string json)
        {
            Debug.WriteLine("JSON" + json);
        }

        public void InitializeComponent2()
        {
            Telerik.Reporting.TableGroup tableGroup1 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup2 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup3 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup4 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.DescendantSelector descendantSelector1 = new Telerik.Reporting.Drawing.DescendantSelector();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.DescendantSelector descendantSelector2 = new Telerik.Reporting.Drawing.DescendantSelector();
            this.detailSection1 = new Telerik.Reporting.DetailSection();
            this.sqlDataSource1 = new Telerik.Reporting.SqlDataSource();
            this.table1 = new Telerik.Reporting.Table();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detailSection1
            // 
            this.detailSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(1D);
            this.detailSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.table1});
            this.detailSection1.Name = "detailSection1";
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionString = SqlConnectionString;
            this.sqlDataSource1.Name = "sqlDataSource1";
            this.sqlDataSource1.SelectCommand = "SELECT        Activity.*\r\nFROM            Activity";
            // 
            // table1
            // 
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929D)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929D)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929D)));
            this.table1.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D)));
            this.table1.Body.SetCellContent(0, 0, this.textBox4);
            this.table1.Body.SetCellContent(0, 1, this.textBox5);
            this.table1.Body.SetCellContent(0, 2, this.textBox6);
            tableGroup1.ReportItem = this.textBox1;
            tableGroup2.ReportItem = this.textBox2;
            tableGroup3.ReportItem = this.textBox3;
            this.table1.ColumnGroups.Add(tableGroup1);
            this.table1.ColumnGroups.Add(tableGroup2);
            this.table1.ColumnGroups.Add(tableGroup3);
            this.table1.DataSource = this.sqlDataSource1;
            this.table1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox4,
            this.textBox5,
            this.textBox6,
            this.textBox1,
            this.textBox2,
            this.textBox3});
            this.table1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.table1.Name = "table1";
            tableGroup4.Groupings.Add(new Telerik.Reporting.Grouping(null));
            tableGroup4.Name = "Detail";
            this.table1.RowGroups.Add(tableGroup4);
            this.table1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.4000000953674316D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.table1.StyleName = "Apex.TableNormal";
            // 
            // textBox1
            // 
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox1.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox1.StyleName = "Apex.TableHeader";
            this.textBox1.Value = "Date Stamp";
            // 
            // textBox2
            // 
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox2.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox2.StyleName = "Apex.TableHeader";
            this.textBox2.Value = "Description";
            // 
            // textBox3
            // 
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox3.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox3.StyleName = "Apex.TableHeader";
            this.textBox3.Value = "Name";
            // 
            // textBox4
            // 
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox4.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox4.StyleName = "Apex.TableBody";
            this.textBox4.Value = "= Fields.DateStamp";
            // 
            // textBox5
            // 
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox5.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox5.StyleName = "Apex.TableBody";
            this.textBox5.Value = "= Fields.Description";
            // 
            // textBox6
            // 
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox6.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox6.StyleName = "Apex.TableBody";
            this.textBox6.Value = "= Fields.Name";
            // 
            // textBox7
            // 
            this.textBox7.Name = "ReportNameTextBox";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.9055118560791016D), Telerik.Reporting.Drawing.Unit.Inch(0.787401556968689D));
            this.textBox7.Style.Font.Bold = true;
            this.textBox7.Style.Font.Name = "Segoe UI";
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14D);
            this.textBox7.Value = "Report2";
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(1D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox7});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.425196647644043D), Telerik.Reporting.Drawing.Unit.Inch(0.60629922151565552D));
            this.textBox8.Name = "ReportPageNumberTextBox";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5748031139373779D), Telerik.Reporting.Drawing.Unit.Inch(0.39370077848434448D));
            this.textBox8.Style.Font.Name = "Segoe UI";
            this.textBox8.Value = "Page: {PageNumber}";
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(1D);
            this.pageFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox8});
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // Report2
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.detailSection1,
            this.pageHeaderSection1,
            this.pageFooterSection1});
            this.Name = "Report2";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.Table), "Apex.TableNormal")});
            styleRule2.Style.BorderColor.Default = System.Drawing.Color.Black;
            styleRule2.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule2.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            styleRule2.Style.Color = System.Drawing.Color.Black;
            styleRule2.Style.Font.Name = "Book Antiqua";
            styleRule2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            descendantSelector1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.Table)),
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.ReportItem), "Apex.TableBody")});
            styleRule3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            descendantSelector1});
            styleRule3.Style.BorderColor.Default = System.Drawing.Color.Black;
            styleRule3.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule3.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            styleRule3.Style.Font.Name = "Book Antiqua";
            styleRule3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            descendantSelector2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.Table)),
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.ReportItem), "Apex.TableHeader")});
            styleRule4.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            descendantSelector2});
            styleRule4.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(103)))), ((int)(((byte)(109)))));
            styleRule4.Style.BorderColor.Default = System.Drawing.Color.Black;
            styleRule4.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule4.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            styleRule4.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(185)))), ((int)(((byte)(102)))));
            styleRule4.Style.Font.Name = "Book Antiqua";
            styleRule4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            styleRule4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2,
            styleRule3,
            styleRule4});
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(6D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

    }
}
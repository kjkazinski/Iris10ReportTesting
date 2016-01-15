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
    using System.Xml;
    using System.Text;
    using System.IO;

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
        public static Filter[] MyFilters = new Filter[50];
        public static int FilterCount = 0;
        public static string myString = "";


        public Report3()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent2();
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            
            Report.Filters.Clear();
            for (int i = 0; i < FilterCount; i++)
            {
                Report.Filters.Add(MyFilters[i]);
            }

            using (var sw = new StringWriter())
            {
                using (var xw = XmlWriter.Create(sw))
                {
                    // Build Xml with xw.
                    Telerik.Reporting.XmlSerialization.ReportXmlSerializer xmlSerializer =
                    new Telerik.Reporting.XmlSerialization.ReportXmlSerializer();

                    xmlSerializer.Serialize(xw, this.Report);

                }
                myString = sw.ToString();
            }

            Debug.WriteLine("not XML: " + Detail.Items[0]);

        }

        public static void GetJSON(string json)
        {
            Debug.WriteLine("JSON: " + json);
            ReportModel data = new ReportModel(json);
            ResetDefaults();
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
                CreateFilterOption(data.Filters.GetValue(i).ToString());
            }

            for (int c = 0; c < data.SumOrCount.Length; c++)
                     {
                      SumOrCount("=Fields." + data.SumOrCount.GetValue(c).ToString().Substring(1, data.SumOrCount.GetValue(c).ToString().Length - 2), data.AggregateType.GetValue(c).ToString(), "Fields." + data.SumOrCount.GetValue(c).ToString().Substring(1, data.SumOrCount.GetValue(c).ToString().Length - 2));
                     }
            

                     ChangeSqlString(data.ConnectionString);
            SQLCommandString = data.SelectCommand;

            
        }

        public static void SetXMLObject()
        {
            //Fill in code to save this to a seperate class
            /*  Example Report XML from serialized Object:

            <?xml version="1.0" encoding="utf-16"?>
            <Report DataSourceName="sqlDataSource1" Width="6in" UnitOfMeasure="Inch" PageNumberingStyle="Continue" StyleName="" DocumentMapText="" xmlns="http://schemas.telerik.com/reporting/2012/3.8">
            <DataSources>
            <SqlDataSource ConnectionString="Initial Catalog=A_Wallowa9;Data Source=10.0.0.40;User ID=developer;Password=aociris;" SelectCommand="SELECT * FROM Activity" Name="sqlDataSource1" />
            </DataSources>
            <Items>
            <DetailSection KeepTogether="True" Height="1in">
            <Items>
            <TextBox Width="1.26666665077209in" Height="0.441666692495346in" Left="1.26666665077209in" Top="0.140833333954215in" Value="=Fields.Activity_Key" Format="{0:$#,0.00}" CanGrow="True" Multiline="True" Culture="null" Name="Activity Key" />
            <TextBox Width="1.26666665077209in" Height="0.441666692495346in" Left="2.63333330154419in" Top="0.140833333954215in" Value="=Fields.DateStamp" CanGrow="True" Name="Date" />
            <TextBox Width="1.26666665077209in" Height="0.441666692495346in" Left="3.99999995231628in" Top="0.140833333954215in" Value="=Fields.Description" Format="{0:$#,0.00}" CanGrow="True" Name="Description" />
            <TextBox Width="1.26666665077209in" Height="0.441666692495346in" Left="5.36666660308838in" Top="0.140833333954215in" Value="=Fields.UOM_Key" Format="{0:$#,0.00}" CanGrow="True" Name="UOM" />
            </Items>
            </DetailSection>
            </Items>
            <PageSettings>
            <PageSettings PaperKind="Letter">
            <Margins>
            <MarginsU Left="1in" Right="1in" Top="1in" Bottom="1in" />
            </Margins>
            </PageSettings>
            </PageSettings>
            <Filters>
            <Filter Expression="=Fields.Activity_Key" Operator="Like" Value="%1%" />
            </Filters>
            <Groups>
            <Group>
            <GroupHeader>
            <GroupHeaderSection Height="0.441666692495346in">
            <Style BackgroundColor="224, 224, 224" />
            <Items>
            <TextBox Width="1.26666665077209in" Height="0.441666692495346in" Left="1.26666665077209in" Top="0.140833333954215in" Value="Activity Key" Format="{0}" CanGrow="True" Name="Activity Key" />
            <TextBox Width="1.26666665077209in" Height="0.441666692495346in" Left="2.63333330154419in" Top="0.140833333954215in" Value="Date" CanGrow="True" Name="Date" />
            <TextBox Width="1.26666665077209in" Height="0.441666692495346in" Left="3.99999995231628in" Top="0.140833333954215in" Value="Description" Format="{0}" CanGrow="True" Name="Description" />
            <TextBox Width="1.26666665077209in" Height="0.441666692495346in" Left="5.36666660308838in" Top="0.140833333954215in" Value="UOM" Format="{0}" CanGrow="True" Name="UOM" />
            </Items>
            </GroupHeaderSection>
            </GroupHeader>
            <GroupFooter>
            <GroupFooterSection PrintAtBottom="False" Height="0.441666692495346in" />
            </GroupFooter>
            </Group>
            <Group>
            <GroupHeader>
            <GroupHeaderSection Height="0.441666692495346in">
            <Style BackgroundColor="224, 224, 224" />
            <Items>
            <TextBox Width="1.26666665077209in" Height="0.441666692495346in" Left="0.140833333954215in" Top="0.140833333954215in" Value="UOM_Key" Format="{0}" CanGrow="True" Name="UOM_Key" />
            <TextBox Width="1.26666665077209in" Height="0.441666692495346in" Left="1.54083333395422in" Top="0.140833333954215in" Value="=Fields.UOM_Key" Format="{0}" CanGrow="True" Name="UOM_Key" />
            </Items>
            </GroupHeaderSection>
            </GroupHeader>
            <GroupFooter>
            <GroupFooterSection Height="0.441666692495346in">
            <Items>
            <TextBox Width="1.26666665077209in" Height="0.441666692495346in" Left="3.99999995231628in" Top="0.140833333954215in" Value="= Count(Fields.Description)" Format="{0:#,0}" CanGrow="True" Name="" />
            <TextBox Width="1.26666665077209in" Height="0.441666692495346in" Left="5.36666660308838in" Top="0.140833333954215in" Value="= Count(Fields.UOM_Key)" Format="{0:#,0}" CanGrow="True" Name="" />
            </Items>
            </GroupFooterSection>
            </GroupFooter>
            <Groupings>
            <Grouping Expression="=Fields.UOM_Key" />
            </Groupings>
            </Group>
            </Groups>
            </Report>



            */
        }


        public static void CreateFilterOption(string obj)
        {
            //var field = "";
            //var operand = "";
            //var val = "";
            obj = obj.Replace('|', ',');
            Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(obj);
            Filter reportFilter = new Filter("=Fields."+values["field"],GetOper(values["operator"]), FixValue(values["operator"],values["value"]));
            //Filter Test
            // Filter reportFilter = new Filter("=Fields.Name", FilterOperator.LessThan, "10");
            //Report.Filters.Add(reportFilter);
            // Filter groupFilter = new Filter("=Fields.Description", FilterOperator.NotLike, "%C");
            // Report.Filters.Add(groupFilter);
            // Filter dateFilter1 = new Filter("=Fields.DateStamp", FilterOperator.GreaterOrEqual, "=parameters.StartDate.Value");
            // Report.Filters.Add(dateFilter1);
            // Filter dateFilter2 = new Filter("=Fields.DateStamp", FilterOperator.LessOrEqual, "=parameters.EndDate.Value");
            MyFilters[FilterCount] = reportFilter;
            FilterCount++;


            
            Debug.WriteLine("obj1: " + values["field"]);
        }
        
        private static FilterOperator GetOper(string op)
        {
            switch (op)
            {
                case "contains":
                    return FilterOperator.Like;
                case "endswith":
                    return FilterOperator.Like;
                case "eq":
                    return FilterOperator.Equal;
                case "neq":
                    return FilterOperator.NotEqual;
                case "startswith":
                    return FilterOperator.Like;
                case "doesnotcontain":
                    return FilterOperator.NotLike;
                case "lte":
                    return FilterOperator.LessOrEqual;
                case "lt":
                    return FilterOperator.LessThan;
                case "gte":
                    return FilterOperator.GreaterOrEqual;
                case "gt":
                    return FilterOperator.GreaterThan;
                
            }
            return FilterOperator.Equal;
        }

        private static string FixValue(string op,string val)
        {
            if(op == "contains")
            {
                return "%" + val + "%";
            }
            else if(op == "doesnotcontain")
            {
                return "%" + val + "%";
            }
            else if(op == "endswith")
            {
                return "%" + val;
            }
            else if (op == "startswith")
            {
                return val+"%";
            }
            else
            {
                return val;
            }
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
                     Debug.WriteLine("my name is: " + name);
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

        public static void SumOrCount(string name, string typeFlag, string field)
        {
            var sumCount = new TextBox();
            int spot = GetDataPosition(name);
            if (typeFlag.Contains("sum"))
            {

                sumCount = GenerateAttributes(MyDataBoxes[spot].Location, "= Sum(" + field + ")", "", "{0:$#,0.00}");
                FooterSections[GroupCount].Items.Add(sumCount);
            }
            else if (typeFlag.Contains("count"))
            {
                sumCount = GenerateAttributes(MyDataBoxes[spot].Location, "= Count(" + field + ")", "", "{0:#,0}");
                FooterSections[GroupCount].Items.Add(sumCount);
            }

            else if (typeFlag.Contains("average"))
                     {
                            sumCount = GenerateAttributes(MyDataBoxes[spot].Location, "= Avg(" + field + ")", "", "{0:$#,0.00}");
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

              private static int GetDataPosition(string name)
              {
                     for (int i = 0; i < MyDataBoxes.Length; i++)
                     {
                            if (MyDataBoxes[i].Value == name)
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
                if (HeaderSections[i] != null)
                {
                    HeaderSections[i].Items.Clear();
                }
                if (FooterSections[i] != null)
                {
                    FooterSections[i].Items.Clear();
                }
                if (AllGroups[i] != null)
                {
                    AllGroups[i].Groupings.Clear();
                }

            }
            FilterCount = 0;
            GroupCount = 0;
            Summing = 1;
            Counting = 0;
            GroupLoc = 0.14083333395421505D;

            Detail.Items.Clear();
            count = 0;
            CapLocX = 1.2666666507720947D;
        }

              //this test   
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
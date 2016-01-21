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
        public static String SqlCommandString;
        public static SqlDataSource SqlDataSource1 = new SqlDataSource();


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
        public static SizeU TBSize = new SizeU(Unit.Inch(CapLocX), Unit.Inch(TBHeight));
        public static Filter[] MyFilters = new Filter[50];
        public static int FilterCount = 0;
        public static string MyString = "";


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

            #region //This code creates an XML object
            using (var sw = new StringWriter())
            {
                using (var xw = XmlWriter.Create(sw))
                {
                    // Build Xml with xw.
                    Telerik.Reporting.XmlSerialization.ReportXmlSerializer xmlSerializer =
                    new Telerik.Reporting.XmlSerialization.ReportXmlSerializer();

                    xmlSerializer.Serialize(xw, this.Report);

                }
                MyString = sw.ToString();
            }
            #endregion
        }

        public static void GetJSON(string json)
        {
            ReportModel data = new ReportModel(json);
            ResetDefaults();
            GenerateHeaderFooters();
            for (int k = 0; k < data.GenerateTitleField.Length; k++)
            {
                GenerateTextField(data.GenerateTitleField.GetValue(k).ToString(), "=Fields." + data.GenerateDataField.GetValue(k).ToString());
            }
            for (int a = 0; a < data.GroupBy.Length; a++)
            {
                GroupBy(data.GroupName.GetValue(a).ToString(), "=Fields." + data.GroupBy.GetValue(a).ToString());
            }
            for (int i = 0; i < data.Filters.Length; i++)
            {
                CreateFilterOption(data.Filters.GetValue(i).ToString());
            }

            for (int c = 0; c < data.SumOrCount.Length; c++)
            {
                SumOrCount("=Fields." + data.SumOrCount.GetValue(c).ToString().Substring(1, data.SumOrCount.GetValue(c).ToString().Length - 2), data.AggregateType.GetValue(c).ToString(), "Fields." + data.SumOrCount.GetValue(c).ToString().Substring(1, data.SumOrCount.GetValue(c).ToString().Length - 2));
            }
            if (data.AddReportFooterSection == true)
            {
                AddReportFooterSection(data);
            }

            ChangeSqlString(data.ConnectionString);
            SqlCommandString = data.SelectCommand;

            
        }

        //Example XML object from report
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
            obj = obj.Replace('|', ',');
            Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(obj);
            Filter reportFilter = new Filter("=Fields."+values["field"],GetOper(values["operator"]), FixValue(values["operator"],values["value"]));
            MyFilters[FilterCount] = reportFilter;
            FilterCount++;
        }
        
        public static void ChangeSqlString(string sqlCon)
        {
            SqlConnectionString = sqlCon;
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

        public static void AddReportFooterSection(ReportModel data)
        {
            var totalBox = new TextBox();
            string name = "";
            for (int i = 0; i < data.AggregateType.Length; i++)
            {
                string typeFlag;
                string field;
                typeFlag = data.AggregateType.GetValue(i).ToString();
                field = "Fields." + data.SumOrCount.GetValue(i).ToString().Substring(1, data.SumOrCount.GetValue(i).ToString().Length - 2);
                name = "=Fields." + data.SumOrCount.GetValue(i).ToString().Substring(1, data.SumOrCount.GetValue(i).ToString().Length - 2);
                int spot = GetDataPosition(name);
                ReportFooter.Items.Add(FooterHelper(typeFlag,field, spot));
            }

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
            int spot = GetDataPosition(name);
            FooterSections[GroupCount].Items.Add(FooterHelper(typeFlag,field,spot));
        }
        
        public static void GenerateTextField(string title, string data) //Adds labels
        {
            var boxLoc = new PointU(Unit.Inch(CapLocX), Unit.Inch(CapLocY));
            MyCaptionBoxes[count] = GenerateAttributes(boxLoc, title, title, "{0}");
            HeaderSections[GroupCount].Items.Add(MyCaptionBoxes[count]);
            MyDataBoxes[count] = GenerateAttributes(boxLoc, data, title, "{0:$#,0.00}");
            Detail.Items.Add(MyDataBoxes[count]);
            CapLocX += 1.3666666507720947D;
            count++;
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
            GroupLoc = 0.14083333395421505D;

            Detail.Items.Clear();
            count = 0;
            CapLocX = 1.2666666507720947D;
        }
   
        private void InitializeComponent2()
        {
            ((ISupportInitialize)(this)).BeginInit();
            SqlDataSource1.ConnectionString = SqlConnectionString;
            SqlDataSource1.Name = "sqlDataSource1";
            SqlDataSource1.SelectCommand = SqlCommandString;
            
            this.DataSource = SqlDataSource1;
            for (int i = 0; i < (GroupCount + 1); i++)
            {
                this.Groups.Add(AllGroups[i]);
            }
            this.Items.Add(Detail);
            if (AddReportFooter) { this.Items.Add(ReportFooter); }
            ((ISupportInitialize)(this)).EndInit();
        }
    }


    //Helper Functions
    public partial class Report3 : Report
    {
        private static TextBox FooterHelper(string type, string field, int spot)
        {
            var sumCount = new TextBox();
            if (type.Contains("sum"))
            {

                sumCount = GenerateAttributes(MyDataBoxes[spot].Location, "= Sum(" + field + ")", "", "{0:$#,0.00}");
            }
            else if (type.Contains("count"))
            {
                sumCount = GenerateAttributes(MyDataBoxes[spot].Location, "= Count(" + field + ")", "", "{0:#,0}");
            }
            else if (type.Contains("average"))
            {
                sumCount = GenerateAttributes(MyDataBoxes[spot].Location, "= Avg(" + field + ")", "", "{0:$#,0.00}");
            }
            return sumCount;
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

        private static string FixValue(string op, string val)
        {
            if (op == "contains")
            {
                return "%" + val + "%";
            }
            else if (op == "doesnotcontain")
            {
                return "%" + val + "%";
            }
            else if (op == "endswith")
            {
                return "%" + val;
            }
            else if (op == "startswith")
            {
                return val + "%";
            }
            else
            {
                return val;
            }
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
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.Reporting;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        public String MyVal;
        public CheckedListBox MyListBox;
        public CheckedListBox.CheckedItemCollection MyList;
        public string MySqlStatement;
        public SortDirection MyD;
        public string SortField;
        public bool Sorting;
        public string TopBotAnswer;
        public string ValuePerN;
        public string TextValue;
        public Form1()
        {
            InitializeComponent();
            reportViewer1.RefreshReport();
        }

       

        private void ChangeReport_Click(object sender, EventArgs e)
        {
            ReportLibrary2.Report1.ResetDefaults();
            ReportLibrary2.Report1.ChangeName(MyVal);
            string sqlString = "Data Source=devServer;Initial Catalog=Z_" + MyVal + ";User ID=sa;Password=data22";
            ReportLibrary2.Report1.ChangeSqlString(sqlString);
            
            
            ReportLibrary2.Report1.ComText = MySqlStatement;
                
            for (int i = 0; i < MyList.Count; i++)
            {
                ReportLibrary2.Report1.GenerateTextField(MyList[i].ToString());
                ReportLibrary2.Report1.GenerateDataField("= Fields." + MyList[i].ToString());
            }

            
            ReportLibrary2.Report1.SortOptions(SortField, MyD, Sorting);
           // ReportLibrary2.Report1.CreateTable();
           // textBox1.Text = ReportLibrary2.Report1.GetData().ToString();
            reportViewer1.RefreshReport();
        }

        private void ServerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var myArg = (ComboBox)sender;
            MyVal = myArg.Text;
        }


        private void CheckListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string myString = "";
            MyListBox = (CheckedListBox)sender;
            MyList = MyListBox.CheckedItems;
            for (int i = 0; i < MyList.Count; i++)
            {
                myString += MyList[i].ToString();
               
                if ((i+1) < MyList.Count)
                {
                    myString += ", ";
                }
            }
            MySqlStatement = "SELECT "+ myString + " FROM Activity";
        }

        private void SortDir_SelectedIndexChanged(object sender, EventArgs e)
        {
            var mySort = (ComboBox)sender;
            if(mySort.Text == "Ascending")
            {
                MyD = SortDirection.Asc;
            }else if(mySort.Text == "Descending")
            {
                MyD = SortDirection.Desc;
            }
        }

        private void FieldBox_TextChanged(object sender, EventArgs e)
        {
            var myField = (System.Windows.Forms.TextBox)sender;
            SortField = myField.Text;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var sort = (System.Windows.Forms.CheckBox)sender;
            Sorting = sort.Checked;
        }

        

        private void filterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void filterOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FilterOptions.Visible = !FilterOptions.Visible;
            reportViewer1.RefreshReport();
        }

        //DatePicker Button
        private void SetDateFilter_Click(object sender, EventArgs e)
        {
            FilterOptions.Visible = !FilterOptions.Visible;
            ReportLibrary2.Report1.myStart = dateTimePicker1;
            ReportLibrary2.Report1.myEnd = dateTimePicker2;
            ReportLibrary2.Report1.SetParams = true;
        }

        private void valueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ValueGroup.Visible = !ValueGroup.Visible;
        }

        private void TopBot_SelectedIndexChanged(object sender, EventArgs e)
        {
            var temp = (ComboBox)sender;
            TopBotAnswer = temp.Text;
        }

        private void ValueOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            var temp = (ComboBox)sender;
            ValuePerN = temp.Text;
        }

        private void ValueTextBox_TextChanged(object sender, EventArgs e)
        {
            var temp = (System.Windows.Forms.TextBox)sender;
            TextValue = "="+temp.Text;
        }

        private void ValueFilterButton_Click(object sender, EventArgs e)
        {
            FilterOperator myOp;

            if(TopBotAnswer == "Top")
            {
                if(ValuePerN == "Percent")
                {
                    myOp = FilterOperator.TopPercent;
                }
                else
                {
                    myOp = FilterOperator.TopN;
                }
            }
            else
            {
                if (ValuePerN == "Percent")
                {
                    myOp = FilterOperator.BottomPercent;
                }
                else
                {
                    myOp = FilterOperator.BottomN;
                }
            }
            ReportLibrary2.Report1.AddFilter("", myOp, TextValue);
            ValueGroup.Visible = !ValueGroup.Visible;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
          
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            
        }
    }
}

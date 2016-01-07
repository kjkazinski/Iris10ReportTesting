using System.Diagnostics;

namespace System.ComponentModel.DataAnnotations
{
    //
    // Summary:
    //     Provides a general-purpose attribute that lets you specify localizable strings
    //     for types and members of entity partial classes.
    [AttributeUsage(AttributeTargets.All)]
    public class ReportAttribute : Attribute
    {
        private int bugNo;
        private string developer;
        private string lastReview;
        public string message;

        public ReportAttribute(int bg, string dev, string d)
        {
            this.bugNo = bg;
            this.developer = dev;
            this.lastReview = d;
        }

        public int BugNo
        {
            get
            {
                return bugNo;
            }
        }

        public string Developer
        {
            get
            {
                return developer;
            }
        }

        public string LastReview
        {
            get
            {
                return lastReview;
            }
        }

        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
            }
        }
    }

    public class TextFieldAttribute : Attribute
    {
        public TextFieldAttribute(string n, string d)
        {

        }
    }

   // [ReportAttribute(45, "Zara Ali", "12/8/2012", Message = "Return type mismatch")]
    [ReportAttribute(49, "Nuha Ali", "10/10/2012", Message = "Unused variable")]
    class MyClass
    {
        protected double length;
        protected double width;
        public MyClass(double l, double w)
        {
            length = l;
            width = w;
        }
        [ReportAttribute(55, "Zara Ali", "19/10/2012", Message = "Return type mismatch")]
        public double GetArea()
        {
            return length * width;
        }
        [ReportAttribute(56, "Zara Ali", "19/10/2012")]
        public void Display()
        {
            Debug.WriteLine("Length: {0}", length);
            Debug.WriteLine("Width: {0}", width);
            Debug.WriteLine("Area: {0}", GetArea());
        }
    }
}
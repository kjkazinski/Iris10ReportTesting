using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportTestApp.Columns.Settings
{
    public class MyColumnSettings
    {
        public string Title { get; set; }

        /// <summary>Property name in row viewmodel that the column is bound to.</summary>
        public string PropertyName { get; set; }
    }
}
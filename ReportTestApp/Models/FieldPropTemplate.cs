//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReportTestApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class FieldPropTemplate
    {
        public int FieldPropTemplate_Key { get; set; }
        public string Description { get; set; }
        public byte IsEnabled { get; set; }
        public byte IsRequired { get; set; }
        public byte AllowUserConfig { get; set; }
        public byte IsNumeric { get; set; }
        public byte AllowNegative { get; set; }
        public byte AllowDecimal { get; set; }
        public byte Hidden { get; set; }
        public byte ValidateMe { get; set; }
        public byte Unbound { get; set; }
        public string FieldType { get; set; }
        public string FieldTypeTemplate { get; set; }
        public byte LeftOfDecimal { get; set; }
        public byte RightOfDecimal { get; set; }
        public int MaxLength { get; set; }
        public Nullable<int> Width { get; set; }
        public Nullable<byte> GridFieldOrder { get; set; }
        public string FieldFormat { get; set; }
        public byte CalculatedField { get; set; }
        public byte CRWOnly { get; set; }
        public byte CRWExclude { get; set; }
        public byte CRWReverseJoin { get; set; }
        public string CRWLoadOnlyInModule_Key { get; set; }
        public byte CRWLoadOnlyWhenBaseTable { get; set; }
    }
}

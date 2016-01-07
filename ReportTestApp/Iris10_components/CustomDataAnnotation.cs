using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Reflection;

namespace IrisWeb
{
    public class CustomDataAnnotation
    {
    }

    #region  ExcludeSQL ****************************************************************************
    /// <summary>
    /// This attribute is used when a property is used in the SQLGenerator to Exclude this columns
        /// </summary>
    public class IsExcludeSqlAttribute : System.Attribute
    {
        public bool Exclude = false;

        public IsExcludeSqlAttribute()
        {
            Exclude = true;
        }
    }
    #endregion

    #region  IsAutoNumber ****************************************************************************
    /// <summary>
    /// This attribute is used when a property that is a autonumber field
    /// </summary>
    /// 
    public class IsAutoNumberAttribute : System.Attribute
    {
        public bool Exclude = false;

        public IsAutoNumberAttribute()
        {
            Exclude = true;
        }
    }
    #endregion

    #region  OrderByField ****************************************************************************
    /// <summary>
    /// This attribute is used when a property is used in the Order By of the Read().   The Index is used when you have 
    /// multiple fields in the Order By.   0 is the first field to Order By, 1 is the 2nd field to Order By
    /// </summary>
    public class OrderByFieldAttribute : System.Attribute
    {
        public Boolean OrderByAssending { get; set; }
        public int Index { get; set; }

        public OrderByFieldAttribute(Boolean orderbyassending = true, int index = -1)
        {
            OrderByAssending = orderbyassending;
            Index = index;
        }
    }
    #endregion

    #region  RelatedTable ****************************************************************************
    /// <summary>
    /// This attribute denotes the SQLTable object to associate automatically generated fields in the SQL Generator.
    /// Currently this is unused as there was only one potential use at this time in the Inventory_LocationDropdown model.
    /// </summary>
    public class RelatedTableAttribute : System.Attribute
    {
        public string TableName;
        
        public RelatedTableAttribute(string TableName)
        {
            this.TableName = TableName;
        }
    }
    #endregion

    #region  RequiredIfOtherFieldIsNullAttribute ******************************************************
    /// <summary>
    /// This attribute denotes this property is required if another property(s) is null
    /// </summary>
    public class RequiredIfOtherFieldIsNullAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly string[] otherProperties;
        public RequiredIfOtherFieldIsNullAttribute(string[] otherProperties)
        {
            this.otherProperties = otherProperties;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            foreach (string propertyName in otherProperties)
            {
                var property = validationContext.ObjectType.GetProperty(propertyName);
                if (property == null)
                {
                    return new ValidationResult(string.Format(
                        CultureInfo.CurrentCulture,
                        "Unknown property {0}",
                        new[] { otherProperties }
                    ));
                }
                var otherPropertyValue = property.GetValue(validationContext.ObjectInstance, null);

                if (otherPropertyValue == null || otherPropertyValue as string == string.Empty)
                {
                    if (value == null || value as string == string.Empty)
                    {
                        return new ValidationResult(string.Format(
                            CultureInfo.CurrentCulture,
                            FormatErrorMessage(validationContext.DisplayName),
                            new[] { otherProperties }
                        ));
                    }
                }
            }
            return null;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "requiredif",
            };
            rule.ValidationParameters.Add("other", otherProperties);
            yield return rule;
        }
    }
    #endregion

    #region  RequiredIfOtherFieldIsNotNullAttribute ******************************************************
    /// <summary>
    /// This attribute denotes this property is required if another property(s) is not null
    /// </summary>
    public class RequiredIfOtherFieldIsNotNullAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly string[] otherProperties;
        public RequiredIfOtherFieldIsNotNullAttribute(string[] otherProperties)
        {
            this.otherProperties = otherProperties;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            foreach (string propertyName in otherProperties)
            {
                var property = validationContext.ObjectType.GetProperty(propertyName);
                if (property == null)
                {
                    return new ValidationResult(string.Format(
                        CultureInfo.CurrentCulture,
                        "Unknown property {0}",
                        new[] { otherProperties }
                    ));
                }
                var otherPropertyValue = property.GetValue(validationContext.ObjectInstance, null);

                //if (otherPropertyValue != null || otherPropertyValue as string != string.Empty)
                if (otherPropertyValue != null)
                {
                    //if (value == null || value as string == string.Empty)
                    if (value == null)
                    {
                        return new ValidationResult(string.Format(
                            CultureInfo.CurrentCulture,
                            FormatErrorMessage(validationContext.DisplayName),
                            new[] { otherProperties }
                        ));
                    }
                }
            }

            return null;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "requiredif",
            };
            rule.ValidationParameters.Add("other", otherProperties);
            yield return rule;
        }
    }
    #endregion

    #region  FieldMustBeNullIfAnotherFieldIsNotNullAttribute ******************************************************
    /// <summary>
    /// This attribute denotes this property must be null if another property(s) is not null
    /// </summary>
    public class FieldMustBeNullIfAnotherFieldIsNotNullAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly string[] otherProperties;
        public FieldMustBeNullIfAnotherFieldIsNotNullAttribute(string[] otherProperties)
        {
            this.otherProperties = otherProperties;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            foreach (string propertyName in otherProperties)
            {
                var property = validationContext.ObjectType.GetProperty(propertyName);
                if (property == null)
                {
                    return new ValidationResult(string.Format(
                        CultureInfo.CurrentCulture,
                        "Unknown property {0}",
                        new[] { otherProperties }
                    ));
                }
                var otherPropertyValue = property.GetValue(validationContext.ObjectInstance, null);

                if (otherPropertyValue != null || otherPropertyValue as string != string.Empty)
                {
                    if (value != null || value as string != string.Empty)
                    {
                        return new ValidationResult(string.Format(
                            CultureInfo.CurrentCulture,
                            FormatErrorMessage(validationContext.DisplayName),
                            new[] { otherProperties }
                        ));
                    }
                }
            }

            return null;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "requiredif",
            };
            rule.ValidationParameters.Add("other", otherProperties);
            yield return rule;
        }
    }
    #endregion

    #region  NumericLessThanAttribute ******************************************************
    /// <summary>
    /// This attribute denotes this property must be Less Than another numeric property
    /// </summary>
    public class NumericLessThanAttribute : ValidationAttribute, IClientValidatable
    {
        private const string LessThanErrorMessage = "{0} must be less than {1}.";
        private const string LessThanOrEqualToErrorMessage = "{0} must be less than or equal to {1}.";                
 
        public string OtherProperty { get; private set; }
 
        private bool allowEquality;
 
        public bool AllowEquality
        {
            get { return this.allowEquality; }
            set
            {
                this.allowEquality = value;
                 
                // Set the error message based on whether or not
                // equality is allowed
                this.ErrorMessage = (value ? LessThanOrEqualToErrorMessage : LessThanErrorMessage);
            }
        }        
 
        public NumericLessThanAttribute(string otherProperty)
            : base(LessThanErrorMessage)
        {
            if (otherProperty == null) { throw new ArgumentNullException("otherProperty"); }
            this.OtherProperty = otherProperty;            
        }        
 
        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, this.OtherProperty);
        }
 
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo otherPropertyInfo = validationContext.ObjectType.GetProperty(OtherProperty);   
             
            if (otherPropertyInfo == null)
            {
                return new ValidationResult(String.Format(CultureInfo.CurrentCulture, "Could not find a property named {0}.", OtherProperty));
            }
 
            object otherPropertyValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);
 
            decimal decValue;
            decimal decOtherPropertyValue;
 
            // Check to ensure the validating property is numeric
            if (!decimal.TryParse(value.ToString(), out decValue))
            {
                return new ValidationResult(String.Format(CultureInfo.CurrentCulture, "{0} is not a numeric value.", validationContext.DisplayName));
            }
 
            // Check to ensure the other property is numeric
            if (!decimal.TryParse(otherPropertyValue.ToString(), out decOtherPropertyValue))
            {
                return new ValidationResult(String.Format(CultureInfo.CurrentCulture, "{0} is not a numeric value.", OtherProperty));
            }
 
            // Check for equality
            if (AllowEquality && decValue == decOtherPropertyValue)
            {
                return null;
            }
            // Check to see if the value is greater than the other property value
            else if (decValue > decOtherPropertyValue)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }            
 
            return null;
        }
 
        public static string FormatPropertyForClientValidation(string property)
        {
            if (property == null)
            {
                throw new ArgumentException("Value cannot be null or empty.", "property");
            }
            return "*." + property;
        }       
 
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {           
            yield return new ModelClientValidationNumericLessThanRule(FormatErrorMessage(metadata.DisplayName), FormatPropertyForClientValidation(this.OtherProperty), this.AllowEquality);
        }

        public class ModelClientValidationNumericLessThanRule : ModelClientValidationRule
        {
            public ModelClientValidationNumericLessThanRule(string errorMessage, object other, bool allowEquality)
            {
                ErrorMessage = errorMessage;
                ValidationType = "numericlessthan";
                ValidationParameters["other"] = other;
                ValidationParameters["allowequality"] = allowEquality;
            }
        }
    }
    #endregion

    #region  NumericGreaterThanAttribute ******************************************************
    /// <summary>
    /// This attribute denotes this property must be Greater Than another numeric property
    /// </summary>
    public class NumericGreaterThanAttribute : ValidationAttribute, IClientValidatable
    {
        private const string GreaterThanErrorMessage = "{0} must be less than {1}.";
        private const string GreaterThanOrEqualToErrorMessage = "{0} must be less than or equal to {1}.";

        public string OtherProperty { get; private set; }

        private bool allowEquality;

        public bool AllowEquality
        {
            get { return this.allowEquality; }
            set
            {
                this.allowEquality = value;

                // Set the error message based on whether or not
                // equality is allowed
                this.ErrorMessage = (value ? GreaterThanOrEqualToErrorMessage : GreaterThanErrorMessage);
            }
        }

        public NumericGreaterThanAttribute(string otherProperty)
            : base(GreaterThanErrorMessage)
        {
            if (otherProperty == null) { throw new ArgumentNullException("otherProperty"); }
            this.OtherProperty = otherProperty;
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, this.OtherProperty);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo otherPropertyInfo = validationContext.ObjectType.GetProperty(OtherProperty);

            if (otherPropertyInfo == null)
            {
                return new ValidationResult(String.Format(CultureInfo.CurrentCulture, "Could not find a property named {0}.", OtherProperty));
            }

            if (value == null)
            {
                return null;
            }



            object otherPropertyValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);

            decimal decValue;
            decimal decOtherPropertyValue;

            // Check to ensure the validating property is numeric
            if (!decimal.TryParse(value.ToString(), out decValue))
            {
                return new ValidationResult(String.Format(CultureInfo.CurrentCulture, "{0} is not a numeric value.", validationContext.DisplayName));
            }

            // Check to ensure the other property is numeric
            if (!decimal.TryParse(otherPropertyValue.ToString(), out decOtherPropertyValue))
            {
                return new ValidationResult(String.Format(CultureInfo.CurrentCulture, "{0} is not a numeric value.", OtherProperty));
            }

            // Check for equality
            if (AllowEquality && decValue == decOtherPropertyValue)
            {
                return null;
            }
            // Check to see if the value is less than the other property value
            else if (decValue < decOtherPropertyValue)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return null;
        }

        public static string FormatPropertyForClientValidation(string property)
        {
            if (property == null)
            {
                throw new ArgumentException("Value cannot be null or empty.", "property");
            }
            return "*." + property;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationNumericGreaterThanRule(FormatErrorMessage(metadata.DisplayName), FormatPropertyForClientValidation(this.OtherProperty), this.AllowEquality);
        }

        public class ModelClientValidationNumericGreaterThanRule : ModelClientValidationRule
        {
            public ModelClientValidationNumericGreaterThanRule(string errorMessage, object other, bool allowEquality)
            {
                ErrorMessage = errorMessage;
                ValidationType = "numericgreaterthan";
                ValidationParameters["other"] = other;
                ValidationParameters["allowequality"] = allowEquality;
            }
        }
    }
    #endregion

    #region  DateGreaterThanAttribute ******************************************************
    /// <summary>
    /// This attribute denotes this Date property must be Greater Than another Date property
    /// </summary>
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private const string GreaterThanErrorMessage = "{1} must be greater than {0}.";
        readonly string otherPropertyName;
 
        public DateGreaterThanAttribute(string otherPropertyName)
            : base(GreaterThanErrorMessage)
        {
            this.otherPropertyName = otherPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult validationResult = ValidationResult.Success;
            try
            {
                // Using reflection we can get a reference to the other date property, in this example the project start date
                var otherPropertyInfo = validationContext.ObjectType.GetProperty(this.otherPropertyName);
                // Let's check that otherProperty is of type DateTime as we expect it to be
                if (otherPropertyInfo.PropertyType.Equals(new DateTime().GetType()))
                {
                    DateTime toValidate = (DateTime)value;
                    DateTime referenceProperty = (DateTime)otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);
                    // if the end date is lower than the start date, than the validationResult will be set to false and return
                    // a properly formatted error message
                    if (toValidate.CompareTo(referenceProperty) < 1)
                    {
                        validationResult = new ValidationResult(ErrorMessageString);
                    }
                }
                else
                {
                    validationResult = new ValidationResult("An error occurred while validating the property. OtherProperty is not of type DateTime");
                }
            }
            catch (Exception ex)
            {
                // Do stuff, i.e. log the exception
                // Let it go through the upper levels, something bad happened
                throw ex;
            }

            return validationResult;
        }
    }
    #endregion

    #region  IsDropDownListAttribute ******************************************************
        /// <summary>
    /// This attribute denotes this field is a DropDownList and will be listed on the Set List Order Screen
    /// </summary>
    public class IsDropDownListAttribute : System.Attribute
    {
        public bool IsDropDownList = true;

        public IsDropDownListAttribute()
        {
            IsDropDownList = true;
        }
    }
    #endregion

}
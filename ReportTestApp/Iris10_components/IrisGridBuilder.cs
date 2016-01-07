using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

using Kendo.Mvc.UI;
using Kendo.Mvc.UI.Fluent;

using IrisWeb.Code.Data.Attributes;
using IrisWeb.Code.Data.Models.System;
using IrisWeb.Code.Data.Services;
using IrisWeb.Code.Extensions;


namespace IrisWeb.Code.Interface.Widgets
{
    public sealed class IrisGridBuilder<T> : IHtmlString where T : class
    {
        GridBuilder<T> builder;

        static Type typeDate = typeof(DateTime);

        private string GetAbsoluteUrl(string path)
        {
            if (path.StartsWith("~"))
            {
                Uri currentUrl = HttpContext.Current.Request.Url;
                return string.Format("{0}://{1}:{2}{3}", currentUrl.Scheme, currentUrl.Host, currentUrl.Port, path.Substring(1));
            }
            else
                throw new NotImplementedException("Format not supported.");
        }

        public IrisGridBuilder(HtmlHelper helper)
        {
            Type modelType = typeof(T);
            IrisGridAttribute gridAttribute = modelType.GetCustomAttribute<IrisGridAttribute>();

            if (gridAttribute == null) throw new InvalidOperationException("You cannot create a grid control with an object that does not have the IrisGridAttribute.");

            DatabaseModelBindings bindings = modelType.GetDatabaseBindings();
            PropertyInfo[] modelProperties = modelType.GetProperties();
            AjaxDataSourceBuilder<T> dataSourceBuilder = null;
            UserService userService = new UserService();

            using (SecurityUserService securityUserService = new SecurityUserService())
            {
                WidgetFactory factory = new WidgetFactory(helper);
                builder = factory.Grid<T>();

                builder.DataSource(ds =>
                {
                    dataSourceBuilder = ds.Ajax();
                    //    .Aggregates(a =>
                    //    {
                    //        a.Add(p => p.Labor_Quantity).Sum();
                    //        a.Add(p => p.Equipment_Quantity).Sum();
                    //        a.Add(p => p.Material_Quantity).Sum();
                    //        a.Add(p => p.OutSideService_Quantity).Sum();
                    //    })
                    //dataSourceBuilder.Events(e =>
                    //{
                    //    e.Error("error");
                    //    e.Change("onChange");
                    //});
                    dataSourceBuilder.PageSize(100);
                    dataSourceBuilder.ServerOperation(true);
                    dataSourceBuilder.Read(r => r.Url(GetAbsoluteUrl(gridAttribute.ReadPath)).Type(HttpVerbs.Get));
                    dataSourceBuilder.Create(c => c.Url(GetAbsoluteUrl(gridAttribute.CreatePath)).Type(HttpVerbs.Post));
                    dataSourceBuilder.Update(u => u.Url(GetAbsoluteUrl(gridAttribute.UpdatePath)).Type(HttpVerbs.Post));
                    dataSourceBuilder.Destroy(d => d.Url(GetAbsoluteUrl(gridAttribute.DestroyPath)).Type(HttpVerbs.Post));


                });

                builder.Name("grid");
                builder.Columns(c =>
                {
                    // Create our command column for this object
                    c.Command(command =>
                    {
                        command.Destroy();
                        command.Edit();
                    }).Lockable(true).Locked(false).Width(180).Visible(userService.ValidateSecurityLevel(bindings.TableName, 3));

                    //.Model(model =>
                    //    {
                    //        model.Id(m => m.Timecard_Key);
                    //        model.Field(m => m.Equipment_Unit_Cost).Editable(true);
                    //        model.Field(m => m.FuelImport).Editable(false);
                    //        model.Field(m => m.DateStamp).Editable(false);
                    //        model.Field(m => m.SecurityUser_Key).Editable(false);
                    //        model.Field(m => m.Timecard_Key).DefaultValue("1600000000");
                    //        model.Field(m => m.SecurityUser_Key).DefaultValue(ViewData["CurrentSecurityUser"]);
                    //        model.Field(m => m.CreatedBySecurityUser_Key).DefaultValue(ViewData["CurrentSecurityUser"]);
                    //        model.Field(m => m.DateStamp).DefaultValue(DateTime.Now);
                    //    })

                    dataSourceBuilder.Aggregates(a =>
                    {
                        dataSourceBuilder.Model(model =>
                        {
                            // Setup the columns from our model properties
                            foreach (PropertyInfo p in modelProperties)
                            {
                                // Get our column settings from the property attribute or use a default one
                                IrisGridColumnAttribute columnAttributes = p.GetCustomAttribute<IrisGridColumnAttribute>();
                                if (columnAttributes == null) columnAttributes = IrisGridColumnAttribute.Default;


                                switch(columnAttributes.Aggregate)
                                {
                                    case AggregateMode.Sum:
                                        a.Add(p.Name, p.PropertyType).Sum();
                                        break;

                                    case AggregateMode.Min:
                                        a.Add(p.Name, p.PropertyType).Min();
                                        break;

                                    case AggregateMode.Max:
                                        a.Add(p.Name, p.PropertyType).Max();
                                        break;

                                    case AggregateMode.Average:
                                        a.Add(p.Name, p.PropertyType).Average();
                                        break;
                                }

                                if (p.Name == bindings.KeyFieldName)
                                    model.Id(p.Name);
                                else
                                {
                                    DataSourceModelFieldDescriptorBuilder<object> fieldBuilder = model.Field(p.Name, p.PropertyType);
                                    if (p.Name.EndsWith("User_Key", StringComparison.InvariantCultureIgnoreCase))
                                        fieldBuilder.DefaultValue(securityUserService.CurrentSecurityUser());
                                }

                                GridBoundColumnBuilder<T> column = c.Bound(p.Name);
                                column.Lockable(true).Locked(false);

                                // Setup our formatting for this column
                                if (string.IsNullOrEmpty(columnAttributes.Format) == false)
                                    column.Format(columnAttributes.Format);

                                // Setup our custom editor templates if they are needed
                                if (p.PropertyType == typeDate)
                                    column.EditorTemplateName("IRISDate");

                                // Setup custom attributes
                                if (p.GetCustomAttribute<RequiredAttribute>() != null)
                                    column.HeaderHtmlAttributes(new { @Class = "required-field" });

                                column.Width(columnAttributes.Width);
                            }
                        });
                    });
                });

                // Setup our column menus for filtering and sorting
                builder.ColumnMenu(m =>
                {
                    m.Enabled(true);
                    m.Filterable(true);
                    m.Sortable(true);
                    m.Columns(true);
                });

                // Setup our editable configuration
                builder.Editable(e =>
                {
                    e.Mode(GridEditMode.InLine);
                    e.CreateAt(GridInsertRowPosition.Top);
                    e.DisplayDeleteConfirmation(false);
                });

                builder.Events(e =>
                {
                    //    e.Edit("onGridEdit");
                    //    e.Save("ValidateRow");              // Used for Update button
                    //    e.SaveChanges("ValidateRow");       // Used for Alt + s
                    //    e.DataBound("SelectFirstRow");
                });


                //.Excel(excel => excel
                //    .FileName("Timecard.xlsx")
                //    .Filterable(true)
                //    .ProxyURL(Url.Action("Excel_Export_Save", "Grid"))
                //)
                //.Pdf(pdf => pdf
                //    .FileName("Timecard.pdf")
                //    .ProxyURL(Url.Action("Pdf_Export_Save", "Grid"))
                //    .AllPages()
                //)

                builder.Filterable();
                builder.Navigatable(setting => setting.Enabled(true));
                builder.Pageable(p => p.Refresh(true).PageSizes(true).ButtonCount(5));
                builder.Scrollable(a => a.Height(500));
                builder.Resizable(resize => resize.Columns(true));
                builder.Reorderable(reorder => reorder.Columns(true));
                builder.Sortable();
                builder.Selectable();
                //.ToolBar(toolBar =>
                //{
                //    toolBar.Create();
                //    toolBar.Excel();
                //    toolBar.Pdf();
                //})

                //.DataSource(ds => ds
                //    .Ajax()

                //    
                //    .Sort(s => s.Add(t => t.Task_Date))



                //.Columns(c =>
                //{
                //    c.Command(command => { command.Destroy(); command.Edit(); }).Lockable(true).Locked(false).Width(180).Visible(AuthHelper.ValidateSecurityLevel("Timecard", 3));
                //    c.Bound(p => p.Task_Date).Width(180).Lockable(true).Locked(false).EditorTemplateName("IRISDate").HeaderHtmlAttributes(new { @Class = "required-field" });
                //    c.ForeignKey(p => p.Activity_Key, (System.Collections.IEnumerable)ViewData["Activities"], "Field1", "Field2").Width(350).Lockable(true).HeaderHtmlAttributes(new { @Class = "required-field" }).EditorTemplateName("IRISGridForeignKeyWithFilter");
                //    c.ForeignKey(p => p.Project_Key, (System.Collections.IEnumerable)ViewData["Projects"], "Field1", "Field2").Width(350).EditorTemplateName("IRISGridForeignKey");
                //    c.ForeignKey(p => p.ProjectSub_Key, (System.Collections.IEnumerable)ViewData["ProjectSubs"], "ProjectSub_Key", "NameDesc").EditorTemplateName("ProjectSub").Width(200);
                //    c.Bound(p => p.Crew_Num).Width(120);
                //    c.ForeignKey(p => p.Employee_Key, (System.Collections.IEnumerable)ViewData["Employees"], "Field1", "Field2").Width(350).EditorTemplateName("IRISGridForeignKey");
                //    c.Bound(p => p.Labor_Quantity).HtmlAttributes(new { style = "text-align: right" }).Width(150).ClientGroupFooterTemplate("Sum: #= kendo.format('{0:n}', sum)#").ClientFooterTemplate("Sum: #= kendo.format('{0:n}', sum)#");
                //    c.ForeignKey(p => p.Pay_Type_Key, (System.Collections.IEnumerable)ViewData["Pay_Types"], "Field1", "Field2").Width(150).EditorTemplateName("IRISGridForeignKey");
                //    c.ForeignKey(p => p.Premium_Key, (System.Collections.IEnumerable)ViewData["Premiums"], "Field1", "Field2").Width(150).EditorTemplateName("IRISGridForeignKey");
                //    c.Bound(p => p.Override_Labor_Rate).Format("{0:c}").HtmlAttributes(new { style = "text-align: right" }).Width(200);
                //    c.Bound(p => p.Production).HtmlAttributes(new { style = "text-align: right" }).Width(130).ClientGroupFooterTemplate("Sum: #= kendo.format('{0:n}', sum)#").ClientFooterTemplate("Sum: #= kendo.format('{0:n}', sum)#");
                //    c.Bound(p => p.Equipment_Quantity).HtmlAttributes(new { style = "text-align: right" }).Width(200).ClientGroupFooterTemplate("Sum: #= kendo.format('{0:n}', sum)#").ClientFooterTemplate("Sum: #= kendo.format('{0:n}', sum)#");
                //    c.ForeignKey(p => p.Equipment_Key, (System.Collections.IEnumerable)ViewData["Equipments"], "Field1", "Field2").Width(350).EditorTemplateName("IRISGridForeignKey");
                //    c.Bound(p => p.Equipment_Unit_Cost).Format("{0:c}").HtmlAttributes(new { style = "text-align: right" }).Width(120);
                //    c.Bound(p => p.Material_Quantity).HtmlAttributes(new { style = "text-align: right" }).Width(120).ClientGroupFooterTemplate("Sum: #= kendo.format('{0:n}', sum)#").ClientFooterTemplate("Sum: #= kendo.format('{0:n}', sum)#");
                //    c.ForeignKey(p => p.Inventory_Location_Key, (System.Collections.IEnumerable)ViewData["Inventory_Locations"], "Inventory_Location_Key", "NameDesc").Width(350).EditorTemplateName("IRISGridForeignKey");
                //    c.Bound(p => p.UOMName).Width(100);
                //    c.Bound(p => p.Material_Description).Width(120);
                //    c.Bound(p => p.Material_Unit_Cost).Format("{0:c}").HtmlAttributes(new { style = "text-align: right" }).Width(150);
                //    c.ForeignKey(p => p.ResourceClass_Key, (System.Collections.IEnumerable)ViewData["ResourceClasss"], "Field1", "Field2").Width(350).EditorTemplateName("IRISGridForeignKey");
                //    c.Bound(p => p.OutSideServiceDescription).Width(120);
                //    c.Bound(p => p.OutSideService_Quantity).HtmlAttributes(new { style = "text-align: right" }).Width(100);
                //    c.Bound(p => p.OutSideServiceCost).Format("{0:c}").HtmlAttributes(new { style = "text-align: right" }).Width(120);
                //    c.ForeignKey(p => p.Mgt_Unit_Key, (System.Collections.IEnumerable)ViewData["Mgt_Units"], "Field1", "Field2").Width(350).EditorTemplateName("IRISGridForeignKey");
                //    c.ForeignKey(p => p.Program_Key, (System.Collections.IEnumerable)ViewData["Programs"], "Field1", "Field2").Width(350).EditorTemplateName("IRISGridForeignKey");
                //    c.ForeignKey(p => p.Zone_Key, (System.Collections.IEnumerable)ViewData["Zones"], "Field1", "Field2").Width(350).EditorTemplateName("IRISGridForeignKey");
                //    c.ForeignKey(p => p.RBF_Key, (System.Collections.IEnumerable)ViewData["RBFs"], "Field1", "Field2").Width(200).EditorTemplateName("IRISGridForeignKey");
                //    c.ForeignKey(p => p.Road_Key, (System.Collections.IEnumerable)ViewData["Roads"], "Field1", "Field2").Width(200).EditorTemplateName("IRISGridForeignKey");
                //    c.ForeignKey(p => p.RoadName_Key, (System.Collections.IEnumerable)ViewData["RoadNames"], "Field1", "Field2").Width(200).EditorTemplateName("IRISGridForeignKey");
                //    c.Bound(p => p.Beg_Point).Width(120);
                //    c.Bound(p => p.End_Point).Width(120);
                //    c.Bound(p => p.FromLocation).Width(180);
                //    c.Bound(p => p.ToLocation).Width(180);
                //    c.ForeignKey(p => p.Reason_Key, (System.Collections.IEnumerable)ViewData["Reasons"], "Field1", "Field2").Width(200).EditorTemplateName("IRISGridForeignKey");
                //    c.Bound(p => p.Comments).Width(280);
                //    c.Bound(p => p.EquipmentMiles).HtmlAttributes(new { style = "text-align: right" }).Width(250);
                //    c.Bound(p => p.EquipmentHours).HtmlAttributes(new { style = "text-align: right" }).Width(250);

                //    // User defined fields
                //    string title;
                //    string userType;


                //    //ViewBag.UserDefinedFields.User1
                //    for (int i = 0; i < ViewBag.UserDefinedFields.Count; i++ )

                //    {
                //        title = ViewBag.UserDefinedFields[i].Title;
                //        userType = ViewBag.UserDefinedFields[i].Type;


                //        switch (userType)
                //        {
                //            case "String":
                //                c.Bound(ViewBag.UserDefinedFields[i].Field).Title(title).Width(120);
                //                break;
                //            case "Date":
                //                c.Bound(ViewBag.UserDefinedFields[i].Field).Title(title).EditorTemplateName("Date").Width(150);
                //                break;
                //            case "Numeric":
                //                c.Bound(ViewBag.UserDefinedFields[i].Field).Title(title).EditorTemplateName("Number").HtmlAttributes(new { style = "text-align: right" }).Width(120);
                //                break;
                //            case "Checkbox":
                //                c.Bound(ViewBag.UserDefinedFields[i].Field).Title(title).HtmlAttributes(new { style = "text-align: center" }).ClientTemplate("<input type='checkbox' #=" + ViewBag.UserDefinedFields[i].Field + " ? checked='checked' : '' # class='chkbx' disabled='disabled'></input>").Width(120);
                //                break;
                //            case "Lookup":
                //                c.ForeignKey(ViewBag.UserDefinedFields[i].Field, (System.Collections.IEnumerable)ViewData["LookupUser" + (i + 1).ToString()], "Field1", "Field2").Width(350).EditorTemplateName("IRISGridForeignKey");
                //                break;
                //        }

                //    }

                //    c.Bound(p => p.FuelImport).HtmlAttributes(new { style = "text-align: center" }).ClientTemplate("<input type='checkbox' #=FuelImport ? checked='checked' : '' # class='chkbx' disabled='disabled' readonly='readonly'></input>").Width(200);
                //    c.Bound(p => p.Error_Message).Width(220);
                //    c.Bound(p => p.DateStamp).Format("{0: MM/d/yyyy hh:mm:ss}").Width(175);
                //    c.ForeignKey(p => p.SecurityUser_Key, (System.Collections.IEnumerable)ViewData["SecurityUsers"], "SecurityUser_Key", "UserName").Width(140);
                //})
                //)
            }
        }



        public string ToHtmlString()
        {
            return builder.ToHtmlString();
        }
    }
}

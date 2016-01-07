using System;
using IrisWeb.Code.Interface;
using System.Web.Mvc;

namespace IrisWeb.Code.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static IrisWidgetFactory<T> Iris<T>(this HtmlHelper helper) where T : class
        {
            return new IrisWidgetFactory<T>(helper);
        }
    }
}

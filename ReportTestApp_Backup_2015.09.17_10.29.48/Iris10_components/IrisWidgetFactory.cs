using System;
using System.Web.Mvc;

using IrisWeb.Code.Interface.Widgets;

namespace IrisWeb.Code.Interface
{
    public sealed class IrisWidgetFactory<T> where T : class
    {
        private HtmlHelper htmlHelper;

        public IrisWidgetFactory(HtmlHelper helper)
        {
            htmlHelper = helper;
        }

        public IrisGridBuilder<T> Grid()
        {
            return new IrisGridBuilder<T>(htmlHelper);
        }
    }
}

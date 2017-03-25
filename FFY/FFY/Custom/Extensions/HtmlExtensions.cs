using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace FFY.Web.Custom.Extensions
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString EnumDisplayNameFor(this HtmlHelper html, Enum item)
        {
            var type = item.GetType();
            var member = type.GetMember(item.ToString());
            DisplayAttribute displayname = (DisplayAttribute)member[0].GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();

            if (displayname != null)
            {
                return new MvcHtmlString(displayname.GetName());
            }

            return new MvcHtmlString(item.ToString());
        }
    }

}
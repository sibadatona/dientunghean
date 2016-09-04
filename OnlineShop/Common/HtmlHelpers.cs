using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace OnlineShop.Common
{
	public static class HtmlHelpers
	{
		public static MvcHtmlString MenuLink(
	this HtmlHelper htmlHelper,
	string linkText,
	string actionName,
	string controllerName
)
		{
			string currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
			//string currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");
			string currentController = htmlHelper.ViewContext.RouteData.Values["controller"].ToString();
			if (actionName == currentAction && controllerName == currentController)
			{
				return htmlHelper.ActionLink(
					linkText,
					actionName,
					controllerName,
					null,
					new
					{
						@class = "active"
					});
			}
			return htmlHelper.ActionLink(linkText, actionName, controllerName);
		}

        public static IHtmlString Details(this HtmlHelper helper,
            string summary,
            string content,
            bool open = false)
        {
            StringBuilder str = new StringBuilder("<details " + (open ? "open" : String.Empty) + ">");
            str.Append("<summary>").Append(summary).AppendLine("</summary>");
            str.AppendLine(content);
            str.AppendLine("</details>");
            return new MvcHtmlString(str.ToString());
            // or
            // return helper.Raw(str.ToString());
        }
    }
}
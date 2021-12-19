using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using QuarterlySales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterlySales.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("paging-link")]
    public class PagingLinkTagHelper : TagHelper
    {
        private LinkGenerator linkBuilder;
        public PagingLinkTagHelper(LinkGenerator lg) => linkBuilder = lg;

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewCtx { get; set; }

        public int Number { get; set; }
        public RouteDictionary Current { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            RouteDictionary routes = Current.Clone();
            routes.PageNumber = Number;

            string controller = ViewCtx.RouteData.Values["controller"].ToString();
            string action = ViewCtx.RouteData.Values["action"].ToString();
            string url = linkBuilder.GetPathByAction(action, controller, routes);

            string linkClasses = "btn btn-primary";
            if (Number == Current.PageNumber)
            {
                linkClasses += " active disabled";
            }

            output.BuildLink(url, linkClasses);
            output.Content.SetContent(Number.ToString());
        }
    }
}

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
    [HtmlTargetElement("sorting-link")]
    public class SortingLinkTagHelper : TagHelper
    {
        private LinkGenerator linkBuilder;
        public SortingLinkTagHelper(LinkGenerator lg) => linkBuilder = lg;

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewCtx { get; set; }

        public RouteDictionary Current { get; set; }
        public string SortField { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var routes = Current.Clone();
            routes.SetSortAndDirection(SortField, Current);

            string controller = ViewCtx.RouteData.Values["controller"].ToString();
            string action = ViewCtx.RouteData.Values["action"].ToString();
            string url = linkBuilder.GetPathByAction(action, controller, routes);

            output.BuildLink(url, "text-dark");
        }
    }
}

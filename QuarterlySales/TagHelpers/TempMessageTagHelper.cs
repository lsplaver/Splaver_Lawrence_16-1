using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterlySales.TagHelpers
{
    [HtmlTargetElement("temp-message")]
    public class TempMessageTagHelper : TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewCtx { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var tempData = ViewCtx.TempData;

            if (tempData.ContainsKey("message"))
            {
                output.BuildTag("h2", "bg-light text-center p-2 mb-2");
                output.Content.SetContent(tempData["message"].ToString());
            }
            else
            {
                output.SuppressOutput();
            }
        }
    }
}

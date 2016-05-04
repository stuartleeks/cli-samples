using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonMark;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HelloMvc.TagHelpers
{
    public class MarkdownTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();
            output.Content.SetHtmlContent(CommonMarkConverter.Convert(childContent.GetContent()));
            output.TagName = null;
        }
    }
}

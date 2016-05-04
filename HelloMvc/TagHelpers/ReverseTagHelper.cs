using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HelloMvc.TagHelpers
{
    public class ReverseTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();
            var reversedContent = string.Join("", childContent.GetContent().Reverse());
            output.Content.SetHtmlContent(reversedContent);
            output.TagName = null;
        }
    }
}

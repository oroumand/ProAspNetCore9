using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ControllersWithViewSample.Taghelpers;

public class TableTagHelper : TagHelper
{
    public string AroTableColor { get; set; }
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.Add("class", $"table table-bordered table-striped table-{AroTableColor}");
        base.Process(context, output);
    }
}

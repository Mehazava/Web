#pragma checksum "E:\Work\Web\Задание 6\Show\Views\Home\Explicit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a897a92b22364c8853997c93885cf85d1e9a059e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Explicit), @"mvc.1.0.view", @"/Views/Home/Explicit.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "E:\Work\Web\Задание 6\Show\Views\_ViewImports.cshtml"
using Show;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "E:\Work\Web\Задание 6\Show\Views\Home\Explicit.cshtml"
using Show.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a897a92b22364c8853997c93885cf85d1e9a059e", @"/Views/Home/Explicit.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4e3cfce3eab7e97f80e6ca2756f47f6ef69c6486", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Explicit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Song>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("    <table class=\"table\">\r\n        <tr>\r\n            <th>Name</th>\r\n            <th>Rating</th>\r\n            <th>Producer</th>\r\n        </tr>\r\n");
#nullable restore
#line 9 "E:\Work\Web\Задание 6\Show\Views\Home\Explicit.cshtml"
         foreach (Song s in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr class=\"table\">\r\n                <td>");
#nullable restore
#line 12 "E:\Work\Web\Задание 6\Show\Views\Home\Explicit.cshtml"
               Write(s.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 13 "E:\Work\Web\Задание 6\Show\Views\Home\Explicit.cshtml"
               Write(s.Rating);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 14 "E:\Work\Web\Задание 6\Show\Views\Home\Explicit.cshtml"
               Write(s.Producer.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n");
#nullable restore
#line 16 "E:\Work\Web\Задание 6\Show\Views\Home\Explicit.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </table>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Song>> Html { get; private set; }
    }
}
#pragma warning restore 1591

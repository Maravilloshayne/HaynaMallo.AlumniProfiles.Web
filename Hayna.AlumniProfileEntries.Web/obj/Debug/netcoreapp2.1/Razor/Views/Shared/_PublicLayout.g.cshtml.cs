#pragma checksum "C:\Users\Shayne Maravillo\Desktop\Hayna.AlumniProfileEntries\Hayna.AlumniProfileEntries.Web\Views\Shared\_PublicLayout.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0f74a1267d57f4badf4e4d265e8764cced3a08cb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__PublicLayout), @"mvc.1.0.view", @"/Views/Shared/_PublicLayout.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/_PublicLayout.cshtml", typeof(AspNetCore.Views_Shared__PublicLayout))]
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
#line 1 "C:\Users\Shayne Maravillo\Desktop\Hayna.AlumniProfileEntries\Hayna.AlumniProfileEntries.Web\Views\_ViewImports.cshtml"
using Hayna.AlumniProfileEntries.Web;

#line default
#line hidden
#line 2 "C:\Users\Shayne Maravillo\Desktop\Hayna.AlumniProfileEntries\Hayna.AlumniProfileEntries.Web\Views\_ViewImports.cshtml"
using Hayna.AlumniProfileEntries.Web.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0f74a1267d57f4badf4e4d265e8764cced3a08cb", @"/Views/Shared/_PublicLayout.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dc76cdb5996d291918cd05d2cef4beadc48688c9", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__PublicLayout : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "C:\Users\Shayne Maravillo\Desktop\Hayna.AlumniProfileEntries\Hayna.AlumniProfileEntries.Web\Views\Shared\_PublicLayout.cshtml"
  
    Layout = "~/views/shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(55, 12, false);
#line 4 "C:\Users\Shayne Maravillo\Desktop\Hayna.AlumniProfileEntries\Hayna.AlumniProfileEntries.Web\Views\Shared\_PublicLayout.cshtml"
Write(RenderBody());

#line default
#line hidden
            EndContext();
            BeginContext(67, 3, true);
            WriteLiteral(";\r\n");
            EndContext();
            DefineSection("pageStyles", async() => {
                BeginContext(90, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(97, 44, false);
#line 6 "C:\Users\Shayne Maravillo\Desktop\Hayna.AlumniProfileEntries\Hayna.AlumniProfileEntries.Web\Views\Shared\_PublicLayout.cshtml"
Write(RenderSection("pageStyles", required: false));

#line default
#line hidden
                EndContext();
                BeginContext(141, 2, true);
                WriteLiteral("\r\n");
                EndContext();
            }
            );
            DefineSection("pageScriptsTop", async() => {
                BeginContext(170, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(177, 48, false);
#line 9 "C:\Users\Shayne Maravillo\Desktop\Hayna.AlumniProfileEntries\Hayna.AlumniProfileEntries.Web\Views\Shared\_PublicLayout.cshtml"
Write(RenderSection("pageScriptsTop", required: false));

#line default
#line hidden
                EndContext();
                BeginContext(225, 2, true);
                WriteLiteral("\r\n");
                EndContext();
            }
            );
            DefineSection("pageScripts", async() => {
                BeginContext(251, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(258, 45, false);
#line 12 "C:\Users\Shayne Maravillo\Desktop\Hayna.AlumniProfileEntries\Hayna.AlumniProfileEntries.Web\Views\Shared\_PublicLayout.cshtml"
Write(RenderSection("pageScripts", required: false));

#line default
#line hidden
                EndContext();
                BeginContext(303, 2, true);
                WriteLiteral("\r\n");
                EndContext();
            }
            );
            BeginContext(306, 1, true);
            WriteLiteral(" ");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
#pragma checksum "C:\Users\tayya\source\repos\Parky\Parky.Web\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "eee2f278a5030e76f6fa54b0b47194f3033095b7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
#line 1 "C:\Users\tayya\source\repos\Parky\Parky.Web\Views\_ViewImports.cshtml"
using Parky.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\tayya\source\repos\Parky\Parky.Web\Views\_ViewImports.cshtml"
using Parky.Web.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"eee2f278a5030e76f6fa54b0b47194f3033095b7", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1b99df38a7230da21fd3420d52cc62028e953dc3", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Parky.Web.Models.ViewModels.IndexVM>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\tayya\source\repos\Parky\Parky.Web\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<div class=\"container\">\r\n    <div class=\"row pb-4 backgroundWhite\">\r\n\r\n");
#nullable restore
#line 11 "C:\Users\tayya\source\repos\Parky\Parky.Web\Views\Home\Index.cshtml"
         foreach (var nationalpark in Model.NationalParksList)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            <div class=""container backgroundWhite pb-4"">
                <div class=""card border"">
                    <div class=""card-header bg-dark text-light ml-0 row container"">
                        <div class=""col-12 col-md-6"">
                            <h1 class=""text-warning"">");
#nullable restore
#line 17 "C:\Users\tayya\source\repos\Parky\Parky.Web\Views\Home\Index.cshtml"
                                                Write(nationalpark.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n                        </div>\r\n                        <div class=\"col-12 col-md-6 text-md-right\">\r\n                            <h1 class=\"text-warning\">State : ");
#nullable restore
#line 20 "C:\Users\tayya\source\repos\Parky\Parky.Web\Views\Home\Index.cshtml"
                                                        Write(nationalpark.State);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h1>
                        </div>
                    </div>
                    <div class=""card-body"">
                        <div class=""container rounded p-2"">
                            <div class=""row"">
                                <div class=""col-12 col-lg-8"">
                                    <div class=""row"">
                                        <div class=""col-12"">
                                            <h3 style=""color:#bbb9b9"">Established:");
#nullable restore
#line 29 "C:\Users\tayya\source\repos\Parky\Parky.Web\Views\Home\Index.cshtml"
                                                                             Write(nationalpark.Established.Date.Year);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </h3>\r\n                                        </div>\r\n                                        <div class=\"col-12\">\r\n");
#nullable restore
#line 32 "C:\Users\tayya\source\repos\Parky\Parky.Web\Views\Home\Index.cshtml"
                                             if (Model.Trails.Where(s => s.NationalParkId == nationalpark.Id).Count() > 0)
                                            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                            <table class=""table table-striped"" style=""border:1px solid #808080 "">
                                                <tr class=""table-secondary"">
                                                    <th>
                                                        Trail
                                                    </th>
                                                    <th>Distance</th>
                                                    <th>Elevation Gain</th>
                                                    <th>Difficulty</th>
                                                </tr>
");
#nullable restore
#line 43 "C:\Users\tayya\source\repos\Parky\Parky.Web\Views\Home\Index.cshtml"
                                                 foreach (var trails in Model.Trails.Where(s => s.NationalParkId == nationalpark.Id))
                                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                    <tr>\r\n                                                        <td>");
#nullable restore
#line 46 "C:\Users\tayya\source\repos\Parky\Parky.Web\Views\Home\Index.cshtml"
                                                       Write(trails.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                                        <td>");
#nullable restore
#line 47 "C:\Users\tayya\source\repos\Parky\Parky.Web\Views\Home\Index.cshtml"
                                                       Write(trails.Distance);

#line default
#line hidden
#nullable disable
            WriteLiteral(" miles</td>\r\n                                                        <td>");
#nullable restore
#line 48 "C:\Users\tayya\source\repos\Parky\Parky.Web\Views\Home\Index.cshtml"
                                                       Write(trails.Elevation);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ft</td>\r\n                                                        <td>");
#nullable restore
#line 49 "C:\Users\tayya\source\repos\Parky\Parky.Web\Views\Home\Index.cshtml"
                                                       Write(trails.DifficultyTypes);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                                    </tr>\r\n");
#nullable restore
#line 51 "C:\Users\tayya\source\repos\Parky\Parky.Web\Views\Home\Index.cshtml"
                                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                            </table>\r\n");
#nullable restore
#line 54 "C:\Users\tayya\source\repos\Parky\Parky.Web\Views\Home\Index.cshtml"
                                            }
                                            else
                                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                <p>Not Trail Exist</p>\r\n");
#nullable restore
#line 58 "C:\Users\tayya\source\repos\Parky\Parky.Web\Views\Home\Index.cshtml"
                                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </div>\r\n                                    </div>\r\n                                </div>\r\n                                <div class=\"col-12 col-lg-4 text-center\">\r\n\r\n");
#nullable restore
#line 65 "C:\Users\tayya\source\repos\Parky\Parky.Web\Views\Home\Index.cshtml"
                                       
                                        var base64 = Convert.ToBase64String(nationalpark.Picture);

                                        var finalStr = String.Format("data:image/jpg;base64,{0}", base64);
                                    

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <img");
            BeginWriteAttribute("src", " src=\"", 3755, "\"", 3770, 1);
#nullable restore
#line 70 "C:\Users\tayya\source\repos\Parky\Parky.Web\Views\Home\Index.cshtml"
WriteAttributeValue("", 3761, finalStr, 3761, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"card-img-top p-2 rounded\" width=\"100%\" />\r\n                                </div>\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n");
#nullable restore
#line 77 "C:\Users\tayya\source\repos\Parky\Parky.Web\Views\Home\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Parky.Web.Models.ViewModels.IndexVM> Html { get; private set; }
    }
}
#pragma warning restore 1591

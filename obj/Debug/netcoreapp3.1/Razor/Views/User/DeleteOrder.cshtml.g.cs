#pragma checksum "D:\study\1-Training\MVC\Project\Views\User\DeleteOrder.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c45ffc5981dbe8f440709f428cb92964f40ad2c6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_DeleteOrder), @"mvc.1.0.view", @"/Views/User/DeleteOrder.cshtml")]
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
#line 1 "D:\study\1-Training\MVC\Project\Views\_ViewImports.cshtml"
using Project;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\study\1-Training\MVC\Project\Views\_ViewImports.cshtml"
using Project.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c45ffc5981dbe8f440709f428cb92964f40ad2c6", @"/Views/User/DeleteOrder.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"89e31fbbec8fb4222cf117a26f28137c5b00e63f", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_User_DeleteOrder : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Project.Models.Order>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "hidden", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\study\1-Training\MVC\Project\Views\User\DeleteOrder.cshtml"
  
    ViewData["Title"] = "DeleteOrder";
    Layout = "~/Views/Shared/_UserLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<link rel=""stylesheet"" href=""https://fonts.googleapis.com/css?family=Roboto|Varela+Round"">
<link rel=""stylesheet"" href=""https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css"">
<link rel=""stylesheet"" href=""https://fonts.googleapis.com/icon?family=Material+Icons"">
<link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css"">
<script src=""https://code.jquery.com/jquery-3.5.1.min.js""></script>
<script src=""https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js""></script>
<script src=""https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js""></script>
<style>
    body {
        font-family: 'Varela Round', sans-serif;
    }

    .modal-confirm {
        color: #636363;
        width: 400px;
    }

        .modal-confirm .modal-content {
            padding: 20px;
            border-radius: 5px;
            border: none;
            text-align: center;
            font-size: 14px;
        }

      ");
            WriteLiteral(@"  .modal-confirm .modal-header {
            border-bottom: none;
            position: relative;
        }

        .modal-confirm h4 {
            text-align: center;
            font-size: 26px;
            margin: 30px 0 -10px;
        }

        .modal-confirm .close {
            position: absolute;
            top: -5px;
            right: -2px;
        }

        .modal-confirm .modal-body {
            color: #999;
        }

        .modal-confirm .modal-footer {
            border: none;
            text-align: center;
            border-radius: 5px;
            font-size: 13px;
            padding: 10px 15px 25px;
        }

            .modal-confirm .modal-footer a {
                color: #999;
            }

        .modal-confirm .icon-box {
            width: 80px;
            height: 80px;
            margin: 0 auto;
            border-radius: 50%;
            z-index: 9;
            text-align: center;
            border: 3px solid #f15e5e;
        ");
            WriteLiteral(@"}

            .modal-confirm .icon-box i {
                color: #f15e5e;
                font-size: 46px;
                display: inline-block;
                margin-top: 13px;
            }

        .modal-confirm .btn,
        .modal-confirm .btn:active {
            color: #fff;
            border-radius: 4px;
            background: #60c7c1;
            text-decoration: none;
            transition: all 0.4s;
            line-height: normal;
            min-width: 120px;
            border: none;
            min-height: 40px;
            border-radius: 3px;
            margin: 0 5px;
        }

        .modal-confirm .btn-secondary {
            background: #c1c1c1;
        }

            .modal-confirm .btn-secondary:hover,
            .modal-confirm .btn-secondary:focus {
                background: #a8a8a8;
            }

        .modal-confirm .btn-danger {
            background: #f15e5e;
        }

            .modal-confirm .btn-danger:hover,
            .");
            WriteLiteral(@"modal-confirm .btn-danger:focus {
                background: #ee3535;
            }

    .trigger-btn {
        display: inline-block;
        margin: 100px auto;
    }
</style>


<!-- Modal HTML -->
<div id=""myModal"" class=""modal fade"">
    <div class=""modal-dialog modal-confirm"">
        <div class=""modal-content"">
            <div class=""modal-header flex-column"">
                <div class=""icon-box"">
                    <i class=""material-icons"">&#xE5CD;</i>
                </div>
                
            </div>
            <div class=""modal-body"">
                <p>Do you really want to delete ");
#nullable restore
#line 133 "D:\study\1-Training\MVC\Project\Views\User\DeleteOrder.cshtml"
                                           Write(Html.DisplayFor(model => model.Craft.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral(" from your Order? </p>\r\n            </div>\r\n            <div class=\"modal-footer justify-content-center\">\r\n                <button type=\"button\" class=\"btn btn-secondary\"");
            BeginWriteAttribute("onclick", "  onclick=\"", 4047, "\"", 4132, 2);
#nullable restore
#line 136 "D:\study\1-Training\MVC\Project\Views\User\DeleteOrder.cshtml"
WriteAttributeValue("", 4058, "window.location.href='" + @Url.Action("CreateCrafts", "Vendor") + "'", 4058, 73, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 4131, ";", 4131, 1, true);
            EndWriteAttribute();
            WriteLiteral(">Cancel</button>\r\n");
#nullable restore
#line 137 "D:\study\1-Training\MVC\Project\Views\User\DeleteOrder.cshtml"
                 using (Html.BeginForm("DeleteOrder", "User", FormMethod.Post))
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                       ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "c45ffc5981dbe8f440709f428cb92964f40ad2c68692", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 139 "D:\study\1-Training\MVC\Project\Views\User\DeleteOrder.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Id);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                      <button type=\"submit\" class=\"btn btn-danger\" >Delete</button>\r\n");
#nullable restore
#line 141 "D:\study\1-Training\MVC\Project\Views\User\DeleteOrder.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n        </div>\r\n    </div>\r\n</div>");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Project.Models.Order> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591

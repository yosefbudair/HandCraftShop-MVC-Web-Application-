#pragma checksum "D:\study\1-Training\MVC\Project\Views\User\Bill.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f1b92308ea4c246dca1966a894c8c23cf2ab1340"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_Bill), @"mvc.1.0.view", @"/Views/User/Bill.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f1b92308ea4c246dca1966a894c8c23cf2ab1340", @"/Views/User/Bill.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"89e31fbbec8fb4222cf117a26f28137c5b00e63f", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_User_Bill : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Project.Models.Order>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\study\1-Training\MVC\Project\Views\User\Bill.cshtml"
  
    ViewData["Title"] = "bell";
    Layout = null;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"


<link href=""https://cdn.datatables.net/1.11.1/css/jquery.dataTables.min.css"" rel=""stylesheet"" />
<link href=""https://cdn.datatables.net/buttons/2.0.0/css/buttons.dataTables.min.css"" rel=""stylesheet"" />
<!------ Include the above in your HEAD tag ---------->
<style>
.check{
            margin: 0 auto;
    display: block;
    width: 6%;
    block-size: 40px;
    font-size: 15px;
    border-radius: 8px;
    margin-top: 20px;
    font-family: inherit;
    border: 0px solid #e6e6e6;
        background-color: #147ef1;
        color:white
        }

        .check:hover {
            background-color: #0e73e1;
            color: white;
        }
</style>
    <h3 style=""font-family: inherit; text-align: center; margin-top: 10rem; margin-bottom:20px"">Your Bell </h3>
<table class=""table"" id=""example"">
        <thead>
            <tr>
                <th>
                Crafts
                </th>
                <th>
                Price
                </th>
                <t");
            WriteLiteral("h>\r\n                Numpieces\r\n                </th>\r\n                <th>\r\n                Total\r\n                </th>\r\n                \r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n");
#nullable restore
#line 52 "D:\study\1-Training\MVC\Project\Views\User\Bill.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
#nullable restore
#line 56 "D:\study\1-Training\MVC\Project\Views\User\Bill.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Craft.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 59 "D:\study\1-Training\MVC\Project\Views\User\Bill.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Craft.Price));

#line default
#line hidden
#nullable disable
            WriteLiteral(" $\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 62 "D:\study\1-Training\MVC\Project\Views\User\Bill.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Numpieces));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 65 "D:\study\1-Training\MVC\Project\Views\User\Bill.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Price));

#line default
#line hidden
#nullable disable
            WriteLiteral(" $\r\n                        <input type=\"hidden\"");
            BeginWriteAttribute("value", " value=\"", 1916, "\"", 1935, 1);
#nullable restore
#line 66 "D:\study\1-Training\MVC\Project\Views\User\Bill.cshtml"
WriteAttributeValue("", 1924, item.Price, 1924, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" name=\"array[]\" />\r\n                    </td>                  \r\n                </tr>\r\n");
#nullable restore
#line 69 "D:\study\1-Training\MVC\Project\Views\User\Bill.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        </tbody>
    </table>

<script src=""https://code.jquery.com/jquery-3.5.1.js""></script>

<script src=""https://cdn.datatables.net/1.11.1/js/jquery.dataTables.min.js"" defer></script>

<script src=""https://cdn.datatables.net/buttons/2.0.0/js/dataTables.buttons.min.js"" defer></script>
<script src=""https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js""></script>

<script src=""https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js""></script>
<script src=""https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js""></script>
<script src=""https://cdn.datatables.net/buttons/2.0.0/js/buttons.html5.min.js"" defer></script>
<script>
    $(document).ready(function () {
        $('#example').DataTable({
            dom: 'Bfrtip',
            buttons: [
                'copyHtml5',
                'excelHtml5',
                'csvHtml5',
                'pdfHtml5'
            ]
        });
    });
</script>
");
#nullable restore
#line 96 "D:\study\1-Training\MVC\Project\Views\User\Bill.cshtml"
 using (Html.BeginForm("Bill", "User", FormMethod.Post))
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div style=\"margin-top:30px; margin-bottom:8rem; \">\r\n        <input type=\"hidden\"");
            BeginWriteAttribute("value", " value=\"", 3155, "\"", 3179, 1);
#nullable restore
#line 99 "D:\study\1-Training\MVC\Project\Views\User\Bill.cshtml"
WriteAttributeValue("", 3163, ViewBag.Address, 3163, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" name=\"Address\" />\r\n        <button type=\"submit\" class=\"check\" style=\" margin-left:auto; margin-right:auto \">Ok</button>\r\n    </div>\r\n");
#nullable restore
#line 102 "D:\study\1-Training\MVC\Project\Views\User\Bill.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Project.Models.Order>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591

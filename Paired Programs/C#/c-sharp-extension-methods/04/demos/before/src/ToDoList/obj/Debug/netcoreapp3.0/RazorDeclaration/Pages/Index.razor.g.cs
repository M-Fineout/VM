#pragma checksum "C:\Users\MFineout\Desktop\C#\c-sharp-extension-methods\04\demos\before\src\ToDoList\Pages\Index.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b89fce4d13224a6251fd5ed92891f9b247a73eec"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace ToDoList.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\MFineout\Desktop\C#\c-sharp-extension-methods\04\demos\before\src\ToDoList\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\MFineout\Desktop\C#\c-sharp-extension-methods\04\demos\before\src\ToDoList\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\MFineout\Desktop\C#\c-sharp-extension-methods\04\demos\before\src\ToDoList\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\MFineout\Desktop\C#\c-sharp-extension-methods\04\demos\before\src\ToDoList\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\MFineout\Desktop\C#\c-sharp-extension-methods\04\demos\before\src\ToDoList\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\MFineout\Desktop\C#\c-sharp-extension-methods\04\demos\before\src\ToDoList\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\MFineout\Desktop\C#\c-sharp-extension-methods\04\demos\before\src\ToDoList\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\MFineout\Desktop\C#\c-sharp-extension-methods\04\demos\before\src\ToDoList\_Imports.razor"
using ToDoList;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\MFineout\Desktop\C#\c-sharp-extension-methods\04\demos\before\src\ToDoList\_Imports.razor"
using ToDoList.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\MFineout\Desktop\C#\c-sharp-extension-methods\04\demos\before\src\ToDoList\Pages\Index.razor"
using ToDoList.Model;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\MFineout\Desktop\C#\c-sharp-extension-methods\04\demos\before\src\ToDoList\Pages\Index.razor"
using ToDoList.Services;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/")]
    public partial class Index : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 14 "C:\Users\MFineout\Desktop\C#\c-sharp-extension-methods\04\demos\before\src\ToDoList\Pages\Index.razor"
       
    int todoCount;

    protected override async Task OnInitializedAsync()
    {
        todoCount = await ToDoService.GetToDoCountAsync();
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ToDoService ToDoService { get; set; }
    }
}
#pragma warning restore 1591

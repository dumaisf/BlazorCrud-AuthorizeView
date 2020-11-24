// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace BlazorCrud.Client.Pages.Home
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\dumai\source\repos\BlazorCrud AuthorizeView\Client\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\dumai\source\repos\BlazorCrud AuthorizeView\Client\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\dumai\source\repos\BlazorCrud AuthorizeView\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\dumai\source\repos\BlazorCrud AuthorizeView\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\dumai\source\repos\BlazorCrud AuthorizeView\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\dumai\source\repos\BlazorCrud AuthorizeView\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\dumai\source\repos\BlazorCrud AuthorizeView\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\dumai\source\repos\BlazorCrud AuthorizeView\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\dumai\source\repos\BlazorCrud AuthorizeView\Client\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\dumai\source\repos\BlazorCrud AuthorizeView\Client\_Imports.razor"
using BlazorCrud.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\dumai\source\repos\BlazorCrud AuthorizeView\Client\_Imports.razor"
using BlazorCrud.Client.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\dumai\source\repos\BlazorCrud AuthorizeView\Client\_Imports.razor"
using BlazorCrud.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\dumai\source\repos\BlazorCrud AuthorizeView\Client\Pages\Home\AnnoncesParRubrique.razor"
using System.Net;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\dumai\source\repos\BlazorCrud AuthorizeView\Client\Pages\Home\AnnoncesParRubrique.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\dumai\source\repos\BlazorCrud AuthorizeView\Client\Pages\Home\AnnoncesParRubrique.razor"
using BlazorCrud.Shared.Classement.Rubriques;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\dumai\source\repos\BlazorCrud AuthorizeView\Client\Pages\Home\AnnoncesParRubrique.razor"
using BlazorCrud.Client.Pages.Home;

#line default
#line hidden
#nullable disable
    public partial class AnnoncesParRubrique : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 45 "C:\Users\dumai\source\repos\BlazorCrud AuthorizeView\Client\Pages\Home\AnnoncesParRubrique.razor"
       
    public ICollection<Rubrique> Rubriques { get; set; }

    /// <summary>
    /// Avoid concurrent requests
    /// </summary>
    private bool Busy;

    /// <summary>
    /// Charger les rubriques <see cref="Rubrique"/>.
    /// </summary>
    /// <returns>A <see cref="Task"/>.</returns>
    protected override async Task OnInitializedAsync()
    {
        if (!Busy)
        {
            Busy = true;

            try
            {
                Rubriques = await HttpClient.GetFromJsonAsync<List<Rubrique>>("home");
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
            catch (HttpRequestException)
            {
                Rubriques = null;
            }
            finally
            {
                Busy = false;
            }
        }

        await base.OnInitializedAsync();
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient HttpClient { get; set; }
    }
}
#pragma warning restore 1591

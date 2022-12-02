using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathNet.Numerics.Statistics;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorPeliculas.Client.Pages
{
    public partial class Counter
    {
        [Inject] protected IJSRuntime JS { get; set; }
        [CascadingParameter]private Task<AuthenticationState> authenticationState { get; set; }

        IJSObjectReference modulo;

        protected int currentCount = 0;
        static int currentCountStatic = 0;

        [JSInvokable]
        public async Task IncrementCount()
        {
            
            var authState = await authenticationState;
            var usuario = authState.User;

            if(usuario.Identity.IsAuthenticated)
            {
            currentCount++;
            currentCountStatic++;

            }
            else
            {
                currentCount--;
                currentCountStatic--;
            }

            await JS.InvokeVoidAsync("pruebaPuntoNetStatic");
        }

        protected async Task IncrementCountJavacript()
        {
            await JS.InvokeVoidAsync("pruebaPuntoNETInstancia",
                    DotNetObjectReference.Create(this));
        }

        [JSInvokable]
        public static Task<int> ObtenerCurrentCount()
        {
            return Task.FromResult(currentCountStatic);
        }
    }
}

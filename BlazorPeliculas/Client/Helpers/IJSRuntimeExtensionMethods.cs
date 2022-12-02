﻿using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorPeliculas.Client.Helpers
{
    public static class IJSRuntimeExtensionMethods
    {
        public static async ValueTask InicializarTimerInactivo<T>(this IJSRuntime js, 
            DotNetObjectReference<T> dotNetObjectReference) where T: class 
        {
            await js.InvokeVoidAsync("timerInactivo", dotNetObjectReference);
        
        
        }
        public static async ValueTask<bool> Confirm(this IJSRuntime js, string mensaje)
        {
            await js.InvokeVoidAsync("console.log", "prueba de console.log");
            return await js.InvokeAsync<bool>("confirm", mensaje);
        }
        public static ValueTask<object> SetInLocalStorage(this IJSRuntime js, string key, string content)
   => js.InvokeAsync<object>(
       "localStorage.setItem",
       key, content
       );

        public static ValueTask<string> GetFromLocalStorage(this IJSRuntime js, string key)
            => js.InvokeAsync<string>(
                "localStorage.getItem",
                key
                );

        public static ValueTask<object> RemoveItem(this IJSRuntime js, string key)
            => js.InvokeAsync<object>(
                "localStorage.removeItem",
                key);
    }
}

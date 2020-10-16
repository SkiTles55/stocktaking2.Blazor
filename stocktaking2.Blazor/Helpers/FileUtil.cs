using Microsoft.JSInterop;
using System;

namespace stocktaking2.Blazor.Helpers
{
    public static class FileUtil
    {
        public static void SaveAs(this IJSRuntime js, string filename, byte[] data)
            => js.InvokeVoidAsync(
                "saveAsFile",
                filename,
                Convert.ToBase64String(data));
    }
}

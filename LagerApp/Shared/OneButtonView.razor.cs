using CommunityToolkit.Maui.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerApp.Shared
{
    partial class OneButtonView
    {
        private async Task Scan()
        {
            var scan = (string)await App.Current.MainPage.ShowPopupAsync(new TSTerminalRewamp.MauiPages.BarCodeScannerPage());
            if (scan == null) return;

            await Task.Delay(0);
        }
    }
}

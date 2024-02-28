using CommunityToolkit.Maui.Views;
using Microsoft.AspNetCore.Components;
using ReactiveUI;
using SqliteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerApp.Shared
{
    partial class TransportView
    {
        private readonly CompositeDisposable disposables;

        private List<Package> itemList;

        private string codeArea1;

        public string CodeArea1
        {
            get { return codeArea1; }
            set 
            {
                if (codeArea1 == value) return;
                codeArea1 = value;
                CodeArea1Changed.InvokeAsync(codeArea1);
            }
        }

        private string codeArea2;
        public string CodeArea2
        {
            get { return codeArea2; }
            set 
            {
                if (codeArea2 == value) return;
                codeArea2 = value;
                CodeArea2Changed.InvokeAsync(codeArea2);
            }
        }


        [Parameter]
        public List<Package> ItemList
        {
            get => itemList;
            set
            {
                if (itemList == value) return;
                itemList = value;
                ItemListChanged.InvokeAsync(itemList);
            }
        }

        private SqliteDB.Branch branchInArea;

        public SqliteDB.Branch BranchInArea
        {
            get => branchInArea;
            set
            {
                if (branchInArea == value) return;
                branchInArea = value;
                BranchInAreaChanged.InvokeAsync(branchInArea);
            }
        }

        private SqliteDB.Item itemOfBranch;

        public SqliteDB.Item ItemOfBranch
        {
            get => itemOfBranch;
            set
            {
                if (itemOfBranch == value) return;
                itemOfBranch = value;
                ItemOfBranchChanged.InvokeAsync(itemOfBranch);
            }
        }



        [Parameter]
        public EventCallback<SqliteDB.Item> ItemOfBranchChanged { get; set; }
        [Parameter]
        public EventCallback<SqliteDB.Branch> BranchInAreaChanged { get; set; }
        [Parameter]
        public EventCallback<string> CodeArea1Changed { get; set; }
        [Parameter]
        public EventCallback<string> CodeArea2Changed { get; set; }
        [Parameter]
        public EventCallback<List<Package>> ItemListChanged { get; set; }

        public TransportView()
        {
            disposables = new CompositeDisposable();
            ItemList = new List<Package>();
        }

        private async Task ScanBarcode1()
        {
            var scan = (string)await App.Current.MainPage.ShowPopupAsync(new TSTerminalRewamp.MauiPages.BarCodeScannerPage());
            if (scan == null) return;
            var area = await ViewModel.ScanArea1Command.Execute(scan);

            CodeArea1 = area.Code;
        }
        private async Task ScanBarcode2()
        {
            var scan = (string)await App.Current.MainPage.ShowPopupAsync(new TSTerminalRewamp.MauiPages.BarCodeScannerPage());
            if(scan == null) return;
            var area = await ViewModel.ScanArea1Command.Execute(scan);

            CodeArea2 = area.Code;
        }

        private async Task ScanQrCode()
        {
            string scan = (string)await App.Current.MainPage.ShowPopupAsync(new TSTerminalRewamp.MauiPages.QrCodeScannerPage());
            if( scan == null) return;

            await ViewModel.ScanQrCommand.Execute(scan);
        }

        protected override void OnAfterRender(bool firstRender)
        {

            if (!firstRender)
            {
                return;
            }
            this.WhenAnyValue(x => x.ViewModel.PackagesForTransport)
                .Where(x => x != null)
                .Do(async (x) =>
                {
                    ItemList = x.ToList();
                    await ItemListChanged.InvokeAsync();
                    StateHasChanged();
                }
                )
                .Subscribe()
                .DisposeWith(disposables);

            this.WhenAnyValue(x => x.ViewModel.BranchInArea)
                .Where(x => x != null)
                .Do(async (x) =>
                {
                    BranchInArea = x;
                    await BranchInAreaChanged.InvokeAsync();
                    StateHasChanged();
                }
                )
                .Subscribe()
                .DisposeWith(disposables);

            this.WhenAnyValue(x => x.ViewModel.ItemOfBranch)
                .Where(x => x != null)
                .Do(async (x) =>
                {
                    ItemOfBranch = x;
                    await ItemOfBranchChanged.InvokeAsync();
                    StateHasChanged();
                }
                )
                .Subscribe()
                .DisposeWith(disposables);
        }

        protected override void Dispose(bool disposing)
        {
            disposables.Dispose();
            base.Dispose(disposing);
        }
    }
}

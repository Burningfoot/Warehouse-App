namespace TSTerminalRewamp.MauiPages;
using CommunityToolkit.Maui.Views;

public partial class QrCodeScannerPage
{
	public QrCodeScannerPage()
	{
		InitializeComponent();
		cameraBarcodeReaderView.Options = new ZXing.Net.Maui.BarcodeReaderOptions
		{
			Formats = ZXing.Net.Maui.BarcodeFormat.QrCode,
			AutoRotate = true,
			Multiple = false,
		};
	}

    private void cameraBarcodeReaderView_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {
		cameraBarcodeReaderView.IsDetecting = false;
		
		Console.WriteLine(e.Results[0].Value);
		Close(e.Results[0].Value);
    }
}
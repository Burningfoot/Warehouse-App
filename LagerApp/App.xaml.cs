namespace LagerApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new MainPage();
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        var window = base.CreateWindow(activationState);

        window.Title = "Lager App";

        //const int newWidth = 400;
        //const int newHeight = 700;

        //window.Width = newWidth;
        //window.Height = newHeight;

        return window;
    }
}

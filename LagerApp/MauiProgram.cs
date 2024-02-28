using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using MudBlazor.Services;
using LagerApp.ViewModels.Pages;
using ZXing.Net.Maui.Controls;
using SqliteDB;
using Microsoft.EntityFrameworkCore;

namespace LagerApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
            .UseBarcodeReader()
            .ConfigureFonts(fonts =>
			{
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        builder.Services.AddMudServices();
        builder.Services.AddMauiBlazorWebView();

        builder.Services.AddDbContext<ApplicationDbContext>(
            options => options.UseSqlite($"DataSource={GetDatabasePath()}", x => x.MigrationsAssembly(nameof(SqliteDB))));

        builder.Services.AddTransient<ItemListViewModel>();
		builder.Services.AddTransient<AreasViewModel>();
        builder.Services.AddTransient<OneButtonViewModel>();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif


		return builder.Build();
	}

    public static string GetDatabasePath()
    {
        var databasePath = "";
        var databaseName = "cache.db";
        if (DeviceInfo.Platform == DevicePlatform.iOS)
        {
            SQLitePCL.Batteries_V2.Init();
            databasePath = Path.Combine(FileSystem.AppDataDirectory, databaseName);
        }
        else
        {
            databasePath = Path.Combine(FileSystem.AppDataDirectory, databaseName);
        }
        return databasePath;
    }
}

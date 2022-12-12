using QuizMaker.Data;
using QuizMaker.View;
using QuizMaker.ViewModel;

namespace QuizMaker;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<QuizViewModel>();
        builder.Services.AddSingleton<QuizService>();
		builder.Services.AddSingleton<LeaderBoardViewModel>();
        builder.Services.AddSingleton<LeaderBoardPage>();
        builder.Services.AddSingleton<QuizMakerPage>();

        builder.Services.AddSingleton<QuizMakerViewModel>();




        return builder.Build();
	}
}

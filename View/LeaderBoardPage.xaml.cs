using QuizMaker.ViewModel;

namespace QuizMaker.View;
public  partial class LeaderBoardPage : ContentPage
{
    private LeaderBoardViewModel _viewMode;

    public LeaderBoardPage(LeaderBoardViewModel vm)
    {
        InitializeComponent();
        _viewMode = vm;
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewMode.GetLeaderBoardCommand.Execute(null);
    }
}
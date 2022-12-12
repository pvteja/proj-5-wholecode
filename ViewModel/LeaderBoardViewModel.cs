using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizMaker.Data;
using QuizMaker.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace QuizMaker.ViewModel;

public partial class LeaderBoardViewModel : ObservableObject
{

    private readonly QuizService _quizService;
    public ObservableCollection<QuizScoresModel> LeaderBoardList { get; set; } = new ObservableCollection<QuizScoresModel>();

    int i = 1;
    public LeaderBoardViewModel(QuizService quizService)
    {
        _quizService =quizService;
    }

    [RelayCommand]
    public async void GetLeaderBoard()
    {
        LeaderBoardList.Clear();
        var scoresList = await _quizService.GetLeaderBoard();
        if (scoresList?.Count > 0)
        {
            //studentList = studentList.OrderBy(f => f.FullName).ToList();
            foreach (var expr in scoresList)
            {
                expr.id=i;
                LeaderBoardList.Add(expr);
                i=i+1;
            }


        }
    }
}
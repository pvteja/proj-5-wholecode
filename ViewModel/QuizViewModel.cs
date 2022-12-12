using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using QuizMaker.Data;
using QuizMaker.Models;
using System.Collections.Generic;

namespace QuizMaker.ViewModel;
public partial class QuizViewModel : ObservableObject
{
    private readonly QuizService _quizService;
    public ObservableCollection<QuizTopicModel> QuizTopicList { get; set; } = new ObservableCollection<QuizTopicModel>();
    public ObservableCollection<QuizTopicModel> QuizQuestionsList { get; set; } = new ObservableCollection<QuizTopicModel>();

    public QuizViewModel(QuizService quizService)
    {
        _quizService =quizService;
    }

    List<QuizQuestionsModel> QList = new List<QuizQuestionsModel>();


    //[ObservableProperty]
    //ObservableCollection<string> items;

    [ObservableProperty]
    public string text;

  

    [RelayCommand]
    public async void GetTopics()
    {
        QuizTopicList.Clear();
        var quizList = await _quizService.GetTopicList();
        if (quizList?.Count > 0)
        {
            //studentList = studentList.OrderBy(f => f.FullName).ToList();
            foreach (var expr in quizList)
            {
                QuizTopicList.Add(expr);
            }

        }
    }


    public async Task<List<QuizQuestionsModel>> GetQuestions(string topicName)
    {
        QList.Clear();
        var tempList = await _quizService.GetQuestions();
        foreach (var expr in tempList)
        {
            if (expr.TopicName == topicName)
            {
                QList.Add(expr);
            }
        }

        return QList;
    }

    public async Task AddScore(string user,int score,float acc)
    {
        await _quizService.AddQuizScores(user, score,acc);
    }





}
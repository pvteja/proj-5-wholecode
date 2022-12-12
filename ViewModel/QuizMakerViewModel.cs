using CommunityToolkit.Mvvm.ComponentModel;
using QuizMaker.Data;
using QuizMaker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizMaker.ViewModel;
public partial class QuizMakerViewModel : ObservableObject
{

    private readonly QuizService _quizService;

    int i = 1;
    public QuizMakerViewModel(QuizService quizService)
    {
        _quizService =quizService;
    }

    public async Task AddQuestion(string topic, string que,string optA, string optB, string optC, string ans)

    {
        Debug.WriteLine(topic,que,optA,optB,optC);

        await _quizService.AddQuestions(topic,que,optA,optB,optC,ans);
    }

    public async Task AddTopic(string author,string topic)
    {
        await _quizService.AddTopic(author,topic);
    }
}
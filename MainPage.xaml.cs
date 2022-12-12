using QuizMaker.Data;
using QuizMaker.Models;
using QuizMaker.ViewModel;
using System.Diagnostics;
using Debug = System.Diagnostics.Debug;

namespace QuizMaker;

public partial class MainPage : ContentPage
{

    private Color lightgray = Colors.LightGray;
    private Color white = Colors.White;

    private Color lightgreen = Colors.LightGreen;
    private Color transparent = Colors.Transparent;
    private Color green = Colors.Green;
    private Color red = Colors.Red;
    int correct = 0;
    string topicSelected = "";
    List<QuizQuestionsModel> exerciseList = null;
    string nameEntered = "";

    private QuizViewModel _quizViewModel;

    int i = 0;

    public MainPage(QuizViewModel vm)
    {
        InitializeComponent();
        _quizViewModel = vm;
        BindingContext = vm;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _quizViewModel.GetTopicsCommand.Execute(null);
    }

    /*  private async void GetQues(string topic)
      {
          var tempList = await _quizViewModel.GetQuestions();
          //Debug.WriteLine("count" + tempList.Count.ToString());
          foreach (var item in tempList)
          {
              if (item.TopicName == topic)
              {
                  exerciseList.Add(item);
              }
          }

      } */

    private async void Button_Clicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string pressed = button.Text;

        Debug.WriteLine(pressed);
        topicSelected=pressed;
        exerciseList = await _quizViewModel.GetQuestions(topicSelected);

        this.TopicGrid.IsVisible=false;
        Debug.WriteLine("count"+exerciseList.Count);
        Debug.WriteLine(exerciseList[i].ToString());
        Debug.WriteLine(i.ToString());
        Debug.WriteLine(exerciseList[i].question); // QList[ind].optionA, QList[ind].optionB, QList[ind].optionC, QList[ind].answer);
        Debug.WriteLine(exerciseList[i].optionA);
        Debug.WriteLine(exerciseList[i].optionB);
        Debug.WriteLine(exerciseList[i].optionC);
        Debug.WriteLine(exerciseList[i].answer);

        this.NameEntry.IsVisible=true;
        pressed="";


    }

    private void DisplayGrid(int i)
    {
        //Debug.WriteLine(ind.ToString(), QList[ind].question, QList[ind].optionA, QList[ind].optionB, QList[ind].optionC, QList[ind].answer);
        Debug.WriteLine(i.ToString());

        this.id.Text = "Question " + ((i+1).ToString());
        this.question.Text = "Q)" + exerciseList[i].question;
        this.optionA.Text = exerciseList[i].optionA;
        this.optionB.Text = exerciseList[i].optionB;
        this.optionC.Text = exerciseList[i].optionC;

    }
    private void DisableOptions()
    {
        this.optionA.IsEnabled = false;
        this.optionB.IsEnabled = false;
        this.optionC.IsEnabled = false;
        /*        this.optionA.BackgroundColor=transparent;
                this.optionB.BackgroundColor=transparent;
                this.optionC.BackgroundColor=transparent; */
        this.optionC.TextColor=white;
        this.optionA.TextColor=white;
        this.optionB.TextColor=white;
    }
    private void EnableOptions()
    {
        this.optionA.IsEnabled = true;
        this.optionB.IsEnabled = true;
        this.optionC.IsEnabled = true;
        this.optionC.TextColor=white;

        this.optionA.TextColor=white;
        this.optionB.TextColor=white;
    }
    private async void Option_Clicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string pressed = button.Text;
        Debug.WriteLine("Pressed" + pressed);
        Debug.WriteLine(exerciseList[i].answer.ToString());
        Debug.WriteLine(exerciseList[i].id + " , " + exerciseList[i].question + " , " + exerciseList[i].optionA + " , " + exerciseList[i].optionB + " , " + exerciseList[i].optionC + " , " + exerciseList[i].answer);
        DisableOptions();
        if (pressed == exerciseList[i].answer)
        {
            button.BackgroundColor = lightgreen;
            this.result.IsVisible = true;
            this.result.Text = "Correct";
            this.BackgroundImageSource = "celebrations2.gif";
            this.result.TextColor = lightgreen;
            correct = correct + 1;
            await Task.Delay(2000);
            Next_Clicked(Next_Button, e);
        }
        else
        {
            button.BackgroundColor = lightgray;
            this.result.IsVisible = true;
            this.result.Text = "Wrong";
            this.tryAgain.IsVisible = true;
            this.Next_Button.IsVisible = true;
            this.result.TextColor = lightgray;

        }
        this.score.Text = "Score: " + correct + "/" + exerciseList.Count();

        pressed="";
    }

    private void ButtonTransparent()
    {
        this.optionA.BackgroundColor = transparent;
        this.optionB.BackgroundColor = transparent;
        this.optionC.BackgroundColor = transparent;
    }

    private void Next_Clicked(object sender, EventArgs e)
    {
        this.tryAgain.IsVisible = false;
        this.Next_Button.IsVisible = false;
        i = i + 1;
        if (i == exerciseList.Count())
        {
            EndofExercise();
            return;
        }
        EnableOptions();

        ButtonTransparent();
        this.result.Text = "";

        DisplayGrid(i);
        this.BackgroundImageSource = "background.png";
    }


    private async void EndofExercise()
    {
        this.QuestionsGrid.IsVisible = false;
        if (correct > exerciseList.Count*0.7)
            this.BackgroundImageSource = "celebrations2.gif";
        else
            this.BackgroundImageSource = "background.png";
        this.finalScore.IsVisible = true;
        this.finalScore.Text = "Final Score is :" + correct + "/" + exerciseList.Count();
        this.quitApp.IsVisible = true;
        this.StartAgain.IsVisible = true;
        float acc = (correct/exerciseList.Count)*100;
        await _quizViewModel.AddScore(nameEntered, correct, acc);
        i=0;
        correct=0;
    }




    private void tryAgain_Clicked(object sender, EventArgs e)
    {
        EnableOptions();
        ButtonTransparent();
        this.result.Text = "";
        this.tryAgain.IsVisible = false;
        this.Next_Button.IsVisible = false;

    }



    private async void StartAgain_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Question?", "Would you like to select game again?", "Yes", "No");
        Debug.WriteLine("bool resart" + answer);
        if (answer)
        {
            exerciseList.Clear();

            // exerciseList = await _quizViewModel.GetQuestions(topicSelected);
            this.QuestionsGrid.IsVisible = false;
            this.TopicGrid.IsVisible=true;
            this.BackgroundImageSource = "background.png";
            this.finalScore.IsVisible = false;
            EnableOptions();
            this.quitApp.IsVisible = false;
            this.StartAgain.IsVisible = false;
            ButtonTransparent();
            this.result.Text = "";
            i=0;
            correct=0;

        }

    }


    private async void quitApp_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Question?", "Would you like to Quit  the game", "Yes", "No");

        if (answer)
            Application.Current?.CloseWindow(Application.Current.MainPage.Window);

    }

    private void OnEntryCompleted(object sender, EventArgs e)
    {
        Debug.WriteLine("reached");
        Debug.WriteLine(i);
        this.QuestionsGrid.IsVisible=true;
        this.NameEntry.IsVisible=false;

        nameEntered=this.NameEntry.Text;
        DisplayGrid(i);



    }
}
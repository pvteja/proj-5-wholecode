using QuizMaker.ViewModel;
using System.Diagnostics;

namespace QuizMaker.View;

public partial class QuizMakerPage : ContentPage
{
	int i = 0;
	int num = 0;
	string author = "";
	string quizTopic = "";


    private QuizMakerViewModel _viewMode;

 
    public QuizMakerPage(QuizMakerViewModel vm)

	{
        InitializeComponent();
        _viewMode = vm;
        BindingContext = vm;
	}

	private async void Button_Clicked(object sender, EventArgs e)
	{
		try
		{
			num = Int32.Parse(this.numQuestions.Text);
		}
		catch
		{
			this.numQuestions.Text="";
            await Application.Current.MainPage.DisplayAlert(" ", "Enter only numbers in no of Questions Field.", "Ok");
			return;
        }
		Debug.WriteLine(num);
		author=this.authorName.Text;
		quizTopic=this.quizName.Text;
        await _viewMode.AddTopic(author, quizTopic);

        this.InfoGrid.IsVisible=false;
		this.QuestionGrid.IsVisible=true;
		


	}

	private async void Question_Button_Clicked(object sender, EventArgs e)
	{
		i++;
		var que=this.QuestionTag.Text;
		var optA=this.optionATag.Text;
		var optB=this.optionBTag.Text;
		var optC=this.optionCTag.Text;
		var ans=this.answerTag.Text;
		Debug.WriteLine("Entered ", QuestionTag.Text, optA, optB, optC, ans);
		await _viewMode.AddQuestion(quizTopic, que, optA, optB, optC, ans);

		if (i ==  num)
		{
			EndofExercise();

        }
		else
			{
			DisplayGrid(i);
		}
	}

	public void DisplayGrid(int i)
	{
		this.queLabel.Text = "Q "+(i+1).ToString()+")";
		this.QuestionTag.Text="";
		this.optionATag.Text="";
		this.optionBTag.Text="";
		this.optionCTag.Text="";
		this.answerTag.Text="";
	}
    private void EndofExercise()
    {
        this.QuestionGrid.IsVisible = false;
        
     
        this.quitApp.IsVisible = true;
        this.StartAgain.IsVisible = true;
		num=0;
		i=0;
    }

	private void StartAgain_Clicked(object sender, EventArgs e)
	{
		this.quitApp.IsVisible=false;
		this.StartAgain.IsVisible=false;
		this.InfoGrid.IsVisible=true;
        num=0;
        i=0;
		this.authorName.Text="";
		this.quizName.Text="";
		this.numQuestions.Text="";
    }

    private async void quitApp_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Question?", "Would you like to Quit  the game", "Yes", "No");

        if (answer)
            Application.Current?.CloseWindow(Application.Current.MainPage.Window);

    }
}
using Microsoft.Maui.Controls.Compatibility;
using QuizMaker.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizMaker.Data
{
    public class QuizService
    {
        private SQLiteAsyncConnection _dbConnection;

        private async Task SetUpDb()
        {
            if (_dbConnection == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "QuizTopic4.db3");
                _dbConnection = new SQLiteAsyncConnection(dbPath);

                await _dbConnection.CreateTableAsync<QuizTopicModel>();
                await _dbConnection.CreateTableAsync<QuizQuestionsModel>();
                await _dbConnection.CreateTableAsync<QuizScoresModel>();




            }
        }

        private async Task AddinitialData()
        {
            await _dbConnection.InsertAsync(new QuizTopicModel { TopicName="Entertainment", author="pvteja" });
            await _dbConnection.InsertAsync(new QuizTopicModel { TopicName="Sports", author="vedasree" });
            await _dbConnection.InsertAsync(new QuizTopicModel { TopicName="Movies", author="pvteja" });

            await _dbConnection.InsertAsync(new QuizQuestionsModel
            {
                TopicName="Sports",
                question="Which two countries have not missed one of the modern-day Olympics?",
                optionA="Greece and Australia",
                optionB="Germany and England",
                optionC="Switzerland and England",
                answer="Greece and Australia"
            });

            await _dbConnection.InsertAsync(new QuizQuestionsModel
            {
                TopicName="Sports",
                question="Which country won the first-ever soccer World Cup in 1930?",
                optionA="Uruguay",
                optionB="Brazil",
                optionC="South Africa",
                answer="Uruguay"
            });



            await _dbConnection.InsertAsync(new QuizQuestionsModel
            {
                TopicName="Sports",
                question="What sport is dubbed the \"king of sports\"?",
                optionA="Soccer",
                optionB="BasketBall",
                optionC="Tennis",
                answer="Soccer"
            });

            await _dbConnection.InsertAsync(new QuizQuestionsModel
            {
                TopicName="Sports",
                question=" Dump, floater and wipe are terms used in which team sport ? ",
                optionA="Soccer",
                optionB="BasketBall",
                optionC="Volleyball",
                answer="Volleyball"
            });
        }
        public async void AddinitialscoresData()
        {
            await _dbConnection.InsertAsync(new QuizScoresModel
            {
                user="Sam",
                score=3,
                acc= 80
            });

            await _dbConnection.InsertAsync(new QuizScoresModel
            {
                user="Tezz",
                score=3,
                acc= 75
            });

        }


        public async Task<List<QuizTopicModel>> GetTopicList()
        {
            await SetUpDb();

            var quizTopicsList = await _dbConnection.Table<QuizTopicModel>().ToListAsync();
            if (quizTopicsList.Count == 0)
            {
                await AddinitialData();
                quizTopicsList = await _dbConnection.Table<QuizTopicModel>().ToListAsync();

            }
            return quizTopicsList;
        }

        public async Task<List<QuizQuestionsModel>> GetQuestions()
        {
            await SetUpDb();
            var questionsList = await _dbConnection.Table<QuizQuestionsModel>().ToListAsync();
            return questionsList;
        }

        public async Task AddQuizScores(string entereduser, int achievedscore, float achievedaccuracy)
        {
            await _dbConnection.InsertAsync(new QuizScoresModel { user=entereduser, score=achievedscore, acc=achievedaccuracy });

        }




        public async Task<List<QuizScoresModel>> GetLeaderBoard()
        {


            var quizscoresList = await _dbConnection.Table<QuizScoresModel>().ToListAsync();
            if (quizscoresList.Count == 0)
            {
                AddinitialscoresData();

            }
            return await _dbConnection.Table<QuizScoresModel>().OrderByDescending(x => x.acc).ToListAsync();
        }

        public async Task AddQuestions(string topic, string que, string optA, string optB, string optC, string ans)
        {
            await _dbConnection.InsertAsync(new QuizQuestionsModel
            {
                TopicName=topic,
                question=que,
                optionA=optA,
                optionB=optB,
                optionC=optC,
                answer=ans
            });
        }

        public async Task AddTopic(string authorName,string topic)
        {
            await _dbConnection.InsertAsync(new QuizTopicModel
            {
                author=authorName,
                TopicName=topic,
          
            });

        }

    }
}

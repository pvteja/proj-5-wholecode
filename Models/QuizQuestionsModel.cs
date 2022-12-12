using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizMaker.Models
{
    public class QuizQuestionsModel
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        public string TopicName { get; set; }
        public string question  {get; set;}
    public  string optionA {get; set;}
        public string optionB { get; set; }
        public string optionC { get; set; }

        public string answer { get; set; }





    }
}

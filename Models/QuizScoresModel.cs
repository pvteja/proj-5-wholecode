using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizMaker.Models
{
    public class QuizScoresModel
    {
        [PrimaryKey,AutoIncrement]
        public int id { get; set; }
        public string user { get; set; }
        public int score { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public float acc { get; set; }
    }
}

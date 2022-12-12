using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QuizMaker.Models
{
    public class QuizTopicModel
    {
        [PrimaryKey, AutoIncrement]
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        
        public string author { get; set; }

     


        public DateTime Created { get; set; } = DateTime.Now;
    }
}

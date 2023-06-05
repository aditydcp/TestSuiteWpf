using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSuiteWpf.Models
{
    public class Question
    {
        public string QuestionText { get; }
        public int Answer { get; }

        public Question (string question, int answer)
        {
            QuestionText = question;
            Answer = answer;
        }

        public Result CheckAnswer(int answer)
        {
            if (answer == Answer) { return Result.Correct; }
            else if (answer < 0) { return Result.Timeout; }
            else { return Result.Incorrect; }
        }
        public Result CheckAnswer(string answerString = "-1")
        {
            int answer = int.Parse(answerString);
            if (answer == Answer) { return Result.Correct; }
            else if (answer < 0) { return Result.Timeout; }
            else { return Result.Incorrect; }
        }
    }
}

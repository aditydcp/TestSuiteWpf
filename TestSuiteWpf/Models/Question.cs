﻿using System;
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

        public bool CheckAnswer(int answer) { return Answer == answer; }
        public bool CheckAnswer(string answer) { return Answer == int.Parse(answer); }
    }
}

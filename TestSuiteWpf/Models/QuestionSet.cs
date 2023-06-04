using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSuiteWpf.Models
{
    public class QuestionSet
    {
        private readonly List<Question> questions;
        public int Level { get; }

        private int lastQuestionGivenIndex = -1;
        // saves the index of the latest (max 5) given questions
        private List<int> questionIndexBuffer = new(5);
        private readonly Random random = new();

        public QuestionSet(List<Question> questions, int level)
        {
            this.questions = questions;
            Level = level;
        }

        /// <summary>
        /// Pick a random question from this question set
        /// </summary>
        /// <returns>A <see cref="Question"/> object</returns>
        public Question GetRandomQuestion()
        {
            bool isNew = false;
            int index = 0;
            // ensure the next question is not the same one as the previous
            while (!isNew)
            {
                index = random.Next(0, questions.Count);
                if (index != lastQuestionGivenIndex)
                {
                    isNew = true;
                }
            }
            lastQuestionGivenIndex = index;
            return questions.ElementAt(index);
        }
    }
}

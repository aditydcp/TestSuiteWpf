using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSuiteWpf.Models;

namespace TestSuiteWpf.Repositories
{
    internal class QuestionSetRepository
    {
        private readonly List<QuestionSet> QuestionSets = new();

        public IEnumerable<QuestionSet> GetAllQuestionSets() { return QuestionSets; }

        // hardcoded list of data
        public QuestionSetRepository() 
        {
            QuestionSets.Clear();
            QuestionSets.Add(
                new QuestionSet(
                    level: 1,
                    questions:
                        new List<Question>()
                        {
                            new Question("1 + 1",2),
                            new Question("1 + 2",3),
                            new Question("1 + 3",4),
                            new Question("1 + 4",5),
                            new Question("1 + 5",6),
                            new Question("1 + 6",7),
                            new Question("1 + 7",8),
                            new Question("1 + 8",9),
                            new Question("2 + 1",4),
                        }
                    )
                );
            QuestionSets.Add(
                new QuestionSet(
                    level: 2,
                    questions:
                        new List<Question>()
                        {
                            new Question("1 * 1",1),
                            new Question("1 * 2",2),
                            new Question("1 * 3",3),
                            new Question("1 * 4",4),
                            new Question("1 * 5",5),
                            new Question("1 * 6",6),
                            new Question("1 * 7",7),
                            new Question("1 * 8",8),
                            new Question("2 * 1",2),
                        }
                    )
                );
        }
    }
}

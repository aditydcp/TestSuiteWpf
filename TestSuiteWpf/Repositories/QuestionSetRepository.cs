using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Windows.Media.Media3D;
using TestSuiteWpf.Models;

namespace TestSuiteWpf.Repositories
{
    internal class QuestionSetRepository
    {
        private readonly List<QuestionSet> QuestionSets = new();

        public IEnumerable<QuestionSet> GetAllQuestionSets() { return QuestionSets; }

        private static int GetAnswer(string[] stringArray)
        {
            List<string> strings = new();
            strings.AddRange(stringArray);

            // get the order in which operator should be done
            // (1) prioritize multiplications and divisions
            // (2) prioritize operations from left
            
            List<int> operatorOrderedList = new();
            int cursor = 100; // a random high number to prevent cursor from accidentally being lower than @operator
            int loopCounter = 0; // how many times have we looped, or on which element our loop is working on
            int timesOnTheRight = 0; // how many times an operator is positioned to the right of the previous operator
            
            // loop to find all the multiplications and divisions from left to right
            for (int i = 1; i < strings.Count; i += 2) {
                if (strings[i] == "*" || strings[i] == "/") { operatorOrderedList.Add(i); }
            }
            // loop to find all the additions and subtraction from left to right
            for (int i = 1; i < strings.Count; i += 2)
            {
                if (strings[i] == "+" || strings[i] == "-") { operatorOrderedList.Add(i); }
            }

            // now all operations have been sorted
            // do the operations by operating the index saved in operatorOrderedList
            // using adjacent indexes to get result of that single operator
            operatorOrderedList.ForEach(@operator =>
            {
                if (loopCounter < operatorOrderedList.Count - 1)
                {
                    // if the operator is positioned to the right of the previous operator,
                    // substract @operator by (2 * loopCounter - timesOnTheRight) to get the updated index.
                    if (cursor <= @operator)
                    {
                        @operator -= 2 * (loopCounter - timesOnTheRight);
                        timesOnTheRight++;
                    }
                }
                // special case for the last operation, force @operator to be on index 1
                // this will ensure the index @operator is pointing to is the last single operator left 
                else
                {
                    @operator = 1;
                }
                

                cursor = @operator;
                double answerBuffer = Operate(strings[@operator], strings[@operator - 1], strings[@operator + 1]);
                strings[@operator] = answerBuffer.ToString(); // TODO: toString? would it cause problem?
                strings.RemoveAt(@operator + 1);
                strings.RemoveAt(@operator - 1);
                loopCounter++;
            });

            // the operation should reduce the size of the list until there is only 1 member
            // which is the answer
            return int.Parse(strings.First());
        }

        private static double Operate(string Operator, string Left, string Right)
        {
            double left = double.Parse(Left);
            double right = double.Parse(Right);
            return Operator switch
            {
                "*" => left * right,
                "/" => left / right,
                "+" => left + right,
                "-" => left - right,
                _ => -1 // -1 as an error code since all solutions are in range of 0-9
            };
        }

        private static void PopulateQuestionSet(QuestionSet questionSet, List<string> questionsList)
        {
            foreach (string question in questionsList)
            {
                questionSet.questions.Add(
                    new Question(
                        question,
                        GetAnswer(question.Split(" "))
                        )
                    );
            }
        }

        // populate repository with data from base
        public QuestionSetRepository() 
        {
            QuestionSets.Clear();
            QuestionSets.Add(
                new QuestionSet(
                    level: 1,
                    questions:
                        new List<Question>()
                    )
                );
            PopulateQuestionSet(QuestionSets.Last(), level1Questions);

            QuestionSets.Add(
                new QuestionSet(
                    level: 2,
                    questions:
                        new List<Question>()
                    )
                );
            PopulateQuestionSet(QuestionSets.Last(), level2Questions);

            QuestionSets.Add(
                new QuestionSet(
                    level: 3,
                    questions: new List<Question>()
                    )
                );
            PopulateQuestionSet(QuestionSets.Last(), level3Questions);

            QuestionSets.Add(
                new QuestionSet(
                    level: 4,
                    questions: new List<Question>()
                    )
                );
            PopulateQuestionSet(QuestionSets.Last(), level4Questions);

            QuestionSets.Add(
                new QuestionSet(
                    level: 5,
                    questions: new List<Question>()
                    )
                );
            PopulateQuestionSet(QuestionSets.Last(), level5Questions);
        }

        #region Questions/problems base
        // level1 problems
        // only 1-2 digit integers(at least one has to be single digit, double digits only from the teens, no 0)
        // only 1 operator
        // only addition or subtraction problems
        private readonly List<string> level1Questions = new()
        {
            "1 - 1", "2 - 1", "3 - 1", "4 - 1", "5 - 1", "6 - 1", "7 - 1", "8 - 1", "9 - 1", "2 - 2",
            "3 - 2", "4 - 2", "5 - 2", "6 - 2", "7 - 2", "8 - 2", "9 - 2", "3 - 3", "4 - 3", "5 - 3",
            "6 - 3", "7 - 3", "8 - 3", "9 - 3", "4 - 4", "5 - 4", "6 - 4", "7 - 4", "8 - 4", "9 - 4",
            "5 - 5", "6 - 5", "7 - 5", "8 - 5", "9 - 5", "6 - 6", "7 - 6", "8 - 6", "9 - 6", "7 - 7",
            "8 - 7", "9 - 7", "8 - 8", "9 - 8", "9 - 9",
            "1 + 1", "2 + 1", "3 + 1", "4 + 1",
            "5 + 1", "6 + 1", "7 + 1", "8 + 1", "1 + 2","2 + 2","3 + 2","4 + 2","5 + 2",
            "6 + 2", "7 + 2", "1 + 3", "2 + 3", "3 + 3","4 + 3","5 + 3","6 + 3",
            "1 + 4", "2 + 4", "3 + 4", "4 + 4", "5 + 4", "1 + 5","2 + 5","3 + 5","4 + 5",
            "1 + 6", "2 + 6", "3 + 6", "1 + 7", "2 + 7", "1 + 8",
            "10 - 1", "10 - 2", "10 - 3", "10 - 4", "10 - 5", "10 - 6", "10 - 7", "10 - 8", "10 - 9",
            "11 - 2", "11 - 3", "11 - 4", "11 - 5", "11 - 6", "11 - 7", "11 - 8", "11 - 9",
            "12 - 3", "12 - 4", "12 - 5", "12 - 6", "12 - 7", "12 - 8", "12 - 9",
            "13 - 4", "13 - 5", "13 - 6", "13 - 7", "13 - 8", "13 - 9",
            "14 - 5", "14 - 6", "14 - 7", "14 - 8", "14 - 9",
            "15 - 6", "15 - 7", "15 - 8", "15 - 9",
            "16 - 7", "16 - 8", "16 - 9",
            "17 - 8", "17 - 9",
            "18 - 9"
        };

        // level2 problems
        // only 1-2 digit integers(at least one has to be single digit, double digits only from the teens, no 0)
        // only 1 operator
        // only multiplication or division
        private readonly List<string> level2Questions = new()
        {
            "1 * 1", "1 * 2", "1 * 3", "1 * 4", "1 * 5", "1 * 6", "1 * 7", "1 * 8", "1 * 9",
            "2 * 1", "2 * 2", "2 * 3", "2 * 4",
            "3 * 1", "3 * 2", "3 * 3",
            "4 * 1", "4 * 2",
            "5 * 1",
            "6 * 1",
            "7 * 1",
            "8 * 1",
            "9 * 1",
            "1 / 1", "2 / 1", "3 / 1", "4 / 1", "5 / 1", "6 / 1", "7 / 1", "8 / 1", "9 / 1",
            "2 / 2", "4 / 2", "6 / 2", "8 / 2", "10 / 2", "12 / 2", "14 / 2", "18 / 2",
            "3 / 3", "6 / 3", "9 / 3", "12 / 3", "15 / 3", "18 / 3",
            "4 / 4", "8 / 4", "12 / 4", "16 / 4",
            "5 / 5", "10 / 5", "15 / 5",
            "6 / 6", "12 / 6", "18 / 6",
            "7 / 7", "14 / 7",
            "8 / 8", "16 / 8",
            "9 / 9", "18 / 9"
        };

        // level3 problems
        // only 1-2 digit integers(double digits only from the teens, no 0)
        // at least 3 numbers are single digits
        // 3 operators => 4 numbers
        // only addition or subtraction problems
        private readonly List<string> level3Questions = new()
        {
            "4 - 8 - 8 + 12", "5 + 2 + 4 - 10", "7 + 6 + 1 - 8", "5 + 10 - 4 - 6", "5 + 3 - 10 + 7", "5 - 10 + 9 + 3",
            "2 + 2 + 8 - 6", "17 - 4 - 1 - 6", "9 - 5 - 1 + 6", "11 - 3 + 6 - 9", "12 - 1 - 1 - 6", "5 + 3 + 4 - 5",
            "7 + 7 + 8 - 16", "6 + 8 - 3 - 6", "16 - 9 - 8 + 7", "5 + 1 + 3 - 9", "19 - 6 - 2 - 7", "12 - 1 - 6 + 4",
            "9 + 1 + 8 - 17", "8 + 3 + 7 - 18", "4 + 7 + 1 - 12", "10 - 6 - 7 + 8", "7 + 5 - 6 - 3", "9 + 9 + 3 - 17",
            "8 + 1 - 1 - 8", "3 - 8 - 7 + 18", "9 - 7 + 8 - 9", "1 - 6 + 8 + 2", "16 - 2 - 9 - 4", "9 - 8 - 2 + 5",
            "7 - 8 + 15 - 6", "2 - 4 - 7 + 13", "2 - 7 - 3 + 10", "5 + 10 - 2 - 7", "8 + 3 - 5 + 2", "8 + 1 - 6 + 5",
            "3 + 9 + 5 - 16", "6 + 9 + 1 - 11", "4 - 3 + 2 - 3", "3 - 1 + 9 - 4", "6 + 9 - 19 + 7", "5 + 4 + 8 - 13",
            "17 - 9 - 7 + 4", "6 - 7 + 7 + 1", "14 + 6 - 7 - 7", "14 - 1 - 9 + 2", "8 + 11 - 8 - 7", "8 - 1 + 6 - 9",
            "13 - 1 - 3 - 9", "5 - 5 + 14 - 9", "9 + 7 - 4 - 4", "3 + 3 + 11 - 9", "19 - 6 - 9 - 3", "10 - 4 + 1 + 1",
            "8 - 19 + 5 + 7", "4 + 7 + 9 - 19", "6 - 5 - 2 + 3", "1 + 16 - 3 - 7", "3 + 17 - 8 - 3", "8 - 5 + 2 + 1",
            "3 + 2 + 10 - 6", "16 - 6 + 1 - 8", "9 + 8 - 1 - 13", "6 - 8 - 6 + 14", "19 - 1 - 4 - 8", "13 - 7 + 3 - 5",
            "6 - 5 + 3 - 1", "4 - 12 + 8 + 9", "4 - 5 + 12 - 6", "5 - 9 + 16 - 4", "19 - 9 - 9 + 6", "8 - 2 - 4 + 1",
            "9 + 6 - 6 - 8", "4 + 14 - 4 - 8", "14 + 1 + 2 - 8", "2 + 2 + 7 - 9", "7 + 9 + 5 - 15", "8 - 5 + 8 - 8",
            "3 + 5 + 3 - 9", "3 + 16 - 6 - 7", "4 + 7 - 18 + 9", "1 + 2 - 7 + 11", "14 - 4 + 1 - 9", "6 - 8 - 1 + 11",
            "2 + 3 + 1 + 3", "4 + 18 - 7 - 9", "12 + 1 - 8 - 5", "14 + 5 - 5 - 7", "5 - 9 - 4 + 12", "4 + 13 - 4 - 5",
            "9 - 19 + 7 + 7", "11 - 6 + 2 - 4", "5 + 16 - 8 - 6", "7 - 1 - 7 + 6", "4 + 3 - 3 - 4", "19 - 6 - 4 - 4",
            "7 - 9 - 6 + 17", "4 - 1 + 6 - 5", "18 - 4 - 1 - 4", "6 - 8 - 9 + 11", "2 + 2 - 6 + 6", "3 - 9 + 1 + 6",
            "7 + 7 - 17 + 3", "3 - 3 + 2 + 5", "16 - 8 + 7 - 6", "10 - 4 + 6 - 4", "5 - 8 + 1 + 2", "4 - 4 + 3 - 2",
            "12 + 2 - 8 + 1", "7 - 12 + 7 + 3", "11 + 7 - 8 - 4"
        };

        // level4 problems
        // only 1 digit integers(no 0)
        // 3 operators
        // only multiplication or division
        private readonly List<string> level4Questions = new()
        {
            "2 / 6 * 6 * 3", "2 * 3 * 6 / 9", "1 * 2 / 2 * 2", "3 * 4 / 8 * 2",
            "7 / 7 / 1 * 2", "5 / 1 / 2 * 2", "9 * 2 * 2 / 4", "2 / 7 / 1 * 7",
            "7 / 7 * 4 / 2", "1 / 1 / 1 * 9", "2 * 1 * 8 / 2", "7 * 8 / 8 * 1",
            "1 / 6 * 4 * 3", "8 / 1 * 6 / 8", "3 * 9 / 3 / 1", "9 / 2 * 4 / 3",
            "5 * 1 / 5 * 3", "3 / 1 * 7 / 3", "4 / 4 / 7 * 7", "3 / 5 * 5 * 3",
            "3 * 2 * 9 / 6", "4 * 3 / 1 / 4", "2 / 1 * 4 / 8", "6 * 6 * 2 / 8",
            "4 / 7 * 7 / 2", "9 / 9 * 2 * 2", "1 * 6 / 2 * 2", "8 / 8 / 8 * 8",
            "1 / 3 / 1 * 9", "9 / 1 * 2 / 3", "3 * 4 / 6 / 1", "9 * 2 / 9 * 4",
            "5 * 8 / 2 / 5", "4 * 3 / 2 / 1", "8 / 2 * 5 / 4", "4 / 1 * 6 / 3",
            "5 * 3 / 9 * 3", "4 * 2 * 2 / 8", "3 / 6 * 8 / 2", "9 / 9 * 4 * 2",
            "2 * 6 * 4 / 6", "4 / 1 / 6 * 3", "5 / 5 * 6 / 3", "8 * 3 / 2 / 6",
            "2 * 4 / 4 * 3", "9 / 3 * 9 / 3", "1 * 2 / 7 * 7", "7 / 7 * 1 * 6",
            "5 / 5 / 5 * 5", "4 * 2 * 2 / 4", "2 / 2 / 1 * 9", "9 / 1 * 1 * 1",
            "8 * 6 / 3 / 2", "4 * 2 / 8 * 5", "8 * 1 / 4 * 2", "4 * 8 / 4 / 4",
            "6 * 2 / 6 * 4", "4 * 3 / 2 / 3", "4 * 1 * 6 / 4", "1 / 4 * 8 * 1",
            "9 * 2 / 6 * 2", "8 / 4 / 3 * 9", "2 * 9 / 2 / 1", "8 / 3 * 3 / 1",
            "1 * 4 / 4 / 1", "2 * 4 / 8 * 9", "4 * 2 / 4 * 1", "8 * 6 / 2 / 8",
            "9 / 6 / 6 * 4", "7 * 2 * 2 / 7", "2 * 8 * 2 / 8", "8 * 7 / 7 / 4",
            "5 / 5 / 7 * 7", "1 / 6 * 6 / 1", "4 * 3 * 1 / 2", "3 * 2 / 2 * 2",
            "6 * 1 * 4 / 4", "5 / 9 / 5 * 9", "9 / 1 / 1 / 9", "6 * 6 / 6 / 6",
            "9 / 3 / 3 * 8", "4 / 8 * 2 * 1", "8 / 8 * 5 / 5", "6 * 5 / 5 / 3",
            "6 * 6 / 4 / 9", "3 / 3 * 2 * 2", "2 * 6 * 3 / 9", "6 * 1 / 2 / 3",
            "3 / 9 / 1 * 6", "4 / 1 * 4 / 4", "1 / 5 * 5 * 1", "5 * 8 / 4 / 2",
            "3 * 3 * 1 / 1", "9 * 1 / 9 * 8", "8 * 4 / 1 / 8", "9 * 7 / 3 / 7"
        };

        // level5 problems (mixed operations)
        // only 1-2 digit integers(at least 3 numbers are single digits, double digits only from the teens, no 0)
        // 3 operators
        // at least 1 addition or subtraction operator AND at least 1 multiplication or division operator
        private readonly List<string> level5Questions = new()
        {
            "3 * 3 + 6 - 8", "2 - 5 / 1 + 4", "8 - 8 + 6 * 1", "1 * 4 - 3 + 2", "1 * 1 - 6 + 11", "2 - 4 + 3 / 1",
            "7 - 5 / 5 - 5", "10 / 4 * 2 + 2", "11 - 4 - 2 / 1", "1 * 5 + 2 - 1", "1 * 12 / 4 + 3", "5 / 8 * 16 - 3",
            "1 * 3 - 3 + 8", "7 - 8 / 1 + 1", "6 + 3 - 18 / 6", "2 * 2 + 8 - 7", "4 - 8 / 1 + 7", "9 - 4 * 5 + 12",
            "1 / 1 + 1 - 1", "1 - 1 * 2 + 2", "3 - 1 + 1 * 7", "9 - 3 / 1 - 1", "8 / 6 * 9 - 8", "13 / 8 + 3 / 8",
            "1 * 6 * 1 - 5", "2 + 5 - 9 / 9", "4 / 4 * 2 - 2", "8 + 14 / 7 - 1", "1 * 5 + 10 / 5", "8 - 1 * 8 + 9",
            "5 - 2 + 2 / 1", "1 * 5 * 3 - 7", "9 - 2 / 4 * 10", "7 * 2 * 1 - 9", "3 * 5 - 7 - 6", "7 * 1 + 9 - 7",
            "8 / 4 + 2 + 4", "2 / 18 - 1 / 9", "7 - 12 + 3 * 2", "3 + 12 / 1 - 8", "8 - 1 * 13 + 5", "8 + 1 * 4 - 9",
            "14 / 2 / 7 + 2", "4 + 6 - 1 * 10", "4 + 16 - 5 * 4", "4 + 14 - 5 * 3", "14 / 7 - 2 + 2", "8 / 8 - 2 + 8",
            "8 + 2 - 2 / 2", "18 / 9 - 2 * 1", "2 * 6 + 2 - 9", "5 / 5 + 3 + 3", "5 + 5 * 4 - 16", "8 / 1 - 5 + 6",
            "14 - 1 - 1 * 9", "5 * 3 + 4 - 11", "16 - 4 * 2 - 2", "4 * 4 + 1 - 11", "19 - 1 - 6 * 3", "9 + 16 / 8 - 4",
            "17 - 1 * 9 - 3", "3 - 15 + 3 * 5", "2 * 6 / 3 + 3", "7 + 7 - 12 / 1", "18 - 8 / 1 - 5", "1 / 7 * 7 + 4",
            "5 - 4 / 18 * 9", "10 - 4 - 2 * 3", "2 * 3 / 6 + 7", "9 - 16 / 8 - 2", "7 * 2 - 14 + 6", "2 + 2 / 4 * 12",
            "2 - 9 + 3 * 4", "16 - 6 / 3 - 5", "3 - 1 - 2 / 1", "17 / 3 * 3 - 8", "7 - 4 / 7 * 7", "17 - 7 - 1 * 1",
            "6 - 12 / 6 * 2", "10 / 1 - 8 + 5", "6 + 6 * 6 / 18", "5 - 9 * 5 / 15", "7 - 6 + 3 * 2", "2 - 2 * 7 + 12",
            "6 / 4 - 7 / 14", "2 * 6 - 18 / 3", "8 + 2 * 1 - 4", "9 / 9 * 6 - 4", "4 / 12 * 6 + 6", "2 + 1 * 1 - 1",
            "7 + 4 / 4 - 2", "11 / 2 + 1 / 2", "5 * 4 / 2 - 6", "7 * 4 - 19 - 4", "19 / 1 - 4 * 4", "2 - 19 + 6 * 4",
            "3 * 8 - 17 - 3", "9 + 1 - 10 / 5", "12 - 4 - 8 / 4", "8 + 6 / 2 / 3", "17 - 3 * 1 - 5", "10 - 7 - 4 / 2",
            "4 - 16 / 8 + 4", "8 - 4 / 1 + 1", "8 * 1 - 8 / 2", "15 / 2 * 2 - 8", "4 * 2 - 8 + 3", "3 * 6 / 1 - 16",
            "1 * 6 + 10 - 8", "4 + 12 / 4 - 2", "15 / 9 * 6 - 6", "7 * 6 - 9 * 4", "4 - 8 + 4 * 2", "5 + 10 - 5 * 2",
            "1 + 3 * 8 - 17"
        };
        #endregion
    }
}

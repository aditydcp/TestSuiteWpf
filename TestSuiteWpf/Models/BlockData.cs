using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TestSuiteWpf.App;

namespace TestSuiteWpf.Models
{
    internal class BlockData : SubjectData
    {
        public Stages Stage { get; }

        public BlockData(double accuracy, int trialsAttempted, int score, string subjectName, DateTime collectionTime, Stages stage) 
            : base(accuracy, trialsAttempted, score, subjectName, collectionTime)
        {
            Accuracy = accuracy;
            TrialsAttempted = trialsAttempted;
            Score = score;
            SubjectName = subjectName;
            CollectionTime = collectionTime;
            Stage = stage;
        }
    }
}

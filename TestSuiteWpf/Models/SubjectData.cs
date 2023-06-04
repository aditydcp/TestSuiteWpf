using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSuiteWpf.Models
{
    public class SubjectData
    {
        public string? SubjectName { get; private set; }
        public string? SubjectId { get; private set; }
        public string? GroupId { get; private set; }
        public DateTime CollectionStartTime { get; }
        public DateTime CollectionEndTime { get; private set; }
        public List<BlockData> Blocks { get; }

        public SubjectData()
        {
            CollectionStartTime = DateTime.Now;
            Blocks = new List<BlockData>();
        }

        public void SetSubjectCredentials(string name, string subjectId, string groupId)
        {
            SubjectName = name;
            SubjectId = subjectId;
            GroupId = groupId;
        }

        public void Set
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSuiteWpf.Models
{
    /// <summary>
    /// Data class containing information for individual subject, or a single collection of data, or an entire run of test.
    /// </summary>
    public class SubjectData
    {
        public string? SubjectName { get; private set; }
        public string? SubjectId { get; private set; }
        public string? GroupId { get; private set; }
        public DateTime CollectionStartTime { get; }
        public DateTime CollectionEndTime { get; set; }
        public List<BlockData> Blocks { get; }

        public SubjectData()
        {
            CollectionStartTime = DateTime.Now;
            Blocks = new List<BlockData>();
        }

        public void SaveBlockData(BlockData data)
        {
            Blocks.Add(data);
        }

        public void SetSubjectCredentials(string name, string subjectId, string groupId)
        {
            SubjectName = name;
            SubjectId = subjectId;
            GroupId = groupId;
        }

        public void ClearSubjectCredentials()
        {
            SubjectName = null;
            SubjectId = null;
            GroupId = null;
        }
    }
}

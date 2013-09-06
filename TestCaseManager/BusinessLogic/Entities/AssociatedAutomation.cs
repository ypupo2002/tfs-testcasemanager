using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCaseManagerApp
{
    public class AssociatedAutomation
    {
        public const string NONE = "None";
        public string Type { get; set; }
        public string Assembly { get; set; }
        public string TestName { get; set; }

        public AssociatedAutomation()
        {
            TestName = NONE;
            Assembly = NONE;
            Type = NONE;
        }

        public AssociatedAutomation(string testInfo)
        {
            string[] infos = testInfo.Split(',');
            TestName = infos[1];
            Assembly = infos[2];
            Type = infos[3];
        }
    }
}

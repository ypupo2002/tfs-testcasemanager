using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.TestManagement.Client;

namespace TestCaseManagerApp
{
    public class Preferences
    {
        public Uri TfsUri { get; set; }
        public string TestProjectName { get; set; }
        public ITestPlan TestPlan { get; set; }
    }
}

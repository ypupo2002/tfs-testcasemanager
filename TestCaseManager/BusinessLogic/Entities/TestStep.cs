using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.TestManagement.Client;

namespace TestCaseManagerApp
{
    public class TestStep
    {
        public bool IsShared { get; set; }
        public ITestStep ITestStep { get; set; }
        public string Title { get; set; }
        public int SharedStepId { get; set; }
        public string StepGuid { get; set; }

        public TestStep(bool isShared, ITestStep iTestStep, string title, string guid)
        {
            IsShared = isShared;
            ITestStep = iTestStep;
            Title = title;
            StepGuid = guid;
        }
        
        public TestStep(bool isShared, ITestStep iTestStep, string title, int sharedStepId, string guid)
            : this(isShared, iTestStep, title, guid)
        {
            SharedStepId = sharedStepId;
        }
    }
}

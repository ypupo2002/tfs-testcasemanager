using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.TestManagement.Client;

namespace TestCaseManagerApp
{
    public class SharedStep
    {
        public ISharedStep ISharedStep { get; set; }
        public string StepsToolTip { get; set; }

        public SharedStep(ISharedStep iSharedStep)
        {
            ISharedStep = iSharedStep;
            StepsToolTip = iSharedStep.GetInnerTestSteps().GenerateTestStepsText();
        }
    }
}

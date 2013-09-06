using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.TestManagement.Client;

namespace TestCaseManagerApp
{
    public class TestCase
    {
        public TestCase(ITestCase iTestCase, ITestSuiteBase iTestSuiteBase)
        {
            ITestCase = iTestCase;
            ITestSuiteBase = iTestSuiteBase;
        }

        public ITestCase ITestCase { get; set; }
        public ITestSuiteBase ITestSuiteBase { get; set; }
    }
}

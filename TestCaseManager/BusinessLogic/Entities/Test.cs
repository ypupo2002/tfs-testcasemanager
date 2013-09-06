using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCaseManagerApp.BusinessLogic.Entities
{
    public class Test
    {
        public string FullName { get; set; }
        public string ClassName { get; set; }
        public Guid MethodId { get; set; }

        public Test(string fullName, string className, Guid methodId)
        {
            this.FullName = fullName;
            this.ClassName = className;
            this.MethodId = methodId;
        }
    }
}

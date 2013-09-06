using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestCaseManagerApp.BusinessLogic.Entities;

namespace TestCaseManagerApp.BusinessLogic.Managers
{
    public class ProjectManager
    {
        public static MethodInfo[] GetProjectTestMethods(string assemblyFullPath)
        {
            Assembly assembly = Assembly.LoadFrom(assemblyFullPath);
            MethodInfo[] methods = null;
            
            try
            {
                methods = GetMethodsWithTestMethodAttribute(assembly, methods);
            }
            catch (ReflectionTypeLoadException ex)
            {
                StringBuilder sb = new StringBuilder();
                foreach (Exception exSub in ex.LoaderExceptions)
                {
                    sb.AppendLine(exSub.Message);
                    if (exSub is FileNotFoundException)
                    {
                        FileNotFoundException exFileNotFound = exSub as FileNotFoundException;
                        if (!string.IsNullOrEmpty(exFileNotFound.FusionLog))
                        {
                            sb.AppendLine("Fusion Log:");
                            sb.AppendLine(exFileNotFound.FusionLog);
                        }
                    }
                    sb.AppendLine();
                }
                string errorMessage = sb.ToString();
                //Display or log the error based on your application.
            }
            
            return methods;
        }

        private static MethodInfo[] GetMethodsWithTestMethodAttribute(Assembly assembly, MethodInfo[] methods)
        {
            methods = assembly.GetTypes().SelectMany(t => t.GetMethods()
                .Where(y =>
                {
                    var attributes = y.GetCustomAttributes(true).ToArray();
                    if (attributes.Length == 0)
                    {
                        return false;
                    }
                    else
                    {
                        bool result = false;
                        foreach (var cAttribute in attributes)
                        {
                            result = cAttribute.GetType().FullName.Equals(typeof(TestMethodAttribute).ToString());
                            if (result)
                            {
                                break;
                            }
                        }

                        return result;
                    }
                })
            ).ToArray();
            return methods;
        }

        public static List<Test> GetTests(string assemblyFullPath)
        {
            MethodInfo[] currentDllMethods = GetProjectTestMethods(assemblyFullPath);
            List<Test> tests = new List<Test>();
            foreach (var cM in currentDllMethods)
            {
                tests.Add(new Test(String.Format("{0}.{1}", cM.DeclaringType.FullName, cM.Name), cM.DeclaringType.Name, GenerateTestMethodId(cM)));
            }

            return tests;
        }

        public static Guid GenerateTestMethodId(MethodInfo methodInfo)
        {
            string currentNameSpace = methodInfo.DeclaringType.FullName;
            string currentTestMethodShortName = methodInfo.Name;
            string currentTestMethodFullName = String.Concat(currentNameSpace, ".", currentTestMethodShortName);
            Guid testId = UnitTestIdGenerator.GuidFromString(currentTestMethodFullName);

            return testId;
        }
    }

    public class UnitTestIdGenerator
    {
        private static HashAlgorithm s_provider = new SHA1CryptoServiceProvider();

        public static HashAlgorithm Provider
        {
            get { return s_provider; }
        }

        /// Calculates a hash of the string and copies the first 128 bits of the hash
        /// to a new Guid.
        public static Guid GuidFromString(string data)
        {
            byte[] hash = Provider.ComputeHash(System.Text.Encoding.Unicode.GetBytes(data));

            byte[] toGuid = new byte[16];
            Array.Copy(hash, toGuid, 16);

            return new Guid(toGuid);
        }
    }
}

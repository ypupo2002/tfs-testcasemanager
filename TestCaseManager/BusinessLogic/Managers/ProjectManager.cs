// <copyright file="ProjectManager.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerApp.BusinessLogic.Managers
{
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

    /// <summary>
    /// Contains helper methods for getting all methods information in specified assembly
    /// </summary>
    public class ProjectManager
    {
        /// <summary>
        /// The service provider for hash algorithm SHA1
        /// </summary>
        private static HashAlgorithm cryptoServiceProvider = new SHA1CryptoServiceProvider();

        /// <summary>
        /// Gets the test project test methods.
        /// </summary>
        /// <param name="assemblyFullPath">The assembly full path.</param>
        /// <returns>array of method info objects</returns>
        public static MethodInfo[] GetProjectTestMethods(string assemblyFullPath)
        {
            MethodInfo[] methods = null;  
            try
            {
                Assembly assembly = Assembly.LoadFrom(assemblyFullPath);
                methods = GetMethodsWithTestMethodAttribute(assembly, methods);
            }
            catch (ReflectionTypeLoadException ex)
            {
                // TODO: Log the ReflectionTypeLoadException
                StringBuilder sb = new StringBuilder();
                foreach (Exception subException in ex.LoaderExceptions)
                {
                    sb.AppendLine(subException.Message);
                    if (subException is FileNotFoundException)
                    {
                        FileNotFoundException fileNotFoundException = subException as FileNotFoundException;
                        if (!string.IsNullOrEmpty(fileNotFoundException.FusionLog))
                        {
                            sb.AppendLine("Fusion Log:");
                            sb.AppendLine(fileNotFoundException.FusionLog);
                        }
                    }
                    sb.AppendLine();
                }
                string errorMessage = sb.ToString();

                // Display or log the error based on your application.
            }
            
            return methods;
        }

        /// <summary>
        /// Gets the test objects from specific assemly.
        /// </summary>
        /// <param name="assemblyFullPath">The assembly full path.</param>
        /// <returns>list of all test objects</returns>
        public static List<Test> GetTests(string assemblyFullPath)
        {
            MethodInfo[] currentDllMethods = GetProjectTestMethods(assemblyFullPath);
            List<Test> tests = new List<Test>();
            foreach (var cM in currentDllMethods)
            {
                tests.Add(new Test(string.Format("{0}.{1}", cM.DeclaringType.FullName, cM.Name), cM.DeclaringType.Name, GenerateTestMethodId(cM)));
            }

            return tests;
        }

        /// <summary>
        /// Generates the test method unique identifier.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <returns>method unique identifier</returns>
        public static Guid GenerateTestMethodId(MethodInfo methodInfo)
        {
            string currentNameSpace = methodInfo.DeclaringType.FullName;
            string currentTestMethodShortName = methodInfo.Name;
            string currentTestMethodFullName = string.Concat(currentNameSpace, ".", currentTestMethodShortName);
            Guid testId = GuidFromString(currentTestMethodFullName);

            return testId;
        }

        /// <summary>
        /// Gets the methods with test method attribute.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="methods">The methods info array</param>
        /// <returns>filtered method infos array</returns>
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
                })).ToArray();

            return methods;
        }

        /// <summary>
        ///  Calculates a hash of the string and copies the first 128 bits of the hash to a new Guid.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>the test method guid</returns>        
        private static Guid GuidFromString(string data)
        {
            byte[] hash = cryptoServiceProvider.ComputeHash(System.Text.Encoding.Unicode.GetBytes(data));

            byte[] toGuid = new byte[16];
            Array.Copy(hash, toGuid, 16);

            return new Guid(toGuid);
        }
    }
}

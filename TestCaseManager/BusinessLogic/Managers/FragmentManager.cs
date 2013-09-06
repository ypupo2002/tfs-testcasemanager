using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCaseManagerApp
{
    public class FragmentManager
    {
        public Dictionary<string, string> Fragments { get; set; }

        public FragmentManager(string fragment)
        {
            Fragments = new Dictionary<string, string>();
            InitializeFragmentsDictionary(fragment);
        }

        private void InitializeFragmentsDictionary(string fragment)
        {
            string[] fragmentParts = fragment.Split('&');
            foreach (var currentFragment in fragmentParts)
            {
                string[] parts = currentFragment.Split('=');
                Fragments.Add(parts[0], parts[1]);
            }
        }

        public string Get(string key)
        {
            string value = String.Empty;
            try
            {
                value = Fragments[key];
            }
            catch (KeyNotFoundException ex)
            {
            }

            return value;
        }
    }
}

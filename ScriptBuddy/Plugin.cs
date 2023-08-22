using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRage.Plugins;
using System.Windows.Forms;

namespace ScriptBuddy
{
    public class Plugin : IPlugin, IDisposable
    {

        public void Dispose() { }

        public void Init(object instance)
        {
            Patches.Patch();
        }

        public void Update()
        {

        }


    }
}

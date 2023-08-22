using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VRage.Utils;

namespace ScriptBuddy
{
    public static class FileDialog
    {
        public static async Task<string> GetScriptPathAsync()
        {
            var tcs = new TaskCompletionSource<string>();

            var thread = new Thread(() =>
            {
                using (var dialog = new OpenFileDialog
                {
                    Title = "Script File",
                    Filter = "txt files (*.txt)|*.txt|cs files (*.cs)|*.cs|All files (*.*)|*.*",
                    CheckFileExists = true,
                    CheckPathExists = true,
                    ShowReadOnly = false,
                    AutoUpgradeEnabled = true
                })
                {
                    var response = dialog.ShowDialog() == DialogResult.OK ? dialog.FileName : null;
                    tcs.SetResult(response);
                }
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

            return await tcs.Task.ConfigureAwait(false);
        }
    }
}
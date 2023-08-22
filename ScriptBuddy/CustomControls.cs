using Entities.Blocks;
using Sandbox.ModAPI.Interfaces.Terminal;
using Sandbox.ModAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRage.Game.Components;
using VRage.Utils;

namespace ScriptBuddy
{
    [MySessionComponentDescriptor(MyUpdateOrder.BeforeSimulation)]

    public class CustomControls : MySessionComponentBase
    {
        private static bool controlsCreated = false;
        public override void LoadData()
        {
            MyAPIGateway.TerminalControls.CustomControlGetter -= InsertControl;
            MyAPIGateway.TerminalControls.CustomControlGetter += InsertControl;
        }
        protected override void UnloadData()
        {
            MyAPIGateway.TerminalControls.CustomControlGetter -= InsertControl;
            controlsCreated = false;
        }

        public static void InsertControl(IMyTerminalBlock block, List<IMyTerminalControl> controls)
        {

            if (controlsCreated)
                return;

            if (block is IMyProgrammableBlock)
            {
                IMyTerminalControlButton myTerminalControlButton = MyAPIGateway.TerminalControls.CreateControl<IMyTerminalControlButton, IMyProgrammableBlock>("OpenScript");
                myTerminalControlButton.Title = MyStringId.GetOrCompute("Open Script");
                myTerminalControlButton.Tooltip = MyStringId.GetOrCompute("Select a Script File.");
                myTerminalControlButton.SupportsMultipleBlocks = false;
                myTerminalControlButton.Action = new Action<IMyTerminalBlock>(Action);
                myTerminalControlButton.Enabled = alwaysTrue => true;
                myTerminalControlButton.Visible = alwaysTrue => true;
                MyAPIGateway.TerminalControls.AddControl<IMyProgrammableBlock>(myTerminalControlButton);
                controls.Add(myTerminalControlButton);
                controlsCreated = true;

            }
        }


        private async static void Action(IMyTerminalBlock block)
        {
            Patches.path = await FileDialog.GetScriptPathAsync().ConfigureAwait(false);
        }



    }
}

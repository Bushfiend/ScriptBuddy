using HarmonyLib;
using Sandbox.Game.Entities.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptBuddy
{
    public static class Patches
    {
        public static Harmony harmony = new Harmony("ScriptBuddy");

        public static string code = string.Empty;
        public static string path = string.Empty;

        public static void Patch()
        {
            harmony.Patch(AccessTools.Method("Sandbox.Game.Entities.Blocks.MyProgrammableBlock:SendRecompile"), new HarmonyMethod(typeof(Patches), "RecompilePatch"));
        }

        private static void RecompilePatch(MyProgrammableBlock __instance)
        {

            var updateProgramRequest = typeof(MyProgrammableBlock).GetMethod("SendUpdateProgramRequest", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var m_programData = (string)typeof(MyProgrammableBlock).GetField("m_programData", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(__instance);

            code = System.IO.File.ReadAllText(path);

            m_programData = code;
            updateProgramRequest.Invoke(__instance, new object[] { code });

        }



    }
}

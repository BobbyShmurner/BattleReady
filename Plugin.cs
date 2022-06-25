using BepInEx;
using BepInEx.Logging;

using HarmonyLib;

namespace BattleReady
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("ULTRAKILL.exe")]
    public class Plugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log;
        internal static Harmony harmony;
        
        private void Awake()
        {
            Logger.LogInfo($"Loading Plugin {PluginInfo.PLUGIN_GUID}...");

            Plugin.Log = base.Logger;
            Plugin.Log.LogInfo("Created Global Logger");

            harmony = new Harmony("BattleReady");

            harmony.PatchAll(typeof(RemoveCleanMusicPatch));
            harmony.PatchAll(typeof(StartWithBattlePatch));
            harmony.PatchAll(typeof(OnEnablePatch));
            harmony.PatchAll(typeof(RemoveArenaEndPatch));

            Plugin.Log.LogInfo("Applied All Patches");

            Plugin.Log.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }

        private void OnDestroy()
        {
            harmony?.UnpatchSelf();
        }
    }
}

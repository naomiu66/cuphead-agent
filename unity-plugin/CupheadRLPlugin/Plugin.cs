using BepInEx;
using BepInEx.Logging;

namespace CupheadRLPlugin.Plugin
{
    [BepInPlugin(
    "com.naomiu66.cupheadrl",
    "Cuphead RL",
    "0.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        internal static new ManualLogSource? Logger;
        
        private void Awake()
        {
            // Plugin startup logic
            Logger = base.Logger;
            Logger.LogInfo($"Плагин запущен сука!!!");
        }
    }
}
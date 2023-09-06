using HarmonyLib;
using KitchenMods;
using PreferenceSystem;
using System.Reflection;
using UnityEngine;

// Namespace should have "Kitchen" in the beginning
namespace KitchenLayoutPreviewTweaker
{
    public class Main : IModInitializer
    {
        public const string MOD_GUID = $"IcedMilo.PlateUp.{MOD_NAME}";
        public const string MOD_NAME = "Layout Preview Tweaker";
        public const string MOD_VERSION = "0.1.1";

        internal const string LAYOUT_INFO_ROTATION_ID = "layoutInfoRotation";
        internal const string LAYOUT_INFO_ROTATION_ANGLE_ID = "layoutInfoRotationAngle";

        internal static PreferenceSystemManager PrefManager;

        public Main()
        {
            Harmony harmony = new Harmony(MOD_GUID);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public void PostActivate(KitchenMods.Mod mod)
        {
            LogWarning($"{MOD_GUID} v{MOD_VERSION} in use!");
            PrefManager = new PreferenceSystemManager(MOD_GUID, MOD_NAME);
            PrefManager
                .AddLabel("Site View Rotation")
                .AddOption<bool>(
                    LAYOUT_INFO_ROTATION_ID,
                    false,
                    new bool[] { false, true },
                    new string[] { "Disabled", "Enabled" })
                .AddLabel("Fixed Angle")
                .AddOption<float>(
                    LAYOUT_INFO_ROTATION_ANGLE_ID,
                    0,
                    new float[] { 0f, 45f, 90f, 135f, 180f, 225f, 270f, 315f },
                    new string[] { "0 deg", "45 deg", "90 deg", "135 deg", "180 deg", "225 deg", "270 deg", "335 deg" })
                .AddSpacer()
                .AddSpacer();

            PrefManager.RegisterMenu(PreferenceSystemManager.MenuType.PauseMenu);
        }

        public void PreInject()
        {
        }

        public void PostInject()
        {
        }

        #region Logging
        public static void LogInfo(string _log) { Debug.Log($"[{MOD_NAME}] " + _log); }
        public static void LogWarning(string _log) { Debug.LogWarning($"[{MOD_NAME}] " + _log); }
        public static void LogError(string _log) { Debug.LogError($"[{MOD_NAME}] " + _log); }
        public static void LogInfo(object _log) { LogInfo(_log.ToString()); }
        public static void LogWarning(object _log) { LogWarning(_log.ToString()); }
        public static void LogError(object _log) { LogError(_log.ToString()); }
        #endregion
    }
}

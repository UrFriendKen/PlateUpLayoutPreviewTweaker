using HarmonyLib;
using Kitchen;
using UnityEngine;

namespace KitchenLayoutPreviewTweaker.Patches
{
    [HarmonyPatch]
    static class LocalViewRouter_Patch
    {
        [HarmonyPatch(typeof(LocalViewRouter), "GetPrefab")]
        [HarmonyPostfix]
        static void GetPrefab_Postix(ViewType view_type, ref GameObject __result)
        {
            if (view_type == ViewType.LayoutInfo && __result != null && __result.GetComponent<RotationLock>() == null)
            {
                RotationLock rotationLock = __result.AddComponent<RotationLock>();
                rotationLock.PreferenceID = Main.LAYOUT_INFO_ROTATION_ID;
                rotationLock.FixedRotation = Quaternion.Euler(0f, 43f, 0f);

                Transform layoutContainer = __result.transform.Find("Container")?.Find("Body")?.Find("Layout Container");
                if (layoutContainer != null)
                {
                    rotationLock.RotationAnimator = layoutContainer.GetComponent<Animator>();
                    rotationLock.LockedTransform = layoutContainer.Find("Container");
                }
            }
        }
    }
}

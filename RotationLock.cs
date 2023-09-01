using UnityEngine;

namespace KitchenLayoutPreviewTweaker
{
    public class RotationLock : MonoBehaviour
    {
        public Transform LockedTransform;

        public Animator RotationAnimator;

        public Quaternion FixedRotation;

        public string PreferenceID;

        private bool WasLocked = false;

        void Update()
        {
            bool shouldLock = ShouldLock() && LockedTransform != null;

            if (RotationAnimator != null && shouldLock)
            {
                RotationAnimator.enabled = false;
            }
            else if (WasLocked)
            {
                RotationAnimator.enabled = true;
            }

            if (shouldLock)
            {
                LockedTransform.localRotation = FixedRotation;
                LockedTransform.localScale = Vector3.one;
            }
            else if (WasLocked)
            {
                LockedTransform.localRotation = Quaternion.identity;
                LockedTransform.localScale = Vector3.zero;
            }
        }

        bool ShouldLock()
        {
            if (PreferenceID == null || PreferenceID == string.Empty)
                return true;
            return !Main.PrefManager?.Get<bool>(PreferenceID) ?? false;
        }
    }
}

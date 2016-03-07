using System.Collections.Generic;
using N.Package.Animation;
using UnityEngine;

namespace N.Package.Animation.Animations
{
    /// Move a target towards a fixed position, given that we're in local space
    public class MoveSingleParented : IAnimation
    {
        /// Initialized original state yet?
        bool initialized;

        /// Origin points of all game objects
        Vector3 origin;

        /// Origin rotations of all game objects
        Quaternion rotation;

        /// Target position
        Vector3 target;

        /// Target rotation
        Quaternion targetRotation;

        public MoveSingleParented(Vector3 target, Quaternion targetRotation)
        {
            this.target = target;
            this.targetRotation = targetRotation;
            initialized = false;
        }

        /// The targets for this animation
        public IAnimationTarget AnimationTarget { set; get; }

        /// The animation curve for this animation
        public IAnimationCurve AnimationCurve { set; get; }

        /// Update this animation
        /// @param step The animation step for this update.
        public void AnimationUpdate(GameObject target)
        {
            if (!initialized)
            {
                initialized = true;
                origin = target.transform.localPosition;
                rotation = target.transform.localRotation;
            }
            target.transform.localPosition = Vector3.Lerp(origin, this.target, AnimationCurve.Value);
            target.transform.localRotation = Quaternion.Lerp(rotation, targetRotation, AnimationCurve.Value);
        }
    }
}

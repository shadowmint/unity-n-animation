using System.Collections.Generic;
using N.Package.Animation;
using UnityEngine;

namespace N.Package.Animation.Animations
{
    /// Move a target towards a fixed position
    public class MoveSingle : IAnimation
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

        public MoveSingle(Vector3 target, Quaternion targetRotation)
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
                origin = target.transform.position;
                rotation = target.transform.rotation;
            }
            target.transform.position = Vector3.Lerp(origin, this.target, AnimationCurve.Value);
            target.transform.rotation = Quaternion.Lerp(rotation, targetRotation, AnimationCurve.Value);
        }
    }
}

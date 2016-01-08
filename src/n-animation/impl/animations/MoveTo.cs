using System.Collections.Generic;
using N.Package.Animation;
using UnityEngine;

namespace N.Package.Animation.Animations
{
    /// Move a target towards a fixed position
    public class MoveTo : IAnimation
    {
        /// Origin points of all game objects
        Dictionary<GameObject, Vector3> origins;

        /// Target position
        Vector3 position;

        public MoveTo(Vector3 position)
        {
            this.position = position;
            origins = new Dictionary<GameObject, Vector3>();
        }

        /// The targets for this animation
        public IAnimationTarget AnimationTarget { set; get; }

        /// The animation curve for this animation
        public IAnimationCurve AnimationCurve { set; get; }

        /// Update this animation
        /// @param step The animation step for this update.
        public void AnimationUpdate(GameObject target)
        {
            if (!origins.ContainsKey(target))
            {
                origins[target] = target.transform.position;
            }
            var origin = origins[target];
            target.transform.position = Vector3.Lerp(origin, position, AnimationCurve.Value);
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using N;

namespace N.Package.Animation
{
    /// Manages all the animations on the scene.
    public class AnimationTimerComponent : MonoBehaviour
    {
        /// The handler associated with this component
        public AnimationHandler handler;

        /// Update all animations
        public void Update()
        {
            if (handler != null)
            {
                handler.Update();
            }
        }
    }
}

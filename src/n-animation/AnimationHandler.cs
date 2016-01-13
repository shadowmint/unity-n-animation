using System.Collections.Generic;
using UnityEngine;
using N;

namespace N.Package.Animation
{
    /// Manages all the animations on the scene.
    /// Using this class will spawn an AnimationData object on the scene.
    public class AnimationHandler
    {
        /// The set of registered animations updaters
        public List<IAnimationUpdater> updaters = new List<IAnimationUpdater>();

        /// The timer we use~
        public Timer timer = new Timer();

        /// Associated game object
        private GameObject timerComponent;

        /// Create a new instance with a new timer component
        public AnimationHandler()
        {
            timerComponent = new GameObject();
            timerComponent.transform.name = "Animation Timer";
            var tc = timerComponent.AddComponent<AnimationTimerComponent>();
            tc.handler = this;
            timer.OnUpdate((ev) =>
            {
                foreach (var updater in updaters)
                {
                    updater.Update(timer.Step());
                }
            });
        }

        /// Add a updater
        public void Add(IAnimationUpdater updater)
        {
            if (!updaters.Contains(updater))
            {
                updaters.Add(updater);
            }
        }

        /// Remove a updater
        public void Remove(IAnimationUpdater updater)
        {
            if (updaters.Contains(updater))
            {
                updaters.Remove(updater);
            }
        }

        /// Update all animations
        public void Update()
        {
            timer.Step();
        }

        /// Get the default instance of the global handler
        private static AnimationHandler instance = null;
        public static AnimationHandler Default
        {
            get
            {
                if (instance == null)
                {
                    instance = new AnimationHandler();
                }
                return instance;
            }
        }

        /// Reset the global instance
        public static void Reset()
        {
            if (instance != null)
            {
                AnimationManager.Reset();
                foreach (var updater in AnimationHandler.instance.updaters)
                {
                    updater.Invalidate();
                }
#if UNITY_EDITOR
                GameObject.DestroyImmediate(instance.timerComponent);
#else
                GameObject.Destroy(instance.timerComponent);
#endif
                instance = null;
            }
        }
    }
}

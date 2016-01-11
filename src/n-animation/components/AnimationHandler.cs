using System.Collections.Generic;
using UnityEngine;
using N;

namespace N.Package.Animation
{
    /// Manages all the animations on the scene.
    /// Using this class will spawn an AnimationData object on the scene.
    public class AnimationHandler : MonoBehaviour
    {
        /// The set of registered animations updaters
        public List<IAnimationUpdater> updaters = new List<IAnimationUpdater>();

        /// The timer we use~
        public Timer timer = new Timer();

        /// Setup the update loop
        public AnimationHandler()
        {
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
        public void Update() {
            timer.Step();
        }

        /// Get the instance of this class on the scene
        public static AnimationHandler Get()
        {
            var rtn = Scene.First<AnimationHandler>();
            if (rtn == null)
            {
                rtn = new GameObject();
                rtn.transform.name = "(auto) Animation Handler";
                rtn.AddComponent<AnimationHandler>();
            }
            return rtn.GetComponent<AnimationHandler>();
        }

        /// Register a updater globally
        public static void Register(IAnimationUpdater updater)
        {
            AnimationHandler.Get().Add(updater);
        }
    }
}

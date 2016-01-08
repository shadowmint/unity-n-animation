using System.Collections.Generic;
using UnityEngine;
using N;

namespace N.Package.Animation
{
    /// Manages all the animations on the scene.
    /// Using this class will spawn an AnimationData object on the scene.
    public class AnimationHandler : MonoBehaviour
    {
        /// The set of registered animations managers
        public List<IAnimationManager> managers = new List<IAnimationManager>();

        /// The timer we use~
        public Timer timer = new Timer();

        /// Setup the update loop
        public AnimationHandler()
        {
            timer.OnUpdate((ev) =>
            {
                foreach (var manager in managers)
                {
                    manager.Update(timer.Step());
                }
            });
        }

        /// Add a manager
        public void Add(IAnimationManager manager)
        {
            if (!managers.Contains(manager))
            {
                managers.Add(manager);
            }
        }

        /// Remove a manager
        public void Remove(IAnimationManager manager)
        {
            if (managers.Contains(manager))
            {
                managers.Remove(manager);
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

        /// Register a manager globally
        public static void Register(IAnimationManager manager)
        {
            AnimationHandler.Get().Add(manager);
        }
    }
}

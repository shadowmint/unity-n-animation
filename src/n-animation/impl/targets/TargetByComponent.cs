using UnityEngine;
using System.Collections.Generic;
using N.Package.Animation;

namespace N.Package.Animation.Targets
{
    /// Notice that the list of targets is built the first time it runs and cached
    public class TargetByComponent<TComponent> : IAnimationTarget where TComponent : Component
    {
        private GameObject[] targets;

        public TargetByComponent()
        {
            targets = Scene.Find<TComponent>().ToArray();
        }

        public IEnumerable<GameObject> GameObjects()
        {
            for (var i = 0; i < targets.Length; ++i)
            {
                yield return targets[i];
            }
        }
    }
}

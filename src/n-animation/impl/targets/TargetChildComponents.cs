using UnityEngine;
using System.Collections.Generic;
using N.Package.Animation;

namespace N.Package.Animation.Targets
{
    /// Notice that the list of targets is built the first time it runs and cached
    public class TargetChildComponents<TComponent> : IAnimationTarget where TComponent : Component
    {
        private TComponent[] targets;

        public TargetChildComponents(GameObject root)
        {
            targets = root.GetComponentsInChildren<TComponent>();
        }

        public IEnumerable<GameObject> GameObjects()
        {
            for (var i = 0; i < targets.Length; ++i)
            {
                yield return targets[i].gameObject;
            }
        }
    }
}

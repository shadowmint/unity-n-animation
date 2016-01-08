using UnityEngine;
using System.Collections.Generic;
using N.Package.Animation;

namespace N.Package.Animation.Targets
{
    public class TargetSingle : IAnimationTarget
    {
        private GameObject target;

        public TargetSingle(GameObject target)
        {
            this.target = target;
        }

        public IEnumerable<GameObject> GameObjects()
        {
            yield return target;
        }
    }
}

using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace N.Package.Animation.Targets
{
    public class TargetGroup : IAnimationTarget
    {
        private GameObject[] targets;

        public TargetGroup(IEnumerable<GameObject> targets)
        {
            this.targets = targets.ToList().ToArray();
        }

        public TargetGroup(GameObject[] targets)
        {
            this.targets = targets;
        }

        public IEnumerable<GameObject> GameObjects()
        {
            foreach (var target in targets)
            {
                yield return target;
            }
        }
    }
}

using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace N.Package.Animation.Targets
{
  public class TargetGroup : IAnimationTarget
  {
    private readonly GameObject[] targets;

    public TargetGroup(IEnumerable<GameObject> targets)
    {
      this.targets = targets.ToArray();
    }

    public TargetGroup(GameObject[] targets)
    {
      this.targets = targets;
    }

    public IEnumerable<GameObject> GameObjects()
    {
      return targets;
    }
  }
}
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace N.Package.Animation.Targets
{
  public class TargetGroup : IAnimationTarget
  {
    private readonly GameObject[] _targets;

    public TargetGroup(IEnumerable<GameObject> targets)
    {
      _targets = targets.ToArray();
    }

    public TargetGroup(GameObject[] targets)
    {
      _targets = targets;
    }

    public IEnumerable<GameObject> GameObjects()
    {
      return _targets;
    }
  }
}
using UnityEngine;
using System.Collections.Generic;
using N.Package.Animation;

namespace N.Package.Animation.Targets
{
  public class TargetSingle : IAnimationTarget
  {
    private readonly GameObject _target;

    public TargetSingle(GameObject target)
    {
      _target = target;
    }

    public IEnumerable<GameObject> GameObjects()
    {
      yield return _target;
    }
  }
}
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using N.Package.Animation;

namespace N.Package.Animation.Targets
{
  /// Notice that the list of targets is built the first time it runs and cached
  public class TargetChildComponents<TComponent> : IAnimationTarget where TComponent : Component
  {
    private readonly TComponent[] _targets;

    public TargetChildComponents(GameObject root)
    {
      _targets = root.GetComponentsInChildren<TComponent>();
    }

    public IEnumerable<GameObject> GameObjects()
    {
      return _targets.Select(t => t.gameObject);
    }
  }
}
using UnityEngine;
using System.Collections.Generic;
using N.Package.Core;

namespace N.Package.Animation.Targets
{
  /// Notice that the list of targets is built the first time it runs and cached
  public class TargetByComponent<TComponent> : IAnimationTarget where TComponent : Component
  {
    private readonly GameObject[] _targets;

    public TargetByComponent()
    {
      _targets = Scene.Find<TComponent>().ToArray();
    }

    public IEnumerable<GameObject> GameObjects()
    {
      return _targets;
    }
  }
}
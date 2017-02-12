using System.Collections.Generic;
using UnityEngine;

namespace N.Package.Animation
{
  /// A set of target game objects for an animation.
  public interface IAnimationTarget
  {
    /// Yield a set of child objects based on a parent
    /// Sometimes the targets may depend on the state of the animation.
    /// @param animation The animation this set of targets is for.
    IEnumerable<GameObject> GameObjects();
  }
}
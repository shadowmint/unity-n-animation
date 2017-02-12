using System.Collections.Generic;
using N.Package.Animation;
using UnityEngine;

namespace N.Package.Animation.Animations
{
  /// Move a target towards a fixed position
  public class MoveTo : AnimationBase, IAnimation
  {

    /// Origin points of all game objects
    private readonly Dictionary<GameObject, Vector3> _origins;

    /// Target position
    private readonly Vector3 _position;

    public MoveTo(Vector3 position)
    {
      _position = position;
      _origins = new Dictionary<GameObject, Vector3>();
    }

    /// The targets for this animation
    public IAnimationTarget AnimationTarget { set; get; }

    /// The animation curve for this animation
    public IAnimationCurve AnimationCurve { set; get; }

    /// Update this animation
    /// @param step The animation step for this update.
    public void AnimationUpdate(GameObject target)
    {
      if (!_origins.ContainsKey(target))
      {
        _origins[target] = target.transform.position;
      }
      var origin = _origins[target];
      target.transform.position = Vector3.Lerp(origin, _position, AnimationCurve.Value);
    }
  }
}
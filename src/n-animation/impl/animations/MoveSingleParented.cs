using System.Collections.Generic;
using N.Package.Animation;
using UnityEngine;

namespace N.Package.Animation.Animations
{
  /// Move a target towards a fixed position, given that we're in local space
  public class MoveSingleParented : AnimationBase, IAnimation
  {
    /// Initialized original state yet?
    private bool _initialized;

    /// Origin points of all game objects
    private Vector3 _origin;

    /// Origin rotations of all game objects
    private Quaternion _rotation;

    /// Target position
    private readonly Vector3 _target;

    /// Target rotation
    private readonly Quaternion _targetRotation;

    public MoveSingleParented(Vector3 target, Quaternion targetRotation)
    {
      _target = target;
      _targetRotation = targetRotation;
      _initialized = false;
    }

    /// The targets for this animation
    public IAnimationTarget AnimationTarget { set; get; }

    /// The animation curve for this animation
    public IAnimationCurve AnimationCurve { set; get; }

    /// Update this animation
    /// @param step The animation step for this update.
    public void AnimationUpdate(GameObject target)
    {
      if (!_initialized)
      {
        _initialized = true;
        _origin = target.transform.localPosition;
        _rotation = target.transform.localRotation;
      }
      target.transform.localPosition = Vector3.Lerp(_origin, this._target, AnimationCurve.Value);
      target.transform.localRotation = Quaternion.Lerp(_rotation, _targetRotation, AnimationCurve.Value);
    }
  }
}
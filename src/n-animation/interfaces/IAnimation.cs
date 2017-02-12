using N.Package.Events;
using UnityEngine;

namespace N.Package.Animation
{
  /// This is the base interface required for an animation component.
  public interface IAnimation
  {
    /// The event handler for this specific animation
    EventHandler EventHandler { get; }

    /// The targets for this animation
    IAnimationTarget AnimationTarget { set; get; }

    /// The animation curve for this animation
    IAnimationCurve AnimationCurve { set; get; }

    /// Update this animation
    /// @param step The animation step for this update.
    void AnimationUpdate(GameObject target);
  }
}
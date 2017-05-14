using UnityEngine;

namespace N.Package.Animation
{
  /// This is the base interface required for an animation component.
  public interface IAnimation
  {
    /// The targets for this animation
    IAnimationTarget AnimationTarget { set; get; }

    /// The animation curve for this animation
    IAnimationCurve AnimationCurve { set; get; }

    /// Update this animation
    /// @param step The animation step for this update.
    void AnimationUpdate(GameObject target);
  }

  /// IAnimation helper methods
  public static class IAnimationExtensions
  {
    /// Update the animations
    public static void AnimationUpdate(this IAnimation self, float delta)
    {
      if (self == null) return;

      self.AnimationCurve.Delta = delta;
      self.AnimationCurve.Elapsed += delta;
      foreach (var target in self.AnimationTarget.GameObjects())
      {
        self.AnimationUpdate(target);
      }
    }
  }
}
namespace N.Package.Animation
{
  /// IAnimation helper methods
  public static class IAnimationExtensions
  {
    /// Update the animations
    public static void AnimationUpdate(this IAnimation self, float delta)
    {
      if (self != null)
      {
        self.AnimationCurve.Delta = delta;
        self.AnimationCurve.Elapsed += delta;
        foreach (var target in self.AnimationTarget.GameObjects())
        {
          self.AnimationUpdate(target);
        }
      }
    }
  }
}
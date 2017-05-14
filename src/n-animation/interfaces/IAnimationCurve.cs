namespace N.Package.Animation
{
  /// Implement this to generate an animation curve
  public interface IAnimationCurve
  {
    /// The total length of time remaining on this animation curve.
    /// If this value is -ve then the animation curve should never end.
    float Remaining { get; }

    /// The total time elasped in this animation curve
    float Elapsed { get; set; }

    /// The last delta
    float Delta { get; set; }

    /// The current value of the curve, given Elapsed and Remaining.
    float Value { get; }

    /// Return turn when the animation is complete
    bool Complete { get; }

    /// Halt the animation early
    void Halt();
  }
}
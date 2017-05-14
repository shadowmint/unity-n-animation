using N.Package.Animation;

namespace N.Package.Animation.Curves
{
  public class Linear : CurveBase, IAnimationCurve
  {
    private float _duration;

    public Linear(float duration)
    {
      _duration = duration;
      if (duration <= 0f)
      {
        throw new AnimationException(AnimationErrors.InvalidDuration);
      }
    }

    protected override float MaxElapsedTime
    {
      get { return _duration; }
    }

    /// The total length of time remaining on this animation curve.
    /// If this value is -ve then the animation curve should never end.
    public float Remaining
    {
      get { return _duration - Elapsed; }
    }

    /// The current value of the curve, given Elapsed and Remaining.
    public float Value
    {
      get { return Elapsed / _duration; }
    }

    /// Finished yet?
    protected override bool CurveComplete
    {
      get { return Value >= 1.0f; }
    }
  }
}
using N.Package.Animation;

namespace N.Package.Animation.Curves
{
  /// Helpful base class for animation curves
  public abstract class CurveBase
  {
    /// Total elapsed time
    private float _elapsed;

    /// Manually halted?
    private bool _halted;

    /// The last delta
    public float Delta { get; set; }

    protected CurveBase()
    {
      _elapsed = 0f;
    }

    /// Implement this to return a cap on the elapsed time
    protected abstract float MaxElapsedTime { get; }

    /// Implement this to return when the curve is complete
    protected abstract bool CurveComplete { get; }

    /// Call this function when the curve is complete
    public void Halt()
    {
      _halted = true;
    }

    /// The total time elasped in this animation curve
    public float Elapsed
    {
      get { return _elapsed; }
      set
      {
        _elapsed = value;
        if ((MaxElapsedTime >= 0f) && (_elapsed > MaxElapsedTime))
        {
          _elapsed = MaxElapsedTime;
        }
      }
    }

    /// Finished yet?
    public bool Complete
    {
      get { return _halted || CurveComplete; }
    }
  }
}
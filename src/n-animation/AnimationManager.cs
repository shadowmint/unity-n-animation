using N.Package.Animation.Animations;

namespace N.Package.Animation
{
  /// Default animation manager
  public sealed class AnimationManager : AnimationManagerBase<Streams>
  {
    /// Singleton
    private static AnimationManager _instance;

    /// Streams for this manager
    protected override AnimationStreams<Streams> TStreams
    {
      get { return _streams; }
    }

    private readonly AnimationStreams<Streams> _streams = new AnimationStreams<Streams>();

    /// Get access to the global instance
    public static AnimationManager Default
    {
      get { return _instance ?? (_instance = new AnimationManager()); }
    }

    /// Destroy default instance, mainly for tests and moving between scenes
    public static void Reset()
    {
      if (_instance != null)
      {
        _instance.Invalidate();
      }
      _instance = null;
    }
  }
}
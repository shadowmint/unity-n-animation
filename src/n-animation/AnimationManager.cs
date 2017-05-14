using System;
using N.Package.Animation.Animations;
using N.Package.Animation.Targets;
using UnityEngine;
using EventHandler = N.Package.Events.EventHandler;

namespace N.Package.Animation
{
  /// The base class for any animation manager
  public abstract class AnimationManagerBase<TStream> : IAnimationManager<TStream>, IAnimationUpdater
  {
    /// Event interface
    public EventHandler Events
    {
      get { return _events; }
    }

    private readonly EventHandler _events;

    /// Is this animation manager discarded and invalid?
    private bool _valid;

    /// Register self on the animation input handler and set events
    protected AnimationManagerBase(EventHandler events)
    {
      _valid = true;
      _events = events;
      AnimationHandler.Default.Add(this);
    }

    /// Register self on the animation input handler
    protected AnimationManagerBase()
    {
      _valid = true;
      _events = new EventHandler();
      AnimationHandler.Default.Add(this);
    }

    /// Add an animation to a specific stream
    public void Add(TStream stream, IAnimation animation, IAnimationCurve curve, GameObject target)
    {
      Validate();
      animation.AnimationCurve = curve;
      animation.AnimationTarget = new TargetSingle(target);
      Streams.Add(stream, animation);
    }

    /// Add an animation to a specific stream
    public void Add(TStream stream, IAnimation animation, IAnimationCurve curve, IAnimationTarget target)
    {
      Validate();
      animation.AnimationCurve = curve;
      animation.AnimationTarget = target;
      Streams.Add(stream, animation);
    }

    /// Clear and destroy an animation stream
    public void Clear(TStream stream)
    {
      Validate();
      Streams.Clear(stream);
    }

    /// Clear all streams
    public void ClearAll()
    {
      Validate();
      Streams.ClearAll();
    }

    /// Update each frame
    public void Update(float delta)
    {
      Validate();
      Streams.Update(delta, this);
    }

    /// Configure a stream
    public void Configure(TStream stream, AnimationStreamType type, int slots)
    {
      Validate();
      Streams.Configure(stream, type, slots);
    }

    /// Bind a standard event handler
    public void AddEventHandler(Action<AnimationCompleteEvent> handler)
    {
      Validate();
      _events.AddEventHandler(handler);
    }

    /// Mark this manager as invalid; it can no longer be used
    private void Validate()
    {
      if (!_valid)
      {
        throw new AnimationException(AnimationErrors.InvalidManager);
      }
    }

    /// Mark this manager as invalid for future use
    public void Invalidate()
    {
      _valid = false;
    }

    /// Return the collections of animation streams
    public AnimationStreams<TStream> Streams
    {
      get
      {
        Validate();
        return RawStreams;
      }
    }

    /// Return the collections of animation streams
    protected abstract AnimationStreams<TStream> RawStreams { get; }
  }

  /// Default animation manager
  public sealed class AnimationManager : AnimationManagerBase<Streams>
  {
    /// Singleton
    private static AnimationManager _instance;

    /// Streams for this manager
    protected override AnimationStreams<Streams> RawStreams
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
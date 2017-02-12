using System.Collections.Generic;

namespace N.Package.Animation
{
  /// A single animation stream
  public class AnimationStreams<TStream>
  {
    /// Default type for streams
    public AnimationStreamType DefaultStreamType = AnimationStreamType.Defer;

    /// Default type for stream length
    public int DefaultStreamSlots = 10;

    /// Set of held animations by stream
    private readonly Dictionary<TStream, AnimationStream> _streams = new Dictionary<TStream, AnimationStream>();

    /// Configure an animation stream
    public void Configure(TStream stream, AnimationStreamType type, int slots)
    {
      if (!_streams.ContainsKey(stream))
      {
        _streams[stream] = new AnimationStream(type, slots);
      }
      else
      {
        throw new AnimationException(AnimationErrors.StreamAlreadyConfigured);
      }
    }

    /// Clear a stream
    public void Clear(TStream stream)
    {
      if (_streams.ContainsKey(stream))
      {
        _streams.Remove(stream);
      }
    }

    /// Clear all streams
    public void ClearAll()
    {
      _streams.Clear();
    }

    /// Add an animation to this stream
    public void Add(TStream stream, IAnimation animation, bool createDefaultIfMissing = true)
    {
      if (!_streams.ContainsKey(stream))
      {
        if (createDefaultIfMissing)
        {
          AddDefaultStream(stream);
        }
        else
        {
          throw new AnimationException(AnimationErrors.InvalidStream);
        }
      }
      _streams[stream].Add(animation);
    }

    /// Return true if any active animations in a stream
    public bool Active(TStream stream)
    {
      if (_streams.ContainsKey(stream))
      {
        return _streams[stream].Active > 0;
      }
      throw new AnimationException(AnimationErrors.InvalidStream);
    }

    /// Return a count of active animations in this stream
    public int Count(TStream stream)
    {
      if (_streams.ContainsKey(stream))
      {
        return _streams[stream].Active;
      }
      throw new AnimationException(AnimationErrors.InvalidStream);
    }

    /// Return true if a stream is completely busy and will immediately process an animation
    public bool Busy(TStream stream)
    {
      if (_streams.ContainsKey(stream))
      {
        return _streams[stream].Busy;
      }
      throw new AnimationException(AnimationErrors.InvalidStream);
    }

    /// Update all held animations
    public void Update(float delta, IAnimationManager<TStream> manager)
    {
      foreach (var entry in _streams)
      {
        entry.Value.Update(delta, manager);
      }
    }

    /// Configure and add a stream with default values
    private void AddDefaultStream(TStream stream)
    {
      var sp = new AnimationStream(DefaultStreamType, DefaultStreamSlots);
      _streams[stream] = sp;
    }
  }
}
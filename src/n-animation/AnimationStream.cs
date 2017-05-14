using N.Package.Animation.Animations;
using System.Collections.Generic;

namespace N.Package.Animation
{
  /// Types of animation streams
  public enum AnimationStreamType
  {
    Defer, // Defer requests until complete
    Reject // Reject requests if a stream is busy
  }

  /// A single animation stream group
  public class AnimationStream
  {
    /// The type of this animation stream
    public AnimationStreamType Type;

    /// The set of animations in this stream
    public IAnimation[] Slots;

    /// If this is a defer type, the list of deferred animations
    public Queue<IAnimation> Deferred;

    /// Number of animations in use
    public int Active;

    /// Is this stream completely busy?
    public bool Busy
    {
      get { return Active >= Slots.Length; }
    }

    /// Create a new stream with a behaviour and a fixed slot size
    public AnimationStream(AnimationStreamType type, int slots)
    {
      Active = 0;
      Type = type;
      Slots = new IAnimation[slots];
      if (Type == AnimationStreamType.Defer)
      {
        Deferred = new Queue<IAnimation>();
      }
    }

    /// Try to add a new animation and either accept or reject it
    public bool Add(IAnimation animation)
    {
      var rtn = true;
      var slot = NextSlot();
      if (slot >= 0)
      {
        Slots[slot] = animation;
        Active += 1;
      }
      else switch (Type)
      {
        case AnimationStreamType.Defer:
          Deferred.Enqueue(animation);
          break;
        case AnimationStreamType.Reject:
          rtn = false;
          break;
      }
      return rtn;
    }

    /// Update this stream
    public void Update<TStream>(float delta, IAnimationManager<TStream> manager)
    {
      UpdateAnimations(delta);
      RemoveCompleted(manager);
      ProcessDeferred();
    }

    /// Find the next free slot
    private int NextSlot()
    {
      if (Busy)
      {
        return -1;
      }
      for (var i = 0; i < Slots.Length; ++i)
      {
        if (Slots[i] == null)
        {
          return i;
        }
      }
      return -1;
    }

    /// Update all animations
    private void UpdateAnimations(float delta)
    {
      var count = 0;
      for (var i = 0; (count < Active) && (i < Slots.Length); ++i)
      {
        if (Slots[i] == null) continue;
        Slots[i].AnimationUpdate(delta);
        count += 1;
      }
    }

    /// Remove completed animations from this stream
    private void RemoveCompleted<TStream>(IAnimationManager<TStream> manager)
    {
      for (var i = 0; i < Slots.Length; ++i)
      {
        if (Slots[i] == null) continue;
        if (!Slots[i].AnimationCurve.Complete) continue;
        var ep = new AnimationCompleteEvent() {Animation = Slots[i]};
        Slots[i] = null;
        Active -= 1;
        if (manager != null) // Eg. in tests.
        {
          manager.Events.Trigger(ep);
        }
      }
    }

    /// Check if we have any deferred items
    private bool AnyDeferredItems()
    {
      var rtn = false;
      if (Deferred != null)
      {
        rtn = Deferred.Count > 0;
      }
      return rtn;
    }

    /// Process any deferred values
    private void ProcessDeferred()
    {
      while ((!Busy) && (AnyDeferredItems()))
      {
        var instance = Deferred.Dequeue();
        if (!instance.AnimationCurve.Complete)
        {
          Add(instance);
        }
      }
    }
  }
}
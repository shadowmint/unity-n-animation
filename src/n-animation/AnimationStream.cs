using N.Package.Animation.Animations;
using System.Collections.Generic;

namespace N.Package.Animation
{
    /// Types of animation streams
    public enum AnimationStreamType
    {
        DEFER, // Defer requests until complete
        REJECT // Reject requests if a stream is busy
    }

    /// A single animation stream group
    public class AnimationStream
    {
        /// The type of this animation stream
        public AnimationStreamType type;

        /// The set of animations in this stream
        public IAnimation[] slots;

        /// If this is a defer type, the list of deferred animations
        public Queue<IAnimation> deferred;

        /// Number of animations in use
        public int active;

        /// Is this stream completely busy?
        public bool Busy { get { return active >= slots.Length; } }

        /// Create a new stream with a behaviour and a fixed slot size
        public AnimationStream(AnimationStreamType type, int slots)
        {
            this.active = 0;
            this.type = type;
            this.slots = new IAnimation[slots];
            if (this.type == AnimationStreamType.DEFER)
            {
                deferred = new Queue<IAnimation>();
            }
        }

        /// Try to add a new animation and either accept or reject it
        public bool Add(IAnimation animation)
        {
            var rtn = true;
            var slot = NextSlot();
            if (slot >= 0)
            {
                slots[slot] = animation;
                active += 1;
            }
            else if (this.type == AnimationStreamType.DEFER)
            {
                deferred.Enqueue(animation);
            }
            else if (this.type == AnimationStreamType.REJECT)
            {
                rtn = false;
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
            for (var i = 0; i < slots.Length; ++i)
            {
                if (slots[i] == null)
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
            for (var i = 0; (count < active) && (i < slots.Length); ++i)
            {
                if (slots[i] != null)
                {
                    slots[i].AnimationUpdate(delta);
                    count += 1;
                }
            }
        }

        /// Remove completed animations from this stream
        private void RemoveCompleted<TStream>(IAnimationManager<TStream> manager)
        {
            for (var i = 0; i < slots.Length; ++i)
            {
                if (slots[i] != null)
                {
                    if (slots[i].AnimationCurve.Complete)
                    {
                        var ep = new AnimationCompleteEvent() { animation = slots[i] };
                        slots[i] = null;
                        active -= 1;
                        if (manager != null)  // Eg. in tests.
                        {
                            manager.Events.Trigger(ep);
                        }
                    }
                }
            }
        }

        /// Check if we have any deferred items
        private bool AnyDeferredItems()
        {
            bool rtn = false;
            if (deferred != null)
            {
                rtn = deferred.Count > 0;
            }
            return rtn;
        }

        /// Process any deferred values
        private void ProcessDeferred()
        {
            while ((!Busy) && (AnyDeferredItems()))
            {
                var instance = deferred.Dequeue();
                if (!instance.AnimationCurve.Complete)
                {
                    Add(instance);
                }
            }
        }
    }
}

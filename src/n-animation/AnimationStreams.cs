using N.Package.Animation.Animations;
using System.Collections.Generic;

namespace N.Package.Animation
{
    /// A single animation stream
    public class AnimationStreams<TStream>
    {
        /// Default type for streams
        public AnimationStreamType defaultStreamType = AnimationStreamType.DEFER;

        /// Default type for stream length
        public int defaultStreamSlots = 10;

        /// Set of held animations by stream
        private Dictionary<TStream, AnimationStream> streams = new Dictionary<TStream, AnimationStream>();

        /// Configure an animation stream
        public void Configure(TStream stream, AnimationStreamType type, int slots)
        {
            if (!streams.ContainsKey(stream))
            {
                streams[stream] = new AnimationStream(type, slots);
            }
            else
            {
                throw new AnimationException(AnimationErrors.STREAM_ALREADY_CONFIGURED);
            }
        }

        /// Clear a stream
        public void Clear(TStream stream)
        {
            if (streams.ContainsKey(stream))
            {
                streams.Remove(stream);
            }
        }

        /// Clear all streams
        public void ClearAll()
        {
            streams.Clear();
        }

        /// Add an animation to this stream
        public void Add(TStream stream, IAnimation animation, bool createDefaultIfMissing = true)
        {
            if (!streams.ContainsKey(stream))
            {
                if (createDefaultIfMissing)
                {
                    AddDefaultStream(stream);
                }
                else
                {
                    throw new AnimationException(AnimationErrors.INVALID_STREAM);
                }
            }
            streams[stream].Add(animation);
        }

        /// Return true if any active animations in a stream
        public bool Active(TStream stream)
        {
            if (streams.ContainsKey(stream))
            {
                return streams[stream].active > 0;
            }
            throw new AnimationException(AnimationErrors.INVALID_STREAM);
        }

        /// Return a count of active animations in this stream
        public int Count(TStream stream)
        {
            if (streams.ContainsKey(stream)) { return streams[stream].active; }
            throw new AnimationException(AnimationErrors.INVALID_STREAM);
        }

        /// Return true if a stream is completely busy and will immediately process an animation
        public bool Busy(TStream stream)
        {
            if (streams.ContainsKey(stream)) { return streams[stream].Busy; }
            throw new AnimationException(AnimationErrors.INVALID_STREAM);
        }

        /// Update all held animations
        public void Update(float delta, IAnimationManager<TStream> manager)
        {
            foreach (KeyValuePair<TStream, AnimationStream> entry in streams)
            {
                entry.Value.Update(delta, manager);
            }
        }

        /// Configure and add a stream with default values
        private void AddDefaultStream(TStream stream)
        {
            var sp = new AnimationStream(defaultStreamType, defaultStreamSlots);
            streams[stream] = sp;
        }
    }
}

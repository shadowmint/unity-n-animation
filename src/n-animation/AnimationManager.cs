using N.Package.Animation.Animations;
using N.Package.Animation.Targets;
using UnityEngine;

namespace N.Package.Animation
{
    /// The base class for any animation manager
    public abstract class AnimationManagerBase<TStream> : IAnimationManager<TStream>, IAnimationUpdater
    {
        /// Event interface
        public N.Events Events { get { return events; } }
        private N.Events events = new Events();

        /// Register self on the animation input handler
        protected AnimationManagerBase()
        {
            AnimationHandler.Register(this);
        }

        /// Add an animation to a specific stream
        public void Add(TStream stream, IAnimation animation, IAnimationCurve curve, GameObject target)
        {
            animation.AnimationCurve = curve;
            animation.AnimationTarget = new TargetSingle(target);
            Streams.Add(stream, animation);
        }

        /// Add an animation to a specific stream
        public void Add(TStream stream, IAnimation animation, IAnimationCurve curve, IAnimationTarget target)
        {
            animation.AnimationCurve = curve;
            animation.AnimationTarget = target;
            Streams.Add(stream, animation);
        }

        /// Clear and destroy an animation stream
        public void Clear(TStream stream)
        {
            Streams.Clear(stream);
        }

        /// Clear all streams
        public void ClearAll()
        {
            Streams.ClearAll();
        }

        /// Update each frame
        public void Update(float delta)
        {
            Streams.Update(delta, this);
        }

        /// Configure a stream
        public void Configure(TStream stream, AnimationStreamType type, int slots)
        {
            Streams.Configure(stream, type, slots);
        }

        /// Bind a standard event handler
        public void AddEventListener(EventHandler handler)
        {
            events += handler;
        }

        /// Return the collections of animation streams
        public abstract AnimationStreams<TStream> Streams { get; }
    }

    /// Default animation manager
    public sealed class AnimationManager : AnimationManagerBase<Streams>
    {
        /// Singleton
        private static AnimationManager instance = null;

        /// Streams for this manager
        public override AnimationStreams<Streams> Streams { get { return streams; } }
        private AnimationStreams<Streams> streams = new AnimationStreams<Streams>();

        /// Get access to the global instance
        public static AnimationManager Default
        {
            get
            {
                if (instance == null)
                {
                    instance = new AnimationManager();
                }
                return instance;
            }
        }

        /// Destroy default instance, mainly for tests and moving between scenes
        public static void Reset()
        {
            instance = null;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace N.Package.Animation
{
    /// A generic animation manager interface
    public interface IAnimationUpdater
    {
        /// Update animations
        void Update(float delta);
    }

    /// A generic animation manager interface
    public interface IAnimationManager<TStream>
    {
        /// Event interface
        N.Events Events { get; }

        /// Return the streams for this manager
        AnimationStreams<TStream> Streams { get; }
    }
}

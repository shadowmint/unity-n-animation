using System.Collections.Generic;
using UnityEngine;

namespace N.Package.Animation
{
    /// A generic animation manager interface
    public interface IAnimationUpdater
    {
        /// Update animations
        void Update(float delta);

        /// Mark the updater as invalid
        void Invalidate();
    }

    /// A generic animation manager interface
    public interface IAnimationManager<TStream>
    {
        /// Event interface
        N.Package.Events.Events Events { get; }
    }
}

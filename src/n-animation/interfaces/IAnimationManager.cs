using System.Collections.Generic;
using UnityEngine;

namespace N.Package.Animation
{
    /// A generic animation manager interface
    public interface IAnimationManager
    {
        /// Event interface
        N.Events Events { get; }

        /// Update animations
        void Update(float delta);
    }
}

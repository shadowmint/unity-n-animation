using System;

namespace N.Package.Animation
{
    /// Error codes
    public enum AnimationErrors
    {
        /// Some kind of error happened
        INTERNAL_ERROR,

        /// An attempt to use an invalid or missing stream was made
        INVALID_STREAM,

        /// Invalid duration for an operation
        INVALID_DURATION,

        /// An attempt to configure an existing stream was made
        STREAM_ALREADY_CONFIGURED
    }

    /// Custom Exception type
    public class AnimationException : Exception
    {
        public AnimationErrors code = AnimationErrors.INTERNAL_ERROR;

        public AnimationException() : base()
        {
        }

        public AnimationException(AnimationErrors code) : base()
        {
            this.code = code;
        }

        public AnimationException(string msg) : base (msg)
        {
        }

        public AnimationException(string msg, Exception inner) : base (msg, inner)
        {
        }
    }
}

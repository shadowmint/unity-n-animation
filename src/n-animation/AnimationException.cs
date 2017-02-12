using System;

namespace N.Package.Animation
{
  /// Error codes
  public enum AnimationErrors
  {
    /// Some kind of error happened
    InternalError,

    /// An attempt to use an invalid or missing stream was made
    InvalidStream,

    /// Invalid duration for an operation
    InvalidDuration,

    /// An attempt to configure an existing stream was made
    StreamAlreadyConfigured,

    /// An attempt to use an invalid animation manager
    InvalidManager
  }

  /// Custom Exception type
  public class AnimationException : Exception
  {
    public AnimationErrors Code = AnimationErrors.InternalError;

    public AnimationException() : base()
    {
    }

    public AnimationException(AnimationErrors code) : base()
    {
      Code = code;
    }

    public AnimationException(string msg) : base(msg)
    {
    }

    public AnimationException(string msg, Exception inner) : base(msg, inner)
    {
    }
  }
}
using N.Package.Events;

namespace N.Package.Animation {

    /// Event for an animation which has completed
    public class AnimationCompleteEvent : IEvent
    {
        public IEventApi Api { get; set; }
        public IAnimation animation;
    }
}

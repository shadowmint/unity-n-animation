using N.Package.Events;

namespace N.Package.Animation.Animations
{
  public class AnimationBase
  {
    private readonly EventHandler _eventHandler = new EventHandler();

    public EventHandler EventHandler
    {
      get { return _eventHandler; }
    }
  }
}
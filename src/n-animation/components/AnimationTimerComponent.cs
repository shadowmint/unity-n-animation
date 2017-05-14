using System.Collections.Generic;
using UnityEngine;
using N;

namespace N.Package.Animation
{
  /// Manages all the animations on the scene.
  public class AnimationTimerComponent : MonoBehaviour
  {
    /// The handler associated with this component
    public AnimationHandler Handler;

    /// Update all animations
    public void Update()
    {
      if (Handler != null)
      {
        Handler.Update();
      }
    }
  }
}
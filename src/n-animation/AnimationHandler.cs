using System.Collections.Generic;
using UnityEngine;
using N.Package.Events;

namespace N.Package.Animation
{
  /// Manages all the animations on the scene.
  /// Using this class will spawn an AnimationData object on the scene.
  public class AnimationHandler
  {
    /// The set of registered animations updaters
    public List<IAnimationUpdater> updaters = new List<IAnimationUpdater>();

    /// Associated game object
    private GameObject timerComponent;

    /// Create a new instance with a new timer component
    public AnimationHandler()
    {
      timerComponent = new GameObject();
      timerComponent.transform.name = "Animation Timer";
      var tc = timerComponent.AddComponent<AnimationTimerComponent>();
      tc.handler = this;
    }

    public void Update()
    {
      Update(Time.deltaTime);
    }

    public void Update(float delta)
    {
      foreach (var updater in updaters)
      {
        updater.Update(delta);
      }
    }

    /// Add a updater
    public void Add(IAnimationUpdater updater)
    {
      if (!updaters.Contains(updater))
      {
        updaters.Add(updater);
      }
    }

    /// Remove a updater
    public void Remove(IAnimationUpdater updater)
    {
      if (updaters.Contains(updater))
      {
        updaters.Remove(updater);
      }
    }

    /// Get the default instance of the global handler
    private static AnimationHandler _instance = null;

    public static AnimationHandler Default
    {
      get
      {
        if (_instance != null) return _instance;
        _instance = new AnimationHandler();
        return _instance;
      }
    }

    /// Reset the global instance
    public static void Reset()
    {
      if (_instance != null)
      {
        AnimationManager.Reset();
        foreach (var updater in AnimationHandler._instance.updaters)
        {
          updater.Invalidate();
        }
#if UNITY_EDITOR
        GameObject.DestroyImmediate(_instance.timerComponent);
#else
        GameObject.Destroy(instance.timerComponent);
#endif
        _instance = null;
      }
    }
  }
}
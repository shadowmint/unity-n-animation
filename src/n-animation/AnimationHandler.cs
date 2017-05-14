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
    public List<IAnimationUpdater> Updaters = new List<IAnimationUpdater>();

    /// Associated game object
    private readonly GameObject _timerComponent;

    /// Create a new instance with a new timer component
    public AnimationHandler()
    {
      _timerComponent = new GameObject();
      _timerComponent.transform.name = "Animation Timer";
      var tc = _timerComponent.AddComponent<AnimationTimerComponent>();
      tc.Handler = this;
    }

    public void Update()
    {
      Update(Time.deltaTime);
    }

    public void Update(float delta)
    {
      foreach (var updater in Updaters)
      {
        updater.Update(delta);
      }
    }

    /// Add a updater
    public void Add(IAnimationUpdater updater)
    {
      if (!Updaters.Contains(updater))
      {
        Updaters.Add(updater);
      }
    }

    /// Remove a updater
    public void Remove(IAnimationUpdater updater)
    {
      if (Updaters.Contains(updater))
      {
        Updaters.Remove(updater);
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
      if (_instance == null) return;

      AnimationManager.Reset();
      foreach (var updater in AnimationHandler._instance.Updaters)
      {
        updater.Invalidate();
      }
#if UNITY_EDITOR
      GameObject.DestroyImmediate(_instance._timerComponent);
#else
      GameObject.Destroy(_instance._timerComponent);
#endif
      _instance = null;
    }
  }
}
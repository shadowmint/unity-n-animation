#if N_ANIM_TESTS
using UnityEngine;
using NUnit.Framework;
using N.Package.Animation;
using N.Package.Animation.Curves;
using N.Package.Animation.Animations;
using N.Package.Animation.Targets;
using N;
using N.Package.Core;
using N.Package.Core.Tests;

public class AnimationManagerTests : TestCase
{
  [Test]
  public void test_basic_working_animation()
  {
    this.TearDownAnimations();
    AnimationManager.Default.Configure(Streams.STREAM_0, AnimationStreamType.REJECT, 1);

    var blank = this.SpawnBlank();
    var anim = new MoveTo(new Vector3(1, 1, 1));
    var curve = new Linear(1f);

    int events = 0;
    AnimationManager.Default.Add(Streams.STREAM_0, anim, curve, blank);
    AnimationManager.Default.AddEventHandler((ep) => { events += 1; });

    // Step
    AnimationHandler.Default.Update(0.5f);
    Assert(events == 0);

    // Step
    AnimationHandler.Default.Update(0.5f);
    Assert(events == 1);

    // Teardown
    TearDownAnimations();
  }

  [Test]
  public void test_basic_deferred_animation()
  {
    this.TearDownAnimations();
    AnimationManager.Default.Configure(Streams.STREAM_0, AnimationStreamType.DEFER, 1);

    var blank = this.SpawnBlank();
    var anim = new MoveTo(new Vector3(1, 1, 1));
    var curve = new Linear(1f);

    var blank2 = this.SpawnBlank();
    var anim2 = new MoveTo(new Vector3(1, 1, 1));
    var curve2 = new Linear(1f);

    int events = 0;
    AnimationManager.Default.Add(Streams.STREAM_0, anim, curve, blank);
    AnimationManager.Default.Add(Streams.STREAM_0, anim2, curve2, blank2);
    AnimationManager.Default.AddEventHandler((ep) => { events += 1; });

    var timer = AnimationHandler.Default;

    // Step
    AnimationHandler.Default.Update(1.0f);
    Assert(events == 1);

    // Step
    AnimationHandler.Default.Update(1.0f);

    Assert(events == 2);

    // Teardown
    TearDownAnimations();
  }

  [Test]
  public void test_invalid_manager_cant_be_used()
  {
    this.TearDownAnimations();
    AnimationManager.Default.Configure(Streams.STREAM_0, AnimationStreamType.DEFER, 1);
    var manager = AnimationManager.Default;
    Assert(!manager.Streams.Active(Streams.STREAM_0));
    AnimationHandler.Reset();
    try
    {
      manager.Streams.Active(Streams.STREAM_0);
      Unreachable();
    }
    catch (AnimationException error)
    {
      Assert(error.code == AnimationErrors.INVALID_MANAGER);
    }
  }

  private void TearDownAnimations()
  {
    this.TearDown();
    AnimationHandler.Reset();
  }
}
#endif
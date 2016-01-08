#if N_ANIM_TESTS
using UnityEngine;
using NUnit.Framework;
using N.Package.Animation;
using N.Package.Animation.Curves;
using N.Package.Animation.Animations;
using N.Package.Animation.Targets;
using N;

public class AnimationManagerTests : N.Tests.Test
{
    [Test]
    public void test_basic_working_animation()
    {
        // Setup
        AnimationManager.Default.Clear(Streams.STREAM_0);
        AnimationManager.Default.Configure(Streams.STREAM_0, AnimationStreamType.REJECT, 1);

        var blank = this.SpawnBlank();
        var anim = new MoveTo(new Vector3(1, 1, 1));
        var curve = new Linear(1f);

        int events = 0;
        AnimationManager.Default.Add(Streams.STREAM_0, anim, curve, blank);
        EventHandler handler = (N.Event ep) =>
        {
            ep.As<AnimationCompleteEvent>().Then((evp) =>
            {
                events += 1;
            });
        };
        var foo = AnimationManager.Default.Events;
        foo += handler;

        var timer = Scene.FindComponent<AnimationHandler>().timer;

        // Step
        timer.Force(0.5f);
        timer.Step();
        Assert(events == 0);

        // Step
        timer.Force(0.5f);
        timer.Step();
        Assert(events == 1);

        // Teardown
        TearDownAnimations();
    }

    [Test]
    public void test_basic_deferred_animation()
    {
        // Setup
        AnimationManager.Default.Clear(Streams.STREAM_0);
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
        AnimationManager.Default.AddEventListener((N.Event ep) =>
        {
            ep.As<AnimationCompleteEvent>().Then((evp) =>
            {
                events += 1;
            });
        });

        var timer = Scene.FindComponent<AnimationHandler>().timer;

        // Step
        timer.Force(1.0f);
        timer.Step();
        Assert(events == 1);

        // Step
        timer.Force(1.0f);
        timer.Step();
        Assert(events == 2);

        // Teardown
        TearDownAnimations();
    }

    private void TearDownAnimations() {
        this.TearDown();
        AnimationManager.Reset();
        GameObject.DestroyImmediate(Scene.First<AnimationHandler>());
    }
}
#endif

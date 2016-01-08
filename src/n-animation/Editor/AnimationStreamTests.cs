#if N_ANIM_TESTS
using UnityEngine;
using NUnit.Framework;
using N.Package.Animation;
using System.Collections.Generic;
using N;

public class FakeAnimation : IAnimation
{
    public IAnimationTarget AnimationTarget { set; get; }
    public IAnimationCurve AnimationCurve { set; get; }
    public void AnimationUpdate(GameObject target) { }
}

public class FakeCurve : IAnimationCurve
{
    public float Remaining { get; set; }
    public float Elapsed { get; set; }
    public float Delta { get; set; }
    public float Value { get; set; }
    public bool Complete { get; set; }
}

public class FakeTarget : IAnimationTarget
{
    GameObject instance;
    public FakeTarget(N.Tests.Test test)
    {
        instance = test.SpawnBlank();
    }

    public IEnumerable<GameObject> GameObjects()
    {
        yield return instance;
    }
}

public class AnimationStreamTests : N.Tests.Test
{
    public IAnimation fixture()
    {
        return new FakeAnimation()
        {
            AnimationCurve = new FakeCurve(),
            AnimationTarget = new FakeTarget(this)
        };
    }

    public Tuple<IAnimation, FakeCurve> fixture_curve()
    {
        var curve = new FakeCurve();
        IAnimation anim = new FakeAnimation() { AnimationCurve = curve, AnimationTarget = new FakeTarget(this) };
        return Tuple.New(anim, curve);
    }

    [Test]
    public void test_reject_stream()
    {
        var stream = new AnimationStream(AnimationStreamType.REJECT, 1);
        var curve = fixture_curve();
        Assert(stream.Add(curve.Item1));

        // can't add while full
        Assert(!stream.Add(fixture()));

        // after complete, can add
        curve.Item2.Complete = true;
        stream.Update(1.0f, null);
        Assert(stream.Add(fixture()));

        this.TearDown();
    }

    [Test]
    public void test_defer_stream()
    {
        var stream = new AnimationStream(AnimationStreamType.DEFER, 1);
        var curve = fixture_curve();
        Assert(stream.Add(curve.Item1));
        Assert(stream.Add(fixture()));

        /// Expire first item
        curve.Item2.Complete = true;
        stream.Update(1.0f, null);

        this.TearDown();
    }
}
#endif

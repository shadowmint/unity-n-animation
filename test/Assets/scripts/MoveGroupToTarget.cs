using N.Package.Animation;
using N.Package.Animation.Animations;
using N.Package.Animation.Targets;
using N.Package.Animation.Curves;
using UnityEngine;

public class MoveGroupToTarget : TriggeredAnimationBase
{
    public Vector3 target;
    public float duration = 1f;

    public override void ConfigureStream()
    {
        AnimationManager.Default.Configure(Streams.STREAM_1, AnimationStreamType.DEFER, 1);
    }

    public override void StartAnimation()
    {
        AnimationManager.Default.Add(
            Streams.STREAM_1,
            new MoveTo(target),
            new Linear(duration),
            new TargetByComponent<MoveGroupMarker>()
        );
    }
}

using UnityEngine;
using N.Package.Animation;

namespace N.Package.Animation.Curves
{
    public class SineWave : CurveBase, IAnimationCurve
    {
        private float duration;

        public SineWave(float duration)
        {
            this.duration = duration;
            if (duration <= 0f)
            {
                throw new AnimationException(AnimationErrors.INVALID_DURATION);
            }
        }

        /// The total length of time remaining on this animation curve.
        /// If this value is -ve then the animation curve should never end.
        public float Remaining { get { return -1; } }

        /// The current value of the curve, given Elapsed and Remaining.
        public float Value { get { return Mathf.Sin(Mathf.PI * elapsed / duration); } }

        /// Never expire
        protected override bool CurveComplete { get { return false; } }

        /// Never expire
        protected override float MaxElapsedTime { get { return -1f; } }
    }
}

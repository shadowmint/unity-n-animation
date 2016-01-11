using UnityEngine;
using N.Package.Animation;

namespace N.Package.Animation.Curves
{
    public class SineWave : IAnimationCurve
    {
        private float duration;

        private float elapsed;

        public SineWave(float duration)
        {
            this.duration = duration;
            this.elapsed = 0f;
            if (duration <= 0f)
            {
                throw new AnimationException(AnimationErrors.INVALID_DURATION);
            }
        }

        /// The total length of time remaining on this animation curve.
        /// If this value is -ve then the animation curve should never end.
        public float Remaining
        {
            get
            {
                return -1;
            }
        }

        /// The total time elasped in this animation curve
        public float Elapsed
        {
            get
            {
                return elapsed;
            }
            set
            {
                elapsed = value;
                if (elapsed > duration)
                {
                    elapsed = elapsed - duration;
                }
            }
        }

        /// The last delta
        public float Delta { get; set; }

        /// The current value of the curve, given Elapsed and Remaining.
        public float Value
        {
            get
            {
                return Mathf.Sin(Mathf.PI * elapsed / duration);
            }
        }

        /// Finished yet?
        public bool Complete
        {
            get
            {
                return false;
            }
        }
    }
}

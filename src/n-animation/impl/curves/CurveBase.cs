using N.Package.Animation;

namespace N.Package.Animation.Curves
{
    /// Helpful base class for animation curves
    public abstract class CurveBase
    {
        /// Total elapsed time
        protected float elapsed;

        /// Manually halted?
        private bool halted;

        /// The last delta
        public float Delta { get; set; }

        protected CurveBase()
        {
            this.elapsed = 0f;
        }

        /// Implement this to return a cap on the elapsed time
        protected abstract float MaxElapsedTime { get; }

        /// Implement this to return when the curve is complete
        protected abstract bool CurveComplete { get; }

        /// Call this function when the curve is complete
        public void Halt()
        {
            halted = true;
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
                if ((MaxElapsedTime >= 0f) && (elapsed > MaxElapsedTime))
                {
                    elapsed = MaxElapsedTime;
                }
            }
        }

        /// Finished yet?
        public bool Complete
        {
            get
            {
                return halted || CurveComplete;
            }
        }
    }
}

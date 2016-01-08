using UnityEngine;

public abstract class TriggeredAnimationBase : MonoBehaviour
{
    public bool active = false;

    public void Start()
    {
        ConfigureStream();
    }

    public void Update()
    {
        if (active) {
          active = false;
          StartAnimation();
        }
    }

    public abstract void StartAnimation();

    public abstract void ConfigureStream();
}

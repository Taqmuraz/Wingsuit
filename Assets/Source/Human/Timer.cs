using UnityEngine;

public sealed class Timer
{
    float start;
    float delay;

    public Timer(float delay)
    {
        start = Time.time;
        this.delay = delay;
    }

    public bool IsOver => Time.time > start + delay;
}

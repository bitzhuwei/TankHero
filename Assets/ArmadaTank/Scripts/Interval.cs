using UnityEngine;
using System.Collections;

public class Interval
{
    public float interval;
    public float passedInterval;
    public Interval(float interval)
    {
        this.interval = interval;
    }
    public bool Passed()
    {
        return passedInterval >= interval;
    }
    public void Reset()
    {
        passedInterval = 0;
    }
    public void Pass(float time)
    {
        passedInterval += time;
    }
    public override string ToString()
    {
        return string.Format("{0}->{1}", passedInterval, interval);
        //return base.ToString();
    }
}

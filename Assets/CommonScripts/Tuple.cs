using UnityEngine;
using System.Collections;
using System;

public class Tuple<T1>
{
    private readonly T1 m_Item1;

    public Tuple(T1 item1)
    {
        this.m_Item1 = item1;
    }

    public override string ToString()
    {
        return string.Format("{0}", m_Item1);
    }

    public T1 Item1
    {
        get
        {
            return this.m_Item1;
        }
    }
}

public class Tuple<T1, T2>
{
    private readonly T1 m_Item1;
    private readonly T2 m_Item2;

    public Tuple(T1 item1, T2 item2)
    {
        this.m_Item1 = item1;
        this.m_Item2 = item2;
    }

    public override string ToString()
    {
        return string.Format("{0},{1}", m_Item1, m_Item2);
    }

    public T1 Item1
    {
        get
        {
            return this.m_Item1;
        }
    }
    public T2 Item2
    { get { return this.m_Item2; } }
}

public class Tuple<T1, T2,T3>
{
    private readonly T1 m_Item1;
    private readonly T2 m_Item2;
    private readonly T3 m_Item3;

    public Tuple(T1 item1, T2 item2,T3 item3)
    {
        this.m_Item1 = item1;
        this.m_Item2 = item2;
        this.m_Item3 = item3;
    }

    public override string ToString()
    {
        return string.Format("{0},{1},{2}", m_Item1, m_Item2, m_Item3);
    }

    public T1 Item1
    {
        get
        {
            return this.m_Item1;
        }
    }
    public T2 Item2
    { get { return this.m_Item2; } }

    public T3 item3
    {
        get { return this.item3; }
    }
}

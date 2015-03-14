using UnityEngine;
using System.Collections;

public enum TankToward 
{
    None,
    Z,
    NZ,
    X,
    NX,
}

public static class TankTowardHepler
{
    public static Vector3 ToVector3(TankToward tankToward)
    {
        var result = Vector3.zero;
        switch (tankToward)
        {
            case TankToward.None:
                break;
            case TankToward.Z:
                result = Vector3.forward;
                break;
            case TankToward.NZ:
                result = Vector3.back;
                break;
            case TankToward.X:
                result = Vector3.right;
                break;
            case TankToward.NX:
                result = Vector3.left;
                break;
            default:
                break;
        }
        return result;
    }
}
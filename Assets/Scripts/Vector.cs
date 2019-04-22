using UnityEngine;

public struct Vector
{
    public double x;
    public double y;
    public double z;

    // -------------------------------------------------------------------------------------------------------------------
    public Vector(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    // -------------------------------------------------------------------------------------------------------------------
    public Vector3 ToVector3()
    {
        return new Vector3((float)x, (float)z, (float)y);
    }

    // -------------------------------------------------------------------------------------------------------------------
    public Vector3 ToVector3(double scale)
    {
        return new Vector3((float)(x * scale), (float)(z * scale), (float)(y * scale));
    }
}

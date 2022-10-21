using UnityEngine;

public static class VectorExtensions
{
    public static Vector3 WithX(this Vector3 vector, float x) => new Vector3(x, vector.y, vector.z);
    public static Vector3 WithY(this Vector3 vector, float y) => new Vector3(vector.x, y, vector.z);
    public static Vector3 WithZ(this Vector3 vector, float z) => new Vector3(vector.x, vector.y, z);

    public static Vector3 Min(this Vector3 a, Vector3 b)
    {
        return new Vector3
            (
                Mathf.Min(a.x, b.x),
                Mathf.Min(a.y, b.y),
                Mathf.Min(a.z, b.z)
            );
    }
    public static Vector3 Max(this Vector3 a, Vector3 b)
    {
        return new Vector3
            (
                Mathf.Max(a.x, b.x),
                Mathf.Max(a.y, b.y),
                Mathf.Max(a.z, b.z)
            );
    }
}

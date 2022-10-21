using UnityEngine;

#if UNITY_EDITOR

public class MeshGenerator : MonoBehaviour
{
    [ContextMenu("GenerateCrossPlanes")]
    void GenerateCrossPlanes()
    {
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0f, -1f, -1f),
            new Vector3(0f, 1f, -1f),
            new Vector3(0f, 1f, 1f),
            new Vector3(0f, -1f, 1f),

            new Vector3(-1f, -1f, 0f),
            new Vector3(-1f, 1f, 0f),
            new Vector3(1f, 1f, 0f),
            new Vector3(1f, -1f, 0f),
        };

        int[] indices = new int[]
        {
            0,1,2,
            0,2,3,
            4,5,6,
            4,6,7
        };

        Vector2[] uvs = new Vector2[]
        {
            new Vector2(0f, 0f),
            new Vector2(1f, 0f),
            new Vector2(1f, 1f),
            new Vector2(0f, 1f),

            new Vector2(0f, 0f),
            new Vector2(1f, 0f),
            new Vector2(1f, 1f),
            new Vector2(0f, 1f),
        };

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = indices;
        mesh.uv = uvs;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        UnityEditor.AssetDatabase.CreateAsset(mesh, "Assets/Meshes/CrossPlane.asset");
    }
}
#endif
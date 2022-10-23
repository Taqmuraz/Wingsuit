using UnityEngine;

public sealed class HumanInstancer : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    private void Start()
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawCube(new Vector3(0f, 1f, 0f), new Vector3(1f, 2f, 1f));
    }
}

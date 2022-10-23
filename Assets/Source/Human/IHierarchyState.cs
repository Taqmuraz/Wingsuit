using UnityEngine;

public interface IHierarchyState
{
    void ApplyParent(Transform parent, Vector3 localPosition, Quaternion localRotation);
}

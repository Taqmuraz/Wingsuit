using UnityEngine;

[System.Serializable]
public sealed class HumanBone
{
    [SerializeField] string name;
    [SerializeField] Transform transform;

    public string Name => name;
    public Transform Transform => transform;
}

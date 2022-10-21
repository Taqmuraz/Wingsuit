using UnityEngine;

public interface ICollisionHandler
{
    void OnCollisionEnter(Vector3 point, Vector3 normal, Vector3 impulse);
}

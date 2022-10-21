using UnityEngine;

public sealed class SpeedImageEffect : ImageEffect
{
    [SerializeField] float strength = 0.1f;
    [SerializeField] float effectSpeed = 2f;
    [SerializeField] Transform target;
    [SerializeField] float maxSpeed = 50f;
    [SerializeField] float minRange;
    Rigidbody body;

    protected override void ProcessMaterial(Material material)
    {
        if (body == null) body = target.GetComponent<Rigidbody>();
        if (body == null) return;

        material.SetFloat("_Speed", Mathf.Clamp01(body.velocity.magnitude / maxSpeed));
        material.SetFloat("_Strength", strength);
        material.SetFloat("_EffectSpeed", effectSpeed);
        material.SetFloat("_MinRange", minRange);
    }
}
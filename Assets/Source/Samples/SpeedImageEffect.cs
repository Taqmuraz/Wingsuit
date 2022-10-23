using UnityEngine;

public sealed class SpeedImageEffect : ImageEffect
{
    [SerializeField] float strength = 0.1f;
    [SerializeField] float effectSpeed = 2f;
    [SerializeField] float maxSpeed = 50f;
    [SerializeField] float minRange;
    
    Vector3 lastPosition;
    Vector3 velocity;

    protected override void ProcessMaterial(Material material)
    {
        material.SetFloat("_Speed", Mathf.Clamp01(velocity.magnitude / maxSpeed));
        material.SetFloat("_Strength", strength);
        material.SetFloat("_EffectSpeed", effectSpeed);
        material.SetFloat("_MinRange", minRange);
    }

    private void FixedUpdate()
    {
        if (PlayerController.Player == null) return;

        Vector3 pos = PlayerController.Player.TransformState.Position;
        velocity = (pos - lastPosition) / Time.fixedDeltaTime;
        lastPosition = pos;
    }
}
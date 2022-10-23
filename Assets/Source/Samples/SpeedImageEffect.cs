using UnityEngine;

public sealed class SpeedImageEffect : ImageEffect
{
    [SerializeField] float strength = 0.1f;
    [SerializeField] float effectSpeed = 2f;
    [SerializeField] float maxSpeed = 50f;
    [SerializeField] float minRange;
    IMoveSystem body;

    protected override void ProcessMaterial(Material material)
    {
        if (PlayerController.Player == null) return;
        else body = PlayerController.Player.MoveSystem;

        material.SetFloat("_Speed", Mathf.Clamp01(body.Velocity.magnitude / maxSpeed));
        material.SetFloat("_Strength", strength);
        material.SetFloat("_EffectSpeed", effectSpeed);
        material.SetFloat("_MinRange", minRange);
    }
}
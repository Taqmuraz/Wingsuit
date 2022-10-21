using UnityEngine;

public sealed class StandardVitality : IVitality
{
    public float EnergyAmount { get; private set; } = MaxAmount;
    const float MaxAmount = 100f;

    public void SpendEnergy(float amount)
    {
        EnergyAmount = Mathf.Clamp(EnergyAmount - amount, 0f, MaxAmount);
    }
    public void RestoreEnergy(float amount)
    {
        EnergyAmount = Mathf.Clamp(EnergyAmount + amount, 0f, MaxAmount);
    }

    public float EnergyPercent => EnergyAmount / MaxAmount;
}

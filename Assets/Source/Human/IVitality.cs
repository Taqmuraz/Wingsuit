public interface IVitality
{
    float EnergyAmount { get; }
    float EnergyPercent { get; }
    void SpendEnergy(float amount);
    void RestoreEnergy(float amount);
}

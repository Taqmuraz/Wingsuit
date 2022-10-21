using UnityEngine;

public sealed class HumanParachuteEquipmentElement : HumanEquipmentElement, IHumanEquipmentElement
{
    [SerializeField] Transform stropeLeftForward;
    [SerializeField] Transform stropeLeftBack;
    [SerializeField] Transform stropeRightForward;
    [SerializeField] Transform stropeRightBack;

    public Transform StropeLF => stropeLeftForward;
    public Transform StropeLB => stropeLeftBack;
    public Transform StropeRF => stropeRightForward;
    public Transform StropeRB => stropeRightBack;

    public Transform[] Stropes => new[] { StropeLF, StropeRF, StropeLB, StropeRB };

    public void Enable()
    {
        gameObject.SetActive(true);
    }
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}

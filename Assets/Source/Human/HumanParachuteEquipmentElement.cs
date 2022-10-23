using UnityEngine;

public sealed class HumanParachuteEquipmentElement : HumanEquipmentElement, IHumanEquipmentElement
{
    [SerializeField] Transform stropeLeftForward;
    [SerializeField] Transform stropeLeftBack;
    [SerializeField] Transform stropeRightForward;
    [SerializeField] Transform stropeRightBack;
    [SerializeField] Transform root;

    public Transform[] Stropes => new[] { stropeLeftForward, stropeLeftBack, stropeRightForward, stropeRightBack };
    public Transform Root => root;

    public void Enable()
    {
        gameObject.SetActive(true);
    }
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}

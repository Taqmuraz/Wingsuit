using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum HumanBuildType
{
    Player,
    Test
}

public sealed class HumanBuildInfoProvider : MonoBehaviour, IHumanBuildInfoProvider
{
    [SerializeField] HumanBuildType buildType;
    [SerializeField] List<HumanBone> bones = new List<HumanBone>();
    [SerializeField] List<HumanEquipmentElement> equipment = new List<HumanEquipmentElement>();

    public IEnumerable<HumanBone> HumanBones => bones;
    public IEnumerable<IHumanEquipmentElement> Equipment => equipment.Cast<IHumanEquipmentElement>().ToArray();

    void Start()
    {
        switch (buildType)
        {
            case HumanBuildType.Player: HumanController.BuildHuman<PlayerController>(gameObject, this); break;
            case HumanBuildType.Test: HumanController.BuildHuman<TestHumanController>(gameObject, this); break;
        }
    }
}

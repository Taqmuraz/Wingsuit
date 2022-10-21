using System.Collections.Generic;
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

    public IEnumerable<HumanBone> HumanBones => bones;

    void Start()
    {
        switch (buildType)
        {
            case HumanBuildType.Player: HumanController.BuildHuman<PlayerController>(gameObject, this); break;
            case HumanBuildType.Test: HumanController.BuildHuman<TestHumanController>(gameObject, this); break;
        }
    }
}

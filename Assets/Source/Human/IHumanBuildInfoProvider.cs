﻿using System.Collections.Generic;

public interface IHumanBuildInfoProvider
{
    IEnumerable<HumanBone> HumanBones { get; }
}
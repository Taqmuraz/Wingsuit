using System;

public sealed class CustomMobileAxis : IMobileAxisProvider
{
    Func<float> valueGetter;

    public CustomMobileAxis(string name, Func<float> valueGetter)
    {
        Name = name;
        this.valueGetter = valueGetter;
    }

    public string Name { get; }
    public float Value => valueGetter();
}

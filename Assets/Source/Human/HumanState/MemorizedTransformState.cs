using UnityEngine;

public sealed class MemorizedTransformState
{
    public MemorizedTransformState(ITransformState state)
    {
        InitialLocalPosition = state.LocalPosition;
        InitialLocalRotation = state.LocalRotation;
        InitialLocalScale = state.LocalScale;
        State = state;
    }

    public Vector3 InitialLocalPosition { get; }
    public Vector3 InitialLocalScale { get; }
    public Quaternion InitialLocalRotation { get; }
    public ITransformState State { get; }

    public void Rotate(Quaternion offset)
    {
        State.LocalRotation = InitialLocalRotation * offset;
    }
    public void PreRotate(Quaternion preOffset)
    {
        State.LocalRotation = preOffset * InitialLocalRotation;
    }
    public void PreRotateSmooth(Quaternion preOffset)
    {
        State.LocalRotation = Quaternion.Lerp(State.LocalRotation, preOffset * InitialLocalRotation, Time.deltaTime * 5f);
    }
    public void RotateSmooth(Quaternion offset)
    {
        State.LocalRotation = Quaternion.Lerp(State.LocalRotation, InitialLocalRotation * offset, Time.deltaTime * 5f);
    }
    public void Reset()
    {
        State.LocalPosition = InitialLocalPosition;
        State.LocalRotation = InitialLocalRotation;
        State.LocalScale = InitialLocalScale;
    }
}

public static class InputProvider
{
    static readonly IInputProvider emptyProvider = new EmptyInputProvider();

    public static IInputProvider GetInputProvider()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        return MobileInputProvider.Instance ?? PCInputProvider.Instance;
#else
        return MobileInputProvider.Instance ?? emptyProvider;
#endif
    }
}

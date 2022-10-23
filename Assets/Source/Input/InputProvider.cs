public static class InputProvider
{
    static readonly IInputProvider emptyProvider = new EmptyInputProvider();

    public static IInputProvider GetInputProvider()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        return MobileInputProvider.Instance ?? emptyProvider;
#else
        return MobileInputProvider.Instance ?? emptyProvider;
#endif
    }
}

namespace Core.UserInterface.Basement
{
    public static class DrawOptionsExtensions
    {
        public static TOptions CloneOptions<TOptions, TElement>(this IDrawOptions<TElement> origin) where TElement : class, IElement where TOptions : IDrawOptions<TElement>, new()
        {
            TOptions options = new TOptions();
            options.CopyFrom(origin);
            return options;
        }
    }
}
namespace Core.UserInterface.Basement
{
    public struct ElementTypeCode
    {
        public static readonly ElementTypeCode Text = 0;
        public static readonly ElementTypeCode Image = 1;
        public static readonly ElementTypeCode Button = 2;
        public static readonly ElementTypeCode Line = 3;
        public static readonly ElementTypeCode Point = 4;
        public static readonly ElementTypeCode Scrollbar = 5;
        public static readonly int ElementsCount = 6;

        int value;

        private ElementTypeCode(int value)
        {
            this.value = value;
        }

        public static implicit operator int(ElementTypeCode code)
        {
            return code.value;
        }
        public static implicit operator ElementTypeCode(int value)
        {
            return new ElementTypeCode(value);
        }

        public static ElementTypeCode GetElementTypeCode<TElement>()
        {
            var type = typeof(TElement);
            if (typeof(IText).IsAssignableFrom(type)) return Text;
            if (typeof(IImage).IsAssignableFrom(type)) return Image;
            if (typeof(IButton).IsAssignableFrom(type)) return Button;
            if (typeof(ILine).IsAssignableFrom(type)) return Line;
            if (typeof(IPoint).IsAssignableFrom(type)) return Point;
            if (typeof(IScrollbar).IsAssignableFrom(type)) return Scrollbar;
            else throw new System.ArgumentException();
        }
    }
}
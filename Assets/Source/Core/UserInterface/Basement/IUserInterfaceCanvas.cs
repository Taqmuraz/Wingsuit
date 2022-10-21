namespace Core.UserInterface.Basement
{
    public interface IUserInterfaceCanvas
    {
        void BeginDraw(DrawOperationIdentifier identifier);
        void EndDraw();
        void Clear();

        void DrawText(IDrawOptions<IText> textOptions);
        void DrawImage(IDrawOptions<IImage> imageOptions);
        bool DrawButton(IDrawOptions<IButton> buttonOptions);
        void DrawLine(IDrawOptions<ILine> lineOptions);
        void DrawPoint(IDrawOptions<IPoint> pointOptions);
        float DrawScrollbar(IDrawOptions<IScrollbar> scrollbarOptions);

        void Release();
    }
}
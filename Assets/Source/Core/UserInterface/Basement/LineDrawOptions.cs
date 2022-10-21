using UnityEngine;

namespace Core.UserInterface.Basement
{
    public sealed class LineDrawOptions : IDrawOptions<ILine>, ILine
    {
        public LineDrawOptions()
        {
        }

        public LineDrawOptions(Vector3 pointA, Vector3 pointB, Color color, float width)
        {
            PointA = pointA;
            PointB = pointB;
            StartColor = EndColor = color;
            StartWidth = EndWidth = width;
        }
        public LineDrawOptions(Vector3 pointA, Vector3 pointB, Color startColor, Color endColor, float startWidth, float endWidth)
        {
            PointA = pointA;
            PointB = pointB;
            StartColor = startColor;
            EndColor = endColor;
            StartWidth = startWidth;
            EndWidth = endWidth;
        }

        public Vector3 PointA { get; set; }
        public Vector3 PointB { get; set; }

        public Color StartColor { get; set; }
        public Color EndColor { get; set; }
        public float StartWidth { get; set; }
        public float EndWidth { get; set; }

        void IDrawOptions<ILine>.SetupElement(ILine element)
        {
            element.SetColor(StartColor, EndColor);
            element.SetWidth(StartWidth, EndWidth);
            element.SetPoints(PointA, PointB);
        }

        IDrawOptions<ILine> IDrawOptions<ILine>.CopyFrom(IDrawOptions<ILine> from)
        {
            from.SetupElement(this);
            return this;
        }

        void ILine.SetPoints(Vector3 a, Vector3 b)
        {
            PointA = a;
            PointB = b;
        }

        void ILine.SetWidth(float start, float end)
        {
            StartWidth = start;
            EndWidth = end;
        }

        void ILine.SetColor(Color start, Color end)
        {
            StartColor = start;
            EndColor = end;
        }
    }
}
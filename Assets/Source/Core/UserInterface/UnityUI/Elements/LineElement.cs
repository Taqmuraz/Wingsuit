namespace Core.UserInterface.UnityUI
{
    using Core.UserInterface.Basement;
    using UnityEngine;
    using Core.Abstractions;

    public sealed class LineElement : MonoBehaviour, ILine, IReleasable
    {
        [SerializeField] LineRenderer lineRenderer;
        Vector3[] positions = new Vector3[2];
        float width;

        void IReleasable.Release()
        {
            Destroy(gameObject);
        }

        void ILine.SetPoints(Vector3 a, Vector3 b)
        {
            (positions[0], positions[1]) = (a, b);
            lineRenderer.SetPositions(positions);
            lineRenderer.material.SetVector("_LineParams", new Vector4((a - b).magnitude, width));
        }

        void ILine.SetWidth(float start, float end)
        {
            lineRenderer.startWidth = start;
            lineRenderer.endWidth = end;
            lineRenderer.alignment = LineAlignment.View;

            width = (start + end) * 0.5f;
        }

        void ILine.SetColor(Color start, Color end)
        {
            lineRenderer.startColor = start;
            lineRenderer.endColor = end;
        }
    }
}
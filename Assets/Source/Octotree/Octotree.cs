using UnityEngine;

public sealed class Octotree<TLocatable>
    where TLocatable : ILocatable
{
    OctotreeNode<TLocatable> root;

    public Octotree(Bounds bounds, int maxLocatablesOnNode, int maxDepth)
    {
        root = new OctotreeNode<TLocatable>(bounds, 0, maxLocatablesOnNode, maxDepth);
    }

    public void VisitTree(ITreeAcceptor<TLocatable> acceptor)
    {
        root.VisitNode(acceptor);
    }

    public void AddLocatable(TLocatable locatable)
    {
        root.AddLocatable(locatable);
    }
}

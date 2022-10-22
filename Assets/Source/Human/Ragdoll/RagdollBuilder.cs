using System.Linq;
using UnityEngine;

public static class RagdollBuilder
{
    static RagdollBuilder()
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer(HumanController.HumanElementLayerName), LayerMask.NameToLayer(HumanController.HumanLayerName), true);
    }

    public static IRagdollSystem BuildForHuman(IHumanController human, GameObject gameObject)
    {
        var templates = gameObject.GetComponentsInChildren<RagdollElementTemplate>();
        var elements = templates.Select(t => t.CreateElement()).ToArray();

        foreach (var template in templates)
        {
            template.InitializeElement(human);
        }

        var rootElement = elements.First(e => e.IsRoot);
        rootElement.SetEnabled(false);

        return new RagdollSystem(rootElement, elements);
    }
}

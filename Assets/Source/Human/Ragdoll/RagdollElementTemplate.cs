using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class RagdollElementTemplate : MonoBehaviour
{
    [ContextMenu("Reset joint")]
    void ResetJoint()
    {
        jointInfo = new JointInfo();
    }
    [ContextMenu("Enable")]
    void Enable()
    {
        transform.root.GetComponent<HumanController>().RagdollSystem.SetElementEnabled(element, true);
    }
    [ContextMenu("Disable")]
    void Disable()
    {
        transform.root.GetComponent<HumanController>().RagdollSystem.SetElementEnabled(element, false);
    }

    [SerializeField] JointInfo jointInfo;
    IRagdollElement element;
    IHumanController human;

    public IRagdollElement CreateElement()
    {
        return element = CreateElement(jointInfo);
    }

    public void InitializeElement(IHumanController human)
    {
        this.human = human;
        element.Initialize(TraceChildren());
    }

    protected IRagdollElement[] TraceChildren()
    {
        var list = new List<RagdollElementTemplate>();
        for (int i = 0; i < transform.childCount; i++)
        {
            TraceChildren(transform.GetChild(i), list);
        }
        return list.Select(l => l.element).ToArray();
    }

    void TraceChildren(Transform root, List<RagdollElementTemplate> list)
    {
        var template = root.GetComponent<RagdollElementTemplate>();
        if (template != null)
        {
            list.Add(template);
            return;
        }
        else
        {
            for (int i = 0; i < root.childCount; i++)
            {
                TraceChildren(root.GetChild(i), list);
            }
        }
    }

    protected abstract IRagdollElement CreateElement(JointInfo jointInfo);

    void OnDrawGizmosSelected()
    {
        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(jointInfo.ConnectionLocal, jointInfo.SwingAxisLocal * 0.3f);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(jointInfo.ConnectionLocal, jointInfo.TwistAxisLocal * 0.3f);
        Gizmos.DrawSphere(jointInfo.ConnectionLocal, 0.03f);

        Gizmos.color = Color.yellow;
        DrawGizmos();
    }
    protected abstract void DrawGizmos();
}

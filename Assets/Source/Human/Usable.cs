using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Usable : MonoBehaviour, IUsable
{
    static List<Usable> usables = new List<Usable>();

    private void Awake()
    {
        usables.Add(this);
    }

    private void OnDestroy()
    {
        usables.Remove(this);
    }

    public static bool FindUsable(Bounds bounds, out IUsable usable)
    {
        usable = usables.FirstOrDefault(u => u.Bounds.Intersects(bounds));
        return usable != null;
    }
    protected abstract Bounds Bounds { get; }

    public abstract IControlAction Use(IUser user);

    public abstract string Description { get; }
}
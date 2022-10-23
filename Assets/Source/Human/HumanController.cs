using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class HumanController : MonoBehaviour, IHumanController
{
    public static string HumanLayerName => "Human";
    public static string HumanElementLayerName => "HumanElement";
    public static string VehicleLayerName => "Vehicle";

    public static void BuildHuman<TController>(GameObject instance, IHumanBuildInfoProvider provider) where TController : HumanController
    {
        var controller = instance.AddComponent<TController>();
        controller.bonesMap = provider.HumanBones.ToDictionary(b => b.Name, b => b.Transform);
        controller.equipment = provider.Equipment;
    }

    IEnumerable<IHumanEquipmentElement> equipment;
    List<ICollisionHandler> collisionHandlers = new List<ICollisionHandler>();
    List<IEventsHandler> eventListeners = new List<IEventsHandler>();
    HumanStateMachile stateMachile;
    Dictionary<string, Transform> bonesMap;
    Dictionary<string, ITransformState> boneStatesMap;

    Dictionary<string, object> variables = new Dictionary<string, object>();

    public T GetVariable<T>(string name)
    {
        if (variables.TryGetValue(name, out var result)) return (T)result;
        else return default;
    }
    public void SetVariable(string name, object value)
    {
        variables[name] = value;
    }

    T AddListener<T>(T listener) where T : IEventsHandler
    {
        eventListeners.Add(listener);
        return listener;
    }
    void CallEvent(string name)
    {
        foreach (var l in eventListeners) l.CallEvent(name);
    }

    void Awake()
    {
        
    }
    void OnDestroy()
    {
        OnFinalize();
    }

    protected virtual void Initialize() { }
    protected virtual void OnUpdate() { }
    protected virtual void OnFinalize() { }

    void Start()
    {
        RagdollSystem = RagdollBuilder.BuildForHuman(this, gameObject);
        Initialize();

        MoveSystem = AddListener(CreateMoveSystem());
        var transformState = new StandardTransformState(transform);
        TransformState = transformState;
        HierarchyState = transformState;
        stateMachile = AddListener(new HumanStateMachile(this));
    }

    private void Update()
    {
        CallEvent("Update");
        OnUpdate();
    }
    void FixedUpdate()
    {
        CallEvent("FixedUpdate");
    }
    void LateUpdate()
    {
        CallEvent("LateUpdate");
    }

    public abstract IHumanControlProvider ControlProvider { get; }
    protected abstract IMoveSystem CreateMoveSystem();
    
    public IMoveSystem MoveSystem { get; private set; }
    public ITransformState TransformState { get; private set; }

    public ITransformState GetBone(string name)
    {
        return new StandardTransformState(bonesMap[name]);
    }

    public IRagdollSystem RagdollSystem { get; private set; }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.contactCount != 0)
        {
            var contact = collision.contacts[0];
            foreach (var handler in collisionHandlers) handler.OnCollisionEnter(contact.point, contact.normal, collision.relativeVelocity);
        }
    }

    public void SubscribeCollisionHandler(ICollisionHandler handler)
    {
        collisionHandlers.Add(handler);
    }

    public void UnsubscribeCollisionHandler(ICollisionHandler handler)
    {
        collisionHandlers.Remove(handler);
    }

    public TElement GetEquipmentElement<TElement>() where TElement : IHumanEquipmentElement
    {
        return (TElement)equipment.First(e => e is TElement);
    }

    public IHierarchyState HierarchyState { get; private set; }
}

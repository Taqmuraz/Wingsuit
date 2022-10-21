using System;
using System.Collections.Generic;

public abstract class EventsHandler : IEventsHandler
{
    Dictionary<string, List<Action>> actions;

    public EventsHandler()
    {
        actions = new Dictionary<string, List<Action>>();
        List<System.Reflection.MethodInfo> methods = new List<System.Reflection.MethodInfo>();

        var type = GetType();

        do
        {
            methods.AddRange(type.GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic));
            type = type.BaseType;
        } while (type != typeof(object));


        var zeroParam = new object[0];
        for (int i = 0; i < methods.Count; i++)
        {
            var method = methods[i];
            if (Attribute.IsDefined(method, typeof(BehaviourEventAttribute)) && method.GetParameters().Length == 0)
            {
                Action action = () => method.Invoke(this, zeroParam);
                if (actions.TryGetValue(method.Name, out var result))
                {
                    result.Add(action);
                }
                else
                {
                    actions.Add(method.Name, new List<Action> { action });
                }
            }
        }
    }

    public bool ContainsAction(string name)
    {
        return actions.ContainsKey(name);
    }

    public void CallEvent(string name)
    {
        if (actions.TryGetValue(name, out var result))
        {
            var actionsList = result;
            for (int i = actionsList.Count - 1; i >= 0; i--) actionsList[i].Invoke();
        }
        OnEventCall(name);
    }

    protected virtual void OnEventCall(string name)
    {

    }
}
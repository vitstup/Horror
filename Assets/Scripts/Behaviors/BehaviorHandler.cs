using System;
using System.Collections.Generic;
using System.Linq;

public class BehaviorHandler
{
    private readonly Dictionary<Type, IBehavior> _behaviors = new();

    public void RegisterBehavior<T>(T behavior) where T : class, IBehavior
    {
        var interfaces = behavior.GetType()
            .GetInterfaces()
            .Where(i => typeof(IBehavior).IsAssignableFrom(i));

        foreach (var iface in interfaces)
        {
            _behaviors[iface] = behavior;
        }
    }

    public void UnregisterBehavior<T>() where T : class, IBehavior
    {
        _behaviors.Remove(typeof(T));
    }

    public T? GetBehavior<T>() where T : class, IBehavior
    {
        _behaviors.TryGetValue(typeof(T), out var behavior);
        return behavior as T;
    }

    public bool HasBehavior<T>() where T : IBehavior
    {
        return _behaviors.ContainsKey(typeof(T));
    }
}
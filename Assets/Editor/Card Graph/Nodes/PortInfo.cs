using System;
using UnityEditor.Experimental.GraphView;

public struct PortInfo
{
    public PortInfo(Type type, string name = null, Port.Capacity capacity = Port.Capacity.Multi)
    {
        Name = name;
        Type = type;
        Capacity = capacity;
    }

    public string Name { get; }
    public Port.Capacity Capacity { get; }
    public Type Type { get; }
}
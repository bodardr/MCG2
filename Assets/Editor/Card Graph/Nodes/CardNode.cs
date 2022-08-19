using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public abstract class CardNode : Node
{
    private readonly PortInfo[] inputPortInfos;
    private readonly PortInfo[] outputPortInfos;
    
    protected CardNode(string title, PortInfo[] inputPortInfos, PortInfo[] outputPortInfos) : base()
    {
        base.title = title;
        this.inputPortInfos = inputPortInfos;
        this.outputPortInfos = outputPortInfos;
        
        CreateNodes();
    }

    private void CreateNodes()
    {
        CreatePorts(inputPortInfos, Direction.Input, inputContainer);
        CreatePorts(outputPortInfos, Direction.Output, outputContainer);
    }

    private void CreatePorts(PortInfo[] portInfos, Direction direction, VisualElement nodeContainer)
    {
        if (portInfos == null)
            return;
        
        foreach (var portInfo in portInfos)
        {
            var port = InstantiatePort(Orientation.Horizontal, direction, portInfo.Capacity, portInfo.Type);
            
            if (!string.IsNullOrEmpty(portInfo.Name))
                port.portName = portInfo.Name;
            
            nodeContainer.Add(port);
        }
    }
}
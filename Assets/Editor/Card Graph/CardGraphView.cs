using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class CardGraphView : GraphView
{
    private CardGraphSearchWindow searchWindow;
    public CardGraphEditorWindow EditorWindow { get; set; }

    public new class UxmlFactory : UxmlFactory<CardGraphView>
    {
    }

    public CardGraphView()
    {
        AddContentManipulators();
        AddGridBackground();
        AddSearchWindow();
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        var compatiblePorts = new List<Port>();
        
        foreach (var port in ports)
        {
            if (startPort == port)
                continue;

            if (startPort.node == port.node)
                continue;

            if (startPort.direction == port.direction)
                continue;

            compatiblePorts.Add(port);
        }

        return compatiblePorts;
    }

    public void AddNode(Vector2 nodePosition, Type type)
    {
        var node = (CardNode)Activator.CreateInstance(type);
        node.SetPosition(new Rect(nodePosition, Vector2.zero));
        
        AddElement(node);
    }

    private void AddContentManipulators()
    {
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
    }

    private void AddSearchWindow()
    {
        if (searchWindow == null)
        {
            searchWindow = ScriptableObject.CreateInstance<CardGraphSearchWindow>();
            searchWindow.CardGraphView = this;
        }

        nodeCreationRequest = ctx => SearchWindow.Open(new SearchWindowContext(ctx.screenMousePosition), searchWindow);
    }

    private void AddGridBackground()
    {
        GridBackground gridBackground = new GridBackground();
        gridBackground.AddToClassList("graphBackground");
        Insert(0, gridBackground);
    }

    public Vector2 GetLocalMousePosition(Vector2 mousePosition, bool fromSearchWindow = false)
    {
        if (fromSearchWindow)
            mousePosition -= EditorWindow.position.position;
        
        return contentViewContainer.WorldToLocal(mousePosition);
    }
}
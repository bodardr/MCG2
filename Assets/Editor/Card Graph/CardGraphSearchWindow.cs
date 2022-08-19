using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Compilation;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CardGraphSearchWindow : ScriptableObject, ISearchWindowProvider
{
    private const string NODES_ASSEMBLY_ROOT_NAMESPACE = "CardEventGraph.Nodes";

    private CardGraphView cardGraphView;
    private Texture2D indentationIcon;

    public CardGraphView CardGraphView
    {
        get => cardGraphView;
        set => cardGraphView = value;
    }

    private void Awake()
    {
        indentationIcon = new Texture2D(1, 1);
        indentationIcon.SetPixel(0, 0, Color.clear);
        indentationIcon.Apply();
    }


    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        var nodeAssemblyRef = CompilationPipeline.GetAssemblies(AssembliesType.Editor)
            .First(x => x.rootNamespace == NODES_ASSEMBLY_ROOT_NAMESPACE);

        var nodeAssembly = System.Reflection.Assembly.Load(nodeAssemblyRef.name);
        var nodeTypes = nodeAssembly.GetTypes();

        List<SearchTreeEntry> searchTreeEntries = new List<SearchTreeEntry>
        {
            new SearchTreeGroupEntry(new GUIContent("Create Node")),
            
            //Add card triggers
            new SearchTreeGroupEntry(new GUIContent("Triggers", indentationIcon), 1)
        };

        foreach (var cardTriggerType in nodeTypes.Where(x => x.IsSubclassOf(typeof(CardTriggerNode))))
        {
            CardTriggerNode node = (CardTriggerNode)Activator.CreateInstance(cardTriggerType);
            searchTreeEntries.Add(new SearchTreeEntry(new GUIContent(node.title, indentationIcon))
                { level = 2, userData = cardTriggerType });
        }
        
        searchTreeEntries.Add(new SearchTreeGroupEntry(new GUIContent("Actions", indentationIcon), 1));

        foreach (var cardActionType in nodeTypes.Where(x => x.IsSubclassOf(typeof(CardActionNode))))
        {
            CardActionNode node = (CardActionNode)Activator.CreateInstance(cardActionType);
            searchTreeEntries.Add(new SearchTreeEntry(new GUIContent(node.title, indentationIcon))
                { level = 2, userData = cardActionType });
        }


        return searchTreeEntries;
    }

    public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
    {
        Vector2 position = cardGraphView.GetLocalMousePosition(context.screenMousePosition, true);

        if (searchTreeEntry.userData == null)
            return false;
        
        CardGraphView.AddNode(position, (Type)searchTreeEntry.userData);

        return true;
    }
}
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class CardTriggerNode : CardNode
{
    protected CardTriggerNode(string title, PortInfo[] outputPortInfos) : base(title,
        null, outputPortInfos)
    {
        var img = EditorGUIUtility.IconContent("d_Lighting").image;
        titleContainer.Insert(0, new Image { image = img });
    }
}
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CardGraphEditorWindow : EditorWindow
{
    [MenuItem("Window/Card Event Graph")]
    public static void ShowExample()
    {
        CardGraphEditorWindow wnd = GetWindow<CardGraphEditorWindow>();
        wnd.titleContent = new GUIContent("Card Event Graph");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/Card Graph/CardGraphEditorWindow.uxml");
        var template = visualTree.Instantiate();

        var graphView = (CardGraphView)template.Children().First();
        graphView.EditorWindow = this;
        
        root.Add(template);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class DialogueGraph : EditorWindow
{
    private DialogueGraphView Graphview;
    private string FileName = "New Story";

   [MenuItem("Graph/Dialogue Graph")]
   public static void OpenDialogGraphWindow()
   {
        DialogueGraph window = GetWindow<DialogueGraph>();
        window.titleContent = new GUIContent(text: "Dialogue Graph");
   }

    private void OnEnable()
    {
        ConstructGraphView();
        ConstructToolbar();
    }

    private void ConstructGraphView()
    {
        Graphview = new DialogueGraphView { name = "Dialogue Graph"};
        Graphview.StretchToParentSize();
        rootVisualElement.Add(Graphview);
    }

    private void ConstructToolbar()
    {
        Toolbar toolbar = new Toolbar();

        TextField FileNameTextField = new TextField(FileName);
        FileNameTextField.SetValueWithoutNotify(FileName);
        FileNameTextField.MarkDirtyRepaint();
        FileNameTextField.RegisterValueChangedCallback(evt => FileName = evt.newValue);

        toolbar.Add(FileNameTextField);

        toolbar.Add(child: new Button(clickEvent: () => StartDataOperation(save: true)) { text = "Save Data" });
        toolbar.Add(child: new Button(clickEvent: () => StartDataOperation(save: false)) { text = "Load Data" });

        var NodeCreateButton = new Button(clickEvent:() => 
        {
            Graphview.CreateNode("Dialogue Node");
        });
        
        NodeCreateButton.text = "Create Node";

        toolbar.Add(NodeCreateButton);
        rootVisualElement.Add(toolbar);
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(Graphview);
    }

    private void StartDataOperation(bool save)
    {
        if (string.IsNullOrEmpty(FileName))
            Debug.LogError("File name does not exist. Please enter a correct File Name. Check File Spelling.");

        GraphSave Saver = GraphSave.GetInstance(Graphview);

        if (save)
            Saver.SaveGraph(FileName);
        else
            Saver.LoadGraph(FileName);
    }
}

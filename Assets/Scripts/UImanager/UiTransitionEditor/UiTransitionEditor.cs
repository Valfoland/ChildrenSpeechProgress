using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public class UiTransitionEditor : EditorWindow
{
    private PanelInstancer panelInstancer = new PanelInstancer();
    private List<string> nameItems = new List<string>();

    private Dictionary<string, Dictionary<string, TransitionTypes>> tempDict = 
        new Dictionary<string, Dictionary<string, TransitionTypes>>();

    private string[] transitionTypes =
    {
        TransitionTypes.Hard.ToString(),
        TransitionTypes.Normal.ToString(),
        TransitionTypes.Soft.ToString(),
        TransitionTypes.None.ToString()
    };
    private string[] itemTypes =
    {
        PanelTypes.Main.ToString(),
        PanelTypes.Secondary.ToString(),
    };
    
    private string nameField;
    private Vector2 scrollPos;

    [MenuItem("Window/UiTransition")]
    private static void Init()
    {
        UiTransitionEditor window = (UiTransitionEditor)GetWindow(typeof(UiTransitionEditor));
        window.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.HelpBox("Создание панелей: ", MessageType.Info);
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        CreatePanels();

        EditorGUILayout.BeginHorizontal("box");
        BtnsCreatePanels();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Separator();
        
        EditorGUILayout.HelpBox("Параметры панелей (связи, типы и т.д.): ", MessageType.Info);
        SetParametersPanels();
        BtnComplete();
        EditorGUILayout.EndScrollView();
    }

    private void CreatePanels()
    {
        for (int i = 0; i < nameItems.Count; i++)
        {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.BeginHorizontal("box");
            nameItems[i] = EditorGUILayout.TextField("Имя панели", nameItems[i],
                GUILayout.Height(20));
            
            if (GUILayout.Button("X", "BoldLabel", GUILayout.Height(20), GUILayout.Width(20)) &&
                nameItems.Count >= 1)
            {
                nameItems.Remove(nameItems[i]);
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
    }

    private void BtnsCreatePanels()
    {
        EditorGUILayout.BeginVertical("box");
        if (GUILayout.Button("Add panel", GUILayout.Height(20), GUILayout.Width(150)))
        {
            nameItems.Add("");
        }
        if (GUILayout.Button("Remove all", GUILayout.Height(20), GUILayout.Width(150)))
        {
            nameItems.Clear();
            ClearData();
        }
        EditorGUILayout.EndVertical();
        if (GUILayout.Button("Generate", GUILayout.Height(50), GUILayout.Width(150)))
        {
            ClearData();
            
            foreach (var item in nameItems)
            {
                PanelInstancer.ItemDict.Add(item, new Dictionary<string, TransitionTypes>());
                tempDict.Add(item, new Dictionary<string, TransitionTypes>());
                PanelInstancer.ItemTypeDict.Add(item, PanelTypes.Main);
            }

            foreach (var item in PanelInstancer.ItemDict)
            {
                foreach (var item1 in PanelInstancer.ItemDict)
                {
                    if (item.Key != item1.Key)
                    {
                        PanelInstancer.ItemDict[item.Key].Add(item1.Key, TransitionTypes.None);
                        tempDict[item.Key].Add(item1.Key, TransitionTypes.None);
                    }
                }
            }
        }
    }

    private void SetParametersPanels()
    {
        int j = 0;
        foreach (var item in PanelInstancer.ItemDict)
        {
            EditorGUILayout.BeginVertical("Box");

            EditorGUILayout.BeginHorizontal("Box");
            EditorGUILayout.TextArea("Панель: " + item.Key, "BoldLabel");
            PanelInstancer.ItemTypeDict[item.Key] = (PanelTypes)
                EditorGUILayout.Popup((int) PanelInstancer.ItemTypeDict[item.Key], itemTypes);
            EditorGUILayout.EndHorizontal();

            foreach (var item1 in item.Value)
            {
                EditorGUILayout.BeginHorizontal("Box");
                EditorGUILayout.TextArea("Связь с панелью: " + item1.Key, "Label");
                tempDict[item.Key][item1.Key] = (TransitionTypes)
                    EditorGUILayout.Popup((int) tempDict[item.Key][item1.Key], transitionTypes);
                EditorGUILayout.EndHorizontal();
            }

            j++;
            EditorGUILayout.EndVertical();
        }

        foreach (var item in tempDict)
        foreach (var item1 in item.Value)
            PanelInstancer.ItemDict[item.Key][item1.Key] = item1.Value;
    }

    private void BtnComplete()
    {
        if (GUILayout.Button("Complete", GUILayout.Height(40)))
        {
            bool isConfirm = EditorUtility.DisplayDialog(
                "Warning",
                "Are you sure?",
                "Ok",
                "Cancel");

            if (isConfirm)
            {
                panelInstancer.InstancePanel();
            }
        }
    }

    private void ClearData()
    {
        tempDict.Clear();
        PanelInstancer.ItemDict.Clear();
        PanelInstancer.ItemTypeDict.Clear();
    }
}

#endif
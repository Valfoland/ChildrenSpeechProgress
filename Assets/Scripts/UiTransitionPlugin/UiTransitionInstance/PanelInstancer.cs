#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PanelInstancer
{
    public static Dictionary<string, Dictionary<string, TransitionTypes>> ItemDict = 
        new Dictionary<string, Dictionary<string, TransitionTypes>>();
    public static Dictionary<string, PanelTypes> ItemTypeDict = 
        new Dictionary<string, PanelTypes>();
    
    private PanelObjectInstancer panelObjectInstancer;
    
    private const string FIELD_PLACEMENT = "Auto_Generated_Code_Placement_Field";
    private const string DATA_INIT_PLACEMENT = "Auto_Generated_Code_Placement_Init";
    private const string NAME_UI_MANAGER = "UiTransitionManager";
    private const string NAME_UI_DATA = "UiTransitionPanelData";

    private string[] forbiddenNames =
    {
        "Panel",
        "DataTransitionInstancer",
        "PanelCodeInstancer",
        "UiTransitionEditor",
        "UiTransitionPanelData",
        "UiTransitionPanelManager",
    };

    private bool replace;
    private bool isForbiddenCreate;
    
    public void InstancePanel()
    {
        replace = true;
        foreach (var name in ItemDict.Keys)
        {
            CreateScriptPanel(name);
            if (!replace || isForbiddenCreate)
                break;
        }

        InstancePanelObject();
        EditAdjustmentData();
    }

    private void InstancePanelObject()
    {
        panelObjectInstancer = GameObject.Find(NAME_UI_MANAGER).GetComponent<PanelObjectInstancer>();
        panelObjectInstancer.InstancePanelObject();
    }
    
    private void CreateScriptPanel(string nameClass)
    {
        var path = "Assets/Scripts/UiTransitionManager/UiTransitionPanels/";
        var fileName = nameClass + ".cs";

        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        
        forbiddenNames.ToList().ForEach(x =>
        {
            if (fileName == x + ".cs")
                isForbiddenCreate = true;
        });

        if (isForbiddenCreate)
        {
            EditorUtility.DisplayDialog(
                "Error!",
                "You are not allowed to create a panel with that name. Please use other name.", "Ok");
            return;
        }

        if (File.Exists(path + fileName) && replace)
        {
            replace = EditorUtility.DisplayDialog(
                "Warning",
                "There are already a files the same name in this location. Do you want to replace?",
                "Ok",
                "Cancel");

            if (!replace) return;
        }

        StreamWriter sw = File.CreateText(path + fileName);
        sw.WriteLine(TextCodePanel(nameClass));
        sw.Close();

        AssetDatabase.Refresh();
    }

    private string TextCodePanel(string nameClass)
    {
        string textCode =
            @"using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Data*
{

}

public class * : Panel
{
    private Data* data*;
    
    public *(DataPanel dataPanel, Data* data*, PanelTypes panelType) : base(dataPanel, panelType)
    {
        this.data* = data*;

        foreach (var data in dataPanel.DataPanelBtns)
        {
            data.BtnPanel.onClick.AddListener(() => OnClickBtn(data.ItemPanelTypes));
        }
    }

private void OnClickBtn(ItemTypes itemTypes)
{
    onClickBtn?.Invoke(this, itemTypes);
}
}";
        return textCode.Replace("*", nameClass);
    }

    private void EditAdjustmentData()
    {
        string path = @"Assets/Scripts/UiTransitionManager/";
        string fileName = NAME_UI_DATA + ".cs";
        string textData = "";

        try
        {
            using (StreamReader sr = new StreamReader(path + fileName))
            {
                textData = sr.ReadToEnd();
            }
        }
        catch (IOException e)
        {
        }

        CreateCodeItemDict(ref textData);
        CreateCodeDataPanels(ref textData);
        
        StreamWriter sw = File.CreateText(path + fileName);
        sw.WriteLine("");
        sw.WriteLine(textData);
        sw.Close();
    }
    
    private void CreateCodeItemDict(ref string textData)
    {
        StringBuilder textDataBuilder = new StringBuilder();
 
        foreach (var item in ItemDict)
        {
            textDataBuilder.Append(
                $"    \nPanel  {item.Key.ToLower()} = " +
                $"new {item.Key} (dataPanelDict[\"{item.Key}\"], data{item.Key}, PanelTypes.{ItemTypeDict[item.Key]});\n");
        }
        
        textDataBuilder.Append("    \nUiTransitionDict = new Dictionary<Panel, Dictionary<Panel, TransitionTypes>>\n{\n");
        
        foreach (var item in ItemDict)
        {
            textDataBuilder.Append("    {\n");
            textDataBuilder.Append($"        {item.Key.ToLower()},\n");
            textDataBuilder.Append("        new Dictionary<Panel, TransitionTypes>\n");
            textDataBuilder.Append("        {\n");
            foreach (var item1 in item.Value)
            {
                textDataBuilder.Append("            {\n");
                textDataBuilder.Append($"                {item1.Key.ToLower()}, TransitionTypes.{item1.Value}\n");
                textDataBuilder.Append("            },\n");
            }
            textDataBuilder.Append("        }\n");
            textDataBuilder.Append("    },\n");
        }

        textDataBuilder.Append("    };\n");
        
        InsertBlockOfCode(ref textData, textDataBuilder.ToString(), DATA_INIT_PLACEMENT);
    }
    
    private void CreateCodeDataPanels(ref string textData)
    {
        StringBuilder textDataBuilder = new StringBuilder();
        string helpText = "    \n[SerializeField] private ";
            
        foreach (var item in ItemDict)
        {
            textDataBuilder.Append(helpText + $"Data{item.Key} data{item.Key};");
        }

        textDataBuilder.Append("\n");
        InsertBlockOfCode(ref textData, textDataBuilder.ToString(), FIELD_PLACEMENT);
    }

    private void InsertBlockOfCode(ref string textData, string textInsertable, string keyWord)
    {
        int id = GetIndexText(ref textData, "#region " + keyWord);
        textData = textData.Insert(id, textInsertable);
    }

    private int GetIndexText(ref string textData, string start)
    {
        int id = textData.IndexOf(start, StringComparison.Ordinal);
        int insertId = id + 1 + start.Length - 1;
        
        for (int i = insertId; i < textData.Length; i++)
        {
            
            if (textData[i] == '#')
            {
                id = i - insertId;
                break;
            }
        }
        
        textData = textData.Remove(insertId, id);
        return insertId;
    }
}

# endif

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

public class UiTransitionManagerData : MonoBehaviour
{
    [Header("Данные панелей для переходов")]
    [SerializeField] protected DataPanel[] dataPanel;
    protected static Dictionary<string, DataPanel> dataPanelDict;

    #region FieldData (Don't_Change_It !!!)
    public static Dictionary<Panel, Dictionary<Panel, TransitionTypes>> UiTransitionDict;
    public static Dictionary<Panel, List<Panel>> RelationPanelDict;
    #endregion

    protected virtual void Start()
    {
        InitData();
    }

#if UNITY_EDITOR
    /// <summary>
    /// Иницииализация данных панели (кнопки переходов)
    /// Применение данного куска кода инициализирует данные в инспекторе
    /// </summary>
    /// <param name="btnDict">словарь кнопок переходов</param>
    public void InitDataPanel(Dictionary<GameObject, Dictionary<string, Button>> btnDict)
    {
        Dictionary<GameObject, Dictionary<Button, ItemTypes>> uiDataPanel =
            new Dictionary<GameObject, Dictionary<Button, ItemTypes>>();

        foreach (var item in btnDict)
        {
            uiDataPanel.Add(item.Key, new Dictionary<Button, ItemTypes>());
            foreach (var item1 in btnDict)
            {
                foreach (var item2 in item1.Value)
                {
                    if (item1.Key == item.Key)
                    {
                        uiDataPanel[item.Key].Add(item2.Value, ItemTypes.Inside);
                    }
                    else
                    {
                        if (item2.Key == item.Key.name)
                        {
                            uiDataPanel[item.Key].Add(item2.Value, ItemTypes.Outside);
                        }
                    }
                }
            }
        }

        int i = 0;
        DataPanel[] dataPanelTemp = new DataPanel[uiDataPanel.Count];

        foreach (var data in uiDataPanel)
        {
            dataPanelTemp[i] = new DataPanel
            {
                NamePanel = data.Key.name,
                PanelObject = data.Key
            };

            int j = 0;
            dataPanelTemp[i].DataPanelBtns = new DataPanelBtn[uiDataPanel.ToList()[i].Value.Count];

            foreach (var data1 in data.Value)
            {
                dataPanelTemp[i].DataPanelBtns[j] = new DataPanelBtn
                {
                    BtnPanel = data1.Key,
                    ItemPanelTypes = data1.Value
                };

                j++;
            }

            i++;
        }
        
        gameObject.GetComponent<UiTransitionManagerData>().dataPanel = dataPanelTemp;
        EditorSceneManager.SaveOpenScenes();
    }
#endif
    
    protected virtual void InitPanels()
    {
        dataPanelDict = new Dictionary<string, DataPanel>();
        foreach (var data in dataPanel)
        {
            dataPanelDict.Add(data.NamePanel, data);
        }

    }
    
    private void InitData()
    {
        RelationPanelDict = new Dictionary<Panel, List<Panel>>();
        if (UiTransitionDict != null)
        {
            foreach (var panel in UiTransitionDict)
            {
                if (panel.Key.PanelType == PanelTypes.Main)
                {
                    RelationPanelDict.Add(panel.Key, new List<Panel>());
                    InitRelationsPanel(panel.Key, panel.Key);
                }
            }
        }
    }

    private void InitRelationsPanel(Panel panel, Panel transPanel)
    {
        foreach (var transitions in UiTransitionDict[transPanel])
        {
            if (transitions.Value == TransitionTypes.Soft)
            {
                InitRelationsPanel(panel, transitions.Key);
                RelationPanelDict[panel].Add(transitions.Key);
            }
        }
    }

}
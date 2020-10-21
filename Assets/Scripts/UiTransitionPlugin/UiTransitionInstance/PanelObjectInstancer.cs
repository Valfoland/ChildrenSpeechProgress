#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelObjectInstancer : MonoBehaviour
{
    [SerializeField] private UiTransitionManagerData dataManager;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject container;
    [SerializeField] private GameObject btnGoTo;
    [SerializeField] private GameObject backGround;
    
    private static Dictionary<GameObject, Dictionary<string, Button>> btnDict = 
        new Dictionary<GameObject, Dictionary<string, Button>>();

    public void InstancePanelObject()
    {
        CreateContainerObjects(out var containerObject);
        
        foreach (var item in PanelInstancer.ItemDict)
        {
            GameObject oldObject = GameObject.Find(item.Key);
            if (oldObject != null) DestroyImmediate(oldObject);

            GameObject objectPanel = Instantiate(container, containerObject.transform);
            objectPanel.name = $"{item.Key}";
            GameObject background = Instantiate(backGround, objectPanel.transform);
            background.name = "BackGround";
            GameObject btnContainer = Instantiate(container, objectPanel.transform);
            btnContainer.name = "Buttons";
            
            btnDict.Add(objectPanel, new Dictionary<string, Button>());
            
            int i = 0;
            foreach (var item1 in item.Value)
            {
                if (item1.Value != TransitionTypes.None)
                {
                    btnDict[objectPanel].Add(item1.Key, CreateButton(item1, btnContainer.transform, i));
                    i++;
                }
            }
        }

        SetBtnsToDataPanel();
    }

    private Button CreateButton(KeyValuePair<string, TransitionTypes> item, Transform parent, int index)
    {
        GameObject buttonObject = Instantiate(btnGoTo, parent);
        RectTransform rectButton = buttonObject.GetComponent<RectTransform>();
        Text btnText = buttonObject.GetComponentInChildren<Text>();
        buttonObject.name = $"BtnGoTo{item.Key}";
        btnText.text = $"Go to {item.Key}";
        
        if (item.Value == TransitionTypes.Soft)
        {
            rectButton.anchorMax = rectButton.anchorMin = new Vector2(0, 1);
            rectButton.anchoredPosition = new Vector2(
                rectButton.sizeDelta.x - 20,
                -rectButton.sizeDelta.y + 20 - index * rectButton.sizeDelta.x);

        }
        else
        {
            rectButton.anchorMax = rectButton.anchorMin = new Vector2(0.5f, 0.5f);
            rectButton.anchoredPosition = new Vector2(
                0 + index * rectButton.sizeDelta.x,
                0);
        }
        
        return buttonObject.GetComponent<Button>();
    }

    private void CreateContainerObjects(out GameObject containerObject)
    {
        containerObject = null;
        
        if (GameObject.Find("Canvas") == null)
        {
            GameObject canvasObject = Instantiate(canvas);
            canvasObject.name = "Canvas";
        }

        if (GameObject.Find("ContainerTransitionPanels") == null)
        {
            containerObject = Instantiate(container, GameObject.Find("Canvas").transform);
            containerObject.name = "ContainerTransitionPanels";
        }
        else
        {
            containerObject = GameObject.Find("ContainerTransitionPanels");
        }
    }
    private void SetBtnsToDataPanel()
    {
        dataManager.InitDataPanel(btnDict);
    }
    
}

#endif

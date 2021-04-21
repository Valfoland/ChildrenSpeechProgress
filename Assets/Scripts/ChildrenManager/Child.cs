using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Child : MonoBehaviour
{
    private static System.Action onChooseChild;
    public static ChildData CurrentChildData;
    public static ChildData DefaultChildData;
    public ChildData ChildData;
    public ChildViewData ChildViewData;

    
    public void InitChild()
    {
        onChooseChild += SetChooseChild;
    }
    
    private void OnDestroy()
    {
        onChooseChild -= SetChooseChild;
    }
    
    public void ClickChooseChild()
    {
        PlayerPrefs.SetInt(ChildDataConfig.CHOOSE_CHILD, ChildData.IdChild);
        CurrentChildData = ChildData;
        onChooseChild?.Invoke();
    }

    private void SetChooseChild()
    {
        if (ChildData.IdChild != PlayerPrefs.GetInt(ChildDataConfig.CHOOSE_CHILD))
        {
            ChildViewData.ChildCheckMarkImage.color = Color.black;
        }
        else
        {
            ChildViewData.ChildCheckMarkImage.color = Color.white;
        }
    }
}


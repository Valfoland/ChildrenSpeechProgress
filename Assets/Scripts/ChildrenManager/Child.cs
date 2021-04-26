using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Child : MonoBehaviour
{
    public static List<Child> ChildList = new List<Child>();
    public static ChildWithLocalData CurrentChildWithLocalData;
    public static ChildWithLocalData DefaultChildWithLocalData;
    public ChildWithLocalData ChildWithLocalData;
    public ChildViewData ChildViewData;

    
    public void InitChild()
    {
        ChildList.Add(this);
    }

    public void ClickChooseChild()
    {
        PlayerPrefs.SetInt(ChildDataConfig.CHOOSE_CHILD, ChildWithLocalData.Id);
        CurrentChildWithLocalData = ChildWithLocalData;
        SetChooseChild();
    }

    private void SetChooseChild()
    {
        try
        {
            foreach (var child in ChildList)
            {
                if (child.ChildWithLocalData.Id != PlayerPrefs.GetInt(ChildDataConfig.CHOOSE_CHILD))
                {
                    child.ChildViewData.ChildCheckMarkImage.color = Color.black;
                }
                else
                {
                    child.ChildViewData.ChildCheckMarkImage.color = Color.white;
                }
            }
            
        }
        catch (MissingReferenceException)
        {
            
        }
        
    }
}


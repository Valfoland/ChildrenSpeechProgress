using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using System.IO;

public class ChildrenManager : MonoBehaviour
{
    private ChildrenDataSaver childSaver = new ChildrenDataSaver();
    public static List<ChildrenData> ChildDataList = new List<ChildrenData>();

    [SerializeField] private GameObject childPrefab;
    [SerializeField] private GameObject parentChild;
    private static GameObject parentSingletonChild;
    
    private void Awake()
    {
        DontDestroy();
        Init();
    }

    private void DontDestroy()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("ChildManager");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    
    private void Init()
    {
        DataAddChildPanel.onAddChild += ClickAddChild;
        AttemptCounter.onSetResult += SetDataChild;
        GameManager.onSetCompletionLevel += SetDataChild;
        InitChildren();
    }
    
    private void OnDestroy()
    {
        DataAddChildPanel.onAddChild -= ClickAddChild;
        AttemptCounter.onSetResult -= SetDataChild;
        GameManager.onSetCompletionLevel -= SetDataChild;
    }
    
    private void InitChildren()
    {
        if (parentSingletonChild == null)
        {
            parentSingletonChild = parentChild;
        }
        
        Child.CountChildren = 0;
        ChildDataList = childSaver.ChildDataRead();
        
        if (ChildDataList.Count != 0)
            RestoreChildren();
    }

    private void SetDataChild()
    {
        childSaver.ChildDataSave(Child.CurrentChildrenData);
    }
    
    private void ClickAddChild(DataAddChildPanel dataAddChildPanel)
    {
        childSaver.ChildDataWrite(InstanceChild(), dataAddChildPanel, null);
        childSaver.ChildDataSave();
        childSaver.ChildCountSave();
        ChildDataList = childSaver.ChildDataRead();
    }

    private void RestoreChildren()
    {
        foreach (var childrenData in ChildDataList)
        {
            childSaver.ChildDataWrite(InstanceChild(), null, childrenData);
        }
    }

    private GameObject InstanceChild()
    {
       GameObject childObject = Instantiate(childPrefab, parentSingletonChild.transform);
       return childObject;
    } 
}
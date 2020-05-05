using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;

public class ChildrenManager : MonoBehaviour
{
    private ChildrenDataSaver childSaver = new ChildrenDataSaver();
    private static Queue<ChildrenData> childDataQueue = new Queue<ChildrenData>();

    [SerializeField] private GameObject childPrefab;
    [SerializeField] private Transform parentTransformChild;

    private void Start()
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
        DataSetAddChildPanel.onAddChild += ClickAddChild;
        AttemptCounter.onSetResult += SetResultChild;
        
        ChildrenDataSaver.InitResultDict();
        Child.CountChildren = 0;
        childDataQueue = childSaver.ChildDataRead();

        if (childDataQueue.Count != 0)
            RestoreChildren();
    }
    
    private void OnDestroy()
    {
        DataSetAddChildPanel.onAddChild -= ClickAddChild;
        AttemptCounter.onSetResult -= SetResultChild;
    }
    
    private void SetResultChild()
    {
        childSaver.ChildDataSave(Child.CurrentChildrenData);
    }
    
    private void ClickAddChild(DataSetAddChildPanel dataSetAddChildPanel)
    {
        childSaver.ChildDataWrite(InstanceChild(), dataSetAddChildPanel, null);
        childSaver.ChildDataSave();
    }

    private void RestoreChildren()
    {
        for (int i = 0; i <= PlayerPrefs.GetInt(ChildrenDataSaver.COUNT_CHILD); i++)
        {
            childSaver.ChildDataWrite(InstanceChild(), null, childDataQueue.Dequeue());
        }
    }

    private GameObject InstanceChild()
    {
       GameObject childObject = Instantiate(childPrefab, parentTransformChild);
       return childObject;
    } 
}
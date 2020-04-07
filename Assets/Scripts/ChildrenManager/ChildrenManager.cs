using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;

public class ChildrenManager : MonoBehaviour
{
    private ChildrenDataSaver childSaver = new ChildrenDataSaver();
    public static Queue<ChildrenData> ChildDataQueue = new Queue<ChildrenData>();

    [SerializeField] private GameObject childPrefab;
    [SerializeField] private Transform parentTransformChild;

    private void Start()
    {
        DataSetAddChildPanel.onAddChild += ClickAddChild;

        Child.CountChildren = 0;
        childSaver.ChildDataRead();
        if (ChildDataQueue.Count != 0)
        {
            AddChild();
        }
    }

    private void OnDestroy()
    {
        DataSetAddChildPanel.onAddChild -= ClickAddChild;
    }

    public void ClickAddChild(DataSetAddChildPanel dataSetAddChildPanel)
    {
        childSaver.ChildDataWrite(InstanceChild(), 0, dataSetAddChildPanel, null);
        childSaver.ChildDataSave();
    }

    public void AddChild()
    {     
        for (int i = 0; i <= PlayerPrefs.GetInt("countChild"); i++)
        {
            childSaver.ChildDataWrite(InstanceChild(), i, null, ChildDataQueue.Dequeue());
        }
    }

    private GameObject InstanceChild()
    {
        return Instantiate(childPrefab, parentTransformChild);
    }

    internal class ChildrenDataSaver
    {
        private ChildrenData childData;
        private Child child;

        internal void ChildDataWrite(GameObject childObject, int id, DataSetAddChildPanel dataSetAddChildPanel, ChildrenData childData)
        {
            child = childObject.GetComponent<Child>();
            child.Name.text = child.ChildrenData.Name = childData == null ? dataSetAddChildPanel.NameField.text : childData.Name;
            child.Age.text = child.ChildrenData.Age = childData == null ? dataSetAddChildPanel.AgeField.text : childData.Age;
            child.GroupName.text = child.ChildrenData.GroupName = childData == null ? dataSetAddChildPanel.GroupField.text : childData.GroupName;
            child.Score.text = dataSetAddChildPanel == null ? PlayerPrefs.GetInt(id.ToString() + "ScoreChild").ToString() : 0.ToString(); 
        }

        internal void ChildDataRead()
        {
            ChildDataQueue.Clear();
            for (int i = 0; i <= PlayerPrefs.GetInt("countChild") && PlayerPrefs.HasKey(i.ToString()); i++)
            {
                ChildDataQueue.Enqueue(JsonUtility.FromJson<ChildrenData>(PlayerPrefs.GetString(i.ToString())));
            }
        }

        internal void ChildDataSave()
        { 
            PlayerPrefs.SetInt("countChild", Child.CountChildren);
            Debug.Log(Child.CountChildren);
            PlayerPrefs.SetString(Child.CountChildren.ToString(), JsonUtility.ToJson(child.ChildrenData));
        }  
    }
}
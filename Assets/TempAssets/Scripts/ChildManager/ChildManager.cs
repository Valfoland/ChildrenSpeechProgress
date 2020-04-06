using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;

public class ChildManager : MonoBehaviour
{
    private ChildDataSaver childSaver = new ChildDataSaver();
    public static Queue<ChildData> ChildDataQueue = new Queue<ChildData>();

    [SerializeField] private GameObject childPrefab;
    [SerializeField] private Transform parentTransformChild;

    private void Start()
    {
        DataSetAddChildPanel.onAddChild += ClickAddChild;

        Child.CountChild = 0;
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

    internal class ChildDataSaver
    {
        private ChildData childData;
        private Child child;

        internal void ChildDataWrite(GameObject childObject, int id, DataSetAddChildPanel dataSetAddChildPanel, ChildData childData)
        {
            child = childObject.GetComponent<Child>();
            child.Name.text = child.ChildData.Name = childData == null ? dataSetAddChildPanel.NameField.text : childData.Name;
            child.Age.text = child.ChildData.Age = childData == null ? dataSetAddChildPanel.AgeField.text : childData.Age;
            child.GroupName.text = child.ChildData.GroupName = childData == null ? dataSetAddChildPanel.GroupField.text : childData.GroupName;
            child.Score.text = dataSetAddChildPanel == null ? PlayerPrefs.GetInt(id.ToString() + "ScoreChild").ToString() : 0.ToString(); 
        }

        internal void ChildDataRead()
        {
            ChildDataQueue.Clear();
            for (int i = 0; i <= PlayerPrefs.GetInt("countChild") && PlayerPrefs.HasKey(i.ToString()); i++)
            {
                ChildDataQueue.Enqueue(JsonUtility.FromJson<ChildData>(PlayerPrefs.GetString(i.ToString())));
            }
        }

        internal void ChildDataSave()
        { 
            PlayerPrefs.SetInt("countChild", Child.CountChild);
            PlayerPrefs.SetString(Child.CountChild.ToString(), JsonUtility.ToJson(child.ChildData));
        }  
    }
}
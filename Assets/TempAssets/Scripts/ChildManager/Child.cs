using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ChildData
{
    public int IdChild;
    public string Name;
    public string Age;
    public string GroupName;
}
public class Child : MonoBehaviour
{
    public Text Name;
    public Text Age;
    public Text GroupName;
    public Text Score;

    public static int CountChild;
    [SerializeField] public ChildData ChildData;
    [SerializeField] private Image setChildImage;

    private static System.Action onChooseChild;

    private void Start()
    {
        Init();
        ChooseChild();
    }

    private void OnDestroy()
    {
        onChooseChild -= SetChooseChild;
    }

    private void Init()
    {
        onChooseChild += SetChooseChild;
        ChildData = new ChildData();
    }

    private void ChooseChild()
    {
        ChildData.IdChild = CountChild;
        CountChild++;
        onChooseChild?.Invoke();
    }

    public void ClickChooseChild()
    {
        PlayerPrefs.SetInt("ChooseChild", ChildData.IdChild);
        onChooseChild?.Invoke();
    }

    private void SetChooseChild()
    {
        if(ChildData.IdChild != PlayerPrefs.GetInt("ChooseChild"))
        {
            setChildImage.color = Color.clear;
        }
        else
        {
            setChildImage.color = Color.white;
        }
    }
}


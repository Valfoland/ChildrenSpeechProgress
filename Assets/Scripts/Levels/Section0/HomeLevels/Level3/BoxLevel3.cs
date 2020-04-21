using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxLevel3 : MonoBehaviour
{
    public Button BtnBox;
    [SerializeField] private Animation animFalse;
    [SerializeField] private Image imageBox;
    [SerializeField] private Text txt; //TEMP
    private string wordBox;
    public static System.Action<string, BoxLevel3> onClickBox;    
        
    private void Start()
    {
        BtnBox.onClick.AddListener(ClickBox);
    }

    private void OnDestroy()
    {
            
    }

    public void AnimBox()
    {
        animFalse.Play();
    }
        
    public void SetDataBox(Sprite spriteBox, string wordBox)
    {
        Debug.Log("sdsd");
        txt.text = wordBox;
        this.wordBox = wordBox;
        //imageBox.sprite = spriteBox;
    }

    public void ClickBox()
    {
        onClickBox?.Invoke(wordBox, this);
    }
}

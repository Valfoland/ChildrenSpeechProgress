using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class NoticeData
{
    public string NoticeText;
    public bool IsSuccess;
}

public class NoticeVisual : MonoBehaviour
{
    [SerializeField] private Button buttonNotice;
    [SerializeField] private GameObject objectMessage;
    [SerializeField] private Text textMessage;
    [SerializeField] private AuthenticationNotice authenticationNotice;
    [SerializeField] private DelayedDisableObject delayedDisableObject;

    List<string> noticeTextList = new List<string>();
    
    private void Start()
    {
        authenticationNotice.onGetNotices += GetNotice;
        buttonNotice.onClick.AddListener(OnClickNoticeButton);
    }

    private void GetNotice<T>(List<T> noticeList) where T: NoticeData
    {
        noticeTextList.Clear();
        
        if (noticeList.Count > 0)
        {
            buttonNotice.gameObject.SetActive(true);

            foreach (var noticeData in noticeList)
            {
                noticeTextList.Add(noticeData.NoticeText);
            }
        }
        else
        {
            buttonNotice.gameObject.SetActive(false);
        }
    }

    private void OnClickNoticeButton()
    {
        var sb = new StringBuilder();
        objectMessage.SetActive(false);
        objectMessage.SetActive(true);

        foreach (var noticeText in noticeTextList)
        {
            sb.Append(noticeText + "\n");
        }

        textMessage.text = sb.ToString();
        delayedDisableObject.StartDelayedDisable(2);
    }
}

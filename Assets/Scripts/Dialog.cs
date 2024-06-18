using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI contentText;

    public void UpdateDialog(string title, string content)
    {
        if (titleText)
        {
            titleText.text = title;
        }
        if (contentText)
        {
            contentText.text = content;
        }
    }
    public void Show(bool isShow)
    {
        gameObject.SetActive(isShow);
    }
}

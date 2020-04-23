using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageWindowController : MonoBehaviour
{
    private Image image;
    private Text message;

    void Start()
    {
        this.image = GameObject.Find("MessageWindow").GetComponent<Image>();
        this.image.enabled = false;
        this.message = GameObject.Find("MessageWindowText").GetComponent<Text>();
    }

    public void Open()
    {
        this.image.enabled = true;
    }

    public void Close()
    {
        this.image.enabled = false;
        this.message.text = "";
    }

    public void SetMessage(string message)
    {
        // TODO 文字の長さに応じて折り曲げる
        this.message.text = message;
    }

    public void SetRandomMessage() {
        string[] randomMessages = {
            "おおしまは QOL が 5 億あがった",
            "おおしまは人生に絶望した"
        };
        System.Random r = new System.Random();
        int index = r.Next(0, randomMessages.Length);

        string message = randomMessages[index];
        this.SetMessage(message);
    }

}

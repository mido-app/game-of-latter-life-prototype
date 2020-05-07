using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MessageWindow : MonoBehaviour
{
    private Image image;
    private Text message;
    private bool waitingClick = false;

    void Start()
    {
        this.image = GameObject
            .FindGameObjectWithTag("MessageWindow")
            .GetComponent<Image>();
        this.image.enabled = false;
        this.message = GameObject
            .Find("MessageWindowMessage")
            .GetComponent<Text>();
    }

    private void Update()
    {
        if (this.waitingClick && Input.GetMouseButtonDown(0)) {
            this.waitingClick = false;
        }
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

    public IEnumerator ShowMessage(string message)
    {
        this.message.text = message;
        this.waitingClick = true;
        while(this.waitingClick)
        {
            yield return null;
        }
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

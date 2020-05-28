using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private Text turnText;

    void Awake()
    {
        this.turnText = GameObject.Find("TurnText").GetComponent<Text>();
    }

    public void SetTurnText(string playerName)
    {
        this.turnText.text = $"{playerName}'s turn";
    }
}

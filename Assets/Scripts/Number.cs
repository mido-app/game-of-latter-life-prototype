using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number : MonoBehaviour
{
    public int number;
    private int prevNumber = -1;

    private GameController gameController;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        this.gameController = GameObject
            .FindGameObjectWithTag("GameController")
            .GetComponent<GameController>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (number != prevNumber)
        {
            this.spriteRenderer.sprite = this.gameController.GetNumberSprite(number);
        }
    }
}

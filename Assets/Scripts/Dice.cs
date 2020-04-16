using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dice : MonoBehaviour
{
    public DiceRoleedEvent OnDiceRolled = new DiceRoleedEvent();
    public float DiceRoleDuration = 1.0f;
    private GameController gameController;
    private SpriteRenderer spriteRenderer;
    private bool rolling;
    private float currentDuration = 0.0f;

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
        if (this.rolling)
        {
            currentDuration += Time.deltaTime;
            var num = Random.Range(1, 7);
            SetDiceSprite(num);

            if (currentDuration >= DiceRoleDuration)
            {
                this.rolling = false;
                OnDiceRolled?.Invoke(num);
            }
        }
    }

    private void SetDiceSprite(int num)
    {
        this.spriteRenderer.sprite = this.gameController.getDiceSprite(num);
    }

    public void Roll()
    {
        if (this.rolling) return;
        this.rolling = true;
        this.currentDuration = 0.0f;
    }
}

[System.Serializable]
public class DiceRoleedEvent : UnityEvent<int> { }

using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Sprite[] numberSprites;
    public Sprite[] diceSprites;
    private PlayerCamera playerCamera;
    private Player player;
    private int currentTileIndex = 0;
    private Dice dice;
    private Board board;

    // Start is called before the first frame update
    void Start()
    {
        this.playerCamera = GameObject
            .FindGameObjectWithTag("MainCamera")
            .GetComponent<PlayerCamera>();
        this.player = GameObject
            .FindGameObjectWithTag("Player")
            .GetComponent<Player>();
        this.dice = GameObject
            .FindGameObjectWithTag("Dice")
            .GetComponent<Dice>();
        this.dice.OnDiceRolled.AddListener(OnDiceRoled);
        this.board = GameObject
            .FindGameObjectWithTag("Board")
            .GetComponent<Board>();
        this.playerCamera.SetTargetPlayer(player);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            dice.Roll();
        }
    }

    public void OnDiceRoled(int num)
    {
        this.currentTileIndex += num;
        var nextPosition = this.board.GetTilePositionByIndex(this.currentTileIndex);
        this.player.MoveTo(nextPosition);
    }

    public Sprite getNumberSprite(int number) {
        return this.numberSprites[number];
    }

    public Sprite getDiceSprite(int number)
    {
        return this.diceSprites[number - 1];
    }
}

using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject playerPrehub;
    public Sprite[] numberSprites;
    public Sprite[] diceSprites;
    private UIController uiController;
    private PlayerCamera playerCamera;
    private List<Player> players = new List<Player>();
    private int currentPlayerIndex = -1;
    private List<int> currentTileIndexes = new List<int>();
    private Dice dice;
    private Board board;

    // Start is called before the first frame update
    void Start()
    {
        this.uiController = GameObject
            .FindGameObjectWithTag("UIController")
            .GetComponent<UIController>();
        this.playerCamera = GameObject
            .FindGameObjectWithTag("MainCamera")
            .GetComponent<PlayerCamera>();
        this.dice = GameObject
            .FindGameObjectWithTag("Dice")
            .GetComponent<Dice>();
        this.dice.OnDiceRolled.AddListener(OnDiceRoled);
        this.board = GameObject
            .FindGameObjectWithTag("Board")
            .GetComponent<Board>();

        for(int id=0; id<3; id++)
        {
            var playerObj = Instantiate(playerPrehub);
            playerObj.transform.parent = this.transform;
            playerObj.name = $"Player{id}";
            var player = playerObj.GetComponent<Player>();
            player.id = id;
            player.nickname = $"Player{id}";
            player.OnReached.AddListener(this.OnPlayerReachedTargetTile);
            this.players.Add(player);
            this.currentTileIndexes.Add(0);
        }

        ActiveteNextPlayer();
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
        this.currentTileIndexes[this.currentPlayerIndex] += num;
        var nextPosition = this.board.GetTilePositionByIndex(this.currentTileIndexes[this.currentPlayerIndex]);
        this.players[this.currentPlayerIndex].MoveTo(nextPosition);
    }

    public void OnPlayerReachedTargetTile(Player player)
    {
        ActiveteNextPlayer();
    }

    private void ActiveteNextPlayer()
    {
        this.currentPlayerIndex = (this.currentPlayerIndex + 1) % this.players.Count;
        this.playerCamera.SetTargetPlayer(this.players[this.currentPlayerIndex]);
        this.uiController.SetTurnText(this.players[this.currentPlayerIndex].nickname);
    }

    public Sprite getNumberSprite(int number) {
        return this.numberSprites[number];
    }

    public Sprite getDiceSprite(int number)
    {
        return this.diceSprites[number - 1];
    }
}

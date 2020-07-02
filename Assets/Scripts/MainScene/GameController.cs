using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GameController : MonoBehaviour
{
    public GameObject playerPrehub;
    public Sprite[] numberSprites;
    public Sprite[] diceSprites;
    public int playerNum = 1;
    public int initialPlayerAge = 50;
    public int initialParentAge = 75;
    public int agePerDiceRoll = 2;
    public int parentDeadAge = 95;
    private UIController uiController;
    private PlayerStatusWindow playerStatusWindow;
    private PlayerCamera playerCamera;
    private List<Player> players = new List<Player>();
    private int currentPlayerIndex = -1;
    private List<int> currentTileIndexes = new List<int>();
    private Dice dice;
    private Board board;
    private bool diceRoleAllowed = true;
    private Event executingEvent = null;
    private bool isGameEndFired = false;

    // Start is called before the first frame update
    void Start()
    {
        this.uiController = GameObject
            .FindGameObjectWithTag("UIController")
            .GetComponent<UIController>();
        this.playerStatusWindow = GameObject
            .FindGameObjectWithTag("PlayerStatusWindow")
            .GetComponent<PlayerStatusWindow>();
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

        for(int id=0; id<playerNum; id++)
        {
            var playerObj = Instantiate(playerPrehub);
            playerObj.transform.parent = this.transform;
            playerObj.name = $"Player{id}";
            var player = playerObj.GetComponent<Player>();
            player.id = id;
            player.nickname = $"Player{id}";
            player.OnReached.AddListener(this.OnPlayerReachedTargetTile);
            player.OnStatusUpdated.AddListener(this.OnPlayerStatusUpdate);
            player.Age = initialPlayerAge - agePerDiceRoll;
            player.ParentAge = initialParentAge - agePerDiceRoll;
            this.players.Add(player);
            this.currentTileIndexes.Add(0);
        }

        ActiveteNextPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameEnded())
        {
            if (!isGameEndFired)
            {
                isGameEndFired = true;
                OnGameEnd();
            }
            return;
        }

        if (diceRoleAllowed && Input.GetMouseButton(0))
        {
            dice.Roll();
            diceRoleAllowed = false;
        }
    }

    public bool IsGameEnded() {
        foreach(Player player in this.players)
        {
            if (player.ParentAge < parentDeadAge)
            {
                return false;
            }
        }
        return true;
    }

    private void OnGameEnd()
    {
        StartCoroutine(
            GenerateSpecificEvent(EventType.SYSTEM, "GameEnd.script", evt =>
            {
                StartCoroutine(evt.Exec(GoToResultScene));
            })
        );
    }

    private void GoToResultScene()
    {
        SceneManager.LoadScene("ResultScene");
    }

    public void OnDiceRoled(int num)
    {
        this.currentTileIndexes[this.currentPlayerIndex] += num;
        var nextPosition = this.board.GetTilePositionByIndex(this.currentTileIndexes[this.currentPlayerIndex]);
        this.players[this.currentPlayerIndex].MoveTo(nextPosition);
    }

    public void OnPlayerReachedTargetTile(Player player)
    {
        Tile targetTile = board.GetTileByIndex(this.currentTileIndexes[this.currentPlayerIndex]);
        StartCoroutine(
            GenerateRandomEvent(targetTile.eventType, evt => {
                this.executingEvent = evt;
                StartCoroutine(this.executingEvent.Exec(ActiveteNextPlayer));
            })
        );
    }

#if UNITY_WEBGL
    private static readonly string GET_RANDOM_EVENT_SCRIPT = "http://google.com";
#endif

    public IEnumerator GenerateRandomEvent(EventType eventType, Action<Event> eventHandler)
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        Debug.Log("GenerateRandomEvent");
        using (var req = UnityWebRequest.Get("http://google.com"))
        {
            Debug.Log("Request start");
            yield return req.SendWebRequest();
            Debug.Log("Request end");
            if (req.isNetworkError || req.isHttpError)
            {
                throw new Exception("HTTPリクエストエラー");
            }
            GameObject gameObject = new GameObject("Event");
            Event evt = gameObject.AddComponent<Event>();
            evt.InitFromScriptText(req.downloadHandler.text);
            eventHandler(evt);            
        }
#else
        Debug.Log($"{Application.dataPath}/Datas/Events/{eventType.GetDirectoryName()}");
        DirectoryInfo dir = new DirectoryInfo($"{Application.dataPath}/Datas/Events/{eventType.GetDirectoryName()}");
        FileInfo[] files = dir.GetFiles("*.script");
        int index = UnityEngine.Random.Range(0, files.Length);
        GameObject gameObject = new GameObject("Event");
        Event evt = gameObject.AddComponent<Event>();
        evt.Init(eventType, files[index].Name);
        eventHandler(evt);
        yield return null;
#endif
    }

    public IEnumerator GenerateSpecificEvent(EventType eventType, string eventFileName, Action<Event> eventHandler)
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        Debug.Log("GenerateRandomEvent");
        using (var req = UnityWebRequest.Get("http://google.com"))
        {
            Debug.Log("Request start");
            yield return req.SendWebRequest();
            Debug.Log("Request end");
            if (req.isNetworkError || req.isHttpError)
            {
                throw new Exception("HTTPリクエストエラー");
            }
            GameObject gameObject = new GameObject("Event");
            Event evt = gameObject.AddComponent<Event>();
            evt.InitFromScriptText(req.downloadHandler.text);
            eventHandler(evt); 
        }
#else
        GameObject gameObject = new GameObject("Event");
        Event evt = gameObject.AddComponent<Event>();
        evt.Init(eventType, eventFileName);
        eventHandler(evt);
        yield return null;
#endif
    }

    public void OnPlayerStatusUpdate(Player player)
    {
        if (player.id == this.currentPlayerIndex)
        {
            this.playerStatusWindow.SetPlayer(player);
        }
    }

    private void ActiveteNextPlayer()
    {
        this.currentPlayerIndex = (this.currentPlayerIndex + 1) % this.players.Count;
        this.players[this.currentPlayerIndex].AddAge(agePerDiceRoll);
        this.players[this.currentPlayerIndex].AddParentAge(agePerDiceRoll);
        this.playerCamera.SetTargetPlayer(this.players[this.currentPlayerIndex]);
        this.uiController.SetTurnText(this.players[this.currentPlayerIndex].nickname);
        this.playerStatusWindow.SetPlayer(this.players[this.currentPlayerIndex]);
        this.diceRoleAllowed = true;
        if (this.executingEvent != null)
        {
            Destroy(this.executingEvent.gameObject);
        }
    }

    public Sprite GetNumberSprite(int number) {
        return this.numberSprites[number];
    }

    public Sprite GetDiceSprite(int number)
    {
        return this.diceSprites[number - 1];
    }

    public Player GetCurrentPlayer()
    {
        return this.players[this.currentPlayerIndex];
    }

    public List<Player> GetPlayers()
    {
        return this.players;
    }
}

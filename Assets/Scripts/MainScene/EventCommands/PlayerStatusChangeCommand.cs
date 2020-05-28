using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStatusChangeCommand : EventCommand
{
    private GameController gameController;
    private static List<String> validPlayer = new List<string>
    {
        "CurrentPlayer",
        "RandomPlayer"
    };
    private static List<String> validStatus = new List<string>
    {
        "QoL",
        "Money",
        "Time"
    };

    // Start is called before the first frame update
    public PlayerStatusChangeCommand(params string[] args) : base(args)
    {
        this.gameController = GameObject
            .FindGameObjectWithTag("GameController")
            .GetComponent<GameController>();
    }

    public override IEnumerator Exec()
    {
        if (this.args.Length != 3)
        {
            throw new ArgumentException("引数が3つ必要です");
        }

        string targetPlayer = this.args[0];
        if (!validPlayer.Contains(targetPlayer)) {
            throw new ArgumentException(
                "第1引数は次の値のいずれかを入力してください: "
                + string.Join("/", validPlayer)
            );
        }

        string targetStatus = this.args[1];
        if (!validStatus.Contains(targetStatus))
        {
            throw new ArgumentException(
                "第2引数は次の値のいずれかを入力してください: "
                + string.Join("/", validStatus)
            );
        }

        if (!int.TryParse(this.args[2], out int value))
        {
            throw new ArgumentException("第3引数は数値を入力してください");
        }

        Player player = targetPlayer == "CurrentPlayer" ?
            gameController.GetCurrentPlayer() :
            gameController.GetPlayers()[UnityEngine.Random.Range(0, gameController.GetPlayers().Count)];
        switch(targetStatus)
        {
            case "QoL":
                player.AddQoL(value);
                break;
            case "Money":
                player.AddMoney(value);
                break;
            case "Time":
                player.AddTime(value);
                break;
        }
        yield return null;
    }
}

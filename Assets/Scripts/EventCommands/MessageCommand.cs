using UnityEngine;

public class MessageCommand : EventCommand
{
    public MessageCommand(params string[] args): base(args) {}

    public override void Exec()
    {
        string message = (string)args[0];
        Debug.Log($"show message: {message}"); // TODO: メッセージウィンドウに表示
    }
}

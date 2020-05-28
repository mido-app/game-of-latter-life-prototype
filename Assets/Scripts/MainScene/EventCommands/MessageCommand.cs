using System.Collections;
using UnityEngine;

public class MessageCommand : EventCommand
{
    private MessageWindow messageWindowController;

    public MessageCommand(params string[] args): base(args) {
        this.messageWindowController = GameObject
            .FindGameObjectWithTag("MessageWindow")
            .GetComponent<MessageWindow>();
    }

    public override IEnumerator Exec()
    {
        string message = (string)args[0];
        yield return this.messageWindowController.ShowMessage(message);
    }
}

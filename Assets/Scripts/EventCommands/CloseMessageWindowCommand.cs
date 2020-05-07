using System.Collections;
using UnityEngine;

public class CloseMessageWindowCommand : EventCommand
{
    private MessageWindow messageWindowController;

    public CloseMessageWindowCommand(params string[] args) : base(args)
    {
        this.messageWindowController = GameObject
            .FindGameObjectWithTag("MessageWindow")
            .GetComponent<MessageWindow>();
    }


    public override IEnumerator Exec()
    {
        this.messageWindowController.Close();
        yield return null;
    }
}

using System.Collections;
using UnityEngine;

public class OpenMessageWindowCommand : EventCommand
{
    private MessageWindow messageWindowController;

    public OpenMessageWindowCommand(params string[] args) : base(args)
    {
        this.messageWindowController = GameObject
            .FindGameObjectWithTag("MessageWindow")
            .GetComponent<MessageWindow>();
    }


    public override IEnumerator Exec()
    {
        this.messageWindowController.Open();
        yield return null;
    }
}

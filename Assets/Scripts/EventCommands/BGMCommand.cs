using UnityEngine;

public class BGMCommand : EventCommand
{
    public BGMCommand(params string[] args): base(args) { }

    public override void Exec() {
        Debug.Log($"start bgm: {args[0]}");
    }
}

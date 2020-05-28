using System.Collections;
using UnityEngine;

public class BGMCommand : EventCommand
{
    public BGMCommand(params string[] args): base(args) { }

    public override IEnumerator Exec() {
        Debug.Log($"start bgm: {args[0]}");
        yield return null;
    }
}

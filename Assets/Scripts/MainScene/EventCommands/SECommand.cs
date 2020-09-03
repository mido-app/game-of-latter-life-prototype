using System.Collections;
using UnityEngine;

public class SECommand : EventCommand
{
    public SECommand(params string[] args) : base(args)
    {
    }

    public override IEnumerator Exec()
    {
        AudioController.Instance.PlaySE(args[0]);
        yield return null;
    }
}

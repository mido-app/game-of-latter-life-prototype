using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class EventCommand
{
    protected string[] args;

    public EventCommand(params string[] args)
    {
        this.args = args;
    }
    public abstract void Exec();
}

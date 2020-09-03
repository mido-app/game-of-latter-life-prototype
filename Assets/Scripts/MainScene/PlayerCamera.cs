using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Player target;

    void Start()
    {

    }

    void Update()
    {
        if (target != null
            && this.transform.position != target.transform.position)
        {
            this.transform.position = new Vector3(
                target.transform.position.x,
                target.transform.position.y,
                this.transform.position.z
            );
        }

    }

    public void SetTargetPlayer(Player player)
    {
        this.target = player;
    }
}

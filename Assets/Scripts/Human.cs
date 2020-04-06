using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    private float velosity = 0.1f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Space button pushed!");
            velosity *= -1.0f;
        }

        this.transform.position = new Vector2(
            this.transform.position.x + velosity,
            this.transform.position.y
        );

    }
}

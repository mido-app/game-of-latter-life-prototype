using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 targetPorision;
    public float moveSpeed = 3.0f;
    public bool Moving
    {
        get
        {
            return this.targetPorision != null;
        }
    }

    private void Start()
    {
        this.targetPorision = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.Moving && this.transform.position != targetPorision)
        {
            var nextPosition = Vector2.MoveTowards(
                this.transform.position,
                this.targetPorision,
                this.moveSpeed * Time.deltaTime
            );
            this.transform.position = new Vector3(
                nextPosition.x,
                nextPosition.y,
                this.transform.position.z
            );
        }
    }

    public void MoveTo(Vector3 position)
    {
        this.targetPorision = position;
    }
}

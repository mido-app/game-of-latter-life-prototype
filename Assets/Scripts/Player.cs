using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public int id;
    public string nickname;
    public float moveSpeed = 3.0f;
    public PlayerReachedTargetTileEvent OnReached = new PlayerReachedTargetTileEvent();
    private bool moving = false;
    private Vector3 targetPorision;

    private void Start()
    {
        this.targetPorision = this.transform.position;
        this.GetComponent<SpriteRenderer>().color = new Color(
            Random.Range(0, 256) / 255.0f,
            Random.Range(0, 256) / 255.0f,
            Random.Range(0, 256) / 255.0f
        );
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.moving) return;
        if (!this.IsReachTarget())
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
        } else
        {
            this.moving = false;
            OnReached?.Invoke(this);
        }
    }

    public void MoveTo(Vector3 position)
    {
        this.targetPorision = position;
        this.moving = true;
    }

    private bool IsReachTarget()
    {
        return this.transform.position.x == targetPorision.x
            && this.transform.position.y == targetPorision.y;
    }
}

public class PlayerReachedTargetTileEvent : UnityEvent<Player> {}

using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public int id;
    public string nickname;
    public float moveSpeed = 3.0f;
    public PlayerReachedTargetTileEvent OnReached = new PlayerReachedTargetTileEvent();
    public PlayerStatusUpdatedEvent OnStatusUpdated = new PlayerStatusUpdatedEvent();
    private bool moving = false;
    private Vector3 targetPorision;

    // ステータス
    public int QoL { get; set; }
    public int Money { get; set; }
    public int Time { get; set; }
    public int Age { get; set; }
    public int ParentAge { get; set; }

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
                this.moveSpeed * UnityEngine.Time.deltaTime
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

    public void AddQoL(int value)
    {
        int newValue = QoL + value;
        if (newValue < 0) this.QoL = 0;
        else this.QoL = newValue;
        OnStatusUpdated?.Invoke(this);
    }

    public void AddMoney(int value)
    {
        int newValue = Money + value;
        if (newValue < 0) this.QoL = 0;
        else this.Money = newValue;
        OnStatusUpdated?.Invoke(this);
    }

    public void AddTime(int value)
    {
        int newValue = Time + value;
        if (newValue < 0) this.QoL = 0;
        else this.Time = newValue;
        OnStatusUpdated?.Invoke(this);
    }

    public void AddAge(int value)
    {
        this.Age += value;
        OnStatusUpdated?.Invoke(this);
    }

    public void AddParentAge(int value)
    {
        this.ParentAge += value;
        OnStatusUpdated?.Invoke(this);
    }
}

public class PlayerReachedTargetTileEvent : UnityEvent<Player> {}
public class PlayerStatusUpdatedEvent : UnityEvent<Player> { }

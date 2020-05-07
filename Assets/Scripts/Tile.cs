using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int number;
    public EventType eventType;
    private List<Number> numbers = new List<Number>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in this.transform)
        {
            this.numbers.Add(child.gameObject.GetComponent<Number>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.numbers[0].number = this.number / 100;
        this.numbers[1].number = (this.number % 100) / 10;
        this.numbers[2].number = this.number % 10;
    }
}

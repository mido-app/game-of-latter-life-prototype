using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject tilePrehub;
    public List<Tile> tiles;

    // Start is called before the first frame update
    void Start()
    {
        for (int y = 0; y < 100; y++)
        {
            var tile = CreateTile();
            tile.number = UnityEngine.Random.Range(0, 1000);
            tile.eventType = GetRandomEventType();
            tile.transform.position = new Vector3(0, y, 0);
            this.tiles.Add(tile);
        }
    }

    public Tile GetTileByIndex(int index) {
        return tiles[index];
    }

    public Vector2 GetTilePositionByIndex(int index)
    {
        return this.tiles[index].transform.position;
    }

    private Tile CreateTile() {
        var tileObj = GameObject.Instantiate(tilePrehub);
        tileObj.transform.parent = this.transform;
        return tileObj.GetComponent<Tile>();
    }

    private EventType GetRandomEventType() {
        List<EventType> eventTypes= Enum
            .GetValues(typeof(EventType))
            .Cast<EventType>()
            .AsEnumerable()
            .Where(t => t != EventType.SYSTEM)
            .ToList();
        int index = UnityEngine.Random.Range(0, eventTypes.Count);
        return eventTypes[index];
    }
}

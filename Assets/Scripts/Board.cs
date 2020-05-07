using System.Collections;
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
            var tile = createTile();
            tile.number = Random.Range(0, 1000);
            tile.transform.position = new Vector3(0, y, 0);
            this.tiles.Add(tile);
        }
    }

    public Vector2 GetTilePositionByIndex(int index)
    {
        return this.tiles[index].transform.position;
    }

    private Tile createTile()
    {
        var tileObj = GameObject.Instantiate(tilePrehub);
        tileObj.transform.parent = this.transform;
        return tileObj.GetComponent<Tile>();
    }
}

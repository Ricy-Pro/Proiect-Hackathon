using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase goodTile;
    public TileBase badTile;
    public Color goodTileColor = Color.green;
    public Color badTileColor = Color.red;

    void Start()
    {
        int mapSize = 50;
        int roadWidth = 3; // 3x3 bad tile roads
        int goodSquareSize = 5;
        Vector3Int middle = new Vector3Int(mapSize / 2, mapSize / 2, 0);

        // Loop through the 50x50 map and set tiles
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                Vector3Int currentPos = new Vector3Int(x, y, 0);

                if (IsInsideGoodSquare(currentPos, middle, goodSquareSize))
                {
                    SetTile(currentPos, goodTile, goodTileColor);
                }
                else if (IsInsideBadRoad(currentPos, middle, mapSize, roadWidth))
                {
                    SetTile(currentPos, badTile, badTileColor);
                }
                else
                {
                    SetTile(currentPos, goodTile, goodTileColor); // Default to good tile
                }
            }
        }
    }

    // Check if the position is inside the 5x5 good square in the center
    bool IsInsideGoodSquare(Vector3Int position, Vector3Int middle, int size)
    {
        int startX = middle.x - size / 2;
        int startY = middle.y - size / 2;
        return position.x >= startX && position.x < startX + size && position.y >= startY && position.y < startY + size;
    }

    // Check if the position is inside one of the 3x3 bad roads
    bool IsInsideBadRoad(Vector3Int position, Vector3Int middle, int mapSize, int roadWidth)
    {
        // North road (starts from the middle and extends upwards)
        bool isNorthRoad = position.x >= middle.x - roadWidth / 2 && position.x <= middle.x + roadWidth / 2 && position.y > middle.y && position.y < mapSize;
        
        // South road (starts from the middle and extends downwards)
        bool isSouthRoad = position.x >= middle.x - roadWidth / 2 && position.x <= middle.x + roadWidth / 2 && position.y < middle.y && position.y > 0;
        
        // East road (starts from the middle and extends to the right)
        bool isEastRoad = position.y >= middle.y - roadWidth / 2 && position.y <= middle.y + roadWidth / 2 && position.x > middle.x && position.x < mapSize;
        
        // West road (starts from the middle and extends to the left)
        bool isWestRoad = position.y >= middle.y - roadWidth / 2 && position.y <= middle.y + roadWidth / 2 && position.x < middle.x && position.x > 0;
        
        // Return true if any road condition is met
        return isNorthRoad || isSouthRoad || isEastRoad || isWestRoad;
    }

    // Set tile and color for the given position
    void SetTile(Vector3Int position, TileBase tile, Color color)
    {
        tilemap.SetTile(position, tile);
        tilemap.SetColor(position, color); // Set color of the tile
    }
}

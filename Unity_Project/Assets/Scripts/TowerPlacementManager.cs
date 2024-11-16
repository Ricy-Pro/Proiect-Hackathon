using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerPlacementManager : MonoBehaviour
{
    public GameObject towerPrefab;  // Reference to the tower prefab
    public Tilemap tilemap;  // Reference to the Tilemap component
    public TileBase goodTile;  // Reference to the good tile used in the map
    public float gridSize = 1f;  // Size of the grid

    private GameObject currentTower;  // The tower object being placed
    private SpriteRenderer towerRenderer;  // Reference to the sprite renderer of the current tower

    void Update()
    {
        HandleTowerPlacement();
    }

    void HandleTowerPlacement()
    {
        if (currentTower == null)
        {
            // Start placing a tower when the left mouse button is clicked
            if (Input.GetMouseButtonDown(0))
            {
                StartPlacingTower();
            }
        }
        else
        {
            Vector3 placementPos = GetMouseWorldPosition();
            placementPos = SnapToGrid(placementPos);  // Snap to grid position
            currentTower.transform.position = placementPos;

            // Check if the placement is valid (whether the position is on the "good" tile)
            bool isValidPlacement = IsValidPlacement(placementPos);

            // Change the tower color based on whether placement is valid
            towerRenderer.color = isValidPlacement ? new Color(0, 1, 0, 0.5f) : new Color(1, 0, 0, 0.5f);

            // Place or cancel the tower placement when left-clicked
            if (Input.GetMouseButtonDown(0))
            {
                if (isValidPlacement)
                {
                    PlaceTower();
                }
                else
                {
                    // Destroy the tower if it's an invalid placement
                    Destroy(currentTower);
                    currentTower = null;  // Reset currentTower so a new one can be placed
                }
            }
        }
    }

    void StartPlacingTower()
    {
        // Create a new tower and start placing it
        currentTower = Instantiate(towerPrefab);
        towerRenderer = currentTower.GetComponent<SpriteRenderer>();  // Get the sprite renderer for color change
        towerRenderer.color = new Color(0, 1, 0, 0.5f);  // Set color to semi-transparent green for preview
    }

    void PlaceTower()
    {
        // Finalize the placement of the tower
        towerRenderer.color = new Color(1, 1, 1, 1);  // Set the tower to full opacity when placed
        currentTower = null;  // Reset the current tower (so player can place another tower)
    }

    Vector3 GetMouseWorldPosition()
    {
        // Convert mouse position to world space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;  // Ensure the z-position is 0 (2D)
        return mousePos;
    }

    Vector3 SnapToGrid(Vector3 worldPosition)
    {
        // Snap the position to the grid and align it to the center of the grid cell
        float x = Mathf.Floor(worldPosition.x / gridSize) * gridSize + gridSize / 2f;
        float y = Mathf.Floor(worldPosition.y / gridSize) * gridSize + gridSize / 2f;
        return new Vector3(x, y, 0f);
    }

    bool IsValidPlacement(Vector3 position)
    {
        // Get the tile at the snapped position in the Tilemap
        Vector3Int tilePosition = tilemap.WorldToCell(position);
        TileBase tile = tilemap.GetTile(tilePosition);

        // Check if the tile is the good tile
        return tile != null && tile == goodTile;  // Valid placement only on good tiles
    }
}

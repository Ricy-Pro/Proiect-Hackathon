using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerPlacementManager : MonoBehaviour
{
    public Tilemap tilemap;         // Reference to the Tilemap component
    public TileBase goodTile;       // Reference to the good tile used in the map
    public float gridSize = 1f;     // Size of the grid

    private GameObject currentTower;
    private SpriteRenderer towerRenderer;
    private HashSet<Vector3Int> occupiedTiles;

    private GameObject towerToPlace; // Tower prefab set by the shop
    private bool isTowerReadyToPlace = false; // Flag to indicate readiness for placement

    void Start()
    {
        occupiedTiles = new HashSet<Vector3Int>();
    }

    void Update()
    {
        HandleTowerPlacement();
    }

    public void SetTowerToPlace(GameObject prefab)
    {
        towerToPlace = prefab;
        isTowerReadyToPlace = true; // Mark the tower as ready to be placed
    }

    void HandleTowerPlacement()
    {
        if (!isTowerReadyToPlace) return; // Skip placement logic if no tower is ready

        if (currentTower == null)
        {
            // Start placing a tower when the left mouse button is clicked
            if (Input.GetMouseButtonDown(0) && towerToPlace != null)
            {
                StartPlacingTower();
            }
        }
        else
        {
            Vector3 placementPos = GetMouseWorldPosition();
            placementPos = SnapToGrid(placementPos);
            currentTower.transform.position = placementPos;

            bool isValidPlacement = IsValidPlacement(placementPos);

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
                    currentTower = null;
                }
            }
        }
    }

    void StartPlacingTower()
    {
        // Instantiate the selected tower prefab
        currentTower = Instantiate(towerToPlace);
        towerRenderer = currentTower.GetComponent<SpriteRenderer>();
        towerRenderer.color = new Color(0, 1, 0, 0.5f); // Semi-transparent green for preview
    }

    void PlaceTower()
    {
        Vector3Int tilePosition = tilemap.WorldToCell(currentTower.transform.position);
        occupiedTiles.Add(tilePosition);

        towerRenderer.color = new Color(1, 1, 1, 1); // Full opacity once placed
        currentTower = null;
        isTowerReadyToPlace = false; // Reset the placement flag
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        return mousePos;
    }

    Vector3 SnapToGrid(Vector3 worldPosition)
    {
        float x = Mathf.Floor(worldPosition.x / gridSize) * gridSize + gridSize / 2f;
        float y = Mathf.Floor(worldPosition.y / gridSize) * gridSize + gridSize / 2f;
        return new Vector3(x, y, 0f);
    }

    bool IsValidPlacement(Vector3 position)
    {
        Vector3Int tilePosition = tilemap.WorldToCell(position);
        TileBase tile = tilemap.GetTile(tilePosition);
        return tile != null && tile == goodTile && !occupiedTiles.Contains(tilePosition);
    }
}

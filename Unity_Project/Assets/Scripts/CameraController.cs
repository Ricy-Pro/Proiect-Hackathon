using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed = 10f;      // How fast the camera zooms in/out
    public float minZoom = 5f;        // Minimum zoom level
    public float maxZoom = 10f;       // Maximum zoom level (reduced to zoom out less)

    public float panSpeed = 20f;      // How fast the camera pans (for keyboard input)
    public int mapSize = 50;          // The size of the map (assumes a square map)
    public float gridCellSize = 1f;   // The size of each grid cell in the Tilemap

    public int margin = 1;            // Number of tiles to leave around the map (camera's bounding box)

    private float halfMapSize;        // Half the map size for easy calculations
    private Vector3 dragOrigin;       // Origin point for dragging the camera

    void Start()
    {
        // Center the camera on the map, but with a margin for the camera’s bounding box
        halfMapSize = mapSize * gridCellSize / 2f;
        transform.position = new Vector3(halfMapSize, halfMapSize, -10f);

        // Adjust the camera's orthographic size to fit the whole map
        float initialZoom = maxZoom / 2f;
        Camera.main.orthographicSize = Mathf.Clamp(initialZoom, minZoom, maxZoom);
    }

    void Update()
    {
        HandleZoom();
        HandlePan();
        HandleDrag();
    }

    void HandleZoom()
    {
        // Adjust the camera's orthographic size based on scroll input
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize -= scroll * zoomSpeed;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);
    }

    void HandlePan()
    {
        Vector3 pos = transform.position;

        // Pan the camera with arrow keys or WASD
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            pos.y += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            pos.y -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            pos.x -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            pos.x += panSpeed * Time.deltaTime;
        }

        // Adjust the camera's position with margin
        float adjustedMapSize = mapSize * gridCellSize + margin * gridCellSize;

        // Clamp the camera position to stay within the adjusted bounds (with margin)
        pos.x = Mathf.Clamp(pos.x, margin * gridCellSize, adjustedMapSize - margin * gridCellSize);
        pos.y = Mathf.Clamp(pos.y, margin * gridCellSize, adjustedMapSize - margin * gridCellSize);

        transform.position = pos;
    }

    void HandleDrag()
    {
        // Start dragging on mouse button down
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        // Drag the camera while holding the mouse button
        if (Input.GetMouseButton(0)) // Left mouse button
        {
            Vector3 difference = dragOrigin - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPosition = transform.position + difference;

            // Adjust the camera's position with margin
            float adjustedMapSize = mapSize * gridCellSize + margin * gridCellSize;

            // Clamp the new position to the map bounds (with margin)
            newPosition.x = Mathf.Clamp(newPosition.x, margin * gridCellSize, adjustedMapSize - margin * gridCellSize);
            newPosition.y = Mathf.Clamp(newPosition.y, margin * gridCellSize, adjustedMapSize - margin * gridCellSize);

            transform.position = newPosition;
        }
    }
}

using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed = 10f;      // How fast the camera zooms in/out
    public float minZoom = 5f;        // Minimum zoom level
    public float maxZoom = 50f;       // Maximum zoom level

    public float panSpeed = 20f;      // How fast the camera pans (for keyboard input)
    public int mapSize = 50;          // The size of the map (assumes a square map)
    public float gridCellSize = 1f;   // The size of each grid cell in the Tilemap

    private float halfMapSize;        // Half the map size for easy calculations
    private Vector3 dragOrigin;       // Origin point for dragging the camera

    void Start()
    {
        // Center the camera on the map
        halfMapSize = mapSize * gridCellSize / 2f;
        transform.position = new Vector3(halfMapSize, halfMapSize, -10f);

        // Adjust the camera's orthographic size to fit the whole map
        float initialZoom = mapSize / 2f;
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

        // Clamp the camera position to stay within the map bounds
        pos.x = Mathf.Clamp(pos.x, 0, mapSize * gridCellSize);
        pos.y = Mathf.Clamp(pos.y, 0, mapSize * gridCellSize);

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

            // Clamp the new position to the map bounds
            newPosition.x = Mathf.Clamp(newPosition.x, 0, mapSize * gridCellSize);
            newPosition.y = Mathf.Clamp(newPosition.y, 0, mapSize * gridCellSize);

            transform.position = newPosition;
        }
    }
}
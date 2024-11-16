using UnityEngine;

public class CastleInteraction : MonoBehaviour
{
    public GameObject miniGamePanel; // Reference to the mini-game panel

    void OnMouseDown()
    {
        // Check if the miniGamePanel is assigned
        if (miniGamePanel != null)
        {
            miniGamePanel.SetActive(true); // Show the mini-game panel
        }
        else
        {
            Debug.LogWarning("MiniGamePanel is not assigned in the Inspector!");
        }
    }
}

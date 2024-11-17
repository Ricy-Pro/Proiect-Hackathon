using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public GameObject king;  // Reference to the king object
    public  TextMeshProUGUI timerText; // UI element to display the timer
    private float timer;  // Timer variable to track the elapsed time

    void Start()
    {
        timer = 0f;  // Initialize the timer to 0
    }

    void Update()
    {
        if (king != null && king.activeInHierarchy)
        {
            timer += Time.deltaTime;
        }
        else
        {
            // Find the Timer object and move it to the specified position when the king dies
            GameObject timerObject = GameObject.FindGameObjectWithTag("Timer");
            if (timerObject != null)
            {
                // Move the Timer object to the new position
                timerObject.transform.position = new Vector3(-201.3f, -201.3f, 0);
               
            }
        }

        // Update the timer text (if the king is alive)
        if (king != null && king.activeInHierarchy)
        {
            timerText.text = "Time: " + Mathf.FloorToInt(timer).ToString();
        }
    }
}

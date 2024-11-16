using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance;
    public int gold = 0; // The player's current gold
    public int passiveGoldPerSecond = 1; // Gold generated per second

    // Event that will notify listeners whenever gold is updated
    public delegate void OnGoldChanged(int newGoldAmount);
    public static event OnGoldChanged GoldChanged;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        // Start passive gold generation (every second)
        InvokeRepeating("GenerateGold", 1f, 1f);
    }

    // Generate gold passively
    void GenerateGold()
    {
        gold += passiveGoldPerSecond;
        GoldChanged?.Invoke(gold); // Notify all listeners (like UI) that gold has changed
    }

    // Function to manually add gold (e.g., from killing monsters)
    public void AddGold(int amount)
    {
        gold += amount;
        GoldChanged?.Invoke(gold); // Notify listeners
    }
}

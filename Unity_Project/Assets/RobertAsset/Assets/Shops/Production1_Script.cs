using UnityEngine;

public class Production1_Script : MonoBehaviour
{
    // Building level
    public int level = 1;

    // Base money generated per level
    public int baseMoneyPerLevel = 100;

    // Time in seconds between money generation
    public float moneyGenerationInterval = 5f;

    // Current amount of money generated
    private int totalMoney = 0;

    void Start()
    {
        // Start generating money at regular intervals
        InvokeRepeating(nameof(GenerateMoney), moneyGenerationInterval, moneyGenerationInterval);
    }

    // Method to calculate and generate money
    void GenerateMoney()
    {
        // Calculate money based on the current level
        int moneyGenerated = baseMoneyPerLevel * level;

        // Add to the total money
        totalMoney += moneyGenerated;

        // Output money details to the console (for debugging)
        Debug.Log($"Building generated {moneyGenerated} money. Total Money: {totalMoney}");
    }

    // Method to level up the building
    public void LevelUp()
    {
        level++;
        Debug.Log($"Building leveled up! New Level: {level}");
    }

    // Method to get the total money (useful for other scripts)
    public int GetTotalMoney()
    {
        return totalMoney;
    }
}
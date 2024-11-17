using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager2 : MonoBehaviour
{
    // Singleton instance
    public static ShopManager2 Instance;

    public GameObject shopPanel;  // Reference to the Shop panel
    public GameObject shopItemButtonPrefab;  // Button prefab to instantiate
    public Transform shopContent;  // The content area to hold the buttons
    public Button closeButton;

    public TowerPlacementManager towerPlacementManager; // Reference to the TowerPlacementManager script

    [System.Serializable]
    public class ShopItem
    {
        public string itemName;  // Name of the item
        public int goldCost;     // Gold cost for the item
        public GameObject towerPrefab; // Prefab of the tower to be placed
    }

    public ShopItem[] shopItems = new ShopItem[]
    {
        new ShopItem { itemName = "Tower1", goldCost = 1, towerPrefab = null },  // Assign appropriate prefabs in the Inspector
        new ShopItem { itemName = "Tower2", goldCost = 2, towerPrefab = null },
        new ShopItem { itemName = "Tower3", goldCost = 3, towerPrefab = null },
    };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        shopPanel.SetActive(false);

        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseShop);
        }
        else
        {
            Debug.LogWarning("Close button is not assigned!");
        }

        PopulateShop();
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
    }

    private void PopulateShop()
    {
        // Clear existing buttons
        foreach (Transform child in shopContent)
        {
            Destroy(child.gameObject);
        }

        // Create new buttons for each shop item
        foreach (var item in shopItems)
        {
            GameObject button = Instantiate(shopItemButtonPrefab, shopContent);
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = $"{item.itemName} - {item.goldCost} Gold";

            button.GetComponent<Button>().onClick.AddListener(() => OnBuyItem(item));
        }
    }

    private void OnBuyItem(ShopItem item)
    {
        if (GoldManager.Instance.gold >= item.goldCost)
        {
            GoldManager.Instance.gold -= item.goldCost;
            Debug.Log($"Bought item: {item.itemName} for {item.goldCost} gold.");

            if (item.towerPrefab != null)
            {
                towerPlacementManager.SetTowerToPlace(item.towerPrefab);
            }
            else
            {
                Debug.LogError($"Tower prefab for {item.itemName} is not assigned!");
            }

            CloseShop(); // Close the shop after purchasing
        }
        else
        {
            Debug.Log("Not enough gold!");
        }
    }
}

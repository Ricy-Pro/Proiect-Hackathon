using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
public class ShopManager2 : MonoBehaviour
{
    public static ShopManager2 Instance;

    public GameObject shopPanel;
    public GameObject shopItemButtonPrefab;
    public Transform shopContent;
    public Button closeButton;

    public TowerPlacementManager towerPlacementManager;
    private int ok = 0;

    [System.Serializable]
    public class ShopItem
    {
        public string itemName;  
        public GameObject towerPrefab; 
        public InventoryManager.CodeBlockType[] requiredBlocks; // Blocks needed for this tower
    }
    [System.Serializable]
    public class ShopItem2
    {
        public string itemName;  
        public GameObject towerPrefab; 
        public int goldCost; 
    }

    public ShopItem[] shopItems = new ShopItem[]
    {
        new ShopItem { 
            itemName = "TwoFactorAuthenticator", 
            towerPrefab = null, 
            requiredBlocks = new InventoryManager.CodeBlockType[]
            {   InventoryManager.CodeBlockType.TwoFactorAuthenticationBlock1, 
                InventoryManager.CodeBlockType.TwoFactorAuthenticationBlock2,
                InventoryManager.CodeBlockType.TwoFactorAuthenticationBlock3
                
                
            }
        },
        new ShopItem { 
            itemName = "SQLInjection", 
            towerPrefab = null, 
            requiredBlocks = new InventoryManager.CodeBlockType[]
            {   InventoryManager.CodeBlockType.SQLInjectionBlock1,
                InventoryManager.CodeBlockType.SQLInjectionBlock2,
                InventoryManager.CodeBlockType.SQLInjectionBlock3,
                InventoryManager.CodeBlockType.SQLInjectionBlock4
                
            }
        },
        new ShopItem { 
            itemName = "DDoSProtection", 
            towerPrefab = null, 
            requiredBlocks = new InventoryManager.CodeBlockType[]
            {   InventoryManager.CodeBlockType.DDoSProtectionBlock1,
                InventoryManager.CodeBlockType.DDoSProtectionBlock2,
                InventoryManager.CodeBlockType.DDoSProtectionBlock3,
                InventoryManager.CodeBlockType.DDoSProtectionBlock4

            }
        },
        new ShopItem { 
            itemName = "FireWall", 
            towerPrefab = null, 
            requiredBlocks = new InventoryManager.CodeBlockType[]
            {
                InventoryManager.CodeBlockType.FirewallBlock1,
                InventoryManager.CodeBlockType.FirewallBlock2,
                InventoryManager.CodeBlockType.FirewallBlock3,
                InventoryManager.CodeBlockType.FirewallBlock4
            }
        }
    };
    
    public ShopItem2[] shopItems2 = new ShopItem2[]
    {   
        new ShopItem2 { 
            itemName = "Generator-TwoFactorAuthenticator", 
            towerPrefab = null, 
            goldCost = 1
        },
        new ShopItem2 { 
            itemName = "Generator-SQLInjection", 
            towerPrefab = null, 
            goldCost = 1
        },
        new ShopItem2 { 
            itemName = "Generator-DDoSProtection", 
            towerPrefab = null, 
            goldCost = 1
        },
        new ShopItem2 { 
            itemName = "Generator-FireWall", 
            towerPrefab = null, 
            goldCost = 1
        }

    };
    

    private void Awake()
    {
        shopPanel.SetActive(false);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

       // DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (ok != 0)
        {ReassignFields();
        ok=1;}
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

private void ReassignFields()
    {
        if (shopPanel == null)
        {
            shopPanel = GameObject.Find("ShopPanel");
            if (shopPanel == null) Debug.LogWarning("Shop Panel is not found!");
        }

        if (shopItemButtonPrefab == null)
        {
            shopItemButtonPrefab = Resources.Load<GameObject>("ShopItemButtonPrefab");  // Ensure the prefab is loaded
            if (shopItemButtonPrefab == null) Debug.LogWarning("Shop Item Button Prefab is not found!");
        }

        if (shopContent == null)
        {
            shopContent = shopPanel ? shopPanel.transform.Find("ShopContent") : null;
            if (shopContent == null) Debug.LogWarning("Shop Content is not found!");
        }

        if (closeButton == null)
        {
            closeButton = shopPanel ? shopPanel.GetComponentInChildren<Button>() : null;
            if (closeButton == null) Debug.LogWarning("Close Button is not found!");
        }

        if (towerPlacementManager == null)
        {
            towerPlacementManager = FindObjectOfType<TowerPlacementManager>();
            if (towerPlacementManager == null) Debug.LogWarning("Tower Placement Manager is not found!");
        }
        shopPanel.SetActive(false);
    }
    public void CloseShop()
    {
        ShopStopper.IsShopOpen = false;
        shopPanel.SetActive(false);
    }

    public void OpenShop()
    {
        if (ShopStopper.IsShopOpen)
        {
            // Prevent opening this shop if another is already open
            Debug.Log("Another shop is already open!");
            return;
        }

        // Open this shop and block others
        ShopStopper.IsShopOpen = true;
        shopPanel.SetActive(true);
    }

    private void PopulateShop()
    {
        foreach (Transform child in shopContent)
        {
            Destroy(child.gameObject);
        }
        

        foreach (var item in shopItems)
        {
            GameObject button = Instantiate(shopItemButtonPrefab, shopContent);
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = $"{item.itemName}";

            button.GetComponent<Button>().onClick.AddListener(() => OnBuyItem(item));
        }
        foreach (var item in shopItems2)
        {
            GameObject button = Instantiate(shopItemButtonPrefab, shopContent);
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = $"{item.itemName}";

            button.GetComponent<Button>().onClick.AddListener(() => OnBuyItem2(item));
        }
    }

    private void OnBuyItem(ShopItem item)
    {
        if (HasRequiredBlocks(item.requiredBlocks))
        {
            UseRequiredBlocks(item.requiredBlocks);
            Debug.Log($"Bought item: {item.itemName}");

            if (item.towerPrefab != null)
            {
                towerPlacementManager.SetTowerToPlace(item.towerPrefab);
            }
            else
            {
                Debug.LogError($"Tower prefab for {item.itemName} is not assigned!");
            }

            CloseShop();
        }
        else
        {
            Debug.Log("Not enough code blocks in inventory!");
        }
    }
    private void OnBuyItem2(ShopItem2 item)
    {
        // Check if the player has enough gold
        if (GoldManager.Instance.gold >= item.goldCost)
        {
            // Deduct gold
            GoldManager.Instance.gold -= item.goldCost;

            // Log the purchase for now
            Debug.Log($"Bought item: {item.itemName} for {item.goldCost} gold.");

            // Add the item to the inventory (based on its type)
            if (item.towerPrefab != null)
                {
                towerPlacementManager.SetTowerToPlace(item.towerPrefab);
                }
            else
            {
                Debug.LogError($"Tower prefab for {item.itemName} is not assigned!");
            }

            CloseShop();
        }
        else
        {
            Debug.Log("Not enough gold!");
        }
    }

    private bool HasRequiredBlocks(InventoryManager.CodeBlockType[] requiredBlocks)
    {
        foreach (var block in requiredBlocks)
        {
            int index = (int)block;
            if (InventoryManager.Instance.inventory[index] <= 0)
            {
                return false; // Missing required block
            }
        }
        return true; // All blocks are available
    }

    private void UseRequiredBlocks(InventoryManager.CodeBlockType[] requiredBlocks)
    {
        foreach (var block in requiredBlocks)
        {
            int index = (int)block;
            InventoryManager.Instance.inventory[index]--; // Deduct the block from inventory
        }

        InventoryManager.Instance.UpdateInventoryUI(); // Update UI after deducting
    }
}

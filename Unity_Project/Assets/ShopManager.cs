using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ShopManager : MonoBehaviour
{
    // Singleton instance
    public static ShopManager Instance;
    //public CameraController cameraController;
    public GameObject shopPanel;  // Reference to the Shop panel
    public GameObject shopItemButtonPrefab;  // Button prefab to instantiate
    public Transform shopContent;  // The content area to hold the buttons (where we’ll add the items)
    public Button closeButton;
    private int ok = 0;
    
    public TextMeshProUGUI codeDisplayText;

    [System.Serializable]
    public class ShopItem
    {
        public string itemName;  // Name of the code block
        public int goldCost;     // Gold cost for the item
    }

    // Hardcoded list of items (the code blocks available for purchase)
    public ShopItem[] shopItems = new ShopItem[] 
    {
        new ShopItem { itemName = "Firewall Block 1", goldCost = 1 },
        new ShopItem { itemName = "Firewall Block 2", goldCost = 1 },
        new ShopItem { itemName = "Firewall Block 3", goldCost = 1 },
        new ShopItem { itemName = "Firewall Block 4", goldCost = 1 },

        new ShopItem { itemName = "SQL Injection Block 1", goldCost = 1 },
        new ShopItem { itemName = "SQL Injection Block 2", goldCost = 1 },
        new ShopItem { itemName = "SQL Injection Block 3", goldCost = 1 },
        new ShopItem { itemName = "SQL Injection Block 4", goldCost = 1 },

        new ShopItem { itemName = "DDoS Protection Block 1", goldCost = 1 },
        new ShopItem { itemName = "DDoS Protection Block 2", goldCost = 1 },
        new ShopItem { itemName = "DDoS Protection Block 3", goldCost = 1 },
        new ShopItem { itemName = "DDoS Protection Block 4", goldCost = 1 },

        new ShopItem { itemName = "2FA Block 1", goldCost = 1 },
        new ShopItem { itemName = "2FA Block 2", goldCost = 1 },
        new ShopItem { itemName = "2FA Block 3", goldCost = 1 }
    };

    private void Awake()
    {
        shopPanel.SetActive(false);
        // Check if an instance already exists
        if (Instance == null)
        {
            Instance = this;  // Assign this instance to the static Instance
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicate instances
        }

        // Don't destroy the ShopManager when switching scenes
       // DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Initially hide the shop panel
        shopPanel.SetActive(false);
        if (ok != 0)
        {
            ReassignFields();
        ok++;}
        // Assign listener to the close button
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseShop);
        }
        else
        {
            Debug.LogWarning("Close button is not assigned!");
        }
        

        // Populate the shop with items
        PopulateShop();

    }

  private void ReassignFields()
    {
        
        // Automatically find and reassign references to the relevant GameObjects in the scene
        if (shopPanel == null)
        {
            shopPanel = GameObject.Find("ShopPanel");
            if (shopPanel == null) Debug.LogWarning("Shop Panel is not found!");
        }

        if (shopItemButtonPrefab == null)
        {
            shopItemButtonPrefab = Resources.Load<GameObject>("ShopItemButtonPrefab");  // Or use another method to load this
            if (shopItemButtonPrefab == null) Debug.LogWarning("Shop Item Button Prefab is not found!");
        }

        if (shopContent == null)
        {
            shopContent = shopPanel ? shopPanel.transform.Find("ShopContent") : null;
            if (shopContent == null) Debug.LogWarning("Shop Content is not found!");
        }

        if (codeDisplayText == null)
        {
            codeDisplayText = shopPanel ? shopPanel.transform.Find("CodeDisplayText").GetComponent<TextMeshProUGUI>() : null;
            if (codeDisplayText == null) Debug.LogWarning("Code Display Text is not found!");
        }
        ShopStopper.IsShopOpen = false;
    }
    public void CloseShop()
    {
        ShopStopper.IsShopOpen = false;
        shopPanel.SetActive(false);
    }

    // Open the shop when the building is clicked (this will be connected to a collider)
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

    // Populate the shop with buttons based on the items
    private void PopulateShop()
    {
        // Clear any existing items
        foreach (Transform child in shopContent)
        {
            Destroy(child.gameObject);
        }

        // Create buttons for each shop item
        foreach (var item in shopItems)
        {
            // Create a new button for each item
            GameObject button = Instantiate(shopItemButtonPrefab, shopContent);

            // Set the button's text to display item name and gold cost
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = $"{item.itemName} - {item.goldCost} Gold";

            // Add a click listener for buying the item
            button.GetComponent<Button>().onClick.AddListener(() => OnBuyItem(item));
        }
    }

    // Handle the item purchase
    private void OnBuyItem(ShopItem item)
    {
        // Check if the player has enough gold
        if (GoldManager.Instance.gold >= item.goldCost)
        {
            // Deduct gold
            GoldManager.Instance.gold -= item.goldCost;

            // Log the purchase for now
            Debug.Log($"Bought item: {item.itemName} for {item.goldCost} gold.");

            // Add the item to the inventory (based on its type)
            AddItemToInventory(item);

            // Show the code
            ShowCode(item);
            CloseShop();

            // Update the inventory UI
            InventoryManager.Instance.UpdateInventoryUI();
        }
        else
        {
            Debug.Log("Not enough gold!");
        }
    }

    private void ShowCode(ShopItem item)
    {
        string code = "";

        if (item.itemName.Contains("Firewall"))
        {
            code = GetFirewallCode(item);
        }
        else if (item.itemName.Contains("SQL Injection"))
        {
            code = GetSQLInjectionCode(item);
        }
        else if (item.itemName.Contains("DDoS Protection"))
        {
            code = GetDDosProtectionCode(item);
        }
        else if (item.itemName.Contains("2FA"))
        {
            code = Get2FACode(item);
        }

        codeDisplayText.text = code;
        codeDisplayText.gameObject.SetActive(true);

        // Hide the code after 5 seconds
        StartCoroutine(HideCodeAfterDelay(5f));
    }

    // Coroutine to hide the code after a delay
    private IEnumerator HideCodeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        codeDisplayText.gameObject.SetActive(false);  // Hide the code display text
    }

    // Functions to get the code based on the block bought
    private string GetFirewallCode(ShopItem item)
    {
        switch (item.itemName)
        {
            case "Firewall Block 1": return "def accept_traffic(packet):\n    if packet.is_valid():\n        return True\n    else:\n        return False";
            case "Firewall Block 2": return "def check_for_threats(packet):\n    known_threats = [\"malware\", \"phishing\", \"botnet\"]\n    if packet.type in known_threats:\n        return True\n    return False";
            case "Firewall Block 3": return "def block_malicious_traffic(packet):\n    if check_for_threats(packet):\n        print(\"Threat detected: Blocking packet\")\n        return False\n    return True";
            case "Firewall Block 4": return "def log_traffic(packet):\n    print(f\"Logging packet {packet.id}: {packet.status}\")";
            default: return "";
        }
    }

    private string GetSQLInjectionCode(ShopItem item)
    {
        switch (item.itemName)
        {
            case "SQL Injection Block 1": return "def validate_input(user_input):\n    if isinstance(user_input, str) and len(user_input) < 100:\n        return True\n    return False";
            case "SQL Injection Block 2": return "def escape_input(user_input):\n    dangerous_characters = [\"'\", \"--\", \";\", \"/*\", \"*/\"]\n    for char in dangerous_characters:\n        user_input = user_input.replace(char, \"\")\n    return user_input";
            case "SQL Injection Block 3": return "def use_prepared_statements(query, params):\n    cursor.execute(query, params)";
            case "SQL Injection Block 4": return "def sanitize_output(output):\n    return output.replace(\"<\", \"&lt;\").replace(\">\", \"&gt;\")";
            default: return "";
        }
    }

    private string GetDDosProtectionCode(ShopItem item)
    {
        switch (item.itemName)
        {
            case "DDoS Protection Block 1": return "def detect_traffic_spike(request_count):\n    threshold = 1000  # Number of requests per second\n    if request_count > threshold:\n        return True\n    return False";
            case "DDoS Protection Block 2": return "def limit_rate(request_count):\n    max_requests = 500";
            case "DDoS Protection Block 3": return "def block_ip(ip):\n    blocked_ips.append(ip)\n    print(f\"IP {ip} blocked due to high request count\")\n    if request_count > max_requests:\n        print(\"Rate limit exceeded: Blocking IP\")\n        return False\n    return True";
            default: return "";
        }
    }

    private string Get2FACode(ShopItem item)
    {
        switch (item.itemName)
        {
            case "2FA Block 1": return "def send_otp(user_email):\n    otp = random.randint(100000, 999999)\n    print(f\"Sending OTP {otp} to {user_email}\")\n    return otp";
            case "2FA Block 2": return "def verify_otp(entered_otp, sent_otp):\n    if entered_otp == sent_otp:\n        return True\n    return False";
            case "2FA Block 3": return "def generate_session_token(user_id):\n    session_token = f\"session_{user_id}_{random.randint(1000, 9999)}\"\n    return session_token";
            default: return "";
        }
    }

    private void AddItemToInventory(ShopItem item)
    {
        // Map the item to its corresponding CodeBlockType
        if (item.itemName.Contains("Firewall Block 1"))
        {
            InventoryManager.Instance.AddCodeBlock(InventoryManager.CodeBlockType.FirewallBlock1);
        }
        else if (item.itemName.Contains("Firewall Block 2"))
        {
            InventoryManager.Instance.AddCodeBlock(InventoryManager.CodeBlockType.FirewallBlock2);
        }
        else if (item.itemName.Contains("Firewall Block 3"))
        {
            InventoryManager.Instance.AddCodeBlock(InventoryManager.CodeBlockType.FirewallBlock3);
        }
        else if (item.itemName.Contains("Firewall Block 4"))
        {
            InventoryManager.Instance.AddCodeBlock(InventoryManager.CodeBlockType.FirewallBlock4);
        }
        else if (item.itemName.Contains("SQL Injection Block 1"))
        {
            InventoryManager.Instance.AddCodeBlock(InventoryManager.CodeBlockType.SQLInjectionBlock1);
        }
        else if (item.itemName.Contains("SQL Injection Block 2"))
        {
            InventoryManager.Instance.AddCodeBlock(InventoryManager.CodeBlockType.SQLInjectionBlock2);
        }
        else if (item.itemName.Contains("SQL Injection Block 3"))
        {
            InventoryManager.Instance.AddCodeBlock(InventoryManager.CodeBlockType.SQLInjectionBlock3);
        }
        else if (item.itemName.Contains("SQL Injection Block 4"))
        {
            InventoryManager.Instance.AddCodeBlock(InventoryManager.CodeBlockType.SQLInjectionBlock4);
        }
        else if (item.itemName.Contains("DDoS Protection Block 1"))
        {
            InventoryManager.Instance.AddCodeBlock(InventoryManager.CodeBlockType.DDoSProtectionBlock1);
        }
        else if (item.itemName.Contains("DDoS Protection Block 2"))
        {
            InventoryManager.Instance.AddCodeBlock(InventoryManager.CodeBlockType.DDoSProtectionBlock2);
        }
        else if (item.itemName.Contains("DDoS Protection Block 3"))
        {
            InventoryManager.Instance.AddCodeBlock(InventoryManager.CodeBlockType.DDoSProtectionBlock3);
        }
        else if (item.itemName.Contains("DDoS Protection Block 4"))
        {
            InventoryManager.Instance.AddCodeBlock(InventoryManager.CodeBlockType.DDoSProtectionBlock4);
        }
        else if (item.itemName.Contains("2FA Block 1"))
        {
            InventoryManager.Instance.AddCodeBlock(InventoryManager.CodeBlockType.TwoFactorAuthenticationBlock1);
        }
        else if (item.itemName.Contains("2FA Block 2"))
        {
            InventoryManager.Instance.AddCodeBlock(InventoryManager.CodeBlockType.TwoFactorAuthenticationBlock2);
        }
        else if (item.itemName.Contains("2FA Block 3"))
        {
            InventoryManager.Instance.AddCodeBlock(InventoryManager.CodeBlockType.TwoFactorAuthenticationBlock3);
        }
        else
        {
            Debug.LogWarning("Item not recognized: " + item.itemName);
        }
    }
}

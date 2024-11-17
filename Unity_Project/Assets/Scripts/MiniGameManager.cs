using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    public GameObject miniGamePanel;      // The panel that holds the input field and button
    public TMP_InputField inputField;    // The input field for the block type
    public Button submitButton;          // Button to submit the input
    public TextMeshProUGUI feedbackText; // Feedback text for the user
    public TextMeshProUGUI blockCodeText; // Text to display the block code (not the block type)

    private string correctBlockType;     // The correct block type for the current generator
    public Button closeButton;
    // Called when the building (e.g., Firewall_Gen) is clicked
    public void StartMiniGame(string blockType)
    {
        if (ShopStopper.IsShopOpen)
        {
            // Prevent opening this shop if another is already open
            Debug.Log("Another shop is already open!");
            return;
        }

        // Open this shop and block others
        ShopStopper.IsShopOpen = true;
    
        // Show the mini-game UI
        miniGamePanel.SetActive(true);

        // Get the correct block type for this block
        correctBlockType = GetCorrectBlockTypeForBlock(blockType);
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseMiniGame);
        }
        else
        {
            Debug.LogWarning("Close button is not assigned!");
        }
        // Display the block's code inside the text block (for visual reference)
        blockCodeText.text = GetCorrectCodeForBlock(blockType);
        
        // Clear the input field and feedback text
        inputField.text = "";
        feedbackText.text = "";
        submitButton.onClick.AddListener(CheckAnswer);
    }
        public void CloseMiniGame()
    {
         ShopStopper.IsShopOpen = false;
        miniGamePanel.SetActive(false);
       
    }

    // When the user presses the submit button
    public void CheckAnswer()
    {
        string userInput = inputField.text;
        Debug.Log(userInput);
        Debug.Log(correctBlockType);
        if (userInput.Equals(correctBlockType, System.StringComparison.OrdinalIgnoreCase))
        {
            feedbackText.text = "Correct!";
            
            // Use the switch statement to call AddCodeBlock
            switch (correctBlockType)
            {
                case "FirewallBlock1":
                    InventoryManager.Instance.AddCodeBlock(InventoryManager.CodeBlockType.FirewallBlock1);
                    break;
                case "FirewallBlock2":
                    InventoryManager.Instance.AddCodeBlock(InventoryManager.CodeBlockType.FirewallBlock2);
                    break;
                case "FirewallBlock3":
                    InventoryManager.Instance.AddCodeBlock(InventoryManager.CodeBlockType.FirewallBlock3);
                    break;
                case "FirewallBlock4":
                    InventoryManager.Instance.AddCodeBlock(InventoryManager.CodeBlockType.FirewallBlock4);
                    break;
                case "SQLInjectionBlock1":
                    InventoryManager.Instance.AddCodeBlock(InventoryManager.CodeBlockType.SQLInjectionBlock1);
                    break;
                case "SQLInjectionBlock2":
                    InventoryManager.Instance.AddCodeBlock(InventoryManager.CodeBlockType.SQLInjectionBlock2);
                    break;
                case "SQLInjectionBlock3":
                    InventoryManager.Instance.AddCodeBlock(InventoryManager.CodeBlockType.SQLInjectionBlock3);
                    break;
                case "SQLInjectionBlock4":
                    InventoryManager.Instance.AddCodeBlock(InventoryManager.CodeBlockType.SQLInjectionBlock4);
                    break;
                case "DDoSProtectionBlock1":
                    InventoryManager.Instance.AddCodeBlock(InventoryManager.CodeBlockType.DDoSProtectionBlock1);
                    break;
                case "DDoSProtectionBlock2":
                    InventoryManager.Instance.AddCodeBlock(InventoryManager.CodeBlockType.DDoSProtectionBlock2);
                    break;
                case "DDoSProtectionBlock3":
                    InventoryManager.Instance.AddCodeBlock(InventoryManager.CodeBlockType.DDoSProtectionBlock3);
                    break;
                case "TwoFactorAuthenticationBlock1":
                    InventoryManager.Instance.AddCodeBlock(InventoryManager.CodeBlockType.TwoFactorAuthenticationBlock1);
                    break;
                case "TwoFactorAuthenticationBlock2":
                    InventoryManager.Instance.AddCodeBlock(InventoryManager.CodeBlockType.TwoFactorAuthenticationBlock2);
                    break;
                case "TwoFactorAuthenticationBlock3":
                    InventoryManager.Instance.AddCodeBlock(InventoryManager.CodeBlockType.TwoFactorAuthenticationBlock3);
                    break;
                default:
                    Debug.Log("Unexpected block type.");
                    break;
            }
        }
        else
        {
            feedbackText.text = "Incorrect, try again!";
        }
    }

    // Get the correct block type for the given block name
    private string GetCorrectBlockTypeForBlock(string blockType)
    {
        switch (blockType)
        {
            case "Firewall Block 1":
                return "FirewallBlock1";
            case "Firewall Block 2":
                return "FirewallBlock2";
            case "Firewall Block 3":
                return "FirewallBlock3";
            case "Firewall Block 4":
                return "FirewallBlock4";
            case "SQL Injection Block 1":
                return "SQLInjectionBlock1";
            case "SQL Injection Block 2":
                return "SQLInjectionBlock2";
            case "SQL Injection Block 3":
                return "SQLInjectionBlock3";
            case "SQL Injection Block 4":
                return "SQLInjectionBlock4";
            case "DDoS Protection Block 1":
                return "DDoSProtectionBlock1";
            case "DDoS Protection Block 2":
                return "DDoSProtectionBlock2";
            case "DDoS Protection Block 3":
                return "DDoSProtectionBlock3";
            case "2FA Block 1":
                return "TwoFactorAuthenticationBlock1";
            case "2FA Block 2":
                return "TwoFactorAuthenticationBlock2";
            case "2FA Block 3":
                return "TwoFactorAuthenticationBlock3";
            default:
                return ""; // Default case
        }
    }

    // Get the code for a given block type (to display the code block)
    private string GetCorrectCodeForBlock(string blockType)
    {
        switch (blockType)
        {
            case "Firewall Block 1":
                return "def accept_traffic(packet):\n    if packet.is_valid():\n        return True\n    else:\n        return False";
            case "Firewall Block 2":
                return "def check_for_threats(packet):\n    known_threats = [\"malware\", \"phishing\", \"botnet\"]\n    if packet.type in known_threats:\n        return True\n    return False";
            case "Firewall Block 3":
                return "def block_malicious_traffic(packet):\n    if check_for_threats(packet):\n        print(\"Threat detected: Blocking packet\")\n        return False\n    return True";
            case "Firewall Block 4":
                return "def log_traffic(packet):\n    print(f\"Logging packet {packet.id}: {packet.status}\")";
            case "SQL Injection Block 1":
                return "def validate_input(user_input):\n    if isinstance(user_input, str) and len(user_input) < 100:\n        return True\n    return False";
            case "SQL Injection Block 2":
                return "def escape_input(user_input):\n    dangerous_characters = [\"'\", \"--\", \";\", \"/*\", \"*/\"]\n    for char in dangerous_characters:\n        user_input = user_input.replace(char, \"\")\n    return user_input";
            case "SQL Injection Block 3":
                return "def use_prepared_statements(query, params):\n    cursor.execute(query, params)";
            case "SQL Injection Block 4":
                return "def sanitize_output(output):\n    return output.replace(\"<\", \"&lt;\").replace(\">\", \"&gt;\")";
            case "DDoS Protection Block 1":
                return "def detect_traffic_spike(request_count):\n    threshold = 1000  # Number of requests per second\n    if request_count > threshold:\n        return True\n    return False";
            case "DDoS Protection Block 2":
                return "def limit_rate(request_count):\n    max_requests = 500";
            case "DDoS Protection Block 3":
                return "def block_ip(ip):\n    blocked_ips.append(ip)\n    print(f\"IP {ip} blocked due to high request count\")\n    if request_count > max_requests:\n        print(\"Rate limit exceeded: Blocking IP\")\n        return False\n    return True";
            case "2FA Block 1":
                return "def send_otp(user_email):\n    otp = random.randint(100000, 999999)\n    print(f\"Sending OTP {otp} to {user_email}\")\n    return otp";
            case "2FA Block 2":
                return "def verify_otp(entered_otp, sent_otp):\n    if entered_otp == sent_otp:\n        return True\n    return False";
            case "2FA Block 3":
                return "def generate_session_token(user_id):\n    session_token = f\"session_{user_id}_{random.randint(1000, 9999)}\"\n    return session_token";
            default:
                return "";  // Return an empty string for unknown block types
        }
    }
}

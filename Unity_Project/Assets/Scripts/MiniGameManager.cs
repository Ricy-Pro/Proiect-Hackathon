using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiniGameManager : MonoBehaviour
{
    public GameObject miniGamePanel;      // The panel that holds the input field and button
    public TMP_InputField inputField;    // The input field for the code
    public Button submitButton;          // Button to submit the input
    public TextMeshProUGUI feedbackText; // Feedback text for the user
    public TextMeshProUGUI blockNameText; // The text element to display the current block's name
    private string correctCode;          // The correct code for the current generator
    private InventoryManager.CodeBlockType correctBlockEnum; // To store the correct block type as an enum

    // Called when the building (e.g., Firewall_Gen) is clicked
    public void StartMiniGame(string blockType)
    {
        // Show the mini-game UI
        miniGamePanel.SetActive(true);

        // Get the correct code and block type for this block
        correctBlockEnum = GetBlockEnumFromString(blockType);
        correctCode = GetCorrectCodeForBlock(blockType);  // Call the method to get the correct code
         blockNameText.text = "Working on: " + blockType;
        // Clear the input field and feedback text
        inputField.text = "";
        feedbackText.text = "";

        // Start the mini-game for the block type (pass the enum)
        InventoryManager.Instance.AddCodeBlock(correctBlockEnum);
        submitButton.onClick.AddListener(CheckAnswer);
    }

    // When the user presses the submit button
public void CheckAnswer()
{
    string userInput = inputField.text.Trim();

    if (IsCodeCorrect(userInput))
    {
        feedbackText.text = "Correct!";
        
        // Add the correct block to the inventory
        InventoryManager.Instance.AddCodeBlock(correctBlockEnum);
        
        // Optionally, you can log it for debugging:
        Debug.Log("Block added: " + correctBlockEnum.ToString());
    }
    else
    {
        feedbackText.text = "Incorrect, try again!";
    }
}


    // Use Regex to check if the user's input is correct (lenient match)
    private bool IsCodeCorrect(string userInput)
    {
        // Clean up user input for matching (e.g., remove extra spaces)
        userInput = userInput.Replace("\n", " ").Replace("\r", " ").Trim();

        // Regex pattern for the correct block of code (ignores spacing differences)
        string pattern = correctCode.Replace("\n", @"\s*\n\s*").Trim();

        return System.Text.RegularExpressions.Regex.IsMatch(userInput, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
    }

 


    // Get the correct code for a given block type
    private string GetCorrectCodeForBlock(string blockType)
    {
        switch (blockType)
        {
            // Firewall Block Types
            case "Firewall Block 1":
                return "def accept_traffic(packet):\n    if packet.is_valid():\n        return True\n    else:\n        return False";
            case "Firewall Block 2":
                return "def check_for_threats(packet):\n    known_threats = [\"malware\", \"phishing\", \"botnet\"]\n    if packet.type in known_threats:\n        return True\n    return False";
            case "Firewall Block 3":
                return "def block_malicious_traffic(packet):\n    if check_for_threats(packet):\n        print(\"Threat detected: Blocking packet\")\n        return False\n    return True";
            case "Firewall Block 4":
                return "def log_traffic(packet):\n    print(f\"Logging packet {packet.id}: {packet.status}\")";

            // SQL Injection Block Types
            case "SQL Injection Block 1":
                return "def validate_input(user_input):\n    if isinstance(user_input, str) and len(user_input) < 100:\n        return True\n    return False";
            case "SQL Injection Block 2":
                return "def escape_input(user_input):\n    dangerous_characters = [\"'\", \"--\", \";\", \"/*\", \"*/\"]\n    for char in dangerous_characters:\n        user_input = user_input.replace(char, \"\")\n    return user_input";
            case "SQL Injection Block 3":
                return "def use_prepared_statements(query, params):\n    cursor.execute(query, params)";
            case "SQL Injection Block 4":
                return "def sanitize_output(output):\n    return output.replace(\"<\", \"&lt;\").replace(\">\", \"&gt;\")";

            // DDoS Protection Block Types
            case "DDoS Protection Block 1":
                return "def detect_traffic_spike(request_count):\n    threshold = 1000  # Number of requests per second\n    if request_count > threshold:\n        return True\n    return False";
            case "DDoS Protection Block 2":
                return "def limit_rate(request_count):\n    max_requests = 500";
            case "DDoS Protection Block 3":
                return "def block_ip(ip):\n    blocked_ips.append(ip)\n    print(f\"IP {ip} blocked due to high request count\")\n    if request_count > max_requests:\n        print(\"Rate limit exceeded: Blocking IP\")\n        return False\n    return True";

            // 2FA Block Types
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

    // Get the corresponding enum for the block type string
    private InventoryManager.CodeBlockType GetBlockEnumFromString(string blockType)
    {
        switch (blockType)
        {
            case "Firewall Block 1":
                return InventoryManager.CodeBlockType.FirewallBlock1;
            case "Firewall Block 2":
                return InventoryManager.CodeBlockType.FirewallBlock2;
            case "Firewall Block 3":
                return InventoryManager.CodeBlockType.FirewallBlock3;
            case "Firewall Block 4":
                return InventoryManager.CodeBlockType.FirewallBlock4;
            case "SQL Injection Block 1":
                return InventoryManager.CodeBlockType.SQLInjectionBlock1;
            case "SQL Injection Block 2":
                return InventoryManager.CodeBlockType.SQLInjectionBlock2;
            case "SQL Injection Block 3":
                return InventoryManager.CodeBlockType.SQLInjectionBlock3;
            case "SQL Injection Block 4":
                return InventoryManager.CodeBlockType.SQLInjectionBlock4;
            case "DDoS Protection Block 1":
                return InventoryManager.CodeBlockType.DDoSProtectionBlock1;
            case "DDoS Protection Block 2":
                return InventoryManager.CodeBlockType.DDoSProtectionBlock2;
            case "DDoS Protection Block 3":
                return InventoryManager.CodeBlockType.DDoSProtectionBlock3;
            case "2FA Block 1":
                return InventoryManager.CodeBlockType.TwoFactorAuthenticationBlock1;
            case "2FA Block 2":
                return InventoryManager.CodeBlockType.TwoFactorAuthenticationBlock2;
            case "2FA Block 3":
                return InventoryManager.CodeBlockType.TwoFactorAuthenticationBlock3;
            default:
                return InventoryManager.CodeBlockType.FirewallBlock1;  // Default value for unknown types
        }
    }
}

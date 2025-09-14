using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("UI Elements")]
    public Text modeText;
    public Text soulsText;
    public Image modeIndicator;

    void Start()
    {
        // Find GameManager and set UI references
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.modeText = modeText;
            gameManager.soulsText = soulsText;
            gameManager.modeIndicator = modeIndicator;
        }
    }
}

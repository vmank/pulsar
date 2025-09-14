using UnityEngine;
using UnityEngine.UI;

public class GameSetup : MonoBehaviour
{
    [ContextMenu("Setup Game Scene")]
    public void SetupGameScene()
    {
        // Create GameManager
        GameObject gameManagerObj = new GameObject("GameManager");
        GameManager gameManager = gameManagerObj.AddComponent<GameManager>();

        // Create Player
        GameObject playerObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        playerObj.name = "Player";
        playerObj.transform.position = Vector3.zero;
        playerObj.transform.localScale = Vector3.one * 1.2f;
        playerObj.tag = "Player";

        // Add PlayerController
        PlayerController playerController = playerObj.AddComponent<PlayerController>();

        // Set player color
        Renderer playerRenderer = playerObj.GetComponent<Renderer>();
        playerRenderer.material.color = Color.blue;

        // Add Rigidbody to player
        Rigidbody playerRb = playerObj.AddComponent<Rigidbody>();
        playerRb.useGravity = false;
        playerRb.freezeRotation = true;

        // Add Collider to player
        Collider playerCollider = playerObj.GetComponent<Collider>();
        playerCollider.isTrigger = true;

        // Setup Camera
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            GameObject cameraObj = new GameObject("Main Camera");
            mainCamera = cameraObj.AddComponent<Camera>();
            cameraObj.tag = "MainCamera";
        }

        mainCamera.orthographic = true;
        mainCamera.orthographicSize = 8f;
        mainCamera.transform.position = new Vector3(0, 0, -10);

        // Add CameraController
        CameraController cameraController = mainCamera.gameObject.AddComponent<CameraController>();

        // Create UI Canvas
        GameObject canvasObj = new GameObject("Canvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObj.AddComponent<CanvasScaler>();
        canvasObj.AddComponent<GraphicRaycaster>();

        // Create UI Panel
        GameObject panelObj = new GameObject("UI Panel");
        panelObj.transform.SetParent(canvasObj.transform, false);

        RectTransform panelRect = panelObj.AddComponent<RectTransform>();
        panelRect.anchorMin = new Vector2(0, 1);
        panelRect.anchorMax = new Vector2(1, 1);
        panelRect.anchoredPosition = new Vector2(0, -50);
        panelRect.sizeDelta = new Vector2(0, 100);

        Image panelImage = panelObj.AddComponent<Image>();
        panelImage.color = new Color(0, 0, 0, 0.5f);

        // Create Mode Text
        GameObject modeTextObj = new GameObject("Mode Text");
        modeTextObj.transform.SetParent(panelObj.transform, false);

        Text modeText = modeTextObj.AddComponent<Text>();
        modeText.text = "ACTIVE MODE";
        modeText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        modeText.fontSize = 24;
        modeText.color = Color.white;
        modeText.alignment = TextAnchor.MiddleCenter;

        RectTransform modeTextRect = modeTextObj.GetComponent<RectTransform>();
        modeTextRect.anchorMin = new Vector2(0, 0.5f);
        modeTextRect.anchorMax = new Vector2(0.5f, 1);
        modeTextRect.offsetMin = Vector2.zero;
        modeTextRect.offsetMax = Vector2.zero;

        // Create Souls Text
        GameObject soulsTextObj = new GameObject("Souls Text");
        soulsTextObj.transform.SetParent(panelObj.transform, false);

        Text soulsText = soulsTextObj.AddComponent<Text>();
        soulsText.text = "Souls: 0";
        soulsText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        soulsText.fontSize = 24;
        soulsText.color = Color.white;
        soulsText.alignment = TextAnchor.MiddleCenter;

        RectTransform soulsTextRect = soulsTextObj.GetComponent<RectTransform>();
        soulsTextRect.anchorMin = new Vector2(0.5f, 0.5f);
        soulsTextRect.anchorMax = new Vector2(1, 1);
        soulsTextRect.offsetMin = Vector2.zero;
        soulsTextRect.offsetMax = Vector2.zero;

        // Create Mode Indicator
        GameObject indicatorObj = new GameObject("Mode Indicator");
        indicatorObj.transform.SetParent(canvasObj.transform, false);

        Image modeIndicator = indicatorObj.AddComponent<Image>();
        modeIndicator.color = new Color(1f, 0.5f, 0.5f, 0.3f); // Start with active color

        RectTransform indicatorRect = indicatorObj.GetComponent<RectTransform>();
        indicatorRect.anchorMin = Vector2.zero;
        indicatorRect.anchorMax = Vector2.one;
        indicatorRect.offsetMin = new Vector2(10, 10);
        indicatorRect.offsetMax = new Vector2(-10, -10);

        // Add UIController
        GameObject uiControllerObj = new GameObject("UIController");
        UIController uiController = uiControllerObj.AddComponent<UIController>();
        uiController.modeText = modeText;
        uiController.soulsText = soulsText;
        uiController.modeIndicator = modeIndicator;

        // Set GameManager UI references
        gameManager.modeText = modeText;
        gameManager.soulsText = soulsText;
        gameManager.modeIndicator = modeIndicator;

        Debug.Log("Game scene setup complete!");
    }
}

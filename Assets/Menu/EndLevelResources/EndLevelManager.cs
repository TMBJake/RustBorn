using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLevelManager : MonoBehaviour
{
    public static bool isEnded = false;
    public Button coinButton;
    public Button contButton;
    public GameObject endCanvas;
    public GameObject restCanvas;
    public GameObject uiCanvas;
    private bool isCollected = false;
    [SerializeField] private CoinCollector coinCollector;
    public GameObject doorChoicePanel;
    [SerializeField] private DoorIconRandomizer doorIconRandomizer;

    private bool freezeOnce = false;

    void Start()
    {
        DontDestroyOnLoad(uiCanvas); // This will ensure the UI canvas persists across scenes
        uiCanvas.SetActive(true);
        if (coinButton != null)
            coinButton.onClick.AddListener(addCoins);

        if (contButton != null)
            contButton.onClick.AddListener(nextScreen);
    }

    private void addCoins()
    {
        if (isCollected) return;

        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager is null. Cannot add coins.");
            return;
        }

        int coinsGained = UnityEngine.Random.Range(1, 6);
        GameManager.Instance.AddCoins(coinsGained);

        if (coinCollector != null)
            coinCollector.CollectCoins(coinsGained);

        isCollected = true;

        CoinData coinDisplay = FindObjectOfType<CoinData>();
        coinDisplay?.UpdateCoinDisplay();
    }

    private void nextScreen()
    {
        if (doorChoicePanel == null || doorIconRandomizer == null)
        {
            Debug.LogError("Door panel or icon randomizer is not assigned.");
            return;
        }

        Time.timeScale = 1.0f;
        endCanvas.SetActive(false);
        doorChoicePanel.SetActive(true);
        doorIconRandomizer.RandomizeIcons();
        //if (!doorChoicePanel.activeSelf && !endCanvas.activeSelf)
        //{
        //    uiCanvas.SetActive(true);
        //}
}

    void Update()
    {
        if (endCanvas != null && endCanvas.activeSelf && !freezeOnce)
        {
            isEnded = true;
            Time.timeScale = 0.0f;
            freezeOnce = true;
            uiCanvas.SetActive(false);

        }
    }
}

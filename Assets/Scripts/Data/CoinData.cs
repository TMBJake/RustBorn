using TMPro;
using UnityEngine;

public class CoinData : MonoBehaviour
{
    public TMP_Text coinNum;

    void Start()
    {
        UpdateCoinDisplay();
    }

    // Call this from any script when coins change
    public void UpdateCoinDisplay()
    {
        coinNum.text = "Coins: " + GameManager.Instance.playerCoins;
    }
}

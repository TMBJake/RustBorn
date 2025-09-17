using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public GameObject floatingTextPrefab;
    public Transform floatingTextParent;
    public Vector3 spawnOffset = new Vector3(0, 50, 0);

    public void CollectCoins(int amount)
    {
        GameObject popup = Instantiate(floatingTextPrefab, floatingTextParent);
        Vector3 screenPosition = Input.mousePosition;
        screenPosition.z = 10f; 
        popup.transform.position = Camera.main.ScreenToWorldPoint(screenPosition) + spawnOffset;
        popup.GetComponent<FloatingText>().Setup($"+{amount}");

        Debug.Log($"Collected {amount} coins!");
    }
}

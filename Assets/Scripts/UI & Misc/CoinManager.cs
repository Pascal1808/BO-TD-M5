using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    [SerializeField]private int coins;
    public TextMeshProUGUI coinText;

    public int Coins
    {
        get{return coins; }
    }

    private void Awake()
    {
        instance = this;
        UpdateCoins(0); // Initialize the coin display
    }

    public void UpdateCoins(int changeAmount)
    {
        coins += changeAmount;

        coinText.text = coins.ToString();
    }
}

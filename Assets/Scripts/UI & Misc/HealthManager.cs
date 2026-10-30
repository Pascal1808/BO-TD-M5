using UnityEngine;
using TMPro;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

    private int health = 100;
    public GameObject container;
    public TextMeshProUGUI healthText;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateHealth(int changeAmount)
    {
        health += changeAmount;

        healthText.text = health.ToString();

        if (health <= 0)
        {
            container.SetActive(true);
            Time.timeScale = 0f;
        }
    }

}

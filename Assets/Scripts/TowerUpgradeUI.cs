using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TowerUpgradeUI : MonoBehaviour
{
    public Tower tower;

    public Button upgradeButton;
    public TextMeshProUGUI priceText;

    private bool justOpened = true;
    
    private void Awake()
    {
        upgradeButton.onClick.AddListener(TryUpgrade);
    }

    private void TryUpgrade()
    {
        if(CoinManager.instance.Coins < tower.upgradeStages[tower.UpgradeStage].Price) return;

        tower.Upgrade();

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (justOpened)
        {
            justOpened = false;
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                return;

            Destroy(gameObject);
        }

        if (tower.UpgradeStage >= tower.upgradeStages.Length)
        {
            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class TowerSelectionUI : MonoBehaviour
{
    public static GameObject SelectedTowerPrefab;

    public void SelectTower(GameObject towerPrefab)
    {
        if (SelectedTowerPrefab == towerPrefab)
        {
            SelectedTowerPrefab = null; // Deselect if the same tower is clicked again
            return;
        }

        if(towerPrefab.GetComponent<Tower>().TowerPrice <= CoinManager.instance.Coins)
        {
            SelectedTowerPrefab = towerPrefab; // Select the new tower
        }
        
    }
}

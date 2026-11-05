using UnityEngine;

[System.Serializable]
public class TowerUpgradeStage
{
    [SerializeField]private float range;
    [SerializeField]private float fireRate;
    public Sprite sprite;
    [SerializeField]private int price;
    public float Range
    {
        get{return range;}
        set{range = value;}
    }
    public float FireRate
    {
        get{return fireRate;}
        set{fireRate = value;}
    }
    public int Price
    {
        get{return price;}
        set{price = value;}    
    }
}

public class Tower : MonoBehaviour
{
    private float range = 5f;
    private float fireRate = 1f;
    public GameObject projectilePrefab;
    public Transform firePoint;

    public TowerUpgradeStage[] upgradeStages;
    private int upgradeStage = 0;
    private SpriteRenderer sr;
    public GameObject towerUpgradeUIPrefab;
    private GameObject currentUI;

    public GameObject cloudPS;

    [SerializeField] private int towerPrice = 1;

    private float fireCooldown = 0f;
    public int UpgradeStage
    {
        get{return upgradeStage;}
    }
    public int TowerPrice
    {
        get{return towerPrice;}
    }

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        Instantiate(cloudPS, transform.position, Quaternion.identity);
    }
    void Update()
    {
        fireCooldown -= Time.deltaTime;

        Enemy target = FindBestTarget();

        if(target != null && fireCooldown <= 0f)
        {
            Shoot(target);
            fireCooldown = 1f / fireRate;
        }
    }

    Enemy FindBestTarget()
    {
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();

        Enemy best = null;
        float bestProgress = -1f;

        foreach (Enemy e in enemies)
        {
            float dist = Vector3.Distance(transform.position, e.transform.position);
            if (dist <= range)
            {
                bestProgress = dist;
                best = e;
            }
        }

        return best;
    }

    void Shoot(Enemy target)
    {
        GameObject p = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Projectile pr = p.GetComponent<Projectile>();
        pr.target = target.transform;
    }

    public void Upgrade()
    {
        TowerUpgradeStage currentUpgradeStage = upgradeStages[upgradeStage];

        range = currentUpgradeStage.Range;
        fireRate = currentUpgradeStage.FireRate;
        sr.sprite = currentUpgradeStage.sprite;
        CoinManager.instance.UpdateCoins(-currentUpgradeStage.Price);
        upgradeStage += 1;
        Instantiate(cloudPS, transform.position, Quaternion.identity);

    }

    private void OnMouseDown()
    {
        if (currentUI == null)
        {
            currentUI = Instantiate(towerUpgradeUIPrefab, FindObjectOfType<Canvas>().transform);
        }

        TowerUpgradeUI currentUpgradeUI = currentUI.GetComponent<TowerUpgradeUI>();
        currentUpgradeUI.tower = this;

        currentUI.transform.position = Input.mousePosition + new Vector3(50, -50);

        if (upgradeStage >= upgradeStages.Length)return;
        currentUpgradeUI.priceText.text = upgradeStages[upgradeStage].Price.ToString();        
    }
}

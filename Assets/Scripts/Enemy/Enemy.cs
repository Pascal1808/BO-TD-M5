using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 2f;
    [SerializeField] private int health = 1;
    [SerializeField] private int damage;
    public Transform[] waypoints;     

    private int currentWayPointIndex = 0;
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }
    void Update()
    {
        if(waypoints == null || waypoints.Length == 0)return;

        Transform target = waypoints[currentWayPointIndex];
        Vector3 dir = (target.position - transform.position).normalized;
        
        transform.position += dir * speed * Time.deltaTime;

        if(Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentWayPointIndex++;

            if(currentWayPointIndex >= waypoints.Length)
            {
                HealthManager.instance.UpdateHealth(damage); // Decrease health by 1 when enemy reaches the end
                CoinManager.instance.UpdateCoins(-2);
                Destroy(gameObject); // Enemy reached the end
            }
        }
    }
}

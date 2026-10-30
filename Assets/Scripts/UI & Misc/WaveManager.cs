using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

[System.Serializable]

public class WaveData
{
    [SerializeField]private float duration = 10f; 
    [SerializeField]private int easyEnemies = 5;
    [SerializeField]private int hardEnemies = 2;

    public float Duration
    {
        get{return duration;}
        set{duration = value;}
    }
    public int EasyEnemies
    {
        get{return easyEnemies;}
        set{easyEnemies = value;}
    }
    public int HardEnemies
    {
        get{return hardEnemies;}
        set{hardEnemies = value;}
    }
}

public class WaveManager : MonoBehaviour
{
    public WaveData[] waves;
    public Button startWaveButton;

    public GameObject easyEnemyPrefab;
    public GameObject hardEnemyPrefab;

    public Transform[] wayPoints;

    public TextMeshProUGUI waveText;

    private int currentWaveIndex = 0;
    private bool waveRunning = false;
    void Start()
    {
        startWaveButton.onClick.AddListener(StartWave);   
    }

    public void StartWave()
    {
        if(waveRunning)return;
        if(currentWaveIndex >= waves.Length)return;

        StartCoroutine(RunWave());
    }

    public IEnumerator RunWave()
    {
        waveRunning = true;
        startWaveButton.interactable = false;

        WaveData wave = waves[currentWaveIndex];

        for (int i = 0; i < wave.EasyEnemies; i++)
        {
            SpawnEnemy(easyEnemyPrefab);
            yield return new WaitForSeconds(wave.Duration / 3 / wave.EasyEnemies);
        }

         for (int i = 0; i < wave.HardEnemies; i++)
        {
            SpawnEnemy(hardEnemyPrefab);
            yield return new WaitForSeconds(wave.Duration / 3 / wave.HardEnemies);
        }

        yield return new WaitForSeconds(wave.Duration / 3);

        waveRunning = false;
        startWaveButton.interactable = true;
        currentWaveIndex += 1;
        waveText.text = (currentWaveIndex + 1).ToString();
        UpdateScoreText();
    }

    void SpawnEnemy(GameObject Prefab)
    {
        GameObject e = Instantiate(Prefab, wayPoints[0].position, Quaternion.identity);
        Enemy enemy = e.GetComponent<Enemy>();
        enemy.waypoints = wayPoints;
    }

    void UpdateScoreText()
    {
        waveText.text = "Wave: " + currentWaveIndex;
    }

}

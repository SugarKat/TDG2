using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class WaveSpawner : MonoBehaviour {

    public GameObject[] enemyPrefabs;

    public int startingRound = 0;
    public static int enemiesAlive;
    public float timeBetweenWaves = 4f;
    private float countdown = 4f;

    public TextMeshProUGUI waveCountdownText;
    public TextMeshProUGUI roundsCounter;

    public Transform spawnPoint;

    private int waveIndex = 0;

    void Start()
    {
        waveIndex = startingRound;
    }
    void Update()
    {
        if (waveIndex == 0)
        {
            enemiesAlive = 0;
        }

        if (enemiesAlive > 0)
            return;

        countdown -= Time.deltaTime;
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        if (waveCountdownText != null)
        {
            waveCountdownText.text = string.Format("{0:00}", countdown);
        }
        else
            Debug.LogError("Nera kur rodyti countdown'o");
        if (roundsCounter != null)
        {
            roundsCounter.text = "Rounds: " + PlayerStats.Rounds;
        }
        else
            Debug.LogError("Nera kur rodyti raundu");
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;
        PlayerStats.Rounds++;

        for (int i = 0; i < waveIndex; i++)
        {
            int enemyID = Random.Range(0, enemyPrefabs.Length);

            SpawnEnemy(enemyID);
            yield return new WaitForSeconds(1.5f);
            enemiesAlive++;
        }
    }

    void SpawnEnemy(int id)
    {
        Instantiate(enemyPrefabs[id], spawnPoint.position, spawnPoint.rotation);
    }
}

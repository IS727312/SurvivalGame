using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class waveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public int count;
        public float timeBetweenSpawns = 2;
    }

    public Enemy[] enemies;
    public Wave[] waves;
    public Transform[] spawnPoints;
    public float timeBetweenWaves;

    private Wave currentWave;
    private int currentWaveIndex = 0;
    private Transform player;

    private bool finishSpawning;

    public GameObject boss;
    public Transform bossSpawnPoints;

    public GameObject healthBar;
    private Text round;
    SceneTransitions sceneTransitions;
    int oldScore;
    int ans = 2;

    private void Start()
    {
        ZPlayerPrefs.Initialize("CD#XL6yQkpZri3/", "CrG16@gA20");
        oldScore = ZPlayerPrefs.GetInt("HighScore",0);
        sceneTransitions = FindObjectOfType<SceneTransitions>();
        round = FindObjectOfType<Text>();
        for(int i = 0;i < waves.Length; i++)
        {

            if(i%2== 0)
            {
                ans++;
            }
            waves[i].count = ans;
            waves[i].timeBetweenSpawns = 1;
        }
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(StartNextWave(currentWaveIndex));
    }
    IEnumerator StartNextWave(int index)
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave(index));
    }

    IEnumerator SpawnWave(int index)
    {
        round.text = (index + 1).ToString();
        currentWave = waves[index];
       
        for(int i = 0; i < currentWave.count; i++)
        {
            if(player == null)
            {
                //globalControl.Instance.currentWave = index + 1;
                yield break;

            }
            if(index < 5)
            {
                Enemy randomEnemy = enemies[0];
                Transform randomSpot = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(randomEnemy, randomSpot.position, randomSpot.rotation);

            }
            else if(index < 10)
            {
                Enemy randomEnemy = enemies[Random.Range(0, enemies.Length - 1)];
                Transform randomSpot = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(randomEnemy, randomSpot.position, randomSpot.rotation);
            }
            else
            {
                if(index % 10 == 0)
                {
                    healthBar.SetActive(true);
                    Instantiate(boss, bossSpawnPoints.position, bossSpawnPoints.rotation);
                }
                Enemy randomEnemy = enemies[Random.Range(0, enemies.Length)];
                Transform randomSpot = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(randomEnemy, randomSpot.position, randomSpot.rotation);
            }


            if(i == currentWave.count - 1)
            {
                finishSpawning = true;
            }
            else
            {
                finishSpawning = false;
            }

            yield return new WaitForSeconds(currentWave.timeBetweenSpawns);
        }
    }

    private void Update()
    {
        if(player != null)
        {
            if ((finishSpawning == true && GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && GameObject.FindGameObjectsWithTag("Boss").Length == 0))
            {
                finishSpawning = false;
                if (currentWaveIndex + 1 < waves.Length)
                {
                    currentWaveIndex++;
                    StartCoroutine(StartNextWave(currentWaveIndex));
                }
                else
                {
                    Debug.Log("GAME FINISHED!!");
                }
            }
        }
        else
        {
            if(oldScore < currentWaveIndex + 1)
            {
                ZPlayerPrefs.SetInt("HighScore", currentWaveIndex + 1);
                globalControl.Instance.currentWave = currentWaveIndex + 1;
            }
            sceneTransitions.loadScene("Lost");
            ZPlayerPrefs.Save();
        }
        
        
    }
}

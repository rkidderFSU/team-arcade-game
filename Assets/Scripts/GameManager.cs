using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float gravityMultiplier;
    public TextMeshProUGUI countdownText;
    public float startDelay;
    public GameObject platform;
    public bool isGameActive;
    public float platformSpawnRange = 2;
    public float spawnDelay;
    public int spawnCount;
    public TextMeshProUGUI scoreText;
    private float score;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.gravity *= gravityMultiplier;
        StartCoroutine(Countdown());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
        UpdateScore();
    }

    IEnumerator Countdown()
    {
        countdownText.gameObject.SetActive(true);
        countdownText.text = "3";
        yield return new WaitForSeconds(startDelay / 3);
        countdownText.text = "2";
        yield return new WaitForSeconds(startDelay / 3);
        countdownText.text = "1";
        yield return new WaitForSeconds(startDelay / 3);
        countdownText.text = "GO!";
        isGameActive = true;
        StartCoroutine(SpawnPlatforms(spawnDelay, spawnCount));
        yield return new WaitForSeconds(1);
        countdownText.gameObject.SetActive(false);
    }

    IEnumerator SpawnPlatforms(float platformSpawnTime, int platformsToSpawn)
    {
        for (int i = 0; i < platformsToSpawn; i++) // Spawns platforms immediately so the players don't have to wait for the first set of platforms to spawn
        {
            Instantiate(platform, GenerateRandomLocation(), platform.transform.rotation);
        }
        while (isGameActive)
        {
            yield return new WaitForSeconds(platformSpawnTime);
            for (int i = 0; i < platformsToSpawn; i++)
            {
                Instantiate(platform, GenerateRandomLocation(), platform.transform.rotation);
            }
        }
    }

    public Vector3 GenerateRandomLocation()
    {
        Vector3 randomLocation = new Vector3(Random.Range(-platformSpawnRange, platformSpawnRange) * 3, transform.position.y + 10);
        return randomLocation;
    }

    public void UpdateScore()
    {
        score = Mathf.RoundToInt(GameObject.Find("Main Camera").transform.position.y - 5) / 3; // Score increases by (movement speed of Camera / 2) every second
        scoreText.text = "Height: " + score + "m";
    }
}

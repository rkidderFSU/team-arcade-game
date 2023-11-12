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
    private float platformSpawnRange = 8f;
    public float spawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityMultiplier;
        StartCoroutine(Countdown());
        StartCoroutine(SpawnPlatforms(spawnDelay));
    }

    // Update is called once per frame
    void Update()
    {
        
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
        yield return new WaitForSeconds(1);
        countdownText.gameObject.SetActive(false);
    }

    IEnumerator SpawnPlatforms(float platformSpawnTime)
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(platformSpawnTime);
            Instantiate(platform, GenerateRandomLocation(), platform.transform.rotation);
            Debug.Log("Spawned Platform");
        }
    }

    private Vector3 GenerateRandomLocation()
    {
        return new Vector3(Random.Range(-platformSpawnRange, platformSpawnRange), 10);
    }
}

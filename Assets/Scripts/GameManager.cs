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
    private int platformSpawnRange = 2;
    public float spawnDelay;

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
        StartCoroutine(SpawnPlatforms(spawnDelay));
        yield return new WaitForSeconds(1);
        countdownText.gameObject.SetActive(false);
    }

    IEnumerator SpawnPlatforms(float platformSpawnTime)
    {
        Instantiate(platform, GenerateRandomLocation(), platform.transform.rotation);
        while (isGameActive)
        {
            yield return new WaitForSeconds(platformSpawnTime);
            Instantiate(platform, GenerateRandomLocation(), platform.transform.rotation);
            Debug.Log("Spawned Platform");
        }
    }

    public Vector3 GenerateRandomLocation()
    {
        Vector3 randomLocation =  new Vector3(Random.Range(-platformSpawnRange, platformSpawnRange) * 3, transform.position.y + 10);
        return randomLocation;
    }
}

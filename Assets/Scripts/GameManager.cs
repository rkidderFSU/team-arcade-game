using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public GameObject gameOverScreen;
    public GameObject titleScreen;
    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject waterfall;
    public GameObject rocks;
    public GameObject ground;
    public GameObject startingPlatforms;
    public bool gameOver;
    public bool titleScreenActive;
    public Camera cameron;
    AudioSource cameronMusic;
    AudioSource music;
    public AudioClip mainMenuMusic;
    public AudioClip countdownSound;
    public AudioClip goSound;
    public AudioClip menuSound;
    public TextMeshProUGUI loadingText;

    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
        cameronMusic = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        titleScreenActive = true;
        music.Play();
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (isGameActive)
        {
            UpdateScore();
        }
        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7)) && titleScreenActive && !isGameActive)
        {
            StartCoroutine(StartGame());
        }
        if ((Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9)) && gameOver && !isGameActive)
        {
            StartCoroutine(RestartGame());
        }
    }

    IEnumerator Countdown()
    {
        isGameActive = false;
        yield return new WaitForSeconds(0.5f);
        countdownText.gameObject.SetActive(true);
        countdownText.text = "3";
        music.PlayOneShot(countdownSound, 1f);
        yield return new WaitForSeconds(startDelay / 3);
        countdownText.text = "2";
        music.PlayOneShot(countdownSound, 1f);
        yield return new WaitForSeconds(startDelay / 3);
        countdownText.text = "1";
        music.PlayOneShot(countdownSound, 1f);
        yield return new WaitForSeconds(startDelay / 3);
        countdownText.text = "GO!";
        music.PlayOneShot(goSound, 1f);
        isGameActive = true;
        cameronMusic.Play();
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
        score = Mathf.RoundToInt(GameObject.Find("Main Camera").transform.position.y - 5) / 3; // Score increases by (movement speed of Camera / 3) every second
        scoreText.text = "Height: " + score + "m";
    }

    IEnumerator StartGame()
    {
        music.Stop();
        music.PlayOneShot(menuSound, 1f);
        titleScreenActive = false;
        titleScreen.SetActive(false);
        scoreText.gameObject.SetActive(true);
        ground.SetActive(true);
        waterfall.SetActive(true);
        rocks.SetActive(true);
        Instantiate(playerOne, playerOne.transform.position, playerOne.transform.rotation);
        Instantiate(playerTwo, playerTwo.transform.position, playerTwo.transform.rotation);
        for (int i = 0; i < 2; i++)
        {
            Instantiate(startingPlatforms, GenerateRandomLocation(), startingPlatforms.transform.rotation);
        }
        Physics2D.gravity *= gravityMultiplier;
        yield return new WaitForSeconds(1);
        music.Stop();
        StartCoroutine(Countdown());
    }

    public void GameOver()
    {
        cameronMusic.Stop();
        isGameActive = false;
        gameOver = true;
        gameOverScreen.SetActive(true);
    }

    IEnumerator RestartGame()
    {
        Physics2D.gravity = new Vector2(0, -9.81f);
        music.PlayOneShot(menuSound, 1f);
        gameOverScreen.SetActive(false);
        loadingText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        music.Stop();
        yield return new WaitForSeconds(Random.Range(0.7f, 2.6f));
        SceneManager.LoadScene(0);
    }
}
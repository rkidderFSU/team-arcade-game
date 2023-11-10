using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public float gravityMultiplier;
    public TextMeshProUGUI countdownText;
    public float timeToWait;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityMultiplier;
        StartCoroutine(Countdown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Countdown()
    {
        countdownText.text = "3";
        yield return new WaitForSeconds(timeToWait/3);
        countdownText.text = "2";
        yield return new WaitForSeconds(timeToWait / 3);
        countdownText.text = "1";
        yield return new WaitForSeconds(timeToWait / 3);
        countdownText.text = "GO!";
        yield return new WaitForSeconds(timeToWait / 3);
        countdownText.gameObject.SetActive(false);
    }
}

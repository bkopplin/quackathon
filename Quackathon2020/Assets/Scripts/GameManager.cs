using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject titleScreen;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    private float score;
    public bool isGameActive;
    [SerializeField] GameObject [] assets;
    public float horizontalBound = 3.0f;
    float spawnRateMin = 1;
    float spawnRateMax = 2;
    float weightEnemy = 40;
    float weightPowerup = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnObjects()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(Random.Range(spawnRateMin, spawnRateMax));
            int index = (int)RandomRange.Range(
                new FloatRange(0f, 1f, weightEnemy),
                new FloatRange(2f, 4f, weightPowerup)
                );
            Instantiate(assets[index], RandomStartPos(), Quaternion.identity);
            Debug.Log("Spawn enemy");
        }
    }


    Vector3 RandomStartPos()
    {
        float randomX = Random.Range(-horizontalBound, horizontalBound);
        Debug.Log(randomX);
        return new Vector3(randomX, 1, 30);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        isGameActive = true;
        score = 0;
        UpdateScore(0);
        StartCoroutine(SpawnObjects());

        titleScreen.SetActive(false);
    }
}

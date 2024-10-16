using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    [SerializeField] private Transform environment;

    [SerializeField] SliderController soulsSliderController;

    [SerializeField] GameObject gameWinPanel;
    [SerializeField] GameObject inGamePanel;
    [SerializeField] GameObject gameOverPanel; 
    [SerializeField] GameObject pausedGamePanel;

    // Phase Control
    public static bool playerDied = false;
    public static bool gameIsOn = true;
    public static int numberOfSoulsCollected = 0;
    private int soulsObjective = 10;

    public static bool[] spawnPoints = new bool[4];

    private void Start()
    {
        foreach (bool spawnPoint in spawnPoints) spawnPoint.Equals(false);
        playerDied = false;
        gameIsOn = true;
        numberOfSoulsCollected = 0;
    }

    // Update
    void Update()
    {
        Vector2 cameraPosition = new Vector2(transform.position.x, transform.position.y);
        //o ambiente de fundo seguir a camera
        environment.transform.position = cameraPosition;

        soulsSliderController.UpdateSliderValue(numberOfSoulsCollected, soulsObjective);

        if (gameIsOn && numberOfSoulsCollected == soulsObjective)
        {
            gameIsOn = false;
            GameWin();
        }

        if (gameIsOn && playerDied)
        {
            gameIsOn = false;
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    void GameWin()
    {
        Time.timeScale = 0f;
        inGamePanel.SetActive(false);
        gameWinPanel.SetActive(true);
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        inGamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void PauseGame()
    {
        // Pausar
        if (gameIsOn)
        {
            gameIsOn = false;
            Time.timeScale = 0f;
            inGamePanel.SetActive(false);
            pausedGamePanel.SetActive(true);
        }
        // Despausar
        else
        {
            gameIsOn = true;
            Time.timeScale = 1f;
            pausedGamePanel.SetActive(false);
            inGamePanel.SetActive(true);
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
}

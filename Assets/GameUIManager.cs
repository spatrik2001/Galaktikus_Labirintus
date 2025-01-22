using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;       // Pontok
    public TextMeshProUGUI livesText;       // Élet
    public GameObject startPanel;           // Kezdő panel
    public GameObject gameOverPanel;        // Game Over panel
    public GameObject pausePanel;           // Pause menü
    public GameObject winPanel;             // win panel

    [Header("Game Settings")]
    public int startingLives = 3;          

    private int score = 0;                 
    private int lives;                   
    private float pointTimer = 0f;         
    private bool isGameRunning = false;     
    
    public bool IsGameRunning => isGameRunning; 
    public int winpoint = 200;
    void Start()
    {
        lives = startingLives;              
        UpdateScore(0);                    
        UpdateLives();                     
        ShowStartPanel();                   
    }

    void Update()
    {
        // ESC gomb
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGameRunning)
            {
                PauseGame();
            }
            else if (pausePanel.activeSelf)
            {
                ResumeGame();
            }
        }

        // Pontszám növelése 3 másodpercenként
        if (isGameRunning)
        {
            pointTimer += Time.deltaTime;
            if (pointTimer >= 3f)
            {
                // Pontszám növelése a spawn rate alapján
                float spawnRate = FindObjectOfType<AsteroidSpawner>().CurrentSpawnInterval;
                int pointsToAdd = Mathf.RoundToInt(10 / spawnRate); 
                UpdateScore(pointsToAdd);
                pointTimer = 0f;
            }
        }
    }

    public void ShowStartPanel()
    {
        startPanel.SetActive(true);          
        gameOverPanel.SetActive(false);   
        pausePanel.SetActive(false);      
        winPanel.SetActive(false);         
        Time.timeScale = 0;             
    }

    public void StartGame()
    {
        isGameRunning = true;              
        Time.timeScale = 1;               
        startPanel.SetActive(false);        
        score = 0;                         
        lives = startingLives;             
        UpdateScore(0);                    
        UpdateLives();                     
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit(); 
    }

    public void GameOver()
    {
        isGameRunning = false;              
        gameOverPanel.SetActive(true);     
        Time.timeScale = 0;                
    }

    public void WinGame()
    {
        isGameRunning = false;             
        winPanel.SetActive(true);          
        Time.timeScale = 0;                 
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void UpdateScore(int points)
    {
        score += points;                    
        scoreText.text = "Score: " + score; 

        if (score >= winpoint)                  
        {
            WinGame();                      
        }
    }

    public void UpdateLives()
    {
        livesText.text = "Lives: " + lives; 

        if (lives <= 0)
        {
            GameOver();                     
        }
    }

    public void LoseLife()
    {
        lives--;                           
        UpdateLives();                      
    }

    public void PauseGame()
    {
        isGameRunning = false;             
        Time.timeScale = 0;                
        pausePanel.SetActive(true);        
    }

    public void ResumeGame()
    {
        isGameRunning = true;               
        Time.timeScale = 1;                 
        pausePanel.SetActive(false);       
    }
}

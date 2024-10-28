using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3, score = 0;

    [SerializeField] Text scoreText, livesText;
    [SerializeField] AudioClip deathSFX;

    AudioSource audioSource;
    private void Awake()
    {
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }
    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            takeLife();
        }
        else
        {
            resetGame();
        }
    }

    private void resetGame()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void takeLife()
    {
        playerLives--;
        livesText.text = playerLives.ToString();
    }
    public void addToScore(int scoreValue)
    {
        score += scoreValue;
        scoreText.text = score.ToString();

    }

    public void addToLife()
    {
        playerLives++;
        livesText.text = playerLives.ToString();
    }
}

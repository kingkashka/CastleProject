using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3, score = 0;

    [SerializeField] Text scoreText, livesText;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] Image[] hearts;

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
        updateHearts();
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

        if (playerLives >= 3)
        {
            playerLives = 3;
        }
        updateHearts();
        livesText.text = playerLives.ToString();

    }

    private void updateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerLives)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}

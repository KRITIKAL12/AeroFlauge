using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;


public class GameManager : MonoBehaviour
{
    public Shooting player;
    public TextMeshProUGUI scoreIndicator;
    public TextMeshProUGUI livesIndicator;
    public Transform gameOverPanel;
    public Transform levelOneEnd;
    public int lives = 3;
    public int score = 0;
    public  static bool gameOver = false;
    public bool levelOneComplete = false;

    public Spawner spawner;


    public float spawnDelay = 3.0f;
    public float layerChangeDelay = 60.0f;
    public CameraShake cameraShake;


    public ParticleSystem explosion;

    private bool isPaused = false;


    private void Awake()
    {

        spawner = FindObjectOfType<Spawner>();

    }

    public void Start()
    {

        if (spawner != null)
        {
            spawner.gameManager = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        Cursor.visible = true;

        if (!isPaused)
        {
            livesIndicator.text = lives.ToString();
            scoreIndicator.text = score.ToString();

            if (gameOver)
            {
                if (Input.GetKey(KeyCode.Return))
                {
                    SceneManager.LoadScene("Menu");
                }
            }
            else if (levelOneComplete)
            {
                if (Input.GetKey(KeyCode.Return))
                {
                    SceneManager.LoadScene("Level2");
                }
            }
        }

    }

    private void PauseGame()
    {
        Time.timeScale = 0f; // Pause the game by setting the time scale to 0
        isPaused = true;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f; // Resume the game by setting the time scale to 1
        isPaused = false;
    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();

        if (asteroid.size < 0.88f)
        {
            score += 100;
        }
        else if (asteroid.size < 1.21)
        {
            score += 55;
        }
        else
        {
            score += 15;
        }
        


    }
   
    public void PlayerDied()
    {

        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();
        StartCoroutine(cameraShake.Shake(.20f, .3f));

        lives -= 1;
        if(this.lives <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), spawnDelay);
        }
        
    }

    private void Respawn()
    {
        //this.player.transform.position = Vector3.zero;
        this.player.gameObject.SetActive(true);
        this.player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        
        Invoke(nameof(ChangePlayerLayer), layerChangeDelay);
        
        StartCoroutine(BlinkPlayer());
    }

    private IEnumerator BlinkPlayer()
    {
        float blinkDuration = 3.0f; // Total duration of blinking in seconds
        float blinkInterval = 0.2f; // Duration of each blink interval

        // Set the player's layer to "Ignore Collisions"
        this.player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");

        SpriteRenderer playerRenderer = this.player.GetComponent<SpriteRenderer>();
        Color originalColor = playerRenderer.color; // Store the original color

        float elapsedTime = 0f;
        bool playerActive = true;
        bool isRed = true;

        while (elapsedTime < blinkDuration)
        {
            yield return new WaitForSeconds(blinkInterval);

            elapsedTime += blinkInterval;

            // Toggle the player's visibility
            playerActive = !playerActive;
            this.player.gameObject.SetActive(playerActive);

            // Toggle the player's color between red and cyan
            if (playerActive)
            {
                if (isRed)
                {
                    playerRenderer.color = Color.red;
                }
                else
                {
                    playerRenderer.color = Color.cyan;
                }

                isRed = !isRed;
            }
        }

        // Reset player visibility and layer
        this.player.gameObject.SetActive(true);
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
        playerRenderer.color = originalColor; // Reset the color to the original color
    }


    private void ChangePlayerLayer()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public void GameOver()   
    {
        //Game Over...
        gameOverPanel.gameObject.SetActive(true);
        gameOver = true;


    }

    public void Level1Complete()
    {
        levelOneComplete = true;
        if (levelOneEnd != null)
        {
            levelOneEnd.gameObject.SetActive(true);
        }

        if (player != null)
        {
            player.gameObject.SetActive(false);
        }

        if (spawner != null)
        {
            spawner.gameObject.SetActive(false);
        }

        
    }

}
   
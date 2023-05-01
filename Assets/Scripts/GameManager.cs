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
    public bool gameOver = false;
    public bool levelOneComplete = false;

    public Spawner spawner;


    public float spawnDelay = 3.0f;
    public float layerChangeDelay = 60.0f;
    public CameraShake cameraShake;


    public ParticleSystem explosion;


   

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
        
        livesIndicator.text = lives.ToString();
        scoreIndicator.text = score.ToString();

        if (gameOver)
        {
            if (Input.GetKey(KeyCode.Return))
            {
                
                SceneManager.LoadScene("Asteroids");
                
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
        StartCoroutine(cameraShake.Shake(.15f, .4f));

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
   
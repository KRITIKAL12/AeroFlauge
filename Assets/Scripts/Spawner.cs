using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnRate = 1.0f;
    public int spawnAmount = 1;
    public float spawnDistance = 25.0f;
    public float trajectoryVariance = 25.0f;
    public float spawnerLifetime = 60.0f;
    public GameObject levelCompletePanel;
    public float time;

    private Rigidbody2D _rigidbody;


    public float smoothTime = 0.25f;
    public GameObject target;
    private Vector3 velocity = Vector3.zero;
    public GameManager gameManager;
    public bool gameOver = false;

    public Asteroid asteroidPrefab;

    public Vector3 spawnDirection = new Vector3(1, 0, 0);

    private bool spawnerAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        
        levelCompletePanel.SetActive(false);
        
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
       time = spawnerLifetime;
    }
    
    public void Spawn()
    {
        Debug.Log("Spawn method called");

        for (int i = 0; i < this.spawnAmount; i++)
        {
            Vector3 spawnPoint = this.transform.position + spawnDirection.normalized * spawnDistance;

            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);

         asteroid.SetTrajectory(rotation * -spawnDirection);
            /*if(asteroid.size >= 1f && asteroid.size < 1.5f)
             {
                 Debug.Log("Big Speed");
                 _rigidbody.AddForce((rotation * -spawnDirection) * this.bigAsteroidSpeed);

             }
             else
             {
                 Debug.Log("Small Speed");
                 _rigidbody.AddForce((rotation * -spawnDirection) * this.smallAsteroidSpeed);
             }*/

            Vector3 targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, 0);
            this.transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

            time -= 1;
        }

        Debug.Log(time);
        if (time == 0)
        {
            if (gameManager != null)
            {
                gameManager.Level1Complete();
            }
        }

        Destroy(this.gameObject, this.spawnerLifetime);
        spawnerAlive = false;

        

    }
  

    private void Update()
    {
        Vector3 targetPosition = new Vector3(target.transform.position.x, target.transform.position.y,0);
        this.transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        
    }

    
   

}

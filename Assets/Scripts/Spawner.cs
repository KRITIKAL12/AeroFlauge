using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnRate = 1.0f;
    public int spawnAmount = 1;
    public float spawnDistance = 25.0f;
    public float trajectoryVariance = 25.0f;

    public float smoothTime = 0.25f;
    public GameObject target;
    private Vector3 velocity = Vector3.zero;

    public Asteroid asteroidPrefab;

    // Start is called before the first frame update
    void Start()
    {
         InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);

        
    }

    private void Spawn()
    {
        

        for (int i = 0; i < this.spawnAmount; i++)
        {

            Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);

            asteroid.SetTrajectory(rotation * -spawnDirection);

        } 


    }

    private void Update()
    {
        Vector3 targetPosition = new Vector3(target.transform.position.x, target.transform.position.y,0);
        this.transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}

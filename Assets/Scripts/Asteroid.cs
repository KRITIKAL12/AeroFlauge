using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;

    public GameObject gameManager;
    private GameManager gameManagerScript;

    public float size = 1.0f;
    public float speed = 50f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    private float maxLifeTime = 10.0f;
    public int random;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();

        //Get game manager
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        
    }
      
    // Start is called before the first frame update
    void Start()
    {
        random = Random.Range(0, sprites.Length);

        
        _spriteRenderer.sprite = sprites[random];

        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 365.0f);
        this.transform.localScale = Vector3.one * this.size;
    }

    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody.AddForce(direction * this.speed);
        Destroy(this.gameObject, this.maxLifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Red Bullet" && random == 1)
        {
            if((this.size * 0.5) >= this.minSize)
            {
                SpawnSmallAsteroids();
            }

            gameManagerScript.AsteroidDestroyed(this);

            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Blue Bullet" && random == 0)
        {
            if ((this.size * 0.5) >= this.minSize)
            {
                SpawnSmallAsteroids();
            }
            gameManagerScript.AsteroidDestroyed(this);

            Destroy(this.gameObject);
        }
    }

    private void SpawnSmallAsteroids()
    {

        int randomInt = Random.Range(0, 2);
        for(int i = 0; i <= random; i ++)
        {
            Vector2 position = this.transform.position;
            position += Random.insideUnitCircle * 0.5f;

            Asteroid smallAsteroid = Instantiate(this, position, this.transform.rotation);
            smallAsteroid.size = this.size * 0.5f;

            smallAsteroid.SetTrajectory(Random.insideUnitCircle.normalized);
        }
       

    }
}

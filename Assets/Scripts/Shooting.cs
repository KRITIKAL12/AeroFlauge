using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject[] bulletPrefab;

    public float bulletForce = 20f;
    public int colorChange;
    public GameObject gameManager;
    private GameManager gameManagerScript;
    public GameObject hitEffect;
    public GameObject muzzleFlashPrefab;
    private float maxLifeTime = 3.0f;

    public GameObject mf; 


    private void Awake()
    {
        Cursor.visible = false;
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
           
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (colorChange == 0)
                colorChange = 1;
            else if (colorChange == 1)
                colorChange = 0;
        }

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        Vector2 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }


 void Shoot()
    {

        //GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
        //Destroy(muzzleFlash, maxLifeTime);

        Coroutine mFlash = StartCoroutine(muzzle());
        GameObject bullet = Instantiate(bulletPrefab[colorChange], firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        //StopCoroutine(mFlash);
        Destroy(bullet, this.maxLifeTime);
    }

   

    public void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.gameObject.tag == "Asteroid")
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);

            this.gameObject.SetActive(false);

            gameManagerScript.PlayerDied();
            FindObjectOfType<AudioManager>().Play("PlayerDeath");

        }
        
    }

    public IEnumerator muzzle()
    {
        mf.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        mf.gameObject.SetActive(false);
        yield return null;
    }
}

//if (collision.gameObject.tag == "Asteroid" || collision.gameObject.tag == "Red Bullet" || collision.gameObject.tag == "Blue Bullet")
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

    private void Awake()
    {
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
    }


 void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab[colorChange], firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Asteroid")
        {
            this.gameObject.SetActive(false);
            gameManagerScript.PlayerDied();
        }
    }
}


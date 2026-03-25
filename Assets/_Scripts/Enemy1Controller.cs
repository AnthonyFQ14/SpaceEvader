using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : MonoBehaviour
{
    public float shotDelay = 1.0f;
    float shotTimer;
    public Transform firePoint;
    public GameObject laserShot;
    public bool visible = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(visible)
        {
            shotTimer -= Time.deltaTime;
            if(shotTimer <= 0)
            {
                Instantiate(laserShot,firePoint.position, firePoint.rotation);
                shotTimer = shotDelay;
            }
        }
        
        
    }

    private void OnBecameVisible()
    {
        visible = true;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }    
    }
}

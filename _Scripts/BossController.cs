using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float shotDelay = 1.0f;
    float shotTimer;
    public Transform firePoint1;
    public Transform firePoint2;
    public Transform firePoint3;
    public Transform firePoint4;
    public GameObject laserShot1;
    public GameObject laserShot2;
    GameController gameController;
    public bool visible = false;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(visible)
        {
            shotTimer -= Time.deltaTime;
            if(shotTimer <= 0)
            {
                Instantiate(laserShot1,firePoint1.position, firePoint1.rotation);
                Instantiate(laserShot1,firePoint2.position, firePoint2.rotation);
                Instantiate(laserShot2,firePoint3.position, firePoint3.rotation);
                Instantiate(laserShot2,firePoint4.position, firePoint4.rotation);
                shotTimer = shotDelay;
            }
        }
        
        
    }

    private void OnBecameVisible()
    {
        visible = true;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        gameController.BossHit();
    }
}

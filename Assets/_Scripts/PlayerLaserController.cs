using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaserController : MonoBehaviour
{
    public float speed = 10.0f;
    GameController gameController;


    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            transform.position.x + speed * Time.deltaTime,
            transform.position.y,
            transform.position.z
        );
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        gameController.LaserHit();
        Destroy(gameObject); //destroy laser


        if(other.gameObject.tag == "BossShield")
        {
            gameController.BossShieldHit();
        } 
        else if(other.gameObject.tag == "Boss")
        {
            gameController.BossHit();
        }
        else
        {
            Destroy(other.gameObject); //destroy what we hit
        }
        


    }
}

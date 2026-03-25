using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserController : MonoBehaviour
{
    public float speed = -8.0f;
    // public GameObject bossShield;

    // Start is called before the first frame update
    void Start()
    {
        
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

    // void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.gameObject.tag == "Enemy")
    //     {
    //         Physics.IgnoreCollision(bossShield.GetComponent<Collider>(), GetComponent<Collider>());
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if(other.gameObject.tag != "Enemy")
        // {
            
        //     Debug.Log(other.gameObject.tag);
        // }
        //Debug.Log(other.gameObject.tag);
        Destroy(gameObject); //destroy laser

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3LaserController : MonoBehaviour
{
    public float speed = 30.0f;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {   
        rb.AddForce(rb.transform.right * speed);
        //rb.velocity = new Vector2(transform.position.x,transform.position.y);
        
        // transform.position = new Vector3(
        //     transform.position.x + speed * Time.deltaTime,
        //     transform.position.y,
        //     transform.position.z
        // );
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

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

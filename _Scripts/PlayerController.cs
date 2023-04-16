using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    GameController gameController;
    Vector2 moveInput;
    public float speed = 8;
    public Transform upperLeftBounds, lowerRightBounds;
    public Transform firePoint;
    public GameObject laserShot;
    public GameObject shield;
    public GameObject shieldTimer;
    public TMP_Text shieldText;
    public float timeRemainingShield = 10;
    public bool timerIsRunningShield = false;
    public float timeRemainingRapid = 10;
    public bool timerIsRunningRapid = false;

    public float shotDelay = 0.4f;
    float shotTimer;
    public bool rapidActive = false;


    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        rb = GetComponent<Rigidbody2D>();
        //rb.velocity = new Vector2(5.0f,5.0f);
        anim = GetComponent<Animator>();
        shotTimer = shotDelay;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );
        rb.velocity = moveInput * speed;
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, upperLeftBounds.position.x, lowerRightBounds.position.x),
            Mathf.Clamp(transform.position.y, lowerRightBounds.position.y, upperLeftBounds.position.y),
            transform.position.z
        );
        anim.SetFloat("PlayerMovement",Input.GetAxisRaw("Vertical"));

        if(Input.GetButtonDown("Fire1"))
        {
            Instantiate(laserShot,firePoint.position, firePoint.rotation);
        }
        if(Input.GetButton("Fire1"))
        {
            shotTimer -= Time.deltaTime;
            if(shotTimer <= 0)
            {
                Instantiate(laserShot,firePoint.position, firePoint.rotation);
                shotTimer = shotDelay;
            }
        }

        if(rapidActive)
        {
            shotDelay = 0.1f;
            shotTimer -= Time.deltaTime;
            if(shotTimer <= 0)
            {
                Instantiate(laserShot,firePoint.position, firePoint.rotation);
                shotTimer = shotDelay;
            }
        }
        else
        {
            shotDelay = 0.4f;
        }

        if(timerIsRunningShield)
        {
            if (timeRemainingShield > 0)
            {
                timeRemainingShield -= Time.deltaTime;
                shieldText.text = "Shield Timer: " + (int)timeRemainingShield; 
            }
            else
            {
                timeRemainingShield = 0;
                timerIsRunningShield = false;
            }
        }

        if(timerIsRunningRapid)
        {
            if (timeRemainingRapid > 0)
            {
                timeRemainingRapid -= Time.deltaTime;
                shieldText.text = "Rapid Fire Timer: " + (int)timeRemainingRapid; 
            }
            else
            {
                timeRemainingRapid = 0;
                timerIsRunningRapid = false;
            }
        }
    }

    public void shieldPowerup()
    {
        shield.SetActive(true);
        shieldTimer.SetActive(true);
        StartCoroutine(TenSecondTimerShield());
    }

    public void rapidPowerup()
    {
        rapidActive = true;
        shieldTimer.SetActive(true);
        StartCoroutine(TenSecondTimerRapid());
    }

    public void healthPowerup()
    {
        gameController.AddHealth();
        StartCoroutine(MoreLivesMessage());
    }

    IEnumerator MoreLivesMessage()
    {   
        shieldText.text = "+5 Lives";
        shieldTimer.SetActive(true);
        yield return new WaitForSeconds(1);
        shieldTimer.SetActive(false);
    }


    IEnumerator TenSecondTimerShield()
    {   
        timerIsRunningShield = true;
        yield return new WaitForSeconds(10);
        shield.SetActive(false);
        shieldTimer.SetActive(false);
    }

    IEnumerator TenSecondTimerRapid()
    {
        timerIsRunningRapid = true;
        yield return new WaitForSeconds(10);
        shieldTimer.SetActive(false);
        rapidActive = false;
        timeRemainingRapid = 10;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "BluePU")
        {
            shieldPowerup();
        }
        else if(other.gameObject.tag == "RedPU")
        {
            rapidPowerup();
        }
        else if(other.gameObject.tag == "GreenPU")
        {
            healthPowerup();
        }
        else if(!shield.activeSelf){
            gameController.LoseLife();
        }

        if(other.gameObject.tag == "Boss")
        {
            gameController.BossHit();
        }
        else if(other.gameObject.tag == "BossShield")
        {
            gameController.BossShieldHit();
        }
        else
        {
            Destroy(other.gameObject);
        }
        
        
        
    }

}

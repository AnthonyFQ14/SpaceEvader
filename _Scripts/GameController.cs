using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public int lives = 3;
    public GameObject[] wave;
    // public GameObject enemy1;
    public GameObject bossShield;
    public GameObject boss;
    public int shieldHealth = 20;
    public int bossLives = 40;
    public Transform spawnPoint;
    public float spawnDelay = 3.0f;
    float spawnTimer;
    public TMP_Text output;
    public AudioSource hitMarker;
    public AudioSource explosion;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaveTimer());
    }

    // Update is called once per frame
    void Update()
    {
        string outputText = "Lives: ";

        for(int i = 0; i < lives; i++)
        {
            outputText += "I";
        }
        
        output.text = outputText;
    }

    public void AddHealth()
    {
        lives += 5;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void YouWin()
    {
        SceneManager.LoadScene("YouWin");
    }

    public void BossShieldHit()
    {
        shieldHealth--;
        LaserHit();
        if(shieldHealth <= 0)
        {
            Destroy(bossShield);
        }
    }

    public void BossHit()
    {
        bossLives--;
        LaserHit();
        if(bossLives <= 0)
        {
            explosion.Play();
            Destroy(boss);
        }
    }

    IEnumerator WaveTimer()
    {
        Instantiate(wave[0],spawnPoint.position, spawnPoint.rotation);
        yield return new WaitForSeconds(8);
        Instantiate(wave[0],spawnPoint.position, spawnPoint.rotation);
        yield return new WaitForSeconds(8);
        Instantiate(wave[1],spawnPoint.position, spawnPoint.rotation);
        yield return new WaitForSeconds(14);
        Instantiate(wave[2],spawnPoint.position, spawnPoint.rotation);
        yield return new WaitForSeconds(14);
        Instantiate(wave[2],spawnPoint.position, spawnPoint.rotation);
        yield return new WaitForSeconds(14);
        Instantiate(wave[3],spawnPoint.position, spawnPoint.rotation);
        yield return new WaitForSeconds(14);
        boss.SetActive(true);
        Instantiate(wave[4],spawnPoint.position, spawnPoint.rotation);   
        yield return new WaitForSeconds(29);
        if(boss == null)
        {
            SceneManager.LoadScene("YouWin");
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }
        

    }

    

    public void LoseLife()
    {
        lives--;
        explosion.Play();
        if(lives <= 0)
        {
            GameOver();
        }
    }

    public void LaserHit()
    {
        hitMarker.Play();
    }

}

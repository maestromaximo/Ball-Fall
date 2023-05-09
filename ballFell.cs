using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ballFell : MonoBehaviour
{
    public GameObject spawner;
    public Text gameoverText;
    public GameObject panel;
    public Text score;
    public Button yesButton, noButton;
    public Text livesText;
    public Text highscoreText;
    public Camera mainCamera;
    public ParticleSystem ballCut;

    public int lives = 3;

    private int highscore = 0;
    private string ball = "Ball";
    private GameObject lastBall;

    [SerializeField] private Animator scoreAnimator;
    // public Text score;

     void Start()
    {
        lives = PlayerPrefs.GetInt("lives");
       

        gameoverText.enabled = false;
        panel.SetActive(false);
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        highscoreText.gameObject.SetActive(false);

        livesText.text = "Lives: " + lives;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ball)
        {
            lives -= 1;
            livesText.text = "Lives: " + lives;
            if (lives <= 0)
            {
                lastBall = other.gameObject;
                FocusOnLastBall();
               
            }
        }
        if (other.tag == "Ignore Ball" || other.tag == "Multi Ball PowerUp")
        {
            Destroy(other.gameObject);
        }

       
    }//fall

    void SetHighscore()
    {
        if (PlayerPrefs.HasKey("highscore"))
        {
            if (int.Parse(score.text) > PlayerPrefs.GetInt("highscore"))
            {
                PlayerPrefs.SetInt("highscore", int.Parse(score.text));
            }

        }
        else
        {
            PlayerPrefs.SetInt("highscore", int.Parse(score.text));
        }

        highscore = PlayerPrefs.GetInt("highscore");

        highscoreText.text = "High Score: " + highscore;

    }//SetHighscore

    public void EndGame()
    {
        ball = "notBall";

        

        SetHighscore();

        FindObjectOfType<AudioManager>().StopPlaying("Theme 1");
        FindObjectOfType<AudioManager>().Play("Theme 2");
        Destroy(livesText.gameObject);
        gameoverText.enabled = true;
        panel.SetActive(true);
        scoreAnimator.SetBool("moveScore", true);
        highscoreText.gameObject.SetActive(true);

        highscoreText.gameObject.transform.parent = panel.gameObject.transform;
        score.gameObject.transform.parent = panel.gameObject.transform;
        Destroy(spawner);
    }

    private void FocusOnLastBall()
    {
        Vector3 ballPos = lastBall.transform.position;

        mainCamera.transform.position = new Vector3(ballPos.x, ballPos.y, ballPos.z - 0.4f);
        mainCamera.ScreenPointToRay(ballPos);

        StartCoroutine(SlowTime());

        
    }

     IEnumerator SlowTime()
    {
        

        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        lastBall.GetComponent<Rigidbody>().drag = 10f;
        yield return new WaitForSecondsRealtime(1f);

        Instantiate(ballCut, lastBall.transform.position, mainCamera.transform.rotation);
        FindObjectOfType<AudioManager>().Play("Katana Cut");
        yield return new WaitForSecondsRealtime(2.8f);
        lastBall.GetComponent<Rigidbody>().useGravity = false;

        Time.timeScale = 1.3f;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        Destroy(lastBall);
        yield return new WaitForSecondsRealtime(1f);

        EndGame();
    }//SlowTime

}

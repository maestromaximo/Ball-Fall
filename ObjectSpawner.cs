using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject ballFellTrigger;

    public GameObject goodSpawn;
    public GameObject bombSpawn;
    public GameObject badSpawn;
    public GameObject multiBallPowerUp;
    public GameObject ignoreBall;

    float fasterDelayGood = 1;
    float fasterDelayBomb = 1;
    float fasterDelayBad = 1;

    public float goodDelay = 2f;
    public float bombDelay = 30f;
    public float badDelay = 13f;
    public float powerUpDelay = 43f;
    public float strengh = 2f;



    Vector3 spawnerInitPosition;

    float goodCurrentTime;
    float bombCurrentTime;
    float badCurrentTime;
    float powerUpCurrentTime;

    public float multiBallPowerupDuration = 5f;
    public bool isMultiBallPowerUpOn = false;
    private int difficulty = 1;

    // Start is called before the first frame update
    void Start()
    {
        spawnerInitPosition = gameObject.transform.position;
        goodCurrentTime = goodDelay;
        badCurrentTime = badDelay;
        bombCurrentTime = bombDelay;
        powerUpCurrentTime = powerUpDelay;

        difficulty = PlayerPrefs.GetInt("difficulty");

        if(difficulty == 0)
        {
            goodSpawn.transform.localScale.Scale(new Vector3(2f,2f,2f));
            goodSpawn.GetComponent<Rigidbody>().drag = goodSpawn.GetComponent<Rigidbody>().drag * 2;
            powerUpDelay = powerUpDelay - 10f;

        }
        else if(difficulty == 1)
        {

        }else if(difficulty == 2)
        {
            goodSpawn.transform.localScale.Scale(new Vector3(0.5f, 0.5f, 0.5f));
            goodSpawn.GetComponent<Rigidbody>().drag = goodSpawn.GetComponent<Rigidbody>().drag / 2;
            powerUpDelay = powerUpDelay + 10f;
            badDelay /= 10;
        }

        Debug.Log(difficulty);

    }

    // Update is called once per frame
    void Update()
    {
        goodCurrentTime -= Time.deltaTime;
        bombCurrentTime -= Time.deltaTime;
        badCurrentTime -= Time.deltaTime;
        powerUpCurrentTime -= Time.deltaTime;

       // goodDelay = goodDelay * fasterDelayGood;

        if(goodCurrentTime <= 0f)
        {

            Instantiate(goodSpawn, new Vector3(spawnerInitPosition.x * Random.insideUnitCircle.y * strengh, spawnerInitPosition.y, spawnerInitPosition.z), gameObject.transform.rotation);

            goodCurrentTime = goodDelay;
        }//goodspawn

        if(bombCurrentTime <= 0f)
        {
            Instantiate(bombSpawn, new Vector3(spawnerInitPosition.x * Random.insideUnitCircle.y * strengh, spawnerInitPosition.y, spawnerInitPosition.z), gameObject.transform.rotation);

            bombCurrentTime = bombDelay;
        }//bombspawn

        if (badCurrentTime <= 0f)
        {
            Instantiate(badSpawn, new Vector3(spawnerInitPosition.x * Random.insideUnitCircle.y * strengh, spawnerInitPosition.y, spawnerInitPosition.z), gameObject.transform.rotation);

            badCurrentTime = badDelay;
        }//badspawn

        if (powerUpCurrentTime <= 0f)
        {
            Instantiate(multiBallPowerUp, new Vector3(spawnerInitPosition.x * Random.insideUnitCircle.y * strengh, spawnerInitPosition.y, spawnerInitPosition.z), gameObject.transform.rotation);

            powerUpCurrentTime = powerUpDelay;
        }//powerUpspawn
        if (isMultiBallPowerUpOn)
        {
            ballFellTrigger.SetActive(false);
            Instantiate(ignoreBall, new Vector3(spawnerInitPosition.x * Random.insideUnitCircle.y * strengh, spawnerInitPosition.y, spawnerInitPosition.z), gameObject.transform.rotation);
            StartCoroutine(PowerUpDuration());
        }

    }//update

    public void InitiateMultiBallPower()
    {
        isMultiBallPowerUpOn = true;
    }//InitiateMultiBallPower

   

    IEnumerator PowerUpDuration()
    {
        yield return new WaitForSeconds(multiBallPowerupDuration);
        isMultiBallPowerUpOn = false;
        yield return new WaitForSeconds(6f);
        ballFellTrigger.SetActive(true);
    }//PowerUpDuration

    public void FasterSpawn()
    {
        if (goodDelay > 0) {
            goodDelay -= 0.01f;
                }
    }//FasterSpawn

    public void ChangeDifficulty(int serviceNum)
    {
        difficulty = serviceNum;
    }

}// Instantiate(goodSpawn, new Vector3(spawnerInitPosition.x + spawnerInitPosition.x / 2 * Random.insideUnitCircle.x, spawnerInitPosition.y, spawnerInitPosition.z), gameObject.transform.rotation);


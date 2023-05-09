using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectHitter : MonoBehaviour
{

    public float rayRange = 100f;
    GameObject hitBall;
    public ballFell bF;
    public Text score;
    float hitCounter = 0;
   

    public ObjectSpawner spawnAcess;
    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1")){
            ShootRay();
            //Debug.Log("shot");
        }
        
    }//Update

    void ShootRay()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, rayRange))
        {
            hitBall = hit.collider.gameObject;
            if (hit.transform.tag == "Ball")
            {
              //  hitBall = hit.collider.gameObject;
                
                Debug.Log(hitBall.tag);

                spawnAcess.FasterSpawn();
                
                Destroy(hitBall);
                hitCounter += 1;

                FindObjectOfType<AudioManager>().Play("Good Ball Hit");
               
                score.text = hitCounter.ToString();
                //Debug.Log(hitCounter);
            }//

            if(hit.transform.tag == "Bad Ball" && !spawnAcess.isMultiBallPowerUpOn)
            {

                FindObjectOfType<AudioManager>().Play("Bad Ball Hit");
                Destroy(hitBall);
                bF.EndGame();

            }
            if(hit.transform.tag == "Bomb")
            {

                Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                rb.isKinematic = true;
            }
            if(hit.transform.tag == "Ignore Ball")
            {
                Destroy(hitBall);
                FindObjectOfType<AudioManager>().Play("Good Ball Hit");
                hitCounter += 1;
                score.text = hitCounter.ToString();
            }
            if(hit.transform.tag == "Multi Ball PowerUp" && !spawnAcess.isMultiBallPowerUpOn)
            {
                FindObjectOfType<AudioManager>().Play("Powerup Hit");
                spawnAcess.InitiateMultiBallPower();
                Destroy(hitBall);
            }

        }//ray
    }//ShootRay
}

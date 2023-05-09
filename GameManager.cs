using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour   
{

    public Text gameoverText;
    public string newTextToSpell = "Try Again?";
    public Slider difficultySlider;
    

    private SceneManager SceneManager;
    private int difficultyValue;

    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.LoadSceneAsync(1);

        
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    public void StartButton()
    {
        SceneManager.LoadScene(1);

        SetLives();
        
    }

    void SetLives()
    {
        int testDif = PlayerPrefs.GetInt("difficulty");
        
        if(testDif == 0)
        {
            PlayerPrefs.SetInt("lives", 5);
        }else if(testDif == 1)
        {
            PlayerPrefs.SetInt("lives", 3);
        }
        else if (testDif == 2)
        {
            PlayerPrefs.SetInt("lives", 1);
        }

    }//SetLives

    public void SettingsButton()
    {

        SceneManager.LoadScene(2);
    }

    public void SliderMoved()
    {
        difficultyValue = (int)difficultySlider.value;

        PlayerPrefs.SetInt("difficulty", difficultyValue);
        //Debug.Log(difficultyValue);
        
    }//SliderMoved

}//GameManager

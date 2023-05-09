using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YesButtonClick : MonoBehaviour
{
   public void RestartGame()
    {
        FindObjectOfType<AudioManager>().Play("Theme 1");
        SceneManager.LoadScene(1);
    }
}

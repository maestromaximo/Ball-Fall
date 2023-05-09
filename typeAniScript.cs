using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class typeAniScript : MonoBehaviour
{
    public Text gameoverText;
    public Button yesButton;
    public Button noButton;
    public string newTextToSpell = "Try Again?";

   
    public void EndGameAnimations()
    {
        string currentText = gameoverText.text;
        int charsInText = currentText.Length;


        StartCoroutine(EndGameSpeller(currentText, charsInText));

    }//EndGameAnimations

    IEnumerator EndGameSpeller(string currentText, int charsInText)
    {


        for (int lastChar = (charsInText - 1); lastChar >= 0; lastChar--)
        {
            string aniText = currentText.Substring(0, lastChar);
            gameoverText.text = aniText;
            yield return new WaitForSeconds(0.2f);
        }//errase

       
        StartCoroutine(SecondPart());
    }//EndgameSpeller


    IEnumerator SecondPart()
    {
        yield return new WaitForSeconds(2f);
        int charsInNew = newTextToSpell.Length;
        Debug.Log(charsInNew);

        for (int i = 0; i < charsInNew; i++)
        {
            
            yield return new WaitForSeconds(0.2f);
            string aniText = newTextToSpell.Substring(0, charsInNew);
            gameoverText.text = aniText;
            Debug.Log(i);
        }//spell

        DisplayButtons();
    }//secomd

    void DisplayButtons()
    {
        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);
    }//displayButtons
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoSlide : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        Slider slider = gameObject.GetComponent<Slider>();

        slider.SetValueWithoutNotify((float)PlayerPrefs.GetInt("difficulty"));
    }

    
}

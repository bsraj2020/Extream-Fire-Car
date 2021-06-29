using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
using TMPro;

public class Speed_text : MonoBehaviour
{
    
     private Car_Yellow Car ; 
    [SerializeField] public TMP_Text speed_text ;
    void Start()
    {
        Car  = FindObjectOfType<Car_Yellow>() ;
    }

   
  public void Refresh_Speed()
    {
        speed_text.text = "Speed : " + (int)((Car.car.velocity.magnitude) *5f +Time.deltaTime)  + " KM/H";

      //  Debug.Log((Car.car.velocity.magnitude));
    }
}

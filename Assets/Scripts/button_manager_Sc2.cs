using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class button_manager_Sc2 : MonoBehaviour
{
    [SerializeField] TMP_Text[] options_texts;

   
  public  void new_game_btn()
    {
           SceneManager.LoadScene( "Scene1" );
    }
  public void Exit_btn()
    { 
         Application.Quit();
    }
    private void Conitinue_btn()
    {
        
    }
}

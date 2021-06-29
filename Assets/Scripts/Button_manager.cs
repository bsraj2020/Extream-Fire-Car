using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Button_manager : MonoBehaviour
{
     private Car_Yellow Car;
     

    private Vector3 start_pos;
   Quaternion starting_rot ;
    [SerializeField]private ParticleSystem Rocket_fire;
    [SerializeField] private AudioSource audioSource ;
    [SerializeField]private AudioClip[] audioClips;
    [SerializeField]private GameObject camera_cube;

    
    private Enemy_Ball enemy_Ball;
    [SerializeField] private ParticleSystem bomb;

    [SerializeField] private GameObject enemy_prefab;
    private int camera_no=0;
    private Vector3[] camera_arr={ new Vector3(0,0,0) ,new Vector3(0,0,0) , };
    private Vector3 pre=Vector3.zero;
    public bool Is_turbo_mode=false;
    [SerializeField]private Button Turbo_img ;
   

  
    void Start()
    {
        Car = FindObjectOfType<Car_Yellow>();
        start_pos = Car.transform.position;
        starting_rot = Car.car.rotation;
        enemy_Ball = FindObjectOfType<Enemy_Ball>();
        camera_arr[0] = new Vector3( 0f,1f,-2f ) ;
        camera_arr[1] = new Vector3( 0f,1f,2f ) ;
        //Turbo_img = GetComponent<Button>();
        // camera_arr[2] = new Vector3() ;

        
       
      
    }

   
  

   public void On_Reset_ButtonClick()
    {
       // Car.car.position = start_pos;
       Car.car.position = Car.car.position+new Vector3(0f,2f,0.5f);
        Car.car.rotation  =  Quaternion.Euler(0f,0f,0f) ;
        //Car.car.velocity = Vector3.zero ;
        Car.car.rotation = starting_rot ;
    }
    public void On_Turbo_button_Click()
    {
        // audioSource.clip = audioClips[0];
        // audioSource.Play();
        // Rocket_fire.Play();

        if (Is_turbo_mode ==false )
        {
            Is_turbo_mode=true;
            Car.MotorForce = 5000;
            Turbo_img.image.color = Color.red ;
            Car.Trust_flame.SetActive(true);
           //StartCoroutine("Turbo_Mode_On") ;      
        }
        else {
            Car.MotorForce = 1000;
            Is_turbo_mode=false;
           Turbo_img.image.color = Color.white ;
            Car.Game_over_text.text="";   
             Car.Trust_flame.SetActive(false); 
        }
        

       
    }

  public void On_camera_change_btn_click()
  {
     
     
    //  var n = camera_arr[0] + Vector3.zero ;
     pre = camera_cube.transform.position = (camera_cube.transform.position - pre) + camera_arr[camera_no++] ;
     camera_no = camera_no%2 ;

}

IEnumerable Turbo_Mode_On()
{
    Is_turbo_mode=true;
    Car.MotorForce = 2000;
     Turbo_img.image.color = Color.red ;
    Car.Game_over_text.text="Turbo Mode On";
    yield return new WaitForSeconds(2) ;
    Car.Game_over_text.text="";

    yield return new WaitForSeconds(8) ;
    Car.MotorForce = 1000;
    Is_turbo_mode=false;
    Turbo_img.image.color = Color.white ;

}

 public void Accerator_btn_Down_Click()
{
    Car.Accerator_btn_Down = 1 ;
}
 public void Accerator_btn_Up_Click()
{
    Car.Accerator_btn_Down = 0 ;
}

public void Options_exit_Scene()
{
    SceneManager.LoadScene("Scene2") ;
}

}
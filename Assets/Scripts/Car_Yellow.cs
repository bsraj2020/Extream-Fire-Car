using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class Car_Yellow : MonoBehaviour
{


    [SerializeField] public Rigidbody car;
    [SerializeField] private GameObject Camera;
    [SerializeField] private float horizontal = 0.0f, vertical = 0.0f;
    [SerializeField] private int  max_steering_angle = 30;
    public int MotorForce = 500,MotorForce_MAIN; 
    private Joystick joystick;
    public TMP_Text Game_over_text;
    
   [SerializeField] private TMP_Text Score_txt;
   [SerializeField] private TMP_Text Your_Score_txt;
   [SerializeField] private TMP_Text health_text;
   private int Score=0;
    
    [SerializeField] private AudioSource Car_engine_sound;
    // [SerializeField]private AudioSource Car_Gun_Sounds;
    // [SerializeField] private AudioClip[] Car_Gun_Clips; Explosive sound Only

    private Speed_text speed_Text_obj;
    [SerializeField] private Image Health_bar;
    [SerializeField] private ParticleSystem bomb;
    [SerializeField] private ParticleSystem Rocket_fire ; 
     
    //[SerializeField] private GameObject Line;
    private Slider Slider_Acc;
    private int max_Car_health = 10000, cur_Car_health = 10000;
    public int Accerator_btn_Down=0;
    public GameObject Trust_flame;

    [SerializeField]private GameObject enemyball;





    //Wheels Colliders
    [SerializeField] private WheelCollider front_left_WheelCollider;
    [SerializeField] private WheelCollider front_right_WheelCollider;
    [SerializeField] private WheelCollider back_left_WheelCollider;
    [SerializeField] private WheelCollider back_right_WheelCollider;

    //Wheels Transform
    [SerializeField] private Transform front_left_WheelTransform;
    [SerializeField] private Transform front_right_WheelTransform;
    [SerializeField] private Transform back_left_WheelTransform;
    [SerializeField] private Transform back_right_WheelTransform;
    private Enemy_Ball enemy_Ball;

    private Vector3 car_starting_pos;
 

    void Start()
    {
        car = GetComponent<Rigidbody>();
        //Camera = GetComponent<GameObject>() ;

        // offset =   Camera.transform.position - car.transform.position ; 

        joystick = FindObjectOfType<Joystick>();
        Car_engine_sound = GetComponent<AudioSource>();
        //health_text= GetComponent<TMP_Text>();

        // Car__Sounds = GetComponent<AudioSource>();
        enemyball  =GetComponent<GameObject>();
        speed_Text_obj = FindObjectOfType<Speed_text>();
        Slider_Acc = GameObject.Find("Acc_Slider").GetComponent<Slider>();
        enemy_Ball = FindObjectOfType<Enemy_Ball>();
       // Game_over_text = GameObject.Find("GameOver_Text").GetComponent<Text>();
        MotorForce_MAIN=MotorForce;
        Slider_Acc.value=0.3f;

        // Fire_flame_Prefab= GetComponent<ParticleSystem>() ;
        car_starting_pos  = car.transform.position;
      
    }


    void Update()
    {
        float joystic_Horizontal ; int sign = joystick.Horizontal < 0 ? -1:1;
        
       joystic_Horizontal =  ((Mathf.Abs(joystick.Horizontal)- 0.3f)>0) ?  Mathf.Abs(joystick.Horizontal) -0.3f : 0f;
        
        horizontal = Input.GetAxis("Horizontal") + joystic_Horizontal*sign   ;
        vertical =   Input.GetAxis("Vertical") + joystick.Vertical + Accerator_btn_Down;
    }

    // Physics Related Updation is Fixed Update
    void FixedUpdate()
    {
        front_left_WheelCollider.motorTorque = MotorForce * Slider_Acc.value * (vertical);
        front_right_WheelCollider.motorTorque = MotorForce * Slider_Acc.value* (vertical );
        back_left_WheelCollider.motorTorque = MotorForce * Slider_Acc.value* (vertical );
        back_right_WheelCollider.motorTorque = MotorForce * Slider_Acc.value* (vertical );


        front_left_WheelCollider.steerAngle = max_steering_angle * (horizontal );
        front_right_WheelCollider.steerAngle = max_steering_angle * (horizontal);

        // Update Visuals of Wheels
        UpadteAllWheels_Visuals();

        var speed_magnitude = car.velocity.magnitude ;

        Car_engine_sound.pitch = 1 + speed_magnitude / 15 ;

        speed_Text_obj.Refresh_Speed();

        if( Mathf.Abs(Vector3.Dot(transform.up,Vector3.down) ) <0.125f) 
        {
            car.centerOfMass = new Vector3(0,-0.9f,0); //flipping problem
        }
        else  car.centerOfMass = new Vector3(0,0,0);


        int layerMask = 1 << 8; // 100000000 

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask; // 011111111 ditect everything excluding Player(layer 8)

        RaycastHit hit;

        // Does the ray intersect any objects excluding the player layer


        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask) )
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);

            //    Car_Gun_Sounds.clip = Car_Gun_Clips[Random.Range(0,Car_Gun_Clips.Length)];
            //  Car_Gun_Sounds.Play();
            //  Debug.Log("Raycast collision") ;
          //  Line.SetActive(true);
            // Bomb sound with enemy
           
           if(hit.transform.gameObject!=null && hit.transform.gameObject.tag == "Finish") 
           {
                bomb.transform.position = hit.transform.position;
                bomb.Play(); Score+=100;
                Score_txt.text ="Score : "+ Score ;
                bomb.Play();
              if(hit.transform.gameObject) enemy_Ball.Hit_Object(hit.transform.gameObject);
              Destroy(hit.transform.gameObject);
             //  StartCoroutine( Deactivated_Ball( hit.transform.gameObject) );
                //hit.transform.gameObject.SetActive(false);

           }
           
            


        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.black);
            //  Debug.Log("Did not Hit");
         //   Line.SetActive(false);
        }

    }

    void UpadteAllWheels_Visuals()
    {
        Update_Wheel_Visual(back_left_WheelCollider, back_left_WheelTransform);
        Update_Wheel_Visual(back_right_WheelCollider, back_right_WheelTransform);
        Update_Wheel_Visual(front_left_WheelCollider, front_left_WheelTransform);
        Update_Wheel_Visual(front_right_WheelCollider, front_right_WheelTransform);

    }


    void Update_Wheel_Visual(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Quaternion rot; //rotation
        Vector3 pos;//position

        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            // Debug.Log("car collison with ball") ;
            cur_Car_health -= 455;
            Health_bar.fillAmount = (float)(cur_Car_health) / (float)max_Car_health;
            health_text.text = "Health : "+ Mathf.Round(Health_bar.fillAmount*100)  + "%" ;
            if (cur_Car_health <= 0) StartCoroutine ("Game_Over");


        }
        else if (collision.gameObject.CompareTag("end_cube"))
        {
            Debug.Log("you completed this level");

           GameObject.Find("Plateform").transform.localScale *= 4 ;
           transform.position = car_starting_pos;

        }
        else if (collision.gameObject.CompareTag("Fire_column"))
        {
            Debug.Log("you died");
            bomb.transform.position = car.transform.position;
            bomb.Play();
           StartCoroutine( Game_Over());
        }


        else Debug.Log("Random Object collison");
        // Debug.Log("car collison with ball bahr") ;
    }


    IEnumerator Game_Over()
    {
        Game_over_text.text = "GAME OVER"; Health_bar.fillAmount=0;
        Game_over_text.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
        MotorForce=0;
        yield return new WaitForSeconds(1);
       Your_Score_txt.text = "Your Score : "+ Score;
           yield return new WaitForSeconds(1);
            
       Game_over_text.text = "TRY AGAIN"; 
       Game_over_text.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
      
         yield return new WaitForSeconds(2) ;
         Your_Score_txt.text = "";
        // MotorForce = MotorForce_MAIN;  Game_over_text.text = "";
        // transform.position = new Vector3(1209.25244f,529.5200f,-735.994446f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
 
    }
    IEnumerator Deactivated_Ball(GameObject obj)
    {
        obj.SetActive(false);
        yield return new WaitForSeconds(10);
        obj.SetActive(true);
    }


}
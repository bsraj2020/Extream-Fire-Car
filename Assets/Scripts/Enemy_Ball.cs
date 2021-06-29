using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ball : MonoBehaviour
{
    
     private Car_Yellow car;
     [SerializeField]private Rigidbody ball;
     [SerializeField]private AudioSource ball_Explosive_Sound;

     
     public int Enemy_Health=100 ;

     void Start()
     {
         car  = FindObjectOfType<Car_Yellow>();
         ball = GetComponent<Rigidbody>() ;
         
         MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
         Material ball_mat_2 = GetComponent<Material>() ;
         ball_Explosive_Sound = GetComponent<AudioSource>() ;
         
        
     }

     void FixedUpdate()
     {
         var pos_car = car.car.position;

        ball.AddForce( (pos_car - transform.position)* Random.Range(50,100));

      //if( Vector3.Distance(transform.position,car.car.position)<=100 ) this.gameObject.SetActive(true);
     }

    public void Hit_Object(GameObject obj)
     {
         Enemy_Health -= 100 ;
        
         if(obj!=null && Enemy_Health<=0  ) { ball_Explosive_Sound.Play();/* Destroy(this.gameObject)*/;}
         
     }

   

}

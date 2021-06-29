using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Column : MonoBehaviour
{
    // Start is called before the first frame update

   private Car_Yellow car;
   [SerializeField]private  GameObject enemy_Ball;
 float time=0, time_to_generate_Balls=60;
    void Start()
    {
        car = FindObjectOfType<Car_Yellow>() ;
        //enemy_Ball = GetComponent<GameObject>() ;

        for(int i=0;i<7;i++)
        {
            Instantiate(enemy_Ball,transform.position+new Vector3(0f,1f,0f), transform.rotation);
            
        }
    }
  
  void Update()
  {
      if(Vector3.Distance(transform.position, car.car.position) <=100 )
      {
           
           if(time >  time_to_generate_Balls)
           {
               time=0;  generate_Balls();
           } 
      } time+= Time.deltaTime;
           
  }

    void generate_Balls()
    {
        for(int i=0;i<5;i++)
        {
            Instantiate(enemy_Ball,transform.position+new Vector3(0f,1f,0f), transform.rotation);
            
        }
    }
   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   [SerializeField] private GameObject car ; 
   [SerializeField] private GameObject cameracube;
    [SerializeField] private float Speed = 1f; //For smoothing of Tralstation of camera

    // private Vector3 offset;   We are not Using this

    void Start()
    {
       // can Iniatize GameObjects here
    }
    
    // in Update method camera Translation would shake because of changing FrameRates So used Fixed Update
    void FixedUpdate()
    {                                           // Lerp( camera, car , smoothing) -> 
         gameObject.transform.position  = Vector3.Lerp( transform.position , cameracube.transform.position, Time.deltaTime *Speed  );
         gameObject.transform.LookAt(car. transform.position) ;
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decorboat : Enemy
{
    [SerializeField] float vel = 6f;
    [SerializeField] float distance=0;
    [SerializeField] float cap = 100;
   
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        this.transform.position = new Vector3( this.transform.position.x + (vel*Time.deltaTime) , this.transform.position.y, this.transform.position.z);
        distance += (vel * Time.deltaTime);
        if ( distance > cap ) { vel = 0; } //stop the boat after certain distance
        
    }

    public void OnCollisionEnter(Collision other )
    {
      //  Debug.Log("target");
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    void Start()
    {
        this.Invoke("SD", 3f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SD()
    {
        Destroy(this.gameObject);
    }
    public void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "MainShip")
        //{
            Destroy(this.gameObject);
        //}
    }
}

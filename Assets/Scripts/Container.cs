using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Container : MonoBehaviour
{
    public AudioSource bang;
    [SerializeField] public bool inside = false;
    //[SerializeField] public Text numtxt;
    [SerializeField] public int innum ;
    GameObject cnt;

    // Start is called before the first frame update
    void Start()
    {
        
        bang = GetComponent<AudioSource>();
        //Loadtext ts = GetComponent<Loadtext>();

        this.GetComponent<Rigidbody>().mass = 40;
        this.GetComponent<Rigidbody>().centerOfMass = new Vector3(0, -1.5f, 0);
        //Debug.Log(this.GetComponent<Rigidbody>().centerOfMass);
    }

    void OnCollisionEnter (Collision other) {

        bang.Play();

        if (other.gameObject.name == "Sinkzone")
        {
            cnt = GameObject.Find("count");
            cnt.GetComponent<Count>().Sank();


            Destroy(this.gameObject); }
        
        
    }

    void OnTriggerEnter(Collider other)
    {
        inside= true;
       // innum +=1;
        //numtxt.text = innum.ToString();
    }

    void OnTriggerExit(Collider other)
    {
        inside = false;
        //innum -= 1;
       // numtxt.text = innum.ToString();
    }

    // Update is called once per frame
    void Update()
    {



    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
    
{
    public AudioSource bang;
    [SerializeField] public GameObject fx;
    // Start is called before the first frame update
    void Start()
    {
        this.Invoke("SD", 3f);
        bang = GetComponent<AudioSource>();
        particle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SD() 
    {
        Destroy(this.gameObject);
    }
    public void OnCollisionEnter(Collision other)
    {
        //if (other.gameObject.tag=="enemy")
        // {
        bang.Play();
        particle();
        Destroy(this.gameObject);
       
     
    }
    void particle() 
    { 
     var _Fire = Instantiate( fx , this.transform.position, this.transform.rotation);
        //_Fire.SetActive(true);
        //_Fire.transform.localScale = new Vector3(8, 8, 8);
        Destroy(_Fire, 0.6f);
    
    }
}

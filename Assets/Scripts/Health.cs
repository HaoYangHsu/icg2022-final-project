using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour


{ [SerializeField] public int max_health = 20;
    [SerializeField]public int currenthealth;
    [SerializeField] GameObject Object;
    [SerializeField] GameObject Explosion;
    [SerializeField] GameObject Fire;
   


    public GameObject score;
    [SerializeField] float pointyield ;
    //public event <float>  OnHealthPctChanged = delegate { };

    public Healthbar healthbar;
    public AudioSource bang;
    private bool hasplayed = false;
    void explode() {

       // GameObject _Explosion = Instantiate(Explosion, this. transform.position, transform.rotation);
        Explosion.SetActive(true);
        //_Explosion.transform.localScale = new Vector3(5, 5, 5);
        Destroy(Explosion,5);

        //GameObject _Fire = Instantiate(Fire, this.transform.position, transform.rotation);
        Fire.SetActive(true);
        Fire.transform.localScale = new Vector3(5,5,5);
        Destroy(Fire, 1);

    }

    // Start is called before the first frame update
    void OnEnable()
    {
        bang = GetComponent<AudioSource>();
        currenthealth = max_health;
        healthbar.SetMaxHealth(max_health);
        score = GameObject.Find("Scoretext");
    }
    private void Start()
    {
        max_health = currenthealth; 
    }
    public void Modifyhealth(int amount) 
    {
        currenthealth += amount;
        //float currenthealthpct = (float)currenthealth / (float)max_health;
        //  OnHealthPctChanged(currenthealthpct);
        healthbar.SetHealth(currenthealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (currenthealth <= 0) 

        {
           // explode();
            SoundOnce();
            Object.transform.position = new Vector3(this.transform.position.x , this.transform.position.y - (5 * Time.deltaTime), this.transform.position.z);
            Destroy(this.gameObject,1.5f);

           //score.GetComponent<Score>().points += pointyield; ;

        }
     
    
    }
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "bullet")
        {
            
            Modifyhealth(-10);
            MeshRenderer renderer = Object.GetComponent<MeshRenderer>();
            renderer.material.color = Color.red;
            Invoke("DamageFlash", 0.25f);
        }
    }
    void DamageFlash() 
    {
        MeshRenderer renderer = Object.GetComponent<MeshRenderer>();
        renderer.material.color = Color.white;
    }
    void SoundOnce()
    {

        if (!hasplayed)
        {
            bang.Play();
            explode();
            score.GetComponent<Score>().points += pointyield; ;
            hasplayed = true;

        }
    }


    }

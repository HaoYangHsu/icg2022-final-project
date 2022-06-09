using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyHealth : MonoBehaviour
{
    [SerializeField] int max_health = 20;
    [SerializeField] public int currenthealth;
    [SerializeField] GameObject Object;

    [SerializeField] public float radius = 50;
    [SerializeField] public float expforce = 1000;
    [SerializeField] GameObject Explosion;
    [SerializeField] GameObject badend;

    public AudioSource bang;
    private bool hasplayed = false;
    //public event <float>  OnHealthPctChanged = delegate { };

    public Healthbar healthbar;

    // Start is called before the first frame update

    void explode()
    {
        Vector3 above =new Vector3(this.transform.position.x , this.transform.position.y + 15, this.transform.position.z);
        GameObject _Explosion = Instantiate(Explosion, above, transform.rotation);
        _Explosion.SetActive(true);
        //Explosion.transform.localScale = new Vector3(100, 100, 100);
        Destroy(_Explosion, 8);

    }

    void OnEnable()
    {
        bang = GetComponent<AudioSource>();
        currenthealth = max_health;
        healthbar.SetMaxHealth(max_health);
    }
      
     
    public void Modifyhealth(int amount)
    {
        
        currenthealth += amount;
        //float currenthealthpct = (float)currenthealth / (float)max_health;
        //  OnHealthPctChanged(currenthealthpct);
        healthbar.SetHealth(currenthealth);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (currenthealth <= 0)
        {

            Collider[] colliders = Physics.OverlapSphere(Object.transform.position, radius);

            foreach (Collider proximity in colliders)

            { if ( proximity.gameObject.tag == "block")
                {
                    Rigidbody rigg = proximity.GetComponent<Rigidbody>();
                    if (rigg != null)
                    {
                        rigg.AddExplosionForce(expforce, transform.position, radius);

                    }
                }
            }
            SoundOnce();
            //LIGHT.color = Color.red + Color.blue * 0.3f + Color.green * 0.1f;
            // LIGHT.intensity = 0.5f;
            // m_camera.clearFlags = CameraClearFlags.SolidColor;
            //  GameOver.SetActive(true);
            badend.SetActive(true);
            Destroy(this.gameObject,1f);

        }


        // if (Input.GetKeyDown(KeyCode.Space)) {
        //   Modifyhealth(-10);
        // }
        currenthealth = Mathf.Clamp(currenthealth, 0, max_health); 

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemybullet")
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
    void SoundOnce() {

        if (!hasplayed) {
            bang.Play();
            explode();
            hasplayed = true;
   
        }
    
    }

    }

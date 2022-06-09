using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretsManager : MonoBehaviour
{

    private GameObject[] m_MyTurrets;
    [SerializeField] GameObject Actions; 
    [SerializeField] GameObject Boat;
    [SerializeField]GameObject score;
    public float  Rangeupgrade = 1.1f;
    public float Rapidupgrade = 1.1f;
    int TurretsLenOrigin;
    public bool visible = false;
    public float Maxlevel = 10;
    public float Rangelevel = 0;
    public float Rapidlevel = 0;
    [SerializeField] Text Rangetxt;
    [SerializeField] Text Rapidtxt;
    [SerializeField] GameObject HealFx;
    [SerializeField] GameObject Fx;


    //public float points;
    // Start is called before the first frame update
    void Start()
    {
        //score = GameObject.Find("Scoretext");
        TurretsLenOrigin = GameObject.FindGameObjectsWithTag("MyTurret").Length;
        UpdateMyTurrets();
    }
    void FX() {
        var primitiveIns = GameObject.Instantiate(Fx);
        primitiveIns.SetActive(true);
        primitiveIns.transform.localPosition = new Vector3(Boat.transform.position.x, Boat.transform.position.y + 10, Boat.transform.position.z);
        Destroy(primitiveIns,2f);
    
    }
    
    // Update is called once per frame
    void Update()
    {
        Rangetxt.text = "LV: " + Rangelevel.ToString();
        Rapidtxt.text = "LV: " + Rapidlevel.ToString();
    }
    public void Range()
    {
      if (Rangelevel < Maxlevel) { 
        if (score.GetComponent<Score>().points >= 1000) {
            
                for (int i = 0; i < m_MyTurrets.Length; i++)
                {
                    m_MyTurrets[i].GetComponent<SphereCollider>().radius *= Rangeupgrade;
                }
                score.GetComponent<Score>().points -= 1000;
                Rangelevel += 1; 
                }
            FX();
        }
    }
    
    public void Heal()
    {
        if (score.GetComponent<Score>().points >= 800 )
        {
            Boat.GetComponent<MyHealth>().Modifyhealth(80);

            var primitiveIns = GameObject.Instantiate(HealFx);
            primitiveIns.SetActive(true);
            primitiveIns.transform.localPosition = new Vector3(Boat.transform.position.x, Boat.transform.position.y+10, Boat.transform.position.z );

            Destroy(primitiveIns,3f);
                
            score.GetComponent<Score>().points -= 800;
        }

    }


    public void Rapidshot()
    {
        if (Rangelevel < Maxlevel)
        {
            if (score.GetComponent<Score>().points >= 2000)
            {
                for (int i = 0; i < m_MyTurrets.Length; i++)
                {
                    m_MyTurrets[i].GetComponent<MyTurret>().ShotInterval /= Rapidupgrade;
                }
                score.GetComponent<Score>().points -= 2000;
             Rapidlevel += 1;
             FX();
            }
            
            
        }   
    }

    public void UpdateMyTurrets()
    {
        m_MyTurrets = GameObject.FindGameObjectsWithTag("MyTurret");
        int TurretsLen = m_MyTurrets.Length;
        //Debug.Log(TurretsLen);

        float ShotInterval = m_MyTurrets[0].GetComponent<MyTurret>().ShotInterval;
        float radius = m_MyTurrets[0].GetComponent<SphereCollider>().radius;

        for (int i = TurretsLenOrigin; i < TurretsLen; i++)
        {
            m_MyTurrets[i].GetComponent<MyTurret>().ShotInterval = ShotInterval;
            m_MyTurrets[i].GetComponent<SphereCollider>().radius = radius;
        }
    }

    public void OpenClose() 

    {
        visible =! visible ;
        Actions.SetActive(visible);
    }
        


       // Actions.SetActive(false);

    
}



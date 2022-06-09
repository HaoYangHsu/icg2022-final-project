using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Count : MonoBehaviour
{
    private GameObject[] getCount;

    [SerializeField] GameObject m_TruckPrefab;
    [SerializeField] GameObject m_lastTruck;
    public Score score;
    [SerializeField] private Text numtxt;
    //[SerializeField] bool inside = false;
    public int ToUnload;
    [SerializeField] GameObject enemygen;
    [SerializeField] int demand;
    [SerializeField] private GameObject Speed;
    public bool visible = false;
    public bool checkpoint1 = true;
    public bool checkpoint2 = true;
    public int Unloaded = 1;
    int sunken = 0;
    [SerializeField] GameObject badend;
    GameObject[] m_Mycont;
    int ingame=0;



    public void Sank()
    { sunken++;
    if(ingame -sunken < demand) {

            badend.SetActive(true);
     
        }
    
    }

    // Start is called before the first frame update
    void Start()
    {
        numtxt.text = "Remaining : " + ToUnload.ToString();
        ToUnload++;
        demand = ToUnload;
        demand--;

        m_Mycont = GameObject.FindGameObjectsWithTag("block");
        ingame = m_Mycont.Length;
    }
        void Speedup1(float time, int HP)
        {
          
           
            Debug.Log("Speedup!");

            visible = !visible;
            Speed.SetActive(visible) ;
            
           

        enemygen.GetComponent<PrimitivesGenerator>().delay /=time ;
        enemygen.GetComponent<PrimitivesGenerator>().HPboost += HP;
             
        }


        void Flashtext(){
            
            visible = !visible;
            Speed.SetActive(visible);

        }
    // Update is called once per frame
    void Update()
    {
         Debug.Log(demand);
        //Debug.Log((demand - Unloaded + 2) / demand);
        getCount = GameObject.FindGameObjectsWithTag("truck");
        int count = getCount.Length;
        //Debug.Log(count);
        if (count == 0)
        {
            ToUnload--;
            Unloaded++;

            score.points += 2000;

            Speedup1(1.2f,10);

            Invoke("Flashtext", 1);
            //Flashtext();

           // m_Mycont = GameObject.FindGameObjectsWithTag("block");
           // int TurretsLen = m_MyTurrets.Length;



            numtxt.text = "Remaining : " + ToUnload.ToString();

            if (ToUnload == 1)
            {
                m_lastTruck.SetActive(true);
            }

            else
            {
                GameObject.Instantiate(m_TruckPrefab);
            }
        }

        
    }
}

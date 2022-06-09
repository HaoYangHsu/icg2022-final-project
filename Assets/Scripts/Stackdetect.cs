using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Stackdetect : MonoBehaviour
{
    [SerializeField] GameObject[] contentity = new GameObject[11];
    [SerializeField] private Text hitxt;
    [SerializeField] private Text numtxt;
    float[] A = new float [11];
    public int sum = 0;
    float[] B = new float[11];


    public float defaultheight = 0;
    public Container script;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {


        detectheight(); 
     }
    void detectheight()
    {
        for (int i = 0; i < 11; i++)
        { A[i] = contentity[i].transform.position.y; }

         defaultheight = A.Max();

        hitxt.text = defaultheight.ToString();
           

        

    }
}

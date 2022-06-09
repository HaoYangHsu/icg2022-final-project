using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birds : MonoBehaviour
{

    [SerializeField] GameObject bird;
    [SerializeField] Vector2 m_Dimension = new Vector2(20, 20);
    public float spd= 14f;
    //GameObject primitiveIns;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawn", 1 , 5);
    }
    void spawn() { 
    
    
      var primitiveIns = GameObject.Instantiate(bird);
        primitiveIns.SetActive(true);
        primitiveIns.transform.localPosition = new Vector3(this.transform.position.x, transform.position.y, Random.Range(-m_Dimension.x, m_Dimension.y));
        

       Destroy(primitiveIns,15);
    
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}


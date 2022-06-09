using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimitivesGenerator : MonoBehaviour
{
    [SerializeField]public  GameObject m_Enemyboat1;
    [SerializeField] float spawn = 1.5f ;
    [SerializeField] public float delay = 1.5f;
    //[SerializeField] GameObject m_SpherePrefab;
    [SerializeField] Vector2 m_Dimension = new Vector2(20,20);
    public int HPboost=0;

    // Start is called before the first frame update
    void Awake ()
    {
        InvokeRepeating ("GeneratePrimitives", spawn , delay);
        // GeneratePrimitives(m_SpherePrefab, Random.Range(5, 10));
    }

    public void GeneratePrimitives()
    {
       
            var primitiveIns = GameObject.Instantiate(m_Enemyboat1);
            primitiveIns.transform.localPosition = new Vector3(this.transform.position.x, transform.position.y, Random.Range(-m_Dimension.x, m_Dimension.y));
            primitiveIns.GetComponent<Health>().currenthealth += HPboost;


    }
    
}

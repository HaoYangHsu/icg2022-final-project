using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TruckControl : MonoBehaviour
{

    const float delayTime = 5;
    float t;

    float m_velocity;
    const float init_velocity = 10f;
    const float stop_pos = 10f;
    const float stop_acc = 20f;

    MeshCollider TrailerEntry;

    [SerializeField] GameObject trailer;
    [SerializeField] Transform truck;
    MeshRenderer m_Renderer;
    // Start is called before the first frame update
    void Start()
    {
        //TrailerEntry = trailer.GetComponent<MeshCollider>();
        m_velocity = init_velocity;
        truck.gameObject.tag = "truck";
        m_Renderer = trailer.GetComponent<MeshRenderer>();
        
    }

    //private void OnTriggerEnter(Collider collision)
    //{
    //    if (collision.gameObject.tag == "block")
    //    {
    //        inside = true;
    //        ToUnload--;
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "block")
        {
            ResetTimer(Color.white);
            //Debug.Log("exit");
            //inside = false;
            //ToUnload++;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "block")
        {
            m_Renderer = trailer.GetComponent<MeshRenderer>();
            if (CheckIfContain(other))
            {
                //Debug.Log("within");
                foreach (Material mat in m_Renderer.materials)
                {
                    mat.color = Color.green;
                }
                t += Time.deltaTime;
                if (t > delayTime)
                {
                    ResetTimer(Color.white);
                    //Debug.Log("complete");
                    
                    Destroy(trailer.GetComponent<BoxCollider>());  //turn off triggering

                    m_velocity += 0.5f;
                    other.transform.SetParent(truck);
                    
                }
            }
            else
            {
                //Debug.Log("out of bound");
                ResetTimer(Color.red);
            }
        }
    }


    //Update is called once per frame
    void Update()
    {
        if (m_velocity > 0f)
        {
            truck.Translate(m_velocity * Time.deltaTime * Vector3.forward);
            float delta_z = truck.position.z - stop_pos;
            //Debug.Log("delta_z = " + delta_z);

            if (Mathf.Abs(delta_z) < stop_acc)
            {
                if (delta_z > 0f)
                {
                    m_velocity += delta_z * Time.deltaTime;
                }
                else if (delta_z > -0.05f)
                {
                    m_velocity = 0f;
                    truck.Translate(0.05f * Vector3.forward);
                }
                else
                {
                    m_velocity = -delta_z / stop_acc * init_velocity;
                }
            }

            else if (delta_z > 150f)
            {
                Destroy(truck.gameObject);
            }


            //if (truck.transform.eulerAngles.y != 0f)
            //{
            //    Debug.Log("not aligned");
            //}



        }
        //Debug.Log("velocity = " + m_velocity);
    }

    bool CheckIfContain(Collider container)
    {
        TrailerEntry = trailer.GetComponent<MeshCollider>();
        var containerBound = container.bounds;
        var trackBound = TrailerEntry.bounds;
        return trackBound.max.x > containerBound.max.x
            && trackBound.max.z > containerBound.max.z
            && trackBound.min.x < containerBound.min.x
            && trackBound.min.z < containerBound.min.z;
    }

    void ResetTimer(Color color)
    {
        t = 0;
        foreach (Material mat in m_Renderer.materials)
        {
            mat.color = color;
        }
    }
}



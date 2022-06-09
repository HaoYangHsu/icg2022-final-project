using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCrane : MonoBehaviour
{
    [SerializeField] const float JIB_ROTATION_SPEED = 10f; // for rotation of the jib
    [SerializeField] const float MOVE_SPEED = 1.8f; // for moving of the trolley
    [SerializeField] const float CRANE_SPEED = 2.0f; // for lifting and lower of the hook
    [SerializeField] const float ROTATOR_SPEED = 20f; // for rotation of the rotator



    [SerializeField] GameObject m_Jib;
    [SerializeField] GameObject m_Trolley;
    [SerializeField] GameObject m_Rotator;
    [SerializeField] LineRenderer m_Cable1;
    [SerializeField] LineRenderer m_Cable2;
    ConfigurableJoint[] m_Joints;



    // Start is called before the first frame update
    void Start()
    {
        m_Joints = m_Rotator.GetComponents<ConfigurableJoint>();
        //Debug.Log(m_Joints.Length);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_Jib.transform.Rotate(0, 0, -JIB_ROTATION_SPEED * Time.fixedDeltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            m_Jib.transform.Rotate(0, 0, JIB_ROTATION_SPEED * Time.fixedDeltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow) && m_Trolley.transform.localPosition.y > -16f)
        {
            m_Trolley.transform.Translate(0, -MOVE_SPEED * Time.fixedDeltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && m_Trolley.transform.localPosition.y < -2f)
        {
            m_Trolley.transform.Translate(0, MOVE_SPEED * Time.fixedDeltaTime, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_Rotator.transform.Rotate(0, -ROTATOR_SPEED * Time.fixedDeltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            m_Rotator.transform.Rotate(0, ROTATOR_SPEED * Time.fixedDeltaTime, 0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            //var joint = m_Trolley.gameObject.GetComponent<ConfigurableJoint>();
            var limit = m_Joints[0].linearLimit;
            if (limit.limit >= 1f)
            {
                limit.limit -= CRANE_SPEED * Time.fixedDeltaTime;
                m_Joints[0].linearLimit = limit;
                m_Joints[1].linearLimit = limit;

            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //var joint = m_Trolley.gameObject.GetComponent<ConfigurableJoint>();
            var limit = m_Joints[0].linearLimit;
            if (limit.limit <= 40f)
            {
                limit.limit += CRANE_SPEED * Time.fixedDeltaTime;
                m_Joints[0].linearLimit = limit;
                m_Joints[1].linearLimit = limit;

            }
        }

        UpdateCable();
    }

    void UpdateCable()
    {
        var Position0 = m_Rotator.transform.position;
        float Rotation0 = m_Rotator.transform.localEulerAngles.x * Mathf.Deg2Rad + Mathf.PI/4.0f;
        //Debug.Log(Rotation0);
        var anchorTransform = m_Rotator.transform;
        m_Cable1.SetPosition(0, anchorTransform.TransformPoint(m_Joints[0].anchor));
        m_Cable2.SetPosition(0, anchorTransform.TransformPoint(m_Joints[1].anchor));
        //m_Cable1.SetPosition(0, new Vector3(Position0.x + 0.2f * Mathf.Cos(Rotation0)
        //    , Position0.y, Position0.z + 0.2f * Mathf.Sin(Rotation0)));
        //m_Cable2.SetPosition(0, new Vector3(Position0.x - 0.2f * Mathf.Cos(Rotation0)
        //    , Position0.y, Position0.z - 0.2f * Mathf.Sin(Rotation0)));
        var connectedBodyTransform = m_Joints[0].connectedBody.transform;
        m_Cable1.SetPosition(1, connectedBodyTransform.TransformPoint(m_Joints[0].connectedAnchor));
        m_Cable2.SetPosition(1, connectedBodyTransform.TransformPoint(m_Joints[1].connectedAnchor));
    }

    void SelfDestruct()
    {
    Destroy(this.gameObject);
    
    }


}

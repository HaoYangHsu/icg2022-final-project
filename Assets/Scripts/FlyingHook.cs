using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingHook : MonoBehaviour
{
    [SerializeField] const float MOVE_SPEED = 4f;
    [SerializeField] const float ATTACH_DISTANCE =2f;
    GameObject m_DetectedObject;
    ConfigurableJoint[] m_JointForObjects = new ConfigurableJoint[4];
    [SerializeField] LineRenderer[] m_Cable;
    Vector3[] AnchorPos = new Vector3[4];


    // Update is called once per frame
    void LateUpdate()
    {
        bool to_detect = true;
        foreach (ConfigurableJoint joint in m_JointForObjects)
        {
            if (joint != null)
            {
                to_detect = false;
                break;
            }
        }


        if (to_detect)
        {
            //Debug.Log("detecting...");
            DetectObjects();
        }
        else { UpdateCable(); }


        if (Input.GetKeyDown (KeyCode.Space))
        {
            AttachOrDetachObject(to_detect);
        }
        
        
    }

    void DetectObjects ()
    {
        Ray ray = new Ray(this.transform.position, Vector3.down);
        RaycastHit hit; // A RaycastHit to store the raycast result
       
        //if (m_JointForObjects != null)
        //{
        //    return;
        //}

        if (Physics.Raycast(ray, out hit, ATTACH_DISTANCE))
        {
            if (m_DetectedObject != null && m_DetectedObject == hit.collider.gameObject)
            {
                return;
                
            }

            RecoverDetectObject();

            MeshRenderer renderer = hit.collider.GetComponent<MeshRenderer>();

            if (renderer != null && hit.collider.gameObject.tag == "block")
            {
                renderer.material.color = Color.yellow;
                m_DetectedObject = renderer.gameObject;
            }
        }
        else
        {
            RecoverDetectObject();
        }
    }

    void RecoverDetectObject()
    {
        if (m_DetectedObject != null)
        {
            m_DetectedObject.GetComponent<MeshRenderer>().material.color = Color.white;
            m_DetectedObject = null;
        }
    }

    void AttachOrDetachObject(bool to_attach)
    {


        if (to_attach)
        {
  
            if (m_DetectedObject != null)
            {
                
                var joint0 = this.gameObject.AddComponent<ConfigurableJoint>();
                var joint1 = this.gameObject.AddComponent<ConfigurableJoint>();
                var joint2 = this.gameObject.AddComponent<ConfigurableJoint>();
                var joint3 = this.gameObject.AddComponent<ConfigurableJoint>();

                Vector3 ContainerHalfSize = m_DetectedObject.GetComponent<BoxCollider>().size / 2.0f;
                //Debug.Log(ContainerHalfSize);

                //Quaternion rotation = m_DetectedObject.transform.rotation;
                //Matrix4x4 m = Matrix4x4.Rotate(rotation);
                //Debug.Log(m);

                AnchorPos[0] = new Vector3(ContainerHalfSize.x
                    , ContainerHalfSize.y, ContainerHalfSize.z);
                AnchorPos[1] = new Vector3(-ContainerHalfSize.x
                    , ContainerHalfSize.y, ContainerHalfSize.z);
                AnchorPos[2] = new Vector3(-ContainerHalfSize.x
                    , ContainerHalfSize.y, -ContainerHalfSize.z); 
                AnchorPos[3] = new Vector3(ContainerHalfSize.x
                    , ContainerHalfSize.y, -ContainerHalfSize.z);

                m_JointForObjects[0] = joint0;
                m_JointForObjects[1] = joint1;
                m_JointForObjects[2] = joint2;
                m_JointForObjects[3] = joint3;
                //Debug.Log(string.Format("number of joints = {0}",m_JointForObjects.Length));

                for (int i = 0; i < m_JointForObjects.Length; i ++)
                {
                    m_JointForObjects[i].xMotion = ConfigurableJointMotion.Limited;
                    m_JointForObjects[i].yMotion = ConfigurableJointMotion.Limited;
                    m_JointForObjects[i].zMotion = ConfigurableJointMotion.Limited;
                    m_JointForObjects[i].angularXMotion = ConfigurableJointMotion.Limited;
                    m_JointForObjects[i].angularYMotion = ConfigurableJointMotion.Limited;
                    m_JointForObjects[i].angularZMotion = ConfigurableJointMotion.Limited;

                    var limit = m_JointForObjects[i].linearLimit;
                    limit.limit = 5.5f;
                    m_JointForObjects[i].linearLimit = limit;

                    m_JointForObjects[i].autoConfigureConnectedAnchor = false;
                    m_JointForObjects[i].connectedAnchor = AnchorPos[i];
                    m_JointForObjects[i].anchor = new Vector3(0f, -0.27f, 0f);
                    //m_JointForObjects[i].anchor = new Vector3(AnchorPos[i].x/10f, 0f, AnchorPos[i].z / 10f);
                    m_JointForObjects[i].connectedBody = m_DetectedObject.GetComponent<Rigidbody>();

                    var damper = m_JointForObjects[i].yDrive.positionDamper;
                    damper = 3f;
                    var ydrive = m_JointForObjects[i].yDrive;
                    ydrive.positionDamper = 30f;
                    m_JointForObjects[i].yDrive = ydrive;
                }
                          

                m_DetectedObject.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
        else
        {
            for (int i = 0; i < m_JointForObjects.Length; i++)
            {
                m_Cable[i].enabled = false;
                GameObject.Destroy(m_JointForObjects[i]);
                m_JointForObjects[i] = null;
            }

            m_DetectedObject.GetComponent<MeshRenderer>().material.color = Color.white;
            m_DetectedObject = null; 
        }
    }

    void UpdateCable()
    {
        for (int i = 0; i < m_JointForObjects.Length; i++)
        {
            m_Cable[i].enabled = m_JointForObjects[i] != null;

            if (m_Cable[i].enabled)
            {
                var anchorTransform = this.transform;
                m_Cable[i].SetPosition(0, anchorTransform.TransformPoint(m_JointForObjects[i].anchor));

                var connectedBodyTransform = m_JointForObjects[i].connectedBody.transform;
                m_Cable[i].SetPosition(1, connectedBodyTransform.TransformPoint(m_JointForObjects[i].connectedAnchor));
            }
        }
    }    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEntity : MonoBehaviour



{
    [SerializeField] GameObject m_Target;
    [SerializeField] GameObject m_Base;
    [SerializeField] GameObject m_Gun;
    [SerializeField] GameObject m_BulletPrefab;

    [SerializeField] float min_disttotarget = 100f;

    const float MAX_BASE_ROTATION_VELOCITY = 360f;
    const float MAX_GUN_ROTATION_VELOCITY = 90f;
    [SerializeField] float Shottime = 0;
    [SerializeField] float ShotInterval = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }
    public void OnTriggerEnter(Collider collider)
    {
        //Debug.Log("Target");

        if (collider.gameObject.tag == "MainShip")
        {
            if (m_Target == null)
            {
                m_Target = collider.gameObject;
                //istargeting = true;
            }
            else
            {
                m_Target = null;
                //istargeting = false;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (m_Target != null)
        {
            Vector3 diffvec = m_Target.transform.position - this.transform.position;
            Vector3 xzProjection = new Vector3(diffvec.x, 0, diffvec.z);

            var targetBaseQuaternion = Quaternion.FromToRotation(
                Vector3.left, xzProjection);

            m_Base.transform.localRotation = Quaternion.RotateTowards(
               m_Base.transform.localRotation,
               targetBaseQuaternion,
               MAX_BASE_ROTATION_VELOCITY * Time.deltaTime);

            float xzProjectedLength = xzProjection.magnitude;

            var targetGunQuaternion = Quaternion.FromToRotation(
                   new Vector3(-xzProjectedLength, 0f, 0f),
                   new Vector3(-xzProjectedLength, diffvec.y, 0f)
                   );

            m_Gun.transform.localRotation = Quaternion.RotateTowards(
                m_Gun.transform.localRotation,
                targetGunQuaternion,
                MAX_GUN_ROTATION_VELOCITY * Time.deltaTime);


            if (diffvec.magnitude < min_disttotarget) //start firing
            {
                if (Shottime > ShotInterval)
                {
                    Fire();
                    Shottime = 0;
                }
                else { Shottime += Time.deltaTime; }


            }
        }

        
    }
    void Fire()
           {
            var bullet = GameObject.Instantiate(m_BulletPrefab);
            bullet.transform.position = m_Gun.transform.position;
        bullet.GetComponent<Rigidbody>().velocity
          = (m_Target.transform.position - m_Gun.transform.position).normalized * 100;

    }
}

 



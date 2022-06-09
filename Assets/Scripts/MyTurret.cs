using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTurret : MonoBehaviour



{
    [SerializeField] GameObject m_Target;
    bool istargeting = false;
    [SerializeField] GameObject m_Base;
    [SerializeField] GameObject m_Gun;
    [SerializeField] GameObject m_BulletPrefab;
    [SerializeField] float Shottime = 0;
    [SerializeField] public float ShotInterval = 1f;
    private GameObject[] m_enemies;

    bool currenttarget = false;

    const float MAX_BASE_ROTATION_VELOCITY = 360f;
    const float MAX_GUN_ROTATION_VELOCITY = 90f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
   private void Update()
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
            if (Shottime > ShotInterval)
            {
                Fire();
                Shottime = 0;
            }
            else { Shottime += Time.deltaTime; } 
        }
    }
    
    public void OnTriggerEnter(Collider boxcollider )
    {
        //Debug.Log("Target");

        if (boxcollider.TryGetComponent<Enemy>(out Enemy enemy))
        {
            if (m_Target == null)
            {  //m_enemies =;
               


                m_Target = boxcollider.gameObject;
                    istargeting = true;            
            }
            else
            {
               m_Target = null;
              istargeting = false;
            }
        }
    }
    public void OnTriggerStay(Collider boxcollider)
    {
        //Debug.Log("Target");

        if (boxcollider.TryGetComponent<Enemy>(out Enemy enemy))
        {
            if (m_Target == null)
            {
               // m_enemies = GameObject.FindGameObjectsWithTag("enemy");
             //   m_enemies.trans



                m_Target = boxcollider.gameObject;
                istargeting = true;
            }
            else
            {
          
            }
        }
    }

 



    void Fire()
    {
        var bullet = GameObject.Instantiate(m_BulletPrefab);
        bullet.transform.position = m_Gun.transform.position;
        bullet.GetComponent<Rigidbody>().velocity = -m_Gun.transform.right * 200;

    }
  
    
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEnding : MonoBehaviour
{
    [SerializeField] GameObject enemygen;
    private GameObject[] enemies;
    [SerializeField] private Text numtxt;
    [SerializeField] Camera m_Camera;
    [SerializeField] Transform LastTruck;
    [SerializeField] GameObject ending_UI;

    const float CAMERA_ROTATION_VELOCITY = 10f;
    const float DecelerateFrom = 5f;
    const float init_velocity = 3.0f;
    float m_velocity;
    int t = 180;

    //public 

    // Start is called before the first frame update
    void Start()
    {
        DeleteEnemies();
        numtxt.text = "Task completed!";
        m_Camera.transform.SetParent(LastTruck);
        var camera_control = m_Camera.GetComponent<FixedCamera>();
        camera_control.enabled = false;
        m_velocity = init_velocity;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //if m_Camera.transform.localRotation.x==0


        if (m_velocity > 0f)
        {
            CameraRotate();

            Vector3 diffVec = LastTruck.position + new Vector3(0, 6, -12) - m_Camera.transform.position;
            m_Camera.transform.Translate(m_velocity * Time.fixedDeltaTime * diffVec.normalized);
            //Debug.Log("d = " + diffVec.magnitude);
            if (diffVec.magnitude < DecelerateFrom)
            {
                m_velocity = diffVec.magnitude / DecelerateFrom * (init_velocity * 0.5f) + (init_velocity * 0.5f);
                //Debug.Log("v = " + m_velocity);
            }



            if (diffVec.magnitude < 0.03f)
            {
                m_velocity = 0f;
            }
            //m_Camera.transform.position = Vector3.MoveTowards(Vector3.zero, diffVec
            //    , m_velocity * Time.fixedDeltaTime);
        }

        else
        {
            float x_pos = LastTruck.position.x;
            if (t > 0)
            {
                t -= 1;
                LastTruck.eulerAngles += Vector3.up / 2f;
                float y_radian = Mathf.Deg2Rad * LastTruck.eulerAngles.y;
                //Debug.Log(y_radian);
                LastTruck.position += 4f * Time.fixedDeltaTime * new Vector3(Mathf.Sin(y_radian), 0, Mathf.Cos(y_radian));
            }

            else if (x_pos < 51f)
            {
                LastTruck.position += 6f * Time.fixedDeltaTime * Vector3.right;
            }

            else if (x_pos < 71f)
            {
                LastTruck.position += (75f - x_pos) / 4f * Time.fixedDeltaTime * Vector3.right;
            }
            else if (x_pos < 75f)
            {
                LastTruck.position += 1f * Time.fixedDeltaTime * Vector3.right;
            }

            else if (t > -400)
            {
                t -= 1;
                m_Camera.transform.Rotate(5f * Time.fixedDeltaTime * Vector3.left);

            }

            else
            {
                ending_UI.SetActive(true);
            }
        }
    }

    //void FixedUpdate()
    //{
    //    if (m_velocity > 0f)
    //    {
    //        Vector3 diffVec = LastTruck.position - this.transform.position;
    //        m_Camera.transform.Translate(m_velocity * Time.fixedDeltaTime * diffVec.normalized);
    //        Debug.Log(diffVec.magnitude);
    //        if (diffVec.magnitude < DecelerateFrom)
    //        {
    //            m_velocity = diffVec.magnitude / DecelerateFrom * init_velocity;
    //        }
    //        //m_Camera.transform.position = Vector3.MoveTowards(Vector3.zero, diffVec
    //        //    , m_velocity * Time.fixedDeltaTime);
    //    }
    //}


    void DeleteEnemies()
    {
        Destroy(enemygen);
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    void CameraRotate()
    {
        Vector3 diffVec = LastTruck.position - m_Camera.transform.position;
        var targetQuaternion =
            Quaternion.FromToRotation(
                Vector3.zero,
                new Vector3(diffVec.x, 0, diffVec.z));

        m_Camera.transform.localRotation = Quaternion.RotateTowards(
            m_Camera.transform.rotation,
            targetQuaternion,
            CAMERA_ROTATION_VELOCITY * Time.fixedDeltaTime);
    }
}

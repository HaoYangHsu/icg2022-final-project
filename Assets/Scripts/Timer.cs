using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer: MonoBehaviour
{
    [SerializeField] public float time = 600;
    [SerializeField] private Text timetxt;
    [SerializeField] Camera m_camera;
    [SerializeField] GameObject Ending;
    [SerializeField] GameObject GameOver;
    [SerializeField] Light LIGHT;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            timetxt.text = time.ToString();
        }
        else
        {
           // Debug.Log("Time's Up!");
          //  LIGHT.color = Color.red + Color.blue * 0.3f + Color.green * 0.1f;
           // LIGHT.intensity = 0.5f;
           // m_camera.clearFlags = CameraClearFlags.SolidColor;
            
          //  GameOver.SetActive(true);


            GameOver.SetActive(true);
        }

        if (Ending.activeInHierarchy == true || GameOver.activeInHierarchy == true)
        {
            this.enabled = false;
        }
    }
}

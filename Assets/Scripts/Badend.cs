using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Badend : MonoBehaviour
{
    [SerializeField] GameObject enemygen;
    private GameObject[] enemies;
    [SerializeField] Light LIGHT;
    [SerializeField] Camera m_camera;
    [SerializeField] GameObject GameOver;
    public GameObject song;
    // Start is called before the first frame update
    void Start()
    {
        DeleteEnemies();

        song = GameObject.Find("Song");
        Destroy(song);


        LIGHT.color = Color.red + Color.blue * 0.3f + Color.green * 0.1f;
        LIGHT.intensity = 0.5f;
        m_camera.clearFlags = CameraClearFlags.SolidColor;
        GameOver.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void DeleteEnemies()
    {
        Destroy(enemygen);
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}

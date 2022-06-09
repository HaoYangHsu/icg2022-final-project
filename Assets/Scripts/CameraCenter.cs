using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCenter : MonoBehaviour
{
    [SerializeField] Camera[] m_Cameras = new Camera[2];
    [SerializeField] GameObject m_TurretSetup;
    [SerializeField] GameObject m_Shop;
    int m_SelectedIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        SelectCamera(m_SelectedIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            NextCamera();
        }
    }

    void NextCamera()
    {
        // Select next camera

        m_SelectedIndex++;

        if (m_SelectedIndex >= m_Cameras.Length)
        {
            m_SelectedIndex = 0;
        }

        SelectCamera(m_SelectedIndex);
    }

    void SelectCamera(int index)
    {
        for (int i = 0; i < m_Cameras.Length; i++)
        {
            m_Cameras[i].enabled = i == index;
            if (index == 1)
            {
                m_TurretSetup.SetActive(true);
                m_Shop.SetActive(false);
            }
            else
            {
                m_TurretSetup.SetActive(false);
                m_Shop.SetActive(true);
            }
        }
    }
}

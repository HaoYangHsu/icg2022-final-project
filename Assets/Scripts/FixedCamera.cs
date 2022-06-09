using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCamera : MonoBehaviour
{
	[SerializeField] GameObject hook;

	Vector3 m_MousePosition;

	//float m_HorizontalAngle = 0;
	float m_VerticalAngle = 0;

	// Update is called once per frame
	void Update()
	{

		if (Input.GetMouseButtonDown(0))
		{

			m_MousePosition = Input.mousePosition;

		}
		else if (Input.GetMouseButton(0))
		{

			//Vector3 mouseDeltaPosition = m_MousePosition - Input.mousePosition;
			Vector3 mouseDeltaPosition = m_MousePosition - Input.mousePosition;

			//m_HorizontalAngle -= mouseDeltaPosition.x * 0.1f;
			m_VerticalAngle = Mathf.Clamp(m_VerticalAngle + mouseDeltaPosition.y * 0.1f, -89f, 89f);

			this.transform.localEulerAngles = new Vector3(m_VerticalAngle + 90f, 0f, 0f);

			m_MousePosition = Input.mousePosition;
		}		
	}
}

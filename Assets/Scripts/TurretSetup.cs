using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSetup : MonoBehaviour
{
    public Camera cam;
    [SerializeField] GameObject TurretPrefabs;
    [SerializeField] GameObject Turrets;
    
    [SerializeField] GameObject score;
    public float energy_required = 4000f;
    // Start is called before the first frame update
    void Start()
    {
        //score = GameObject.Find("Scoretext");
    }

    // Update is called once per frame
    void Update()
    {
        if (score.GetComponent<Score>().points > energy_required)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    //Debug.Log(hit.collider.gameObject);
                    SetTurret(hit.point);
                }
            }
        }
    }

    void SetTurret(Vector3 point)
    {
        score.GetComponent<Score>().points -= energy_required;
        float posY = 2f;
        float scalse_factor = (cam.transform.position.y - posY) / (cam.transform.position.y - point.y);
        float posX = cam.transform.position.x + (point.x - cam.transform.position.x) * scalse_factor;
        float posZ = cam.transform.position.z + (point.z - cam.transform.position.z) * scalse_factor;
        var NewTurret = GameObject.Instantiate(TurretPrefabs);
        NewTurret.transform.position = new Vector3(posX, posY, posZ);
        NewTurret.transform.SetParent(Turrets.transform);
        var Manager = Turrets.GetComponent<TurretsManager>();
        Manager.UpdateMyTurrets();
    }
}

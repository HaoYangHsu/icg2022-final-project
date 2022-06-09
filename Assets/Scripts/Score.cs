using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] Text scoretxt;

    public float points = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void print()
    {
        Debug.Log(" You scored "+ points.ToString()+ " Nice! ");
      //  UnityEditor.EditorApplication.isPlaying = false;
    }
    // Update is called once per frame
    void Update()
    {

      //  points = (timeremaining.time) + (unloaded.ToUnload * 100);
        scoretxt.text = "Points : " + points.ToString();

    }
}

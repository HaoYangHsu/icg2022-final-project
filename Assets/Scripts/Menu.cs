using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    
    [SerializeField] GameObject GameCanvas;
    [SerializeField] GameObject Enemygen;
    [SerializeField] GameObject MenuCam;
    [SerializeField] GameObject Count;
    [SerializeField] GameObject GameCam;
    [SerializeField] GameObject Camcenter;
    [SerializeField] GameObject Containers;
    [SerializeField] GameObject diff;
    [SerializeField] GameObject Topcam;
    [SerializeField] GameObject Actions;
    //[SerializeField] Slider vol;
    [SerializeField] GameObject Boat;
    bool visible;
    public string a;
    public GameObject difftxt;
    private void Start()
    {
       difftxt = GameObject.Find("Difficulty");

       Text DIFFICULTY = difftxt.GetComponent<Text>();
        

    }


   // public void Changevol() {

     //   AudioListener.volume = vol.value;
   // }

    public void PlayGame() {

        this.gameObject.SetActive(false);
        GameCanvas.SetActive(true);
        Enemygen.SetActive(true);
        MenuCam.SetActive(false);
        Topcam.SetActive(true);
        Count.SetActive(true);
        Boat.SetActive(true);
        GameCam.SetActive(true);
        Camcenter.SetActive(true);
        Containers.SetActive(true);
        

        //SceneManager.LoadScene(a);
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
       // UnityEditor.EditorApplication.isPlaying = false;
    }
    public void back() {

        SceneManager.LoadScene(a);
    }

    public void easy() {

       diff.GetComponent<Count>().ToUnload = 3;
        Text DIFFICULTY = difftxt.GetComponent<Text>();
        DIFFICULTY.text = "DIFFICULTY:EASY";
    }
    public void Normal() {

       diff.GetComponent<Count>().ToUnload = 6;
        Text DIFFICULTY = difftxt.GetComponent<Text>();
        DIFFICULTY.text = "DIFFICULTY:NORMAL";
    }
    public void Hard() {

        diff.GetComponent<Count>().ToUnload = 10;
        Text DIFFICULTY = difftxt.GetComponent<Text>();
        DIFFICULTY.text = "DIFFICULTY:HARD";
    }

    public void Optionstoggle() {

        
        visible = !visible;
        Actions.SetActive(visible);

    }
    //public void Optionstoggle() { }



}

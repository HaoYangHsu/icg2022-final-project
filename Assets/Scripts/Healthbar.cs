using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Healthbar : MonoBehaviour
{   [SerializeField] private Image foregroundImage;
    [SerializeField] private float updateSpeedSeconds = 0.5f;

    public Slider slider;

    public void SetHealth(int health)
    {
        
        slider.value = health;
    }
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    // Start is called before the first frame update
    //  void Awake()
    // {
    //GetComponentInParent<Health>().OnHealthPctChanged += HandleHealthChanged;
    //  }
    //private void HandleHealthChanged(float pct)
    //{
    //foregroundImage.fillAmount = pct;
    //  StartCoroutine(ChangeToPct(pct));
    //}

    //private IEnumerator ChangeToPct(float pct) 
    //{
    //  float preChangePct = foregroundImage.fillAmount;
    //float elapsed = 0f;

    //  while (elapsed < updateSpeedSeconds) {
    //    elapsed += Time.deltaTime;
    //  foregroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / updateSpeedSeconds);
    //   yield return null;

    //}
    //foregroundImage.fillAmount = pct;

    //}


    // Update is called once per frame



    void LateUpdate()
    {
      // transform.LookAt(Camera.main.transform);
      // transform.Rotate(0,180,0);
    }
}

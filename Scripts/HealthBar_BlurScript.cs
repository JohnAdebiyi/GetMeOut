using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar_BlurScript : MonoBehaviour {

    public Image barRedBlur;

    // Use this for initialization
    void Start ()
    {
        barRedBlur.enabled = false;  
    }


    public void ShowBlur()
    {

        StartCoroutine(OnOff_Healthblur());

    }

    public IEnumerator OnOff_Healthblur()
    {

        barRedBlur.enabled = true;
        yield return new WaitForSeconds(0.10f);
        barRedBlur.enabled = false;

    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeypadSafes_ButtonColorTrans : MonoBehaviour {

    public Camera fpsCam;
    public Image button0;
    public Image button1;
    public Image button2;
    public Image button3;
    public Image button4;
    public Image button5;
    public Image button6;
    public Image button7;
    public Image button8;
    public Image button9;
    public Image buttonStern;
    public Image buttonRaute;
    public Image buttonDelete;
    public Color32 onPressedColor = new Color32(14, 12, 12, 225);
    public Color32 normalColor = new Color32(225, 225, 225, 225);//tip-> slide the alpha bar value in the inspector to maximum
    private float colorTime = 0.3f;
    private bool _inTrigger;


    IEnumerator OnPressed_Button0()
    {
        button0.color = onPressedColor;
        yield return new WaitForSeconds(colorTime);
        button0.color = normalColor;
    }
    IEnumerator OnPressed_Button1()
    {
        button1.color = onPressedColor;    
        yield return new WaitForSeconds(colorTime);
        button1.color = normalColor; 
    }
    IEnumerator OnPressed_Button2()
    {
        button2.color = onPressedColor;
        yield return new WaitForSeconds(colorTime);
        button2.color = normalColor;
    }
    IEnumerator OnPressed_Button3()
    {
        button3.color = onPressedColor;
        yield return new WaitForSeconds(colorTime);
        button3.color = normalColor;
    }
    IEnumerator OnPressed_Button4()
    {
        button4.color = onPressedColor;
        yield return new WaitForSeconds(colorTime);
        button4.color = normalColor;
    }
    IEnumerator OnPressed_Button5()
    {
        button5.color = onPressedColor;
        yield return new WaitForSeconds(colorTime);
        button5.color = normalColor;
    }
    IEnumerator OnPressed_Button6()
    {
        button6.color = onPressedColor;
        yield return new WaitForSeconds(colorTime);
        button6.color = normalColor;
    }
    IEnumerator OnPressed_Button7()
    {
        button7.color = onPressedColor;
        yield return new WaitForSeconds(colorTime);
        button7.color = normalColor;
    }
    IEnumerator OnPressed_Button8()
    {
        button8.color = onPressedColor;
        yield return new WaitForSeconds(colorTime);
        button8.color = normalColor;
    }
    IEnumerator OnPressed_Button9()
    {
        button9.color = onPressedColor;
        yield return new WaitForSeconds(colorTime);
        button9.color = normalColor;
    }

    IEnumerator OnPressed_ButtonRaute()
    {
        buttonRaute.color = onPressedColor;
        yield return new WaitForSeconds(colorTime);
        buttonRaute.color = normalColor;
    }
    IEnumerator OnPressed_ButtonStern()
    {
        buttonStern.color = onPressedColor;
        yield return new WaitForSeconds(colorTime);
        buttonStern.color = normalColor;
    }
    IEnumerator OnPressed_ButtonDelete()
    {
        buttonDelete.color = onPressedColor;
        yield return new WaitForSeconds(colorTime);
        buttonDelete.color = normalColor;
    }




    // Update is called once per frame
    void Update()
    {


        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 2))
        {
            if (hit.collider.gameObject.tag == "safe_Bedroom" || hit.collider.gameObject.tag == "Safe_Livingroom")
            {
                _inTrigger = true;
            }
        }
        else
        {
            _inTrigger = false;
        }

        if (_inTrigger)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
            {

                StartCoroutine(OnPressed_Button0());

            }

            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            {

                StartCoroutine(OnPressed_Button1());
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            {

                StartCoroutine(OnPressed_Button2());
            }

            if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
            {

                StartCoroutine(OnPressed_Button3());
            }

            if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
            {

                StartCoroutine(OnPressed_Button4());
            }

            if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
            {

                StartCoroutine(OnPressed_Button5());
            }

            if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
            {

                StartCoroutine(OnPressed_Button6());
            }

            if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
            {

                StartCoroutine(OnPressed_Button7());
            }

            if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
            {

                StartCoroutine(OnPressed_Button8());
            }

            if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
            {

                StartCoroutine(OnPressed_Button9());
            }
            if (Input.GetKeyDown(KeyCode.Hash))//hash key not working. Maybe a bug in unity 5.4.0
            {

                StartCoroutine(OnPressed_ButtonRaute());
            }
            if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.KeypadMultiply))
            {

                StartCoroutine(OnPressed_ButtonStern());
            }
            if (Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Backspace))
            {
                StartCoroutine(OnPressed_ButtonDelete());

            }

        }

    }
}

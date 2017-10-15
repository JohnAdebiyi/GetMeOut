using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Keypad_LivingRoom_SAFE_Script : MonoBehaviour
{

    public Animator _animator;

    public Collider keycard;

    public GameObject openPanel_Keypad;
    public GameObject destroyPasswordMinimalized;//using tag to destroy

    public Camera fpsCam;

    public string currentPassword_LivingRoom;

    public bool inTrigger;
    public bool doorOpended;
    public bool keyPadScreen;

    //keypad input
    string number = null;
    public InputField myNumber;

    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
    }


    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 2))
        {
            if (hit.collider.gameObject.tag == "Safe_Livingroom")
            {
                inTrigger = true;
                openPanel_Keypad.SetActive(true);// panel can now being seen                
            }
            
        }
        else
        {
            inTrigger = false;
            keyPadScreen = false;
            openPanel_Keypad.SetActive(false);// panel can now being seen          
        }



        destroyPasswordMinimalized = GameObject.FindGameObjectWithTag("Panel_minimized");

        if (!EventSystem.current.IsPointerOverGameObject()) //stop raycast on UI clicks. when UI is activ, gameObjects arent hit with raycast.
        {
            //if inside the trigger 
            if (inTrigger)
            {



                //keyboard
                if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
                {
                    keypadInput("0");
                    FindObjectOfType<SFX_Manager>().Play("clickButtonKeypad");
                }

                if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
                {
                    keypadInput("1");
                    FindObjectOfType<SFX_Manager>().Play("clickButtonKeypad");
                }

                if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
                {
                    keypadInput("2");
                    FindObjectOfType<SFX_Manager>().Play("clickButtonKeypad");
                }

                if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
                {
                    keypadInput("3");
                    FindObjectOfType<SFX_Manager>().Play("clickButtonKeypad");
                }

                if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
                {
                    keypadInput("4");
                    FindObjectOfType<SFX_Manager>().Play("clickButtonKeypad");
                }

                if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
                {
                    keypadInput("5");
                    FindObjectOfType<SFX_Manager>().Play("clickButtonKeypad");
                }

                if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
                {
                    keypadInput("6");
                    FindObjectOfType<SFX_Manager>().Play("clickButtonKeypad");
                }

                if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
                {
                    keypadInput("7");
                    FindObjectOfType<SFX_Manager>().Play("clickButtonKeypad");
                }

                if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
                {
                    keypadInput("8");
                    FindObjectOfType<SFX_Manager>().Play("clickButtonKeypad");
                }

                if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
                {
                    keypadInput("9");
                    FindObjectOfType<SFX_Manager>().Play("clickButtonKeypad");
                }
                if (Input.GetKeyDown(KeyCode.Hash))//hash key not working. Maybe a bug in unity 5.4.0
                {
                    keypadInput("#");
                    FindObjectOfType<SFX_Manager>().Play("clickButtonKeypad");
                }
                if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.KeypadMultiply))
                {
                    keypadInput("*");
                    FindObjectOfType<SFX_Manager>().Play("clickButtonKeypad");
                }
                if (Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Backspace))
                {

                    myNumber.text = "";
                    FindObjectOfType<SFX_Manager>().Play("clickButtonKeypad");

                }

            }
        }
      
  
        if (doorOpended)
        {
            _animator.SetBool("Safe_DoorOpen_Livingroom", true);
            openPanel_Keypad.SetActive(false);
            inTrigger = false;
            keyPadScreen = false;
            Destroy(destroyPasswordMinimalized);
            keycard.enabled = true;            
        }

    }

    //Number is passed from mouse on Click() function when clicked with mouse 
    public void keypadInput(string key)
    {
        
        number = key;
        myNumber.text += number;
        
        if(key == "d")
        {
            myNumber.text = "";
        }
        if (myNumber.text == currentPassword_LivingRoom)
        {
            FindObjectOfType<SFX_Manager>().Play("openSafe");
            doorOpended = true;
        }
    }
}


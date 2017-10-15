using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Keypad_BedRoom_SAFE_Script : MonoBehaviour
{
    public Animator _animator;

    public Camera fpsCam;

    public Collider keycard_Terminal1;// activate the keycard collider when door is opened

    public GameObject openPanel_Keypad;
    public GameObject destroyPasswordMinimalized;//using tag to destroy

    public string currentPassword_BedRoom;

    public bool bedroom_inTrigger;
    public bool bedroom_doorOpended;
    public bool bedroom_keyPadScreen;
    public static bool enemiesAreDead;



    //keypad input
    string bedroom_number = null;
    public InputField bedroom_myNumber = null;


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
            if (hit.collider.gameObject.tag == "safe_Bedroom")
            {
                bedroom_inTrigger = true;
                openPanel_Keypad.SetActive(true);
            }
            else
                {
                    bedroom_inTrigger = false;
                    bedroom_keyPadScreen = false;
                    openPanel_Keypad.SetActive(false);
                }
            if (hit.collider.gameObject.tag == "chestdrawer_Bedroom")// in case raycast hits the chestdrawer Safe then deactivate the bedroom safe panel
            {
                openPanel_Keypad.SetActive(false);
            }

        }
        else
        {
            bedroom_inTrigger = false;
            bedroom_keyPadScreen = false;
            openPanel_Keypad.SetActive(false);
        }


        if (!EventSystem.current.IsPointerOverGameObject()) //stop raycast on UI clicks. when UI is activ, gameObjects arent hit with raycast.
        {
            //if inside the trigger 
            if (bedroom_inTrigger)
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

                    bedroom_myNumber.text = "";
                    FindObjectOfType<SFX_Manager>().Play("clickButtonKeypad");

                }

            }


        }



        if (bedroom_doorOpended)
        {           
            _animator.SetBool("Safe_DoorOpen_Bedroom", true);
            openPanel_Keypad.SetActive(false);
            bedroom_inTrigger = false;
            bedroom_keyPadScreen = false;
            Destroy(destroyPasswordMinimalized);
            keycard_Terminal1.enabled = true;//collider is enabled       
        }
    }



    //Number is passed from on Click() function when clicked with mouse 
    public void keypadInput(string bedroom_key)
    {

        bedroom_number = bedroom_key;
        bedroom_myNumber.text += bedroom_number;
        
        if(bedroom_key == "d")
        {
            bedroom_myNumber.text = "";
        }

        if (bedroom_myNumber.text == currentPassword_BedRoom)
        {
            if (enemiesAreDead)
            { 
            FindObjectOfType<SFX_Manager>().Play("openSafe");
            bedroom_doorOpended = true;
            }
        }

    }
}


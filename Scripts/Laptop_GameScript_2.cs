using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Laptop_GameScript_2 : MonoBehaviour
{

    private Animator _animator;
    private PlayerStatusScript playerStatus; //playerStatus.GetDamage(damage);
    private HealthBar_BlurScript healthbar_blur;//off on healthbar blur when damage is taken
    [Space]
    [Space]
    public GameObject laptop_OpenPanel2;//open panel when player is near laptop
    public GameObject reward_OpenPanel2;// open panel if anwser is correct
    [Space]
    [Space]
    public GameObject keycardIsActive_minimized003;// to activate panel keycardIsActive_minimized when answer is correct
    public GameObject shownMinimizedKeycard003; //for deactivating panel_keycard04_minimized when answer is correct
    [Space]
    [Space]
    public GameObject wrongAnswer;//if anwser is wrong display "wrong answer"
    public GameObject enterA_Number;//if input field is empty and player presses enter display "type in a number"
    [Space]
    [Space]
    public Camera fpsCam;// fpscam for raycast
    public ParticleSystem computerLocationEff;
    [Space]
    [Space]
    private bool laptop_isInsideTrigger = false;
    public string currentAnswer = "12";
    private bool correct = false;
    public static bool keyCard_To_Laptop;
    public static bool putOffPanel_Game2;// in case this gamescript wasnt played then dont show the panel 
    [Space]
    [Space]
    private float damage = 0.03f;// 0.03 * 100 = 3 damage units

    //keypad input
    string laptop_number = null;
    public InputField laptop_myNumber = null;

    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
        playerStatus = GetComponent<PlayerStatusScript>();
        healthbar_blur = GetComponent<HealthBar_BlurScript>();
    }


    // if raycast hits the laptop collider, set _isInsideTrigger to true else set to false
    void _RaycastHit()
    {

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 2))
        {
            if (hit.collider.gameObject.tag == "Game_Laptop")
            {
                laptop_isInsideTrigger = true;
                if (keyCard_To_Laptop == true)
                {
                    laptop_OpenPanel2.SetActive(true);
                    _animator.SetBool("Laptop_on", true);
                }
                else if (putOffPanel_Game2 == true)
                {
                    laptop_OpenPanel2.SetActive(false);
                }
            }
            if (hit.collider.gameObject.tag == "Book_binary")//in case raycast hits the open book then deactivate the laptop panel
            {
                laptop_OpenPanel2.SetActive(false);
            }


        }
        else
        {
            laptop_isInsideTrigger = false;
            laptop_OpenPanel2.SetActive(false);

            laptop_myNumber.text = "";//reset input field when raycast looks away from collider
            wrongAnswer.SetActive(false);// deactivate wrong answer when raycast looks away from collider
            _animator.SetBool("Laptop_on", false);
            enterA_Number.SetActive(false);
        }


    }


    // if player is insideTrigger show panel keyboard
    void InsideTrigger()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) //stop raycast on UI clicks. when UI is activ, gameObjects arent hit with raycast.
        {
            //if(true)
            if (keyCard_To_Laptop == true)
            {

                if (laptop_isInsideTrigger)
                {

                    if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
                    {
                        keypadInput("0");
                        wrongAnswer.SetActive(false);
                        enterA_Number.SetActive(false);
                        FindObjectOfType<SFX_Manager>().Play("clickButtonLaptop");
                    }

                    if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
                    {

                        keypadInput("1");
                        wrongAnswer.SetActive(false);
                        enterA_Number.SetActive(false);
                        FindObjectOfType<SFX_Manager>().Play("clickButtonLaptop");
                    }

                    if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
                    {
                        keypadInput("2");
                        wrongAnswer.SetActive(false);
                        enterA_Number.SetActive(false);
                        FindObjectOfType<SFX_Manager>().Play("clickButtonLaptop");
                    }

                    if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
                    {
                        keypadInput("3");
                        wrongAnswer.SetActive(false);
                        enterA_Number.SetActive(false);
                        FindObjectOfType<SFX_Manager>().Play("clickButtonLaptop");
                    }

                    if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
                    {
                        keypadInput("4");
                        wrongAnswer.SetActive(false);
                        enterA_Number.SetActive(false);
                        FindObjectOfType<SFX_Manager>().Play("clickButtonLaptop");
                    }

                    if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
                    {
                        keypadInput("5");
                        wrongAnswer.SetActive(false);
                        enterA_Number.SetActive(false);
                        FindObjectOfType<SFX_Manager>().Play("clickButtonLaptop");
                    }

                    if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
                    {
                        keypadInput("6");
                        wrongAnswer.SetActive(false);
                        enterA_Number.SetActive(false);
                        FindObjectOfType<SFX_Manager>().Play("clickButtonLaptop");
                    }

                    if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
                    {
                        keypadInput("7");
                        wrongAnswer.SetActive(false);
                        enterA_Number.SetActive(false);
                        FindObjectOfType<SFX_Manager>().Play("clickButtonLaptop");
                    }

                    if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
                    {
                        keypadInput("8");
                        wrongAnswer.SetActive(false);
                        enterA_Number.SetActive(false);
                        FindObjectOfType<SFX_Manager>().Play("clickButtonLaptop");
                    }

                    if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
                    {
                        keypadInput("9");
                        wrongAnswer.SetActive(false);
                        FindObjectOfType<SFX_Manager>().Play("clickButtonLaptop");

                    }

                    if (Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Backspace))
                    {
                        laptop_myNumber.text = "";
                        wrongAnswer.SetActive(false);
                        FindObjectOfType<SFX_Manager>().Play("clickButtonLaptop");
                    }
                    if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
                    {
                        if (laptop_myNumber.text == currentAnswer)
                        {
                            laptop_OpenPanel2.SetActive(false);
                            laptop_OpenPanel2 = null;
                            correct = true;
                            FindObjectOfType<SFX_Manager>().Play("correctAnswer");
                            computerLocationEff.Stop();
                        }
                        else if (laptop_myNumber.text == "")
                        {
                            enterA_Number.SetActive(true);
                            FindObjectOfType<SFX_Manager>().Play("error");
                        }

                        else
                        {
                            //laptop_myNumber.text = "wrong answer";
                            wrongAnswer.SetActive(true);
                            playerStatus.GetDamage(damage);
                            healthbar_blur.ShowBlur();
                            FindObjectOfType<SFX_Manager>().Play("error");
                        }
                    }
                }


                if (correct && laptop_isInsideTrigger)
                {
                    StartCoroutine(ShowSuccess());
                    DoorKitchenScript.keycardIsActiv = true; // key card is now activ for the door to open
                    DoorKitchenScript.keyCard_To_Laptop = false;// dont show key needs to be activated since the answer is correct   
                    _animator.SetBool("Laptop_on", false);
                    shownMinimizedKeycard003.SetActive(false);//for deactivating panel_keycard04_minimized when answer is correct
                    keycardIsActive_minimized003.SetActive(true); //to activate panel keycardIsActive_minimized when answer is correct
                }
                if (correct && laptop_isInsideTrigger == false)
                {
                    shownMinimizedKeycard003.SetActive(false);//for deactivating minimized panel keycard03(key card obtained) when answer is correct
                    keycardIsActive_minimized003.SetActive(true); //to activate minimized panel "key card is activ" when answer is correct
                }

            }
        }
    }



    //show the the panel "key card is activ" for some seconds and then deactivate
    IEnumerator ShowSuccess()
    {
        reward_OpenPanel2.SetActive(true);
        yield return new WaitForSeconds(2f);
        reward_OpenPanel2.SetActive(false);
        // shownMinimizedKeycard003.SetActive(false);//for deactivating minimized panel keycard03(key card obtained) when answer is correct
        //keycardIsActive_minimized003.SetActive(true); //to activate minimized panel "key card is activ" when answer is correct
    }




    // Update is called once per frame
    void Update()
    {
        _RaycastHit();
        InsideTrigger();
    }



    //Numbers are passed from mouse or keyboard when clicked
    public void keypadInput(string laptop__key)
    {

        laptop_number = laptop__key;
        laptop_myNumber.text += laptop_number;
        wrongAnswer.SetActive(false);
        enterA_Number.SetActive(false);

        //delete button
        if (laptop__key == "d")
        {
            laptop_myNumber.text = "";
            wrongAnswer.SetActive(false);

        }
    }

    //enter button
    //when enter is pressed using the mouse, e is passed to keypadInput(string laptop_key) 
    public void keypadInputEnter(string laptop__key)
    {


        if (laptop__key == "e")
        {
            if (laptop_myNumber.text == currentAnswer)
            {
                laptop_OpenPanel2.SetActive(false);
                laptop_OpenPanel2 = null;
                correct = true;
                computerLocationEff.Stop();
            }

            else
            {
                //laptop_myNumber.text = "wrong answer";
                wrongAnswer.SetActive(true);
                playerStatus.GetDamage(damage);
                healthbar_blur.ShowBlur();
            }

        }

        if (correct && laptop_isInsideTrigger)
        {
            reward_OpenPanel2.SetActive(true);
            DoorKitchenScript.keycardIsActiv = true;// key card is now activ for the door to open
            DoorKitchenScript.keyCard_To_Laptop = false;// dont show key needs to be activated since the answer is correct
            _animator.SetBool("Laptop_on", false);
            shownMinimizedKeycard003.SetActive(false);//for deactivating panel_keycard04_minimized when answer is correct
            keycardIsActive_minimized003.SetActive(true); //to activate panel keycardIsActive_minimized when answer is correct
        }
        if (correct && laptop_isInsideTrigger == false)
        {
            reward_OpenPanel2.SetActive(false);
            shownMinimizedKeycard003.SetActive(false);//for deactivating panel_keycard04_minimized when answer is correct
            keycardIsActive_minimized003.SetActive(true); //to activate panel keycardIsActive_minimized when answer is correct
        }

    }
}


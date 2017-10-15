using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Laptop_GameScript : MonoBehaviour {

    // q: what is binary 010

    private Animator _animator;
    private PlayerStatusScript playerStatus; //playerStatus.GetDamage(damage);
    private HealthBar_BlurScript healthbar_blur;//off on healthbar blur when damage is taken

    public GameObject laptop_OpenPanel; //open panel when player is near laptop
    public GameObject reward_OpenPanel; // open panel if anwser is correct
    public GameObject passwordShownMinimalized; //show password if answer is correct

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
    private bool correct;
    private bool laptop_isInsideTrigger;
    public string currentAnswer = "6";
    public static bool putOffPanel_Game1;// in case this gamescript wasnt played then dont show the panel 
    [Space]
    [Space]
    private float damage = 0.01f;// 0.01 * 100 = 1 damage unit

    [Space]
    [Space]
    //keypad input
    string laptop_number;
    public InputField laptop_myNumber;

    private bool update = true;// for getting rid of errors -> NullReferenceException: Object reference not set to an instance of an object

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
                if (putOffPanel_Game1)// in case this gamescript wasnt played then dont show the panel
                {
                    laptop_OpenPanel.SetActive(false);
                }
                else
                {
                    laptop_isInsideTrigger = true;
                    laptop_OpenPanel.SetActive(true);
                    _animator.SetBool("Laptop_on", true);
                }
            }
            if (hit.collider.gameObject.tag == "Book_binary")//in case raycast hits the open book then deactivate the laptop panel
            {
                laptop_OpenPanel.SetActive(false);
            }


        }
        else
        {   
            
            laptop_isInsideTrigger = false;
            laptop_OpenPanel.SetActive(false);
            _animator.SetBool("Laptop_on", false);
            laptop_myNumber.text = "";//reset input field when raycast looks away from collider
            wrongAnswer.SetActive(false);// deactivate wrong answer when raycast looks away from collider
            enterA_Number.SetActive(false);// deactivate type in a digit when raycast looks away from collider

        }
    }


    // if player is insideTrigger show panel keyboard
    void InsideTrigger()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) //stop raycast on UI clicks. when UI is activ, gameObjects arent hit with raycast.
        {
            if (laptop_isInsideTrigger)
            {

                if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
                {
                    keypadInput("0");
                    wrongAnswer.SetActive(false);// in case wrong answer is displayed. on click dont show wrong answer
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
                    enterA_Number.SetActive(false);
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
                    FindObjectOfType<SFX_Manager>().Play("clickButtonLaptop");

                    if (laptop_myNumber.text == currentAnswer)
                    {
                        laptop_OpenPanel.SetActive(false);
                        laptop_OpenPanel = null;
                        correct = true;
                        _animator.SetBool("Laptop_on", false);
                        FindObjectOfType<SFX_Manager>().Play("correctAnswer");

                        computerLocationEff.Stop();

                        update = false;
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
            }
        }
    }

    //show the the password panel for some seconds and then deactivate and set the mini password panel to true
    IEnumerator ShowSuccess()
    {
        reward_OpenPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        reward_OpenPanel.SetActive(false);
        passwordShownMinimalized.SetActive(true);
    }



    // Update is called once per frame
    void Update()
    {
        if (update)
        {
            _RaycastHit();
            InsideTrigger();
        }
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

                laptop_OpenPanel.SetActive(false);
                laptop_OpenPanel = null;
                correct = true;
                _animator.SetBool("Laptop_on", false);

                computerLocationEff.Stop();

                update = false;
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
            reward_OpenPanel.SetActive(true);
            
        }
        if (correct && laptop_isInsideTrigger == false)
        {
            reward_OpenPanel.SetActive(false);
            passwordShownMinimalized.SetActive(true); 
        }

    }
}

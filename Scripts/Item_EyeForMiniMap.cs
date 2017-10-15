using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Item_EyeForMiniMap : MonoBehaviour {

    private Animator _animator;

    public Button closeObjectiv2;
    public Button nextObjectiv2;

    public Camera fpsCam;

    public GameObject openPanel;
    public GameObject miniMapCam;
    public GameObject enemyCounter_Panel;
    public GameObject pointerToEnemy_Yellow;//activate only the yellow  pointer
    public GameObject pointerToEnemy_Yellow1;//activate only the yellow  pointer
    public GameObject pointerToEnemy_Yellow2;//activate only the yellow  pointer
    public GameObject pointerToEnemy_Yellow3;//activate only the yellow  pointer
    public GameObject pointerToEnemy_Yellow4;//activate only the yellow  pointer

    public Image pauseBackground;
    public Image objectivBackground;
    public Image objectiv_pointers;
    public Image tutorial2;

    public Transform Player;
 
    public string openText = "Take Item";
    public string closeText = "";

    public bool inTrigger;
    private bool _isOpen;

    // Use this for initialization
    void Start ()
    {
        _animator = GetComponent<Animator>();
    }


    // for updating the item panel text
    private void UpdatePanelText()
    {
        Text panelText = openPanel.transform.FindChild("Text").GetComponent<Text>();
        if (panelText != null)
        {
            panelText.text = _isOpen ? closeText : openText;//if _isOpen is true return closeText or else return openText
        }
    }

    // if raycast hits the item collider, set _isInsideTrigger to true else set to false
    void _RaycastHit()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 2))
        {
            if (hit.collider.gameObject.tag == "item_Eye")
            {
                inTrigger = true;
                UpdatePanelText();
                openPanel.SetActive(true);
            }
        }
        else
        {
            openPanel.SetActive(false);
            inTrigger = false;
        }
    }



    // if _isInsideTrigger is true and mouse is pressed destroy item and display "item obtained" 
    void InsideTrigger()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) //stop raycast on UI clicks. when UI is activ, gameObjects arent hit with raycast.
        {
            if (inTrigger)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    FindObjectOfType<SFX_Manager>().Play("gotSpecialItem");
                    miniMapCam.SetActive(true);
                    Destroy(GameObject.FindWithTag("item_Eye"));// destroy cross health item
                    openPanel.SetActive(false);
                    _animator.SetBool("floating_Rocks", true);
                    enemyCounter_Panel.SetActive(true);


                    pointerToEnemy_Yellow.SetActive(true);
                    pointerToEnemy_Yellow1.SetActive(true);
                    pointerToEnemy_Yellow2.SetActive(true);
                    pointerToEnemy_Yellow3.SetActive(true);
                    pointerToEnemy_Yellow4.SetActive(true);

                    objectivBackground.gameObject.SetActive(true);
                    objectiv_pointers.gameObject.SetActive(true);

                    closeObjectiv2.gameObject.SetActive(true);
                    nextObjectiv2.gameObject.SetActive(true);

                    tutorial2.gameObject.SetActive(true);// show tutorial 2 in the pause options

                    Time.timeScale = 0; //stop every movement around the enviroment
                    Player.GetComponent<FirstPersonController>().enabled = false; //stop the player from moving
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true; // show cursor
                    IsPause.escape_buttonsEnabled = false; // disable the pause button
                    Objectiv2.objectiv2_Started = true;
                }
            }
        }
    }


    // Update is called once per frame
    void Update ()
    {
        _RaycastHit();
        InsideTrigger();
    }
}

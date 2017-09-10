using UnityEngine;
using UnityEngine.EventSystems;
public class KeyCardToilet_To_Kitchen : MonoBehaviour {


    
    public GameObject openPanel = null;
    public GameObject shownMinimizedKeycard003;
    public Camera fpsCam;

    public string openText = "Take key card";
    public string closeText = "";

    private bool _isOpen = false;
    public bool inTrigger;



    // for checking if the key card panel is activ
    private bool IsOpenPanelActive
    {
        get
        {
            return openPanel.activeInHierarchy;
        }
    }


    // for updating the key card panel text
    private void UpdatePanelText()
    {
        UnityEngine.UI.Text panelText = openPanel.transform.FindChild("Text").GetComponent<UnityEngine.UI.Text>();
        if (panelText != null)
        {
            panelText.text = _isOpen ? closeText : openText;//if _isOpen is true return closeText or else return openText
        }
    }


    // if raycast hits the key card collider, set _isInsideTrigger to true else set to false
    void _RaycastHit()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 3))
        {
            if (hit.collider.gameObject.tag == "keycard_Toilet")
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


    //if _isInsideTrigger is true and mouse is pressed destroy key card and display "key card obtained" 
    void InsideTrigger()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) //stop raycast on UI clicks. when UI is activ, gameObjects arent hit with raycast.
        {
            if (inTrigger)
            {
                if (Input.GetMouseButtonDown(1))
                {

                    DoorKitchenScript.keyCard_To_Laptop = true;// set to true so the kitchen door shows "go do activation to open" 
                    Laptop_GameScript_2.keyCard_To_Laptop = true;// set to true so player can play the second game

                    Laptop_GameScript.putOffPanel_Game1 = true;// in case first game wasnt played then dont show the panel
                                                               //Laptop_GameScript_2.putLaptopOn = true;
                    Destroy(this.gameObject);
                    openPanel.SetActive(false);// panel is invincible
                    shownMinimizedKeycard003.SetActive(true);
                    Timer.countKeycards -= 1;

                    FindObjectOfType<SFX_Manager>().Play("gotItem");
                }
            }
        }
    }
    void Update()
    {
        _RaycastHit();
        InsideTrigger();

    }
}

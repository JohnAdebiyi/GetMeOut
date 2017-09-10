using UnityEngine;
using UnityEngine.EventSystems;
public class KeyCardKitchen_To_Weapon : MonoBehaviour {

    
    public GameObject openPanel = null;
    public GameObject shownMinimizedKeycard004;
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
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 2))
        {
            if (hit.collider.gameObject.tag == "keycard_Kitchen")
            {
                inTrigger = true;
                UpdatePanelText();
                openPanel.SetActive(true);// panel can now being seen
            }


        }
        else
        {
            openPanel.SetActive(false);// panel is invincible
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
                    //DoorBedroomScript.keyCardBedroom = true;
                    Laptop_GameScript_3.keyCard_To_Laptop = true;// set to true so player can play the third game
                    WeaponSafe_CorridorScript.keyCard_To_Laptop = true; // set to true so the weapon safe shows "go do activation to open" 

                    Laptop_GameScript_2.putOffPanel_Game2 = true;// in case second game wasnt played then dont show the panel
                    Laptop_GameScript.putOffPanel_Game1 = true;// in case first game wasnt played then dont show the panel
                    Destroy(this.gameObject);
                    openPanel.SetActive(false);// panel is invincible
                    shownMinimizedKeycard004.SetActive(true);

                    Timer.countKeycards -= 1;
                    FindObjectOfType<SFX_Manager>().Play("gotItem");
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

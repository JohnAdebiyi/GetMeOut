using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
public class Take_OutsideItemsScript : MonoBehaviour
{

    public GameObject openPanel = null;
    public GameObject showpassword_Panel;//small
    public GameObject password_BigPanel;//big
    public Camera fpsCam;
    private PlayerStatusScript giveHealth;


    public string openText = "Take item";
    public string closeText = "";
    private bool _isOpen = false;

    private bool inTrigger;
    private bool inTrigger1;
    private bool inTrigger2;
    private bool inTrigger3;
    private bool inTrigger4;

    private float health = 0.10f;


    void Start()
    {
        giveHealth = GetComponent<PlayerStatusScript>();
    }

    // for checking if the health cross panel is activ
    private bool IsOpenPanelActive
    {
        get
        {
            return openPanel.activeInHierarchy;
        }
    }

    // for updating the health cross panel text
    private void UpdatePanelText()
    {
        UnityEngine.UI.Text panelText = openPanel.transform.FindChild("Text").GetComponent<UnityEngine.UI.Text>();
        if (panelText != null)
        {
            panelText.text = _isOpen ? closeText : openText;//if _isOpen is true return CloseText or else return openText
        }
    }


    // if raycast hits the health cross collider, set _isInsideTrigger to true else set to false
    void _RaycastHit()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 2))
        {
            //health cross 0
            if (hit.collider.gameObject.tag == "take_Health")
            {
                inTrigger = true;
                UpdatePanelText();
                openPanel.SetActive(true);
            }
            //health cross 1
            if (hit.collider.gameObject.tag == "take_Health(1)")
            {
                inTrigger1 = true;
                UpdatePanelText();
                openPanel.SetActive(true);
            }
            //health cross 2
            if (hit.collider.gameObject.tag == "take_Health(2)")
            {
                inTrigger2 = true;
                UpdatePanelText();
                openPanel.SetActive(true);
            }
            //health cross 3
            if (hit.collider.gameObject.tag == "take_Health(3)")
            {
                inTrigger3 = true;
                UpdatePanelText();
                openPanel.SetActive(true);
            }
            //health cross 4
            if (hit.collider.gameObject.tag == "take_PaperOutside")
            {
                inTrigger4 = true;
                UpdatePanelText();
                openPanel.SetActive(true);
            }


        }
        else
        {
            openPanel.SetActive(false);
            inTrigger = false;
            inTrigger1 = false;
            inTrigger2 = false;
            inTrigger3 = false;
            inTrigger4 = false;
        }
    }



    // if _isInsideTrigger is true and mouse is pressed destroy object and increase health 
    void InsideTrigger()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) //stop raycast on UI clicks. when UI is activ, gameObjects arent hit with raycast.
        {
            //health cross 0
            if (inTrigger)
            {

                if (Input.GetMouseButtonDown(1))
                {
                    giveHealth.IncreaseHealth(health);// increase health
                    Destroy(GameObject.FindWithTag("take_Health"));// destroy cross health item
                    openPanel.SetActive(false);// panel is invincible
                    inTrigger = false;
                    FindObjectOfType<SFX_Manager>().Play("gotItem");
                }
            }
            //health cross 1
            if (inTrigger1)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    giveHealth.IncreaseHealth(health);// increase health
                    Destroy(GameObject.FindWithTag("take_Health(1)"));// destroy cross health item
                    openPanel.SetActive(false);// panel is invincible
                    inTrigger1 = false;
                    FindObjectOfType<SFX_Manager>().Play("gotItem");
                }
            }
            //health cross 2
            if (inTrigger2)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    giveHealth.IncreaseHealth(health);// increase health
                    Destroy(GameObject.FindWithTag("take_Health(2)"));// destroy cross health item
                    openPanel.SetActive(false);// panel is invincible
                    inTrigger2 = false;
                    FindObjectOfType<SFX_Manager>().Play("gotItem");
                }
            }
            //health cross 3
            if (inTrigger3)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    giveHealth.IncreaseHealth(health);// increase health
                    Destroy(GameObject.FindWithTag("take_Health(3)"));// destroy cross health item
                    openPanel.SetActive(false);// panel is invincible
                    inTrigger3 = false;
                    FindObjectOfType<SFX_Manager>().Play("gotItem");
                }
            }
            //paper
            if (inTrigger4)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    StartCoroutine(ShowPassword());
                    Destroy(GameObject.FindWithTag("take_PaperOutside"));// destroy paper item
                    openPanel.SetActive(false);// panel is invincible
                    inTrigger4 = false;
                    FindObjectOfType<SFX_Manager>().Play("gotItem");
                }
            }
        }
    }

    //show the the password panel for some seconds and then deactivate and set the mini password panel to true
    IEnumerator ShowPassword()
    {
        password_BigPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        password_BigPanel.SetActive(false);
        showpassword_Panel.SetActive(true);
    }

    void Update()
    {
        _RaycastHit();
        InsideTrigger();
    }
}

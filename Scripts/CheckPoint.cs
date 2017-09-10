using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour
{

    public static Vector3 reachedPoint; // to teleport the player to the checkpoint collider
    public GameObject reachedCheckPoint_Panel;
    public GameObject reachedCheckPoint_BigPanel;
    public Collider reachedCheckPointCollider;
    public GameObject buttonCheckPoint;
    public Animator _anim;
    public static float currentHealth;

    void OnEnable()
    {
        _anim = GetComponent<Animator>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            reachedPoint = transform.position;

            StartCoroutine(PutOnOff_ReachedCheckpointPanels());

            reachedCheckPointCollider.enabled = false;
            currentHealth = PlayerStatusScript.respawnHealth;
            buttonCheckPoint.SetActive(true);// show restart checkpoint button only when player enters the trigger
        }
    }


    IEnumerator PutOnOff_ReachedCheckpointPanels()
    {
        reachedCheckPoint_BigPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        reachedCheckPoint_BigPanel.SetActive(false);

        reachedCheckPoint_Panel.SetActive(true);
        yield return new WaitForSeconds(10f);
        reachedCheckPoint_Panel.SetActive(false);
    }
}

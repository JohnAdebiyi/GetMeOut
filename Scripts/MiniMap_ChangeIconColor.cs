using UnityEngine;


public class MiniMap_ChangeIconColor : MonoBehaviour {

    public bool changeMiniMapColor = false;
    public Renderer rend;
    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
	
         if (changeMiniMapColor == true)
        {
            rend.material.color = Color.red;
        }
        else
        {
            rend.material.color = Color.yellow;
        }
	}
}

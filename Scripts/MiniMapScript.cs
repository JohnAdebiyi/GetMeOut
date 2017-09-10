using UnityEngine;


public class MiniMapScript : MonoBehaviour {

    public Transform player;
	
	// Update is called once per frame
	void LateUpdate ()
    {
        transform.position = new Vector3(player.position.x, player.position.y + 20, player.position.z);
	}
}

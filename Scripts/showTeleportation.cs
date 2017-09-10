using UnityEngine;


public class showTeleportation : MonoBehaviour {

    public ParticleSystem hexagon;
    public ParticleSystem pole;
    public ParticleSystem stars;


    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("inside");
        hexagon.Play();
        stars.Play();
        pole.Play();
    }

    void OnTriggerExit(Collider other)
    {
       // Debug.Log("outside");
        hexagon.Stop();
        stars.Stop();
        pole.Stop();
    }
}

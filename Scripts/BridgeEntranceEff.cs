using UnityEngine;


//script is on Bridge --> bridgeEntranceEffect
public class BridgeEntranceEff : MonoBehaviour {

    public ParticleSystem bridgeEntranceEff;

    void OnTriggerEnter(Collider other)
    {
        bridgeEntranceEff.Stop();
    }
}

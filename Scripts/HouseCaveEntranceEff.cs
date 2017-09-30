using UnityEngine;

//script is on Apartment_ect -> Apartment_Rocks_Outside -> houseCaveEntranceEffectCollider

public class HouseCaveEntranceEff : MonoBehaviour {

    public ParticleSystem houseCaveEntranceEff;

    void OnTriggerEnter(Collider other)
    {
    houseCaveEntranceEff.Stop();        
    }
}

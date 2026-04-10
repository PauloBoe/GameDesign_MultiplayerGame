using UnityEngine;


// AUTEUR: Stijn Grievink
// Small script to turn off the rigidbody and collider of an enemy 5 seconds after it dies, for performance reasons.
public class DeadObject : MonoBehaviour
{
    // Invoke the freeze function to run after 5 seconds
    void Start()
    {
        Invoke("FreezeObject", 5f);
    }

    // Turn the rigidbody to kinematic and turn off the collider to freeze the object
    void FreezeObject()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<CapsuleCollider>().enabled = false;
    }
}

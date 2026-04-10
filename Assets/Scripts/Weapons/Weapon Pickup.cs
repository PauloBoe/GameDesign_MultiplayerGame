using Unity.VisualScripting;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField]
    private Weapon _weapon;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer != 3)
            return;
        //if (keypress interract)

        //needs right location to instantiate to.
        Instantiate(_weapon, collision.gameObject.transform, false);
        Destroy(gameObject);
    }
}

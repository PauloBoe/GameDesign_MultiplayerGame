//Auteur: Dion Vos
//User Story: Als speler wil ik een basiswapen hebben met modale statistieken en demping zodat ik geluidloos vijanden kan neerhalen zonder anderen te alarmeren.
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private int _bulletSpeed;
    [SerializeField]
    private float _bulletLifeTime;

    public int bulletDamage = 0;

    private void Awake()
    {
        StartCoroutine(Despawn());
    }
    void FixedUpdate()
    {
        //propells the bullet forward
        transform.Translate(new Vector3(0, 0, _bulletSpeed) * Time.deltaTime);
    }

    void OnTriggerEnter(Collider collider)
    {
        Health targetHealth = collider.gameObject.GetComponent<Health>();
        if (targetHealth)
        {
            targetHealth.Damage(bulletDamage);
        }
        Destroy(gameObject);
    }

    //Despawning after a certain amount of time
    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(_bulletLifeTime);
        Destroy(gameObject);
    }
}

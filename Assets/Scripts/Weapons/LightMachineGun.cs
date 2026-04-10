//Auteur: Dion Vos
//User Story: Als speler wil ik een zwaar wapen hebben met veel kracht en gelimiteerd gebruik zodat ik sterke vijanden kan neerhalen.
using UnityEngine;

public class LightMachineGun : Weapon
{
    public override void Fire(Vector3 pHitPoint)
    {
        if (_canFire && HasBullets() && !_isReloading)
        {
            StartCoroutine(FireRate());
            //fixed by: Stijn, Dion en Paulo
            //sets direction bullet looks towards by subtracting where the player looks by where it currently is
            Vector3 direction = pHitPoint - _bulletSpawnpoint.transform.position;
            GameObject bullet = Instantiate(_bulletGameobject, _bulletSpawnpoint.transform.position, Quaternion.LookRotation(direction, Vector3.up), null);
            bullet.GetComponent<Bullet>().bulletDamage = _damage;

            _currentMag -= _bulletConsumption;
        }
    }
}

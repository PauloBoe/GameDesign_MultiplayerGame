using UnityEngine;

public class PP19Bizon : Weapon
{
    public override void Fire(Vector3 pHitPoint)
    {
        if (_canFire && HasBullets() && !_isReloading)
        {
            StartCoroutine(FireRate());
            // gefixed door: Stijn, Dion en Paulo
            Vector3 direction = pHitPoint - _bulletSpawnpoint.transform.position;
            GameObject bullet = Instantiate(_bulletGameobject, _bulletSpawnpoint.transform.position, Quaternion.LookRotation(direction, Vector3.up), null);
            bullet.GetComponent<Bullet>().bulletDamage = _damage;

            _currentMag -= _bulletConsumption;
        }
    }
}

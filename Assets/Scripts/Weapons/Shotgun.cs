using UnityEngine;

public class Shotgun : Weapon
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Fire(Vector3 pHitPoint)
    {
        if (_canFire && HasBullets() && !_isReloading)
        {
            StartCoroutine(FireRate());

            // gefixed door: Stijn, Dion en Paulo
            Vector3 direction = pHitPoint - _bulletSpawnpoint.transform.position;
            direction.Normalize();
            for (int i = 0; i < _bulletConsumption; i++)
            {
                GameObject bullet = Instantiate(_bulletGameobject, _bulletSpawnpoint.transform.position, Quaternion.LookRotation(new Vector3(direction.x + RandomNumber(), direction.y + RandomNumber(), direction.z), Vector3.up), null);
                bullet.GetComponent<Bullet>().bulletDamage = _damage;
            }

            _currentMag -= _bulletConsumption;
        }
    }

    private float RandomNumber()
    {
        return Random.Range(-_bulletSpread, _bulletSpread);
    }
}

//Auteur: Dion Vos
//User Story: Als speler wil ik een basiswapen hebben met modale statistieken en demping zodat ik geluidloos vijanden kan neerhalen zonder anderen te alarmeren.
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


//parent class of weapon containing information all guns share
public class Weapon : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField]
    protected GameObject _bulletGameobject;
    [SerializeField]
    protected GameObject _bulletSpawnpoint;

    [Header("Weapon Statistics")]
    [SerializeField]
    protected string _name;
    [SerializeField]
    protected int _bulletsPerMinute;
    [SerializeField]
    protected int _damage;
    [SerializeField]
    protected int _sound;
    [SerializeField]
    protected int _magCap;
    [SerializeField]
    protected int _stashCap;
    [SerializeField]
    protected int _bulletConsumption;
    [SerializeField]
    protected float _reloadTime;
    [SerializeField]
    protected float _bulletSpread;

    protected int _currentStash;
    protected int _currentMag;
    protected bool _canFire;
    protected bool _isReloading;
    protected float _reloadAnimStep;

    protected void Awake()
    {
        _reloadAnimStep = 360f / _reloadTime;

        _currentStash = _stashCap;
        _canFire = true;
        _isReloading = false;
        _currentMag = _magCap;
    }

    public bool IsReloading() => _isReloading;
    protected bool HasBullets()
    {
        if (_currentMag - _bulletConsumption > -1)
            return true;
        else 
            return false;
    }

    protected float FireRateCalc()
    {
        return 1f / (_bulletsPerMinute / 60f);
    }

    public int MagCapacity() => _magCap;
    public int CurrentBullets() => _currentMag;

    public virtual void Fire(Vector3 pHitPoint) { }


    //Starts the reloading sequence. Used by weapon controller to reload
    public void Reload() 
    {
        if (!_isReloading && _currentStash > 0 && _currentMag != _magCap)
        {
            StartCoroutine(ReloadAction());
        }
    }

    protected IEnumerator FireRate()
    {
        _canFire = false;
        yield return new WaitForSeconds(FireRateCalc());
        _canFire = true;
    }

    private IEnumerator ReloadAction()
    {
        while (_magCap > _currentMag)
        {
            _isReloading = true;
            _canFire = false;
            _currentMag++;
            _currentStash--;
            if (_currentStash == 0)
                break;
        }
        yield return new WaitForSeconds(_reloadTime);
        _canFire = true;
        _isReloading = false;

        ResetRotation();
    }

    private void ReloadAnim()
    {
        if (_isReloading)
        {
            transform.Rotate(Vector3.forward, _reloadAnimStep * Time.fixedDeltaTime);
        }
    }

    private void FixedUpdate()
    {
        ReloadAnim();
    }


    //Resets gun rotation, used after gun animation is done.
    private void ResetRotation()
    {
        transform.localRotation = Quaternion.Euler(new Vector3(0f, -90f, 0f));
    }
}

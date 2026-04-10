//Auteur: Dion Vos
//User Story: Als speler wil ik een basiswapen hebben met modale statistieken en demping zodat ik geluidloos vijanden kan neerhalen zonder anderen te alarmeren.
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//The component to use multiple weapons for the player
public class WeaponController : MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField]
    private List<GameObject> _weaponObjects = new();
    private List<Weapon> _weaponClasses = new();
    private int _currentWeapon = 0;


    // Paulo
    private Camera _camera;
    [SerializeField] private LayerMask _cameraLayerMask;


    private void Awake()
    {
        foreach (GameObject weapon in _weaponObjects)
            _weaponClasses.Add(weapon.GetComponent<Weapon>());

        // Paulo 
        _camera = Camera.main;
    }

    // Void OnMouseShoot gemaakt door: Paulo, Dion en Stijn
    //shoots raycast from player camera, uses the first intersection as endpoint for the bullet
    public void OnMouseShoot()
    {
        RaycastHit hit;
        Vector3 actualHitPoint;

        if (Physics.Raycast(_camera.gameObject.transform.position, _camera.gameObject.transform.forward, out hit, 100f, _cameraLayerMask))
        {
            actualHitPoint = hit.point;
        }
        else
        {
            actualHitPoint = _camera.transform.position + _camera.transform.forward * 100f;
        }
        _weaponClasses[_currentWeapon].Fire(actualHitPoint);
    }

    public void OnReload()
    {
        _weaponClasses[_currentWeapon].Reload();
    }

    public void OnSwitchGuns()
    {
        if (_weaponClasses[_currentWeapon].IsReloading())
            return;

        _currentWeapon = _currentWeapon == _weaponObjects.Count - 1 ? 0 : _currentWeapon += 1;
        for (int i = 0; i < _weaponObjects.Count; i++)
        {
            if (i == _currentWeapon)
            {
                _weaponObjects[i].SetActive(true);
                continue;
            }
            _weaponObjects[i].SetActive(false);
        }
    }
}

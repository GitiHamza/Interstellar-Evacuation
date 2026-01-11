using System;
using UnityEngine;

using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera _mainCam;
    private Vector3 _mousePos;

    public GameObject _projectile;
    public Transform _projectileTransform;

    public float _timeBetweenFiring = 0.5f;
    public float _projectileSpeed = 10f;

    private bool _canFire = true;
    private float _timer;

    private void Start()
    {
        _mainCam = Camera.main;
    }

    private void Update()
    {
        // Rotation zur Maus
        _mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = _mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        // Schießen
        if (Input.GetMouseButton(0) && _canFire)
        {
            Shoot();
        }

        // Cooldown
        if (!_canFire)
        {
            _timer += Time.deltaTime;
            if (_timer >= _timeBetweenFiring)
            {
                _canFire = true;
                _timer = 0f;
            }
        }
    }

    private void Shoot()
    {
        _canFire = false;
        Instantiate(_projectile, _projectileTransform.position, Quaternion.identity);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Towers : MonoBehaviour
{
    Detection _detection;
    float _timeToShoot;
    [SerializeField] float _delayBeforeNextShot = 1f;
    bool _canShoot;
    [SerializeField] GameObject _projectile;
    

    void Start()
    {
        _detection = GetComponentInChildren<Detection>();
    }

    void Update()
    {
        if(_detection.TroopsInRange.Count > 0)
        {
            ShootSpeed();
            Shoot();

        }
    }

    private void ShootSpeed()
    {
        if(Time.time > _timeToShoot)
        {
            _canShoot = true;
            _timeToShoot = Time.time + _delayBeforeNextShot;
        }
        else
        {
            _canShoot = false;
        }
    }

    private void Shoot()
    {
        if(_canShoot)
        {
            Instantiate(_projectile, transform.position, _projectile.transform.rotation, transform);
        }
    }

}

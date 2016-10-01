using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Gun : MonoBehaviour
{
    public Player player;
    public GameObject projectile;
    public List<Transform> barrels = new List<Transform>();
    public float shakePositionOffset = 50.0f;
    public float shakeRotationOffset = 10.0f;
    public float shakeCuration = 0.05f;

    public float randomProjectileRotation = 1.0f;

    public float fireRate = 0.5f;
    [HideInInspector]
    public float timeTillNextShot = 0;

    public abstract void OnEquip();

    public abstract void OnUnEquip();

    public abstract void Fire();
}

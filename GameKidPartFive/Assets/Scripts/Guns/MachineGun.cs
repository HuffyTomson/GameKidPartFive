using UnityEngine;
using System.Collections;

public class MachineGun : Gun
{
    int barrelIndex = 0;
    public override void OnEquip()
    {
    }

    public override void OnUnEquip()
    {
    }

    public override void Fire()
    {
        if (timeTillNextShot < 0)
        {
            timeTillNextShot = fireRate;
            player.playerCamera.ShakeCamera(shakePositionOffset, shakeRotationOffset, shakeCuration);

            Instantiate(projectile, barrels[barrelIndex].position, barrels[barrelIndex].rotation * Quaternion.Euler(new Vector3(0, Random.Range(-randomProjectileRotation, randomProjectileRotation), 0)));

            barrelIndex++;
            if (barrelIndex > barrels.Count - 1)
                barrelIndex = 0;
        }
    }

    void Update()
    {
        timeTillNextShot -= Time.deltaTime;
    }
}

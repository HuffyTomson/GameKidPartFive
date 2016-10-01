using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public GameObject turret;
    public PlayerCamera playerCamera;

    public Gun currentGun;

    public float turretTurnSpeed = 10;
    public float bodyTurnSpeed = 10;
    public float bodyMoveSpeed = 0.1f;
    public float bodyAcceleration = 0.5f;

    public float bodyMaxSpeed = 1;
    public float bodyMinSpeed = -0.5f;

    public float bodyMoveDelta = 0;
    public float bodyTurnDelta = 0;
    public float turretTurnDelta = 0;
    
	void Update ()
    {
        #region Input
        bool modKeyDown = false;
        if(Input.GetKey(KeyCode.L) || Input.GetButton("Fire2"))
        {
            modKeyDown = true;
        }

        if(Input.GetKey(KeyCode.K) || Input.GetButton("Fire1"))
        {
            // fire gun
            currentGun.Fire();
        }

        Vector3 inputDelta = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical"));

        if(modKeyDown)
        {
            // turret stuff
            turretTurnDelta = inputDelta.x;
        }
        else
        {
            // body stuff
            bodyTurnDelta = inputDelta.x;
            bodyMoveDelta += inputDelta.z * bodyAcceleration;
            bodyMoveDelta = Mathf.Clamp(bodyMoveDelta, bodyMinSpeed, bodyMaxSpeed);
        }

        if(bodyMoveDelta < 2f && bodyMoveDelta > -2f)
        {
            bodyMoveDelta *= 0.98f;
        }


        turret.transform.Rotate(Vector3.up, turretTurnDelta * turretTurnSpeed * Time.deltaTime);
        this.transform.Rotate(Vector3.up, bodyTurnDelta * bodyTurnSpeed * Time.deltaTime);
        transform.position += transform.forward * bodyMoveSpeed * bodyMoveDelta * Time.deltaTime;

        turretTurnDelta *= 0.95f;
        bodyTurnDelta *= 0.95f;

        #endregion
    }

    void HitTaken(float _damage)
    {
        playerCamera.ShakeCamera(200, 50, 0.15f);
    }
}

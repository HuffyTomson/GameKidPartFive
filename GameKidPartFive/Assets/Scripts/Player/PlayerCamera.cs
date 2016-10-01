using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{
    public GameObject player;
    public Transform turretTarget;
    public Transform bodyTarget;
    public Vector3 cameraOffset = new Vector3(0,11,0);
    public float moveSpeed = 2;
    public float lookSpeed = 5;
    
    public float shakeDrag = 0.8f;

    private Vector3 shakePositionOffset = Vector3.zero;
    private Vector3 shakeLookOffset = Vector3.zero;
    
    void FixedUpdate()
    {
        shakePositionOffset *= shakeDrag;
        shakeLookOffset *= shakeDrag;
    }

	void Update ()
    {
        Vector3 lookOffset = ((turretTarget.position + bodyTarget.position) * 0.5f) + shakePositionOffset;

        transform.position = Vector3.Lerp(transform.position, player.transform.position + cameraOffset + shakePositionOffset, Time.deltaTime * moveSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookOffset - transform.position), lookSpeed * Time.deltaTime);
    }

    public void ShakeCamera(float _position, float _rotation,float _duration)
    {
        StartCoroutine(iShakeCamera(_position, _rotation, _duration));
    }

    IEnumerator iShakeCamera(float _position, float _rotation, float _duration)
    {
        float currentTime = 0;
        while(currentTime < _duration)
        {
            currentTime += Time.deltaTime;
            shakePositionOffset += new Vector3(Random.Range(-_position, _position), Random.Range(-_position, _position), Random.Range(-_position, _position)) * Time.deltaTime;
            shakeLookOffset += new Vector3(Random.Range(-_rotation, _rotation), Random.Range(-_rotation, _rotation), Random.Range(-_rotation, _rotation)) * Time.deltaTime;
            yield return null;
        }

    }
}

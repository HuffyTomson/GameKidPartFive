using UnityEngine;
using System.Collections;

public abstract class Projectile : MonoBehaviour
{
    public float speed = 10;
    public float lifeTime = 1.5f;

    private float currentTime = 0;

    public virtual void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > lifeTime)
            Destroy(this.gameObject);

        transform.position += transform.forward * speed * Time.deltaTime;
    }
}

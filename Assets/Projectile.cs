using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speeed = 10f;

    public float timeOut = 10;

    float timeStarted;

    public GameObject Explosion;

    void Awake()
    {
        StartCoroutine(SelfDestory());
    }

	void Update ()
	{
	    transform.position = transform.position + transform.forward * speeed;
	}

    IEnumerator SelfDestory()
    {
        timeStarted = Time.time;

        while (Time.time - timeStarted < timeOut)
        {
            yield return null;
        }
        Instantiate(Explosion, transform.position, transform.rotation);

        yield return null;
        Destroy(gameObject);
    }
}

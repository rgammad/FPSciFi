using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootController : MonoBehaviour
{
    public GameObject bullet;
    public GameObject shootPosition;
    public int currentClip = 100;
    public int maxClip = 100;
    public float shootCD = .05f;
    public float reloadTime = 1f;

    private float lastShot = 0.0f;
    private bool reloading = false;

    void Start()
    {
        currentClip = maxClip;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 30, 100, 20), "Ammo: " + currentClip + "/" + maxClip);
        GUI.Label(new Rect(10, 40, 100, 20), "Reloading: " + reloading);
    }

    void Update()
    {
        _Shoot();
        Debug.Log(Input.GetKeyDown(KeyCode.R));
        if (Input.GetKeyDown(KeyCode.R) || reloading)
        {
            reloading = true;
            StartCoroutine(_ReloadCoroutine());
        }
    }

    void _Shoot()
    {
        GameObject bulletClone;
        if (Input.GetButton("Fire1"))
        {
            if (!reloading)
            {
                if (Time.time > shootCD + lastShot)
                {
                    if (currentClip <= maxClip && currentClip != 0)
                    {
                        currentClip--;
                        bulletClone = BetterPool.Spawn(bullet, shootPosition.transform.position);
                        bulletClone.transform.rotation = Quaternion.LookRotation(transform.forward, transform.up);
                        bulletClone.transform.Rotate(new Vector3(90, 0,0));
                        Debug.Log("Shot Bullet");
                    }
                    else
                        reloading = true;
                    lastShot = Time.time;
                }
            }
        }
    }

    IEnumerator _ReloadCoroutine()
    {
        if (reloading && currentClip < maxClip)
        {
            yield return new WaitForSeconds(reloadTime);
            currentClip = maxClip;
        }
        reloading = false;
    }
}

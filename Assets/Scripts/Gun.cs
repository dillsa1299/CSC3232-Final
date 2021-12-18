using UnityEngine;

/*
    Script based on code from the following source:
    https://www.youtube.com/watch?v=THnivyG0Mvo&ab_channel=Brackeys
*/

public class Gun : MonoBehaviour
{
    int damage = 1;
    public float range = 50;

    public Camera fpsCam;
    public LayerMask mask;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public AudioSource gunshot;

    public Vector3 upRecoil;
    Vector3 originalRotation;

    void Start()
    {
        originalRotation = transform.localEulerAngles;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        else
        {
            transform.localEulerAngles = originalRotation;
        }
    }

    void Shoot()
    {
        gunshot.pitch = Random.Range(0.7f, 0.8f);
        gunshot.Play();
        transform.localEulerAngles += upRecoil;
        muzzleFlash.Play();
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, mask))
        {
            Zombie zombie = hit.transform.GetComponent<Zombie>();
            if (zombie != null)
            {
                zombie.TakeDamage(damage);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }
}

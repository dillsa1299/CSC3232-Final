using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{

    public float dropTime = 2.0f;
    public float resetTime = 8.0f;
    public bool leafHit = false;
    public float hitTimer = 0.0f;
    Rigidbody rb;
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
        leafGravity(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (leafHit)
        {
            hitTimer += Time.deltaTime;
        }

        if ((hitTimer > dropTime) && leafHit)
        {
            leafGravity(true);
        }

        if ((hitTimer > resetTime) && leafHit)
        {
            resetLeaf();
        }
    }

    void resetLeaf()
    {
        transform.position = startPos;
        transform.eulerAngles = new Vector3(0,0,0);
        leafGravity(false);
        hitTimer = 0.0f;
        leafHit = false;
    }

    void leafGravity(bool isAffected)
    {
        rb.isKinematic = !isAffected;
        rb.useGravity = isAffected;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            leafHit = true;
        }
    }
}

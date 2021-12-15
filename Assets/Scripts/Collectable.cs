using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    float time = 0f;
    public Transform target;
    public GameObject collectable;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        Vector3 pos = transform.position;
        pos.y += 0.01f*Mathf.Sin(time);
        transform.position = pos;
        transform.LookAt(target);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            collectable.SetActive(false);
        }
    }
}

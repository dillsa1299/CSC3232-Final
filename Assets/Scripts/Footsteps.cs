using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioSource footstepSound;
    CharacterController cc;
    public float velocity;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = cc.velocity.magnitude;
        if(cc.isGrounded == true && cc.velocity.magnitude > 0f && footstepSound.isPlaying == false)
        {
            footstepSound.volume = Random.Range(0.4f, 0.6f);
            footstepSound.pitch = Random.Range(0.9f, 1.1f);
            footstepSound.Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSounds : MonoBehaviour
{
    public AudioSource sound1;
    public AudioSource sound2;
    public AudioSource sound3;
    public AudioSource sound4;

    float waitTime = 10f;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer > waitTime)
        {
            makeSound();
        }
        timer += Time.deltaTime;
    }

    void makeSound()
    {
        int random = Random.Range(0,3);
        switch (random)
        {
            case 0:
                sound1.volume = Random.Range(0.2f, 0.3f);
                sound1.pitch = Random.Range(0.9f, 1.1f);
                sound1.Play();
                break;
            case 1:
                sound2.volume = Random.Range(0.2f, 0.3f);
                sound2.pitch = Random.Range(0.9f, 1.1f);
                sound2.Play();
                break;
            case 2:
                sound3.volume = Random.Range(0.2f, 0.3f);
                sound3.pitch = Random.Range(0.9f, 1.1f);
                sound3.Play();
                break;
            case 3:
                sound4.volume = Random.Range(0.2f, 0.3f);
                sound4.pitch = Random.Range(0.9f, 1.1f);
                sound4.Play();
                break;
        }
        waitTime = Random.Range(5f,10f);
        timer = 0;
    }
}

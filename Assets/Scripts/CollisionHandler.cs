using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] float loadDelay = 2f;

    void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
    }

    void StartCrashSequence()
    {
        DisablePlayer();
    }

    void DisablePlayer()
    {
        GetComponent<PlayerControls>().enabled = false;
        explosionParticles.Play();

        

        foreach(MeshRenderer ren in GetComponentsInChildren<MeshRenderer>())
        ren.enabled = false;

        foreach(BoxCollider box in GetComponentsInChildren<BoxCollider>())
        box.enabled = false;



        Invoke("ReloadScene", loadDelay);
    }
    void ReloadScene()
    {
        int levelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(levelIndex);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    Movement mv; 
    [SerializeField] float delay = 2.0f;

    [SerializeField] ParticleSystem success;
    [SerializeField] ParticleSystem crash;
    

    bool isTransitioning = false;
    bool collisionDisabled = false;

    private void Start()
    {
        mv = GetComponent<Movement>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L)) 
        { 
        RespawnNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || collisionDisabled)
            return;
        else
        {
            if (collision.gameObject.CompareTag("Untagged"))
            {
                crashSequence();
            }
            else if (collision.gameObject.CompareTag("Finish"))
            {
                finishSequence();

            }
        }

    }

    private void finishSequence()
    {
        isTransitioning = true;
        mv.enabled = false;
        success.Play();
        Invoke("RespawnNextLevel", delay);
    }

    private void crashSequence()
    {
        isTransitioning = true;
        mv.enabled = false;
        crash.Play();
        Invoke("Respawn", delay);
    }

    private void RespawnNextLevel()
    { 
        int NextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(NextSceneIndex);

        if (NextSceneIndex == SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(0);
    }
    private void Respawn()
    { 
       
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

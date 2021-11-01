using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float waitTime = 1f;
    [SerializeField] ParticleSystem explosionVFX;

    PlayerControls playerControlsScript;
    MeshRenderer myMeshRenderer;
    BoxCollider myBoxCollider;

    void Start()
    {
        playerControlsScript = GetComponent<PlayerControls>();
        myMeshRenderer = GetComponent<MeshRenderer>();
        myBoxCollider = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(StartCrashSequence(waitTime));
    }

    IEnumerator StartCrashSequence(float waitTime)
    {
        playerControlsScript.enabled = false;
        playerControlsScript.ActivateLasers(false);
        myMeshRenderer.enabled = false;
        myBoxCollider.enabled = false;
        explosionVFX.Play();

        yield return new WaitForSeconds(waitTime);

        playerControlsScript.enabled = true;
        ReloadLevel();
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
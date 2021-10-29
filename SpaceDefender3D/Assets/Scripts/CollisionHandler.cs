using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float waitTime = 1f;

    PlayerControls playerControlsScript;

    void Start()
    {
        playerControlsScript = GetComponent<PlayerControls>();
    }

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(StartCrashSequence(waitTime));
    }

    IEnumerator StartCrashSequence(float waitTime)
    {
        playerControlsScript.enabled = false;

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
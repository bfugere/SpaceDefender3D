using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] float moveSpeed = 30f;

    void Start()
    {
        
    }

    void Update()
    {
        float xTilt = Input.GetAxis("Horizontal");
        float yTilt = Input.GetAxis("Vertical");

        float xOffset = xTilt * moveSpeed * Time.deltaTime;
        float yOffset = yTilt * moveSpeed * Time.deltaTime;

        float newXPos = transform.localPosition.x + xOffset;
        float newYPos = transform.localPosition.y + yOffset;

        transform.localPosition = new Vector3(newXPos, newYPos, transform.localPosition.z);
    }
}

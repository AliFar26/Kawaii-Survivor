using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    [Header("Element")]
    [SerializeField] private Transform target;

    [Header("Setting")]
    [SerializeField] private Vector2 minManxXY;
    private void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 targetPosition = target.position;
        targetPosition.z = -10;

        targetPosition.x = Mathf.Clamp(targetPosition .x ,- minManxXY.x, minManxXY.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -minManxXY.y, minManxXY.y);



        transform.position = targetPosition;
    }
}

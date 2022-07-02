using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   public Transform followTarget;
   public float smoothSpeed = 0.125f;
   public Vector3 cameraOffset;

   private void FixedUpdate()
   {
      Vector3 targetPos = followTarget.position + cameraOffset;
      Vector3 smoothPos = Vector3.Lerp(transform.position,targetPos,smoothSpeed);
      transform.position = smoothPos;
   }
}

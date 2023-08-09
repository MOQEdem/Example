using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasRotator : MonoBehaviour
{
   private Camera _camera;

   private void Awake()
   {
      _camera = Camera.main;
   }

   private void Update()
   {
      transform.forward = transform.position - _camera.transform.position;
   }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemTest : MonoBehaviour
{
   [SerializeField] private ParticleSystem _particleSystem;

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Space))
      {
         _particleSystem.Play();
      }
   }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalPrefab : MonoBehaviour
{
    public GameObject animalBody;//the mesh of the object
    public GameObject groundCheck;//the ground check position object
    public ParticleSystem collideEffect;//the stars effect
    public Animator anim;//animal animator
    public ParticleSystem movePs;//the movement dust particle effect
    public ParticleSystem specialPs;//special ability particle effect
}

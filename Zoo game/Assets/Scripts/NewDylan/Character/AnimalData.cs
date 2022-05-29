using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Animal", menuName = "Animals/Animal")]
public class AnimalData : ScriptableObject
{
    new public string name = "AnimalName";

    [Header("Shop data")]
    public int price = 0;
    public int unlockId = 0;
    public Sprite animalImage = null;//image that is shown in the shop

    [Header("Animal in game stats")]
    public float speed = 10;
    public float runSpeed = 10;
    public bool canRun = false;
    public bool canJump = false;
    public bool canDig = false;
    public bool canBreak = false;

    [Header("In Game data")]
    public GameObject AnimalObject;
}

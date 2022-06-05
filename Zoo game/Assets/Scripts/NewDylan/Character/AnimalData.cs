using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Animal", menuName = "Animals/Animal")]
public class AnimalData : ScriptableObject
{
    public string animalName = "AnimalName";
    [Header("Shop data")]
    public int price = 0;
    public int unlockId = 0;
    public string info1 = "Some info 1";
    public string info2 = "Some info 2";
    public string fact = "Some fact";
    public Sprite animalImage = null;//image that is shown in the shop

    [Header("Animal in game stats")]
    public float speed = 10;
    public float runSpeed = 10;
    public float jumpHeight = 20;
    public bool canRun = false;
    public bool canJump = false;
    public bool canDig = false;
    public bool canBreak = false;

    [Header("In Game data")]
    public GameObject AnimalObject;
    public int playLevelIndex = 1;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Animal", menuName = "Animals/Animal")]
public class AnimalData : ScriptableObject
{
    public string[] animalName = new string[2];
    [Header("Shop data")]
    public int price = 0;
    public bool codeunlock = false;
    public string code = "VisitedWildLands426!";
    public string[] info1 =  new string[2];
    public string[] info2 =  new string[2];
    public string[] fact =  new string[2];
    public string[] unlockPagesEng =  new string[3];
    public string[] unlockPagesDutch =  new string[3];
    public Sprite[] pageImages =  new Sprite[3];
    public Sprite animalImage = null;//image that is shown in the shop

    [Header("Animal in game stats")]
    public int health = 4;
    public float animalMass = 4;
    public float gravity = 9;
    public float speed = 10;
    public float jumpHeight = 20;
    public bool canJump = false;
    public bool canDig = false;
    public bool canBreak = false;

    [Header("In Game data")]
    public GameObject AnimalObject;
    public Vector3 camOffset = new Vector3(0,5,-15);
    public int playLevelIndex = 1;

}

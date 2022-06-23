using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Animal", menuName = "Animals/Animal")]
public class AnimalData : ScriptableObject
{
    public string[] animalName = new string[2];
    [Header("Shop data")]
    public int price = 0;//the cost of the animal
    public bool codeunlock = false;//if its unlocked with a code
    public string code = "VisitedWildLands426!";//the code for unlocking the animal if its via a code
    public string[] info1 =  new string[2];
    public string[] info2 =  new string[2];
    public string[] fact =  new string[2];//the ability
    public string[] unlockPagesEng =  new string[3];//unlock page english info
    public string[] unlockPagesDutch =  new string[3];//unlock page dutch info
    public Sprite[] pageImages =  new Sprite[3];
    public Sprite animalImage = null;//image that is shown in the shop

    [Header("Animal in game stats")]
    public int health = 4;//max amount of times the player can collide with something 
    public float animalMass = 4;//rb mass
    public float gravity = 9;
    public float jumpHeight = 20;
    public float regainSpeed = 0.5f;//speed the ability bar is filled again
    public float abilityTime = 3;//max time of ability 
    public bool canJump = false;
    public bool canDig = false;
    public bool canBreak = false;
    public bool canCharge = false;//if the animal can charge
    public bool rockJump = false;//if the animal can jump heigher facing a rock

    [Header("In Game data")]
    public bool showSpace = false;//show space bar instruction
    public GameObject AnimalObject;//the animal spawnable object
    public Vector3 camOffset = new Vector3(0,5,-15);//the custom offset for the camera
    public int playLevelIndex = 3;//the level index that should be loaded if we want to have different levels in the future

}

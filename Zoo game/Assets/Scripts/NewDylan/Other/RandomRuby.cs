using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRuby : MonoBehaviour
{
    [SerializeField] private GameObject[] types;//versions of rubbies
    [SerializeField] private Material rubyMat;//the material of the ruby
    [SerializeField] private Transform holder;//spawn point/holder
    [SerializeField] private float spawnScale = 0.2f;//scale of the spawned rubby

    void Start()//spawns random ruby from the list
    {
        GameObject randomRuby = Instantiate(types[Random.Range(0,types.Length)],holder.position,Quaternion.Euler(0,0,0),holder);
        randomRuby.transform.GetChild(0).GetComponent<MeshRenderer>().material = rubyMat;
        randomRuby.transform.localScale = new Vector3(spawnScale, spawnScale, spawnScale);
    }
}

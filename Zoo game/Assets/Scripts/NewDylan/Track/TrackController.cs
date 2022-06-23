using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour
{   
    [Header("Scripts")]
    [SerializeField] private CollectController collecScript;//rubby collection script

    [Header("Track data")]
    [SerializeField] private Vector3 spawnOffset = new Vector3(0,0,10);//the spawn offset for a new track part
    [SerializeField] private int lengthInfront = 10;//amount of tracks infront of the player
    [SerializeField] private GameObject startTrack;//the first track part
    [SerializeField] private List<TrackPart> partTracks;//spawned track parts
    [SerializeField] private Transform[] lanePositions;//the position the player moves between

    private void Start()
    {
        spawnNewPart(transform.position,startTrack,false);//spawns the first track
        for(int i=0; i<lengthInfront-2; i++)//spawns start tracks infront of the player
        {
            partTracks[i].createNewPart();
        }
    }

    public void spawnNewPart(Vector3 lastPos,GameObject newPart,bool offset)//spawns new track part at the end of the complete track
    {
        Vector3 spawnPos = lastPos;
        if(offset)//if it needs offset for spawning
        {
            spawnPos += spawnOffset;
        }
        if(!newPart)//if for some reason there is no part giving spawn the start track
        {
            newPart = startTrack;
        }

        GameObject trackPart = Instantiate(newPart, spawnPos, Quaternion.Euler(0,0,0),transform) as GameObject; //spawn new track part
        TrackPart partScript = trackPart.GetComponent<TrackPart>();//track part script
        partScript.setStart(this,collecScript);//sets the start of the track part 
        partTracks.Add(partScript);//ads new part to the list
        if(partTracks.Count-3 > lengthInfront)//deletes parts behind the player
        {
            Destroy(partTracks[0].gameObject);//deletes object
            partTracks.RemoveAt(0);//removes part from the list at postion 0 so the oldest part
        }
    }

    public void createEnd()//calls to spawn new track part on the newest track part 
    {
        partTracks[partTracks.Count-1].createNewPart();//cals this function on the part that then calls the spawnNewPart function in this script so it spawns the correct parts
    }

    public Transform[] getLanes()//send the lane positions to the player movement script
    {
        return lanePositions;
    }
   
}

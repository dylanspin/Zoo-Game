// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class elephanttrunkcontroller : MonoBehaviour
// {
//     [Header("Input")]
    
//     public KeyCode trunkKey = KeyCode.E;

//     [Header("Sensitivity")]
//     public float sensX;
//     public float sensY;

//     public float mouseX;
// public float mouseY;
//     public GameObject ta;
//     // Start is called before the first frame update
//     void Start()
//     {
//        ta = GameObject.GetComponent<Target>();
//        mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
//        mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY; 
       
//             }

//     // Update is called once per frame
//     private void Update()
//     {
//      TrunkController();
//     }

//     private void TrunkController()
//     {
//         if(Input.GetKeyDown(trunkKey))
//         {
//            ta.Transform.posistion = mouseX + mouseY;
//         }
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTip : MonoBehaviour
{
   public string message;

   private void OnMouseEnter() 
   {
       TooltipManager._instance.SetAndShowToolTip(message);
   }

   private void OnMouseExit() 
   {
       TooltipManager._instance.HideToolTip();
   }
}

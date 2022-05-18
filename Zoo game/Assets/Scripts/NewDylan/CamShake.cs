using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    private Vector3 originalPos;
    
    private void Start()
    {
        originalPos = this.transform.localPosition;
    }

    public IEnumerator Shake (float duration, float magnitude)
    {
        float elepsed = 0.0f;

        while(elepsed < duration)
        {
            float x = Random.Range(-1f,1f) * magnitude;
            float y = Random.Range(-0.4f,0.4f) * magnitude;
            
            this.transform.localPosition = new Vector3(x,y,originalPos.z);

            elepsed += Time.deltaTime;

            yield return null;
        }

        this.transform.localPosition = originalPos;
    }
}

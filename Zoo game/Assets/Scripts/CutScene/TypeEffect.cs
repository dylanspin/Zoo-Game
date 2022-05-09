using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{

    Text _text;
		string writer;

	[SerializeField] float delayBeforeStart = 0f;
	[SerializeField] float timeBtwChars = 0.1f;
	[SerializeField] string leadingChar = "";
	[SerializeField] bool leadingCharBeforeDelay = false;

    AudioSource Typing;

     //private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>()!;
		

		if(_text != null)
        {
			writer = _text.text;
			_text.text = "";

			StartCoroutine("TypeWriterText");
		}

		
    }




    IEnumerator TypeWriterText()
	{
		_text.text = leadingCharBeforeDelay ? leadingChar : "";

		yield return new WaitForSeconds(delayBeforeStart);

		foreach (char c in writer)
		{
			if (_text.text.Length > 0)
			{
				_text.text = _text.text.Substring(0, _text.text.Length - leadingChar.Length);
			}
			_text.text += c;
			_text.text += leadingChar;

             if (c != ' ')
                    {
                        //audioManager.PlaySFX(SFXType.Typing);
                        Typing = GetComponent<AudioSource>();
                        Typing.Play();
                   } 
                    
			yield return new WaitForSeconds(timeBtwChars);

		}

		if(leadingChar != "")
        {
			_text.text = _text.text.Substring(0, _text.text.Length - leadingChar.Length);
		}
	}

    // Update is called once per frame
    
}

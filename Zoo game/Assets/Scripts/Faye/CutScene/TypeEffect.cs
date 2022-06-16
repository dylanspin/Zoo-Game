using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
	[SerializeField] private TMPro.TextMeshProUGUI _text;
	[SerializeField] private AudioSource Typing;
	[SerializeField] private GameObject skipButton;
	string writer;

	//Adding fields that can be directly changed in Unity, convinient for testing.

	[SerializeField] private float delayBeforeStart = 0f;
	[SerializeField] private float timeBtwChars = 0.1f;
	[SerializeField] private string leadingChar = "";
	[SerializeField] private bool leadingCharBeforeDelay = false;
	private bool done = false;

	//private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
		if(_text != null)
        {
			_text.text = "";
		}
    }

	public void setLangText(string newText)
	{
		writer = newText;
		if(!done)
		{
			StopCoroutine("TypeWriterText");
			StartCoroutine("TypeWriterText");
		}
		else
		{
			_text.text = writer;
		}
	}

	//Checking the length of the text, if the character is not null, the next letter is typed and added to the current length of the text. 

    IEnumerator TypeWriterText()
	{
		_text.text = leadingCharBeforeDelay ? leadingChar : "";

		yield return new WaitForSeconds(delayBeforeStart);
		
		Typing.Play();
		
		foreach (char c in writer)
		{
			if (_text.text.Length > 0)
			{
				_text.text = _text.text.Substring(0, _text.text.Length - leadingChar.Length);
			}
			_text.text += c;
			_text.text += leadingChar;

			yield return new WaitForSeconds(timeBtwChars);
		}

		if(leadingChar != "")
        {
			_text.text = _text.text.Substring(0, _text.text.Length - leadingChar.Length);
		}
		doneWriting();
	}

	private void doneWriting()
	{
		Typing.Stop();
		done = true;
		skipButton.SetActive(false);
	}

	
	public void skip()
	{
		StopCoroutine("TypeWriterText");
		_text.text = writer;
		doneWriting();
	}
}

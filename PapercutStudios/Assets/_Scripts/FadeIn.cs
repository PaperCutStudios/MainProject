using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {		

	public float FadeInTime;
	public AudioClip clip;

	private bool bFadingIn = false;
	private Image thisImage;
	// Use this for initialization
	void Start () {
		bFadingIn = true;
		thisImage = gameObject.GetComponent<Image>();
		AudioSource.PlayClipAtPoint(clip,transform.position);
	}
	// Update is called once per frame
	void Update () {
		if(bFadingIn) {
			if(thisImage.color.a <= 0.05f)
			{
				// ... set the colour to clear and disable the GUITexture.
				thisImage.color = Color.clear;
				thisImage.enabled = false;
				
				bFadingIn = false;
			} else {
				FadeToClear();
			}
		}
	}
	
	void FadeToClear ()
	{
		// Lerp the colour of the texture between itself and transparent.
		thisImage.color = Color.Lerp(thisImage.color, Color.clear, FadeInTime * Time.deltaTime);
	}
}

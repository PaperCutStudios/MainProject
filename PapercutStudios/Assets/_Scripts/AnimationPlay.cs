using UnityEngine;
using System.Collections;

public class AnimationPlay : MonoBehaviour {

	public GameObject Anim;
	// Use this for initialization
	void Start () {
		Anim.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void AnimationActive(){
		Anim.gameObject.SetActive (true);
	}
}

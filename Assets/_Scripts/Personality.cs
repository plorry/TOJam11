using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Personality : MonoBehaviour {

	public string name;
	public CardboardAudioSource audioSource;
	private Dictionary<string, AudioClip> audioClips;
	// Use this for initialization
	void Start () {
		audioClips = new Dictionary<string, AudioClip> ();
		audioSource = GetComponentInChildren<CardboardAudioSource> ();
		print (audioSource);
		LoadAudio ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void LoadAudio() {
		AudioClip saved = (AudioClip)Resources.Load (name + "-saved");
		AudioClip talk01 = (AudioClip)Resources.Load (name + "-talk01");
		AudioClip talk02 = (AudioClip)Resources.Load (name + "-talk02");
		AudioClip talk03 = (AudioClip)Resources.Load (name + "-talk03");
		AudioClip talk04 = (AudioClip)Resources.Load (name + "-talk04");
		AudioClip talk05 = (AudioClip)Resources.Load (name + "-talk05");
		AudioClip talk06 = (AudioClip)Resources.Load (name + "-talk06");
		AudioClip whoa = (AudioClip)Resources.Load (name + "-whoa");
		audioClips.Add ("saved", saved);
		audioClips.Add ("talk01", talk01);
		audioClips.Add ("talk02", talk02);
		audioClips.Add ("talk03", talk03);
		audioClips.Add ("talk04", talk04);
		audioClips.Add ("talk05", talk05);
		audioClips.Add ("talk06", talk06);
		audioClips.Add ("whoa", whoa);
	}

	public void Say(string phrase) {
		audioSource.clip = audioClips [phrase];
		audioSource.Play ();
	}
}

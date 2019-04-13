using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : Singleton<SoundManager> {

	[SerializeField, Tooltip("When player click on the screen.")]
	private AudioClip playerClick;

	[SerializeField, Tooltip("When cube lands.")]
	private AudioClip playerLand;

	[SerializeField, Tooltip("Game Over SFX.")]
	private AudioClip gameOver;

	// GameObject to reference the soundmanager itself
	private AudioSource _soundManager;

	// Use this for initialization
	void Start() {
		_soundManager = gameObject.GetComponent<AudioSource>();
	}

	public void PlayButtonClickOn() {
		_soundManager.PlayOneShot(playerClick);
	}


	public void PlayButtonClickOff() {
		_soundManager.PlayOneShot(playerLand);
	}

	public void PlayGameOver() {
		_soundManager.PlayOneShot(gameOver);
	}
}
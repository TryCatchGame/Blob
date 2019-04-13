using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyBox;

public class ButtonCapsule : MonoBehaviour {

	public delegate void CapsuleCollisionWithPlayer(CapsulePos capsulePos);

	public CapsuleCollisionWithPlayer OnCapsuleCollisionWithPlayer;

	[SerializeField, Tooltip("Modifier value of how fast this button will appear/hide."), Range(1f, 10f)]
	private float lerpModifier;

	[SerializeField, Tooltip("Where this capsule will appear from the center cube")]
	private CapsulePos capsulePos;

	public CapsulePos Position {
		get {
			return capsulePos;
		}
	}

	private void Awake() {
		OnCapsuleCollisionWithPlayer = delegate { };
	}

	public void Pop(System.Action callback = null) {
		transform.position = Vector3.zero;
		GameObject centerCube = CenterCube.Instance.gameObject;

		Vector3 centerCubeSize = centerCube.GetComponent<BoxCollider>().size;

		Vector3 newCapsulePos = Vector3.zero;

		// Determine where to move the capsule to in order to pop it up.
		if(capsulePos == CapsulePos.Top) {
			newCapsulePos.y = centerCube.transform.position.y + (centerCubeSize.y / 2f);
		} else if(capsulePos == CapsulePos.Bottom) {
			newCapsulePos.y = centerCube.transform.position.y - (centerCubeSize.y / 2f);
		} else if(capsulePos == CapsulePos.Right) {
			newCapsulePos.x = centerCube.transform.position.x + (centerCubeSize.x / 2f);
		} else if(capsulePos == CapsulePos.Left) {
			newCapsulePos.x = centerCube.transform.position.x - (centerCubeSize.x / 2f);
		}

		StartCoroutine(LerpToNewPosition(newCapsulePos, callback));
	}

	public void Hide(System.Action callback = null) {
		StartCoroutine(LerpToNewPosition(Vector3.zero, callback));
	}

	private IEnumerator LerpToNewPosition(Vector3 newPos, System.Action callback = null) {
		Vector3 oldPos = transform.position;

		float progress = 0f;

		while(progress < 1f) {
			transform.position = Vector3.Lerp(oldPos, newPos, progress);
			progress += Time.deltaTime * lerpModifier;

			yield return new WaitForEndOfFrame();
		}

		transform.position = newPos;

		callback?.Invoke();

		yield return null;
	}

	private void OnTriggerEnter(Collider other) {
		if(other.CompareTag("PlayerCube")) {
			SoundManager.Instance.PlayButtonClickOff();
			OnCapsuleCollisionWithPlayer(Position);
		}
	}
}

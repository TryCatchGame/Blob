using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCapsule : MonoBehaviour {

    [SerializeField, Tooltip("Where this capsule will appear from the center cube")]
    private CapsulePos capsulePos;

    public void Pop() {
        GameObject centerCube = CenterCube.Instance.gameObject;

        Vector3 centerCubeSize = centerCube.GetComponent<BoxCollider>().size;

        Vector3 newCapsulePos = Vector3.zero;

        // Determine where to move the capsule to in order to pop it up.
        if (capsulePos == CapsulePos.Top) {
            newCapsulePos.y = centerCube.transform.position.y + (centerCubeSize.y / 2f);
        } else if (capsulePos == CapsulePos.Bottom) {
            newCapsulePos.y = centerCube.transform.position.y - (centerCubeSize.y / 2f);
        } else if (capsulePos == CapsulePos.Right) {
            newCapsulePos.x = centerCube.transform.position.x + (centerCubeSize.x / 2f);
        } else if (capsulePos == CapsulePos.Left) {
            newCapsulePos.x = centerCube.transform.position.x - (centerCubeSize.x / 2f);
        }

        StartCoroutine(LerpToNewPosition(newCapsulePos));
    }

    public void Hide() {
        StartCoroutine(LerpToNewPosition(Vector3.zero));
    }

    private IEnumerator LerpToNewPosition(Vector3 newPos) {
        Vector3 oldPos = transform.position;

        float progress = 0f;

        while (progress < 1f) {
            transform.position = Vector3.Lerp(oldPos, newPos, progress);
            progress += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }
}

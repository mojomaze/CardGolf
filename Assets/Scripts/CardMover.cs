using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMover : MonoBehaviour {

	private CardMoveToStackEventArgs e;
	public float duration = 1f;

	public event CardMoveToStackEventHandler CardMoveToStackEvent;

	public void MoveCard(Vector3 moveToPosition) {
	  StartCoroutine(Move(moveToPosition));
	}

	public void MoveCardToStack(CardMoveToStackEventArgs e) {
		this.e = e;
	  StartCoroutine(Move(e.position));
	}

	IEnumerator Move(Vector3 moveToPosition) {
		float time = 0f;
    Vector3 startPosition = transform.position;
    float journeyLength = Vector3.Distance(startPosition, moveToPosition);

		while (time <= duration) {
			time = time + Time.deltaTime / duration;
      float distCovered = journeyLength * time;
      transform.position = Vector3.Lerp(startPosition, moveToPosition, distCovered);
			yield return new WaitForFixedUpdate ();
		}

		if (e != null && CardMoveToStackEvent != null) {
			CardMoveToStackEvent(this, e);
			e = null;
		}
	}

}

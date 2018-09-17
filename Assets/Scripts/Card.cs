using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

	private SpriteRenderer spriteRenderer;
  public CardModel cardModel;

  void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void ToggleFace(bool showFace, int sortingOrder) {
    if (cardModel == null) {
      return;
    }
		if (showFace) {
			spriteRenderer.sprite = cardModel.cardFace;
		} else {
			spriteRenderer.sprite = cardModel.cardBack;
		}

		spriteRenderer.sortingOrder = sortingOrder;

    cardModel.isFaceUp = showFace;
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFlipper : MonoBehaviour {
	SpriteRenderer spriteRenderer;

	public AnimationCurve scaleCurve;
	public float duration = 0.5f;

  private CardModel cardModel;

	void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void FlipCard(CardModel cardModel) {
    Sprite startImage = cardModel.isFaceUp ? cardModel.cardFace : cardModel.cardBack;
    Sprite endImage = cardModel.isFaceUp ? cardModel.cardBack : cardModel.cardFace;

		StopCoroutine(Flip(startImage, endImage, cardModel));
		StartCoroutine(Flip(startImage, endImage, cardModel));
	}

	IEnumerator Flip(Sprite startImage, Sprite endImage, CardModel cardModel) {
		spriteRenderer.sprite = startImage;

		float time = 0f;
		while (time <= 1f) {
			float scale = scaleCurve.Evaluate (time);
			time = time + Time.deltaTime / duration;

			Vector3 localScale = transform.localScale;
			localScale.x = scale;
			transform.localScale = localScale;

			if (time >= 0.5f) {
				spriteRenderer.sprite = endImage;
			}

			yield return new WaitForFixedUpdate ();
		}

    cardModel.isFaceUp = !cardModel.isFaceUp;
	}

}

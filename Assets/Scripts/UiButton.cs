using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiButton : MonoBehaviour {

	[SerializeField] private GameObject targetObject;
	[SerializeField] private string targetMessage;
	public Color highlightColor = Color.cyan;

	//when mouse hover button it changes color
	public void OnMouseOver() {
		SpriteRenderer sprite = GetComponent<SpriteRenderer>();
		if (sprite != null) {
			sprite.color = highlightColor;
		}
	}
	
	// when unactive brings it's color back
	public void OnMouseExit() {
		SpriteRenderer sprite= GetComponent<SpriteRenderer>();
		if (sprite != null) {
			sprite.color = Color.white;
		}
	}
	// obviously when we click on our button
	public void OnMouseDown() {
		transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
	}

	public void OnMouseUp() {
		// same as (1, 1, 1) and it's cool
		transform.localScale = Vector3.one;
		if (targetObject != null) {
			targetObject.SendMessage(targetMessage);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	public const int gridRows = 2;
	public const int gridCols = 4;
	public const float offsetX = 2f;
	public const float offsetY = 2.5f;

	private MemoryCard _firstRevealed;
	private MemoryCard _secondRevealed;
	private int _score = 0;

	[SerializeField] private MemoryCard originalCard;
	[SerializeField] private Sprite[] images;
	[SerializeField] private TextMesh scoreLabel;

	public bool canReveal {
		// returns false if second card is open
		// is second card is already open? because we need only two cards be opened
		get {return _secondRevealed == null;}
	}

	void Start () {
		Vector3 startPos = originalCard.transform.position;

		int[] numbers = {0, 0, 1, 1, 2, 2, 3, 3};
		numbers = ShuffleArray(numbers);

		// place cards in a grid
		for (int i = 0; i < gridCols; i++) {
			for (int j = 0; j < gridRows; j++) {
				MemoryCard card;

				// use the original for the first grid space
				if (i == 0 && j == 0) {
					card = originalCard;
				} else {
					card = Instantiate(originalCard) as MemoryCard;
				}

				// next card in the list for each grid space
				int index = j * gridCols + i;
				int id = numbers[index];
				card.SetCard(id, images[id]);


				float posX = (offsetX * i) + startPos.x;
				float posY = -(offsetY * j) + startPos.y;
				card.transform.position = new Vector3(posX, posY, startPos.z); 
			}
		}
	}

	private int[] ShuffleArray(int[] numbers) {
		// realisation of Knut shuffle algorithm
		int[] newArray = numbers.Clone() as int[];
		for (int i = 0; i < newArray.Length; i++) {
			int tmp = newArray[i];
			int r = Random.Range(0, newArray.Length);
			newArray[i] = newArray[r];
			newArray[r] = tmp;
		}
		return newArray;
	}

	public void CardRevealed(MemoryCard card) {
		if (_firstRevealed == null) {
			_firstRevealed = card;
		} else {
			_secondRevealed = card;
			// after two card were open it starts coroutine
			StartCoroutine(CheckMatch());
		}
	}

	private IEnumerator CheckMatch() {
		if (_firstRevealed.id == _secondRevealed.id) {
			_score++;
			scoreLabel.text = "Score: " + _score;
		} else {
			yield return new WaitForSeconds(.5f);

			// closing cards
			_firstRevealed.Unreveal();
			_secondRevealed.Unreveal();
		}

		// cleaning variables
		_firstRevealed = null;
		_secondRevealed = null;
	}

	public void Restart() {
		SceneManager.LoadScene("MainScene");
	}
}

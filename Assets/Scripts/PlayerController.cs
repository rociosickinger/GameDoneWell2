using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	public float speed = 1;
	public TextMeshProUGUI countText;

	public GameObject winTextObject;

	private float movementX;
	private float movementY;

	private Rigidbody rb;
	private int count;
	Scene scene;




	// At the start of the game..
	void Start()
	{
		scene = SceneManager.GetActiveScene();
		Debug.Log("Active Scene name is: " + scene.name + "\nActive Scene index: " + scene.buildIndex);

		// Assign the Rigidbody component to our private rb variable
		rb = GetComponent<Rigidbody>();

		// Set the count to zero 
		count = 0;

		SetCountText();

		// Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
		winTextObject.SetActive(false);


	}

	void FixedUpdate()
	{
		// Create a Vector3 variable, and assign X and Z to feature the horizontal and vertical float variables above
		Vector3 movement = new Vector3(movementX, 0.0f, movementY);

		rb.AddForce(movement * speed);
	}

	void OnTriggerEnter(Collider other)
	{
		// ..and if the GameObject you intersect has the tag 'Pick Up' assigned to it..
		if (other.gameObject.CompareTag("PickUp"))
		{
			other.gameObject.SetActive(false);

			// Add one to the score variable 'count'
			count = count + 1;

			// Run the 'SetCountText()' function (see below)
			SetCountText();
		}
	}

	void OnMove(InputValue value)
	{
		Vector2 v = value.Get<Vector2>();

		movementX = v.x;
		movementY = v.y;
	}

	void SetCountText()
	{
		countText.text = "Count: " + count.ToString();

		if (count >= 1)
		{
			LoadNextLevel();
		}
	}
	public void LoadNextLevel()
	{
	
		Debug.Log("Active Scene name is: " + scene.name + "\nActive Scene index: " + scene.buildIndex);

		if (scene.buildIndex < 4)
		{
			SceneManager.LoadScene(scene.buildIndex + 1, LoadSceneMode.Single);
		}


		else if (scene.buildIndex == 4)
		{
			Debug.Log("Active Scene name is: " + scene.name + "\nActive Scene index: " + scene.buildIndex);

			SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
		}
		
	}
}
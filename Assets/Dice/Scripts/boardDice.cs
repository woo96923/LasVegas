using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boardDice : MonoBehaviour
{

	// constant of current demo mode
	private const int MODE_GALLERY = 1;
	private const int MODE_ROLL = 2;
	// current demo mode
	private int mode = 0;

	// next camera position when moving the camera after switching mode
	private GameObject nextCameraPosition = null;
	// start camera position when moving the camera after switching mode
	private GameObject startCameraPosition = null;
	// store gameObject (empty) for mode MODE_ROLL camera position
	private GameObject camRoll = null;
	// store gameObject (empty) for mode MODE_GALLERY camera position
	private GameObject camGallery = null;
	// speed of moving camera after switching mode
	private float cameraMovementSpeed = 0.8F;
	private float cameraMovement = 0;

	// initial/starting die in the gallery
	private string galleryDie = "d6-red";
	private GameObject galleryDieObject = null;

	// handle drag rotating the die in the gallery
	private bool dragging = false;
	private Vector2 dragStart;
	private Vector3 dragStartAngle;
	private Vector3 dragLastAngle;

	// rectangle GUI area's 
	private Rect rectGallerySelectBox;
	private Rect rectGallerySelect;
	private Rect rectModeSelect;

	// GUI gallery die selector texture
	private Texture txSelector = null;

	// Start is called before the first frame update
	void Start()
    {
		// store/cache mode assiociated camera positions
		camRoll = GameObject.Find("cameraPositionRoll");
		camGallery = GameObject.Find("cameraPositionGallery");
		// set GUI rectangles of the (screen related) gallery selector
		rectGallerySelectBox = new Rect(Screen.width - 260, 10, 250, 170);
		rectGallerySelect = new Rect(Screen.width - 250, 35, 219, 109);
		rectModeSelect = new Rect(10, 10, 180, 80);
		// set (first) mode to gallery
		

	}

	private GameObject spawnPoint = null;
	private Vector3 Force()
	{
		Vector3 rollTarget = Vector3.zero + new Vector3(2 + 7 * Random.value, .5F + 4 * Random.value, -2 - 3 * Random.value);
		return Vector3.Lerp(spawnPoint.transform.position, rollTarget, 1).normalized * (-35 - Random.value * 20);
	}

	private bool PointInRect(Vector2 p, Rect r)
	{
		return (p.x >= r.xMin && p.x <= r.xMax && p.y >= r.yMin && p.y <= r.yMax);
	}

	private Vector2 GuiMousePosition()
	{
		Vector2 mp = Input.mousePosition;
		Vector3 vp = Camera.main.ScreenToViewportPoint(new Vector3(mp.x, mp.y, 0));
		mp = new Vector2(vp.x * Camera.main.pixelWidth, (1 - vp.y) * Camera.main.pixelHeight);
		return mp;
	}

	string randomColor
	{
		get
		{
			string _color = "blue";
			int c = System.Convert.ToInt32(Random.value * 6);
			switch (c)
			{
				case 0: _color = "red"; break;
				case 1: _color = "green"; break;
				case 2: _color = "blue"; break;
				case 3: _color = "yellow"; break;
				case 4: _color = "white"; break;
				case 5: _color = "black"; break;
			}
			return _color;
		}
	}


	// Update is called once per frame
	void Update()
    {
		spawnPoint = GameObject.Find("spawnPoint");
		// check if we have to roll dice
		if (Input.GetMouseButtonDown(Dice.MOUSE_LEFT_BUTTON) && !PointInRect(GuiMousePosition(), rectModeSelect))
		{
			// left mouse button clicked so roll random colored dice 2 of each dieType
			Dice.Clear();

			Dice.Roll("1d10", "d10-" + randomColor, spawnPoint.transform.position, Force());
			Dice.Roll("1d10", "d10-" + randomColor, spawnPoint.transform.position, Force());
			Dice.Roll("1d10", "d10-" + randomColor, spawnPoint.transform.position, Force());
			Dice.Roll("1d10", "d10-" + randomColor, spawnPoint.transform.position, Force());
			Dice.Roll("1d6", "d6-" + randomColor, spawnPoint.transform.position, Force());
			Dice.Roll("1d6", "d6-" + randomColor, spawnPoint.transform.position, Force());
			Dice.Roll("1d6", "d6-" + randomColor, spawnPoint.transform.position, Force());
			Dice.Roll("1d6", "d6-" + randomColor, spawnPoint.transform.position, Force());
		}
		else
		if (Input.GetMouseButtonDown(Dice.MOUSE_RIGHT_BUTTON) && !PointInRect(GuiMousePosition(), rectModeSelect))
		{
			// right mouse button clicked so roll 8 dice of dieType 'gallery die'
			Dice.Clear();
			string[] a = galleryDie.Split('-');
			Dice.Roll("8" + a[0], galleryDie, spawnPoint.transform.position, Force());
		}
	}
}

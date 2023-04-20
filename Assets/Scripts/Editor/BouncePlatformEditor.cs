using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BouncePlatform))]
public class BouncePlatformEditor : Editor {
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		BouncePlatform bouncePlatform = (BouncePlatform)target;

		bouncePlatform.bounceAngle = Mathf.Repeat(bouncePlatform.bounceAngle + 180f, 360f) - 180f;
		bouncePlatform.directionVisualiser.localRotation = Quaternion.Euler(0, 0, bouncePlatform.bounceAngle);
	}
}

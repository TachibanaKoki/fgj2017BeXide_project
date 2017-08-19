using UnityEngine;
using System.Collections;

public class FaceUpdate : MonoBehaviour
{
	public AnimationClip[] animations;

	Animator anim;

	public float delayWeight;

	void Start ()
	{
		anim = GetComponent<Animator> ();
	}

	//void OnGUI ()
	//{
	//	foreach (var animation in animations) {
	//		if (GUILayout.Button (animation.name)) {
	//			anim.CrossFade (animation.name, 0);
	//		}
	//	}
	//}

	float current = 0;
}

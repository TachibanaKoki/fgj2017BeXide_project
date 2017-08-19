using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviourFast<SoundManager> {

	CriAtomSource atomSourceBgm;
	// Use this for initialization
	void Start () {
		atomSourceBgm = this.gameObject.AddComponent<CriAtomSource> ();
		atomSourceBgm.cueSheet = "CueSheet_0";
		atomSourceBgm.use3dPositioning = false;


		//StartCoroutine ("SoundTest");


	}

	/// <summary>
	/// Sounds the test.
	/// </summary>
	/// <returns>The test.</returns>
	IEnumerator SoundTest()
	{
		//	title
		SoundManager.Instance.SoundEvent(EnumBgmEvent.title);
		yield return new WaitForSeconds(2);
		//	bgm_low
		SoundManager.Instance.SoundEvent(EnumBgmEvent.bgm_low);
		yield return new WaitForSeconds(5);
		//	bgm_hi
		SoundManager.Instance.SoundEvent(EnumBgmEvent.bgm_hi);
		yield return new WaitForSeconds(5);
		//	bgm_low
		SoundManager.Instance.SoundEvent(EnumBgmEvent.bgm_low);
		yield return new WaitForSeconds(5);
		SoundManager.Instance.SoundEvent(EnumBgmEvent.bgm_hi);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public enum EnumBgmEvent
	{
		title,
		bgm_low,
		bgm_hi

	}

	/// <summary>
	/// 呼び出し例 ->	SoundManager.Instance.SoundEvent(EnumBgmEvent.bgm_hi);
	/// </summary>
	/// <param name="bgmEvent">Bgm event.</param>
	public void SoundEvent(EnumBgmEvent bgmEvent)
	{
		switch (bgmEvent) {
		case  EnumBgmEvent.title:
			atomSourceBgm.cueName = "title";
			atomSourceBgm.Play ();
			break;
		case  EnumBgmEvent.bgm_low:
			atomSourceBgm.player.SetSelectorLabel ("BgmState", "low");
			if (atomSourceBgm.cueName != "bgm") {
				atomSourceBgm.cueName = "bgm";
				atomSourceBgm.Play ();
			}
			atomSourceBgm.player.UpdateAll ();
			break;
		case  EnumBgmEvent.bgm_hi:
			atomSourceBgm.player.SetSelectorLabel ("BgmState", "hi");
			if (atomSourceBgm.cueName != "bgm") {
				atomSourceBgm.cueName = "bgm";
				atomSourceBgm.Play ();
			}
			atomSourceBgm.player.UpdateAll ();
			break;
		

		}
	}
}

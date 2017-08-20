using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviourFast<SoundManager> {

	CriAtomSource atomSourceBgm;
	CriAtomSource atomSourceSe;
	CriAtomSource atomSourceLifeGage;
	// Use this for initialization
	void Start () {
		
		SoundSetup ();

		//StartCoroutine ("SoundTest");


	}

	void SoundSetup()
	{
		atomSourceBgm = this.gameObject.AddComponent<CriAtomSource> ();
		atomSourceBgm.cueSheet = "CueSheet_0";
		atomSourceBgm.use3dPositioning = false;

		atomSourceSe = this.gameObject.AddComponent<CriAtomSource> ();
		atomSourceSe.cueSheet = "CueSheet_0";
		atomSourceSe.use3dPositioning = false;

		atomSourceLifeGage = this.gameObject.AddComponent<CriAtomSource> ();
		atomSourceLifeGage.cueSheet = "CueSheet_0";
		atomSourceLifeGage.use3dPositioning = false;
		atomSourceLifeGage.Play ("life");	//	ライフ音
		atomSourceLifeGage.Stop ();	//	ライフ音

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
		
		if (SoundManager.Instance.atomSourceLifeGage == null) {
			
		} else 
		{
			Update_Sound ();

		}
	}

	public void Update_Sound()
	{
		CriAtomSource.Status status = atomSourceLifeGage.status;
		if ((status == CriAtomSource.Status.Stop) || (status == CriAtomSource.Status.PlayEnd)) {//停止かエンドか
			
		} else {
			if (CharacterManager.Instance != null) {
				SoundManager.Instance.atomSourceLifeGage.SetAisac ("Any", 1.0f - CharacterManager.Instance.m_nakedGage);
			}
		}
	}

	public enum EnumBgmEvent
	{
		/// <summary>
		/// タイトル画面
		/// </summary>
		title,
		/// <summary>
		/// ゲーム中
		/// </summary>
		bgm_low,
		/// <summary>
		/// ゲーム中みつかった時
		/// </summary>
		bgm_hi,

		/// <summary>
		/// ダンボールゲット
		/// </summary>
		danbowl_get,
		/// <summary>
		/// ゲームクリア
		/// </summary>
		clear,

		/// <summary>
		/// Unityちゃんが走っていたら
		/// </summary>
		Footstep,
		/// <summary>
		/// Unityちゃんが立ち止まったら
		/// </summary>
		Standing,

		/// <summary>
		/// ゲームオーバー
		/// </summary>
		gameover

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

			atomSourceLifeGage.Stop();	//	ライフ音
			break;
		case  EnumBgmEvent.bgm_hi:
			atomSourceBgm.player.SetSelectorLabel ("BgmState", "hi");
			if (atomSourceBgm.cueName != "bgm") {
				atomSourceBgm.cueName = "bgm";
				atomSourceBgm.Play ();
			}
			atomSourceBgm.player.UpdateAll ();
			atomSourceSe.Play ("odoroki");

				atomSourceLifeGage.Play ("life");	//	ライフ音
			break;
		
		case  EnumBgmEvent.danbowl_get:
			atomSourceSe.Play ("danbowl_get");
			break;
		case  EnumBgmEvent.clear:
			atomSourceSe.Play ("clear");
			break;		
		case  EnumBgmEvent.Footstep:
			atomSourceSe.Play ("Footstep");
			break;
		case  EnumBgmEvent.Standing:
			atomSourceSe.Play ("Standing");
			break;
		case  EnumBgmEvent.gameover:
			atomSourceSe.Play ("gameover");
			break;
		}
	}
}

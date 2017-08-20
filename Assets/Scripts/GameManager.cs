using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager I;

    [SerializeField]
    Sprite GameClearImage;

    [SerializeField]
    Sprite GameOverImage;

    public Image gameOverImage;
    public bool isGameClear = false;
    public void Start()
    {
        I = this;
        
        if (SoundManager.Instance==null)
        {
            SceneManager.LoadSceneAsync("SoundSetupScene", LoadSceneMode.Additive);
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("main"));
        SoundManager.Instance.SoundEvent(SoundManager.EnumBgmEvent.bgm_low);
    }

	public void GameClear()
    {
        if (isGameClear) return;
        isGameClear = true;
        //とりあえず仮でシーンをループ
        gameOverImage.sprite = GameClearImage;
        gameOverImage.rectTransform.DOLocalMoveY(0.0f, 2.0f).SetEase(Ease.OutBounce).OnKill(() =>
        {
            SceneManager.LoadSceneAsync("Clear");
            SoundManager.Instance.SoundEvent(SoundManager.EnumBgmEvent.clear);

        }
);
    }

    public void GameOver()
    {
        if (isGameClear) return;
        isGameClear = true;
		gameOverImage.sprite = GameOverImage;
		SoundManager.Instance.PlayEnemyVoice ("v_hadakaja");
        gameOverImage.rectTransform.DOLocalMoveY(0.0f, 2.0f).SetEase(Ease.OutBounce).OnKill(() =>
			{
            SceneManager.LoadSceneAsync("Game Over");
            SoundManager.Instance.SoundEvent(SoundManager.EnumBgmEvent.gameover);
        }
        );
    }
}

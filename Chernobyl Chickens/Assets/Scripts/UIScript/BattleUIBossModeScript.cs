using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIBossModeScript : MonoBehaviour
{
    public GameObject RedOverlay;
    public GameObject BossOverlay;
    public float duration;
    public float opacity;
    public LeanTweenType easeType;

    // Start is called before the first frame update
    void OnEnable()
    {
        Debug.Log("BOSSMOOOOOODDEEE");
        LeanTween.alpha(RedOverlay.GetComponent<RectTransform>(), opacity, duration).setDelay(1f).setLoopPingPong();
        LeanTween.delayedCall(4f, () => 
        {
            OnComplete();
        }
        );
    }

    public void OnComplete()
    {
        BossOverlay.SetActive(false);
        //Time.timeScale = 1f;
    }
}

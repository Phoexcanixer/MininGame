using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class OreView : MonoBehaviour
{
    public Image HPImg;
    public OreManager OreManager;
    void Start()
    {
        OreManager.HP.Subscribe(hp => HPImg.fillAmount = hp / OreManager.BaseHP);
        Observable.EveryUpdate()
            .Where(_ => gameObject.activeInHierarchy)
            .Subscribe(_ =>
            {
                transform.rotation = Camera.main.transform.rotation;
                transform.forward = Camera.main.transform.forward;
            });
    }
}

using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class OreManager : MonoBehaviour
{
    public GameObject ThisView;
    public AudioClip DigSound;
    public ReactiveProperty<float> HP = new();
    public float BaseHP;
    void Awake()
    {
        HP.Value = 10;
        BaseHP = HP.Value;
    }
    public void ATK()
    {
        AudioSource.PlayClipAtPoint(DigSound, Camera.main.transform.position);
        HP.Value--;
        if (HP.Value <= 0)
            gameObject.SetActive(false);
    }
    public void OpenView() => ThisView.SetActive(true);
    public void CloseView() => ThisView.SetActive(false);
}

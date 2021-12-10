using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsSlider : MonoBehaviour
{
    public Slider volume_S;
    public TMPro.TMP_Text volumeNum;

    private void Awake()
    {
        volume_S.value = Settings.volume;

        UpdateVolume();
    }
    public void UpdateVolume()
    {
        Settings.ChangeVolume((int)volume_S.value);
        volume_S.value = Settings.volume;
        volumeNum.text = volume_S.value.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider slider;
    public void SetBossMaxHealth(int bosshealth)
    {
        slider.maxValue = bosshealth;
        slider.value = bosshealth;
    }

    public void SetBossHealth(int bosshealth)
    {
        slider.value = bosshealth;
    }
}

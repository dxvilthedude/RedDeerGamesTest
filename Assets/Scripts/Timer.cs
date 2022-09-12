using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timer_Txt;
    [SerializeField] private float sectionCurrentTime;

    private bool isTimerGoing;
    private string timePlaying_Str;
    private TimeSpan timePlaying;

    void Start()
    {
        isTimerGoing = false;
        timer_Txt.text = "00:00.0";
    }
    public void BeginTimer()
    {
        isTimerGoing = false;
        StopCoroutine(UpdateTimer());

        sectionCurrentTime = 0f;
        isTimerGoing = true;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (isTimerGoing)
        {
            sectionCurrentTime += Time.deltaTime;

            timePlaying = TimeSpan.FromSeconds(sectionCurrentTime);
            timePlaying_Str = timePlaying.ToString("mm':'ss'.'f");
            timer_Txt.text = timePlaying_Str;

            yield return null;
        }
    }

}
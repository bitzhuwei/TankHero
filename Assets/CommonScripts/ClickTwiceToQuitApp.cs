﻿using UnityEngine;
using System.Collections;

public class ClickTwiceToQuitApp : MonoBehaviour {

    public string tip = "Click again to quit app";
    public float fadingSpeed = 1;
    private UnityEngine.UI.Text tipTextBox;
    private bool fading;
    private float startFadingTime;
    private Color originalColor;
    private Color transparentColor;

    // Use this for initialization
    void Start()
    {
        tipTextBox = this.GetComponent<UnityEngine.UI.Text>();
        originalColor = tipTextBox.color;
        transparentColor = originalColor;
        transparentColor.a = 0;
        tipTextBox.text = tip;
        tipTextBox.color = transparentColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (startFadingTime == 0)
            {
                tipTextBox.color = originalColor;
                startFadingTime = Time.time;
                fading = true;
            }
            else
            {
                Application.Quit();
            }
        }

        if (fading)
        {
            tipTextBox.color = Color.Lerp(originalColor, transparentColor, (Time.time - startFadingTime) * fadingSpeed);

            if (tipTextBox.color.a < 2.0 / 255)
            {
                tipTextBox.color = transparentColor;
                startFadingTime = 0;
                fading = false;
            }
        }
    }
}

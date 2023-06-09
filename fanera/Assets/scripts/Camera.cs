using System;
using System.IO;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private void Start()
    {
        // ∆дЄм 3 секунды
        Invoke("TakeScreenshot", 1.62f);
    }

    private void TakeScreenshot()
    {
        string folderPath = "C:/Screenshots";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string filePath = folderPath + "/screenshot_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
        ScreenCapture.CaptureScreenshot(filePath);

        Debug.Log("Screenshot taken and saved to " + filePath);
    }
}
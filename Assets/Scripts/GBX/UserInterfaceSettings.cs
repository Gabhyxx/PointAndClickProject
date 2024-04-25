using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class UserInterfaceSettings : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resolutionText;
    [SerializeField] TextMeshProUGUI fullscreenText;
    Resolution[] resolutions;
    
    int resolutionIndex;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionIndex = Screen.resolutions.Length - 1;
    }

    private void Update()
    {
        SetResolutions();
    }

    private void SetResolutions()
    {
        List<string> options = new List<string>();

        resolutionText.text = resolutions[resolutionIndex].width + " x " + resolutions[resolutionIndex].height;

        for (int i = 0; i < resolutionIndex; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
        }
    }

    public void IncreaseResValue()
    {
        resolutionIndex++;

        if (resolutionIndex == resolutions.Length)
        {
            resolutionIndex = 0;
        }
    }

    public void DecreaseResValue()
    {
        resolutionIndex--;

        if (resolutionIndex < 0)
        {
            resolutionIndex = resolutions.Length - 1;
        }
    }
}

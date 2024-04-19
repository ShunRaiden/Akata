using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GraphicManager : MonoBehaviour
{
    public TMP_Dropdown graphicDropdown;
    // Start is called before the first frame update
    void Start()
    {
        graphicDropdown.value = QualitySettings.GetQualityLevel();
    }

    public void UpdateGraphicSettings(int level)
	{
        QualitySettings.SetQualityLevel(level);
	}
}

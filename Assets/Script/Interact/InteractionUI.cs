using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionUI : MonoBehaviour
{
    public TMP_Text interactionText;
	public TMP_Text openNPCUIText;

    public void Show(string message01,string message02)
	{
		interactionText.enabled = true;
        openNPCUIText.enabled = true;

        interactionText.text = message01;
		openNPCUIText.text = message02;
	}

	public void Hide()
	{
		interactionText.enabled = false;
		openNPCUIText.enabled = false;
	}
}

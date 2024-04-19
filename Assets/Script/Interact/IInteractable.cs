using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    string GetInteractionText();
    string GetInteractionUIText();

    public void Interact();

    public void OpenNPCUI();
}

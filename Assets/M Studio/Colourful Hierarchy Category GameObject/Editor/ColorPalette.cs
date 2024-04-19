using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MStudio
{
    /// <summary>
    /// Details of custom design
    /// </summary>
    [System.Serializable]
    public class ColorDesign
    {
        [Tooltip("Rename gameObject begin with this keychar")]
        public string keyChar;
        [Tooltip("Don't forget to change alpha to 255")]
        public Color textColor;
        [Tooltip("Don't forget to change alpha to 255")]
        public Color backgroundColor;
        public TextAnchor textAlignment;
        public FontStyle fontStyle;

        public string token;
        public string newName;

        public Vector2Int textOffset = new Vector2Int(18, 0);
        public Vector2 backgroundRectOffset = new Vector2(16, 0);
    }

    /// <summary>
    /// ScriptableObject:Save list of ColorDesign
    /// </summary>
    public class ColorPalette : ScriptableObject
    {
        public List<ColorDesign> colorDesigns = new List<ColorDesign>();
    }
}
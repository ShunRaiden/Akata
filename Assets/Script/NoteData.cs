using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoteForDev
{
    [CreateAssetMenu(fileName = "Note_", menuName = "Note")]
    public class NoteData : ScriptableObject
    {
        public string noteTopic;
        [TextArea(10, 10)]
        public string noteText;
    }
}

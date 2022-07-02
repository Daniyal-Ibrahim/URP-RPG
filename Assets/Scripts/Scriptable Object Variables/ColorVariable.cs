using UnityEngine;

namespace _Scripts.Scriptable_Object_Variables
{
    [CreateAssetMenu(fileName = "New Color", menuName = "SO Variables/Color")]
    public class ColorVariable : ScriptableObject
    {
        public Color value;
    }
}

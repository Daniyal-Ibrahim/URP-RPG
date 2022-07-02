using UnityEngine;

namespace _Scripts.Scriptable_Object_Variables
{
    [CreateAssetMenu(fileName = "New int", menuName = "SO Variables/Int")]
    public class IntVariable : ScriptableObject
    {
        public int value;
    }
}

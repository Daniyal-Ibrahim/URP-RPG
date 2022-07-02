using UnityEngine;

namespace _Scripts.Scriptable_Object_Variables
{
    [CreateAssetMenu(fileName = "New Bool", menuName = "SO Variables/Bool")]
    public class BoolVariable : ScriptableObject
    {
        public bool value;
    }
}

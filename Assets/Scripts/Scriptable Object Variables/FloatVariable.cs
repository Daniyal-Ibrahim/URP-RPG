using System;
using UnityEngine;

namespace _Scripts.Scriptable_Object_Variables
{
    [CreateAssetMenu(fileName = "New Float", menuName = "SO Variables/Float")]
    public class FloatVariable : ScriptableObject
    {
        public float value;
    }

    [Serializable]
    public class FloatReference
    {
        public bool useConstant = true;
        public float constantValue;
        public FloatReference variable;

        public float Value => useConstant ? constantValue : variable.Value;
    }
}

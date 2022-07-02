using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace _Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public Vector2 destination;


        public NavMeshAgent agent;
    
        public GameObject normalGrid;
        private void Awake()
        {
            Instance = this;
        
            DontDestroyOnLoad(this.gameObject);
        }

        void FindPath()
        {
            var openSet = new List<int[,]>();
            while (openSet.Count > 0)
            {
            
            }
        }
    }
}

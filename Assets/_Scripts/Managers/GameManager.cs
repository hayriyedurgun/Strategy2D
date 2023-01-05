using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager m_Instance = null;
        public static GameManager Instance => m_Instance;

        public PlayerController Player;

        public ProductFactory ProductFactory;

        private void Awake()
        {
            m_Instance = this;
        }

        private void OnDestroy()
        {
            m_Instance = null;
        }
    }
}


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

        public InputController InputController;

        public ProductFactory ProductFactory;

        public GamePlaySettings GamePlaySettings;

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


using Assets._Scripts.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    public class GUIManager : MonoBehaviour
    {
        private static GUIManager m_Instance = null;
        public static GUIManager Instance => m_Instance;

        public WindowBehaviour WindowBehaviour;

        public RectTransform ProductPanel;

        public InfoPanel InfoPanel;

        private void Awake()
        {
            m_Instance = this;
        }

        private void Start()
        {
            WindowBehaviour.WindowResized += OnWindowResized;
        }

        private void OnDestroy()
        {
            m_Instance = null;
            WindowBehaviour.WindowResized -= OnWindowResized;
        }


        private void OnWindowResized()
        {
            //GridManager.Instance.Resize();
            Debug.Log("** Resized!!");
        }
    }
}

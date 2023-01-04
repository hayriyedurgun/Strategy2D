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

        public TileObjectPooler TilePool;

        private void Awake()
        {
            m_Instance = this;
        }

        private void OnDestroy()
        {
            m_Instance = null;
        }

        //public GameObject Canvas;

        //public Image BuildingImg;

        //public int GridX;
        //public int GridY;

        //[SerializeField]
        //private float m_BuildingCurrentSizeX;
        //[SerializeField]
        //private float m_BuildingCurrentSizeY;

        //[SerializeField]
        //private float m_DefaultPosX;
        //[SerializeField]
        //private float m_DefaultPosY;


        private void Start()
        {
            //var buildingRect = BuildingImg.GetComponent<RectTransform>();

            //m_BuildingCurrentSizeX = buildingRect.sizeDelta.x;
            //m_BuildingCurrentSizeY = buildingRect.sizeDelta.y;

            //m_DefaultPosX = buildingRect.anchoredPosition.x;
            //m_DefaultPosY = buildingRect.anchoredPosition.y;
        }
    }
}


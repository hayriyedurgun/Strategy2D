using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts
{
    public class TileBehaviour : MonoBehaviour
    {
        public int PosX;
        public int PosY;
        public float CellSize;

        public Image AvailityImage;

        public TextMeshPro Text;

        [NonSerialized]
        private bool m_IsOccupied;
        public bool IsOccupied
        {
            get => m_IsOccupied;
            set
            {
                m_IsOccupied = value;
                if (m_IsOccupied)
                {
                    Value = 1;
                }
                else
                {
                    Value = 0;
                }
            }
        }

        private int m_Value;
        public int Value
        {
            get => m_Value;
            set
            {
                m_Value = value;
                Text.text = Value.ToString();
            }
        }

        public void Init(int posX, int posY, float cellSize)
        {
            PosX = posX;
            PosY = posY;
            CellSize = cellSize;

            Text.text = Value.ToString();
        }

        public void SetStatus(bool canPlace)
        {
            AvailityImage.gameObject.SetActive(true);
            AvailityImage.color = canPlace ? new Color(0, 1, 0, .5f) : new Color(1, 0, 0, .5f);
        }

        public void ClearStatus()
        {
            AvailityImage.gameObject.SetActive(false);
        }
    }

    public enum TileStatus
    {
        None,
        Available,
        NotAvailable
    }
}

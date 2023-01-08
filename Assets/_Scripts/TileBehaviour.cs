using System;
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

        public SpriteRenderer TileRenderer;
        public Sprite OddSprite;
        public Sprite EvenSprite;

        public TextMeshPro Text;

        [NonSerialized]
        public BaseProduct Product;
        [NonSerialized]
        public int GCost;
        [NonSerialized]
        public int HCost;
        [NonSerialized]
        public TileBehaviour PreviousTile;

        public GamePlaySettings Settings => GameManager.Instance.GamePlaySettings;

        public int FCost => GCost + HCost;

        private bool m_IsAvailable;
        public bool IsAvailable
        {
            get => m_IsAvailable;
            set
            {
                m_IsAvailable = value;
                Text.SetText(m_IsAvailable ? "0" : "1");
            }
        }

        private void Update()
        {
            Text.gameObject.SetActive(Settings.IsDebugMode);
        }

        public void Init(int posX, int posY, float cellSize)
        {
            PosX = posX;
            PosY = posY;
            CellSize = cellSize;

            if ((posX % 2 == 0 && posY % 2 != 0) ||
                posX % 2 != 0 && posY % 2 == 0)
            {
                TileRenderer.sprite = EvenSprite;
            }
            else
            {
                TileRenderer.sprite = OddSprite;
            }

            IsAvailable = true;
            Text.gameObject.SetActive(Settings.IsDebugMode);
        }

        public void SetStatus(bool canPlace)
        {
            AvailityImage.gameObject.SetActive(true);
            IsAvailable = canPlace;

            AvailityImage.color = canPlace ? new Color(0, 1, 0, .5f) : new Color(1, 0, 0, .5f);
        }

        public void ClearStatus()
        {
            IsAvailable = Product == null;
            AvailityImage.gameObject.SetActive(false);
        }

        public void Reset()
        {
            GCost = 0;
            HCost = 0;
        }

        public void SetDebugStatus()
        {
            if (Settings.IsDebugMode)
            {
                AvailityImage.gameObject.SetActive(true);
                AvailityImage.color = new Color(.5f, 0, .5f, .6f);
            }
        }

        public void Clear()
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

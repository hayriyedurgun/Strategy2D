using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UI
{
    public class InfoPanel : MonoBehaviour
    {
        public TextMeshProUGUI Header;
        public Image Image;
        private ProductBehaviour m_Product;
        public RectTransform ProductionParent;

        public Image ProductionImage;
        public Button ProductionButton;
        //public TextMeshProUGUI SecsToSpawn;

        [NonSerialized]
        public bool HasProduction;

        private void Update()
        {
            if (gameObject.activeInHierarchy && HasProduction)
            {
                //ProductionButton.interactable = m_Product.SecondsToSpawn <= 0;
                //SecsToSpawn.gameObject.SetActive(m_Product.SecondsToSpawn <= 0);
                //SecsToSpawn.SetText(m_Product.SecondsToSpawn.ToString());
            }
        }

        public void SetVisibility(bool visible)
        {
            gameObject.SetActive(visible);
        }

        public void Init(ProductBehaviour product)
        {
            m_Product = product;

            var info = GameManager.Instance.ProductFactory.GetProductInfo(m_Product.Type);
            Header.SetText(info.Name);
            Image.sprite = info.Sprite;

            HasProduction = info.HasProduction;
            if (HasProduction)
            {
                ProductionParent.gameObject.SetActive(true);
                ProductionImage.sprite = info.ProductionSprite;
            }
            else
            {
                ProductionParent.gameObject.SetActive(false);
            }
        }

        public void OnClosePressed()
        {
            SetVisibility(false);
        }

        public void OnProductionPressed()
        {
            m_Product.TryCreateProduction();
        }
    }
}

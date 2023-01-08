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
        private BaseBuilding m_Product;
        public RectTransform ProductionParent;

        public Image ProductionImage;
        public Button ProductionButton;

        public bool HasProduction => m_Product is BarrackBuilding;

        private void Update()
        {
            if (gameObject.activeInHierarchy && 
                HasProduction && 
                m_Product is BarrackBuilding barrackProduct)
            {
                ProductionButton.interactable = barrackProduct.SecondsToSpawn <= 0;
            }
        }

        public void SetVisibility(bool visible)
        {
            gameObject.SetActive(visible);
        }

        public void Init(BaseBuilding product)
        {
            m_Product = product;

            var info = GameManager.Instance.ProductFactory.GetProductInfo(m_Product.Type);
            Header.SetText(info.Name);
            Image.sprite = info.Sprite;

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
            if (m_Product is BarrackBuilding product)
            {
                product.CreateProduction();
            }
        }
    }
}

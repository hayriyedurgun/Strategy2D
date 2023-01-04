﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts
{
    public class PlayerController : MonoBehaviour
    {
        private ProductBehaviour m_Product;

        public ProductBehaviour BarrackPrefab;
        public ProductBehaviour PowerPlantPrefab;
        public ProductBehaviour SoldierUnitPrefab;

        private void Update()
        {
            if (m_Product == null) return;

            if (m_Product)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    if (!m_Product.TryPlace())
                    {
                        Destroy(m_Product.gameObject);
                    }

                    m_Product.ClearTiles();
                    m_Product = null;

                    return;
                }

                m_Product.SendRay();

                var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldPos.z = 0;

                m_Product.transform.position = worldPos;
            }
        }

        public void SetProduct(ProductType productType)
        {
            StartCoroutine(SetProductEndOfFrame(productType));
        }

        private IEnumerator SetProductEndOfFrame(ProductType productType)
        {
            yield return new WaitForEndOfFrame();

            if (productType == ProductType.Barrack)
            {
                m_Product = Instantiate(BarrackPrefab);
            }
            else if (productType == ProductType.PowerPlant)
            {
                m_Product = Instantiate(PowerPlantPrefab);
            }
            else if (productType == ProductType.SoldierUnit)
            {
                m_Product = Instantiate(SoldierUnitPrefab);
            }

            m_Product.Init();

            var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0;
            m_Product.transform.position = worldPos;
        }

    }
}

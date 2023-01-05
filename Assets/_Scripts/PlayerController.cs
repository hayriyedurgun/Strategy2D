using Assets._Scripts.Managers;
using System;
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

        private void Update()
        {
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
            else
            {
                RaycastHit2D hit;
                if (Input.GetMouseButtonDown(0))
                {
                    hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                    if (hit.collider)
                    {
                        var tile = GridManager.Instance.ConvertToTile(hit.point);
                        if (tile && tile.Product != null)
                        {
                            GUIManager.Instance.InfoPanel.Init(tile.Product.Type);
                            GUIManager.Instance.InfoPanel.SetVisibility(true);  
                        }
                    }
                }
            }

        }

        public void SetProduct(ProductType productType)
        {
            StartCoroutine(SetProductEndOfFrame(productType));
        }

        private IEnumerator SetProductEndOfFrame(ProductType productType)
        {
            yield return new WaitForEndOfFrame();

            var prefab = GameManager.Instance.ProductFactory.GetProduct(productType);
            m_Product = Instantiate(prefab);
            m_Product.Init();
            m_Product.Type = productType;

            var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0;
            m_Product.transform.position = worldPos;
        }

    }
}


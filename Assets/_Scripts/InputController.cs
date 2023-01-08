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
    public class InputController : MonoBehaviour
    {
        private BaseProduct m_ProductToBeCreate;
        private TileBehaviour m_SelectedTile;

        private void Update()
        {
            Debug.Log(Input.mouseScrollDelta);

            if (m_ProductToBeCreate)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    if (!m_ProductToBeCreate.TryPlace())
                    {
                        Destroy(m_ProductToBeCreate.gameObject);
                    }

                    m_ProductToBeCreate.ClearTiles();
                    m_ProductToBeCreate = null;

                    return;
                }

                if (Input.GetMouseButtonUp(1))
                {
                    Destroy(m_ProductToBeCreate.gameObject);
                    m_ProductToBeCreate.ClearTiles();
                    m_ProductToBeCreate = null;

                    return;
                }

                m_ProductToBeCreate.SendRay();

                var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldPos.z = 0;

                m_ProductToBeCreate.transform.position = worldPos;

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
                            m_SelectedTile = tile;
                            GUIManager.Instance.InfoPanel.Init(tile.Product);
                            GUIManager.Instance.InfoPanel.SetVisibility(true);
                        }
                    }
                }

                if (Input.GetMouseButtonDown(1) && m_SelectedTile != null)
                {
                    GridManager.Instance.ClearTiles();

                    var endTile = GridManager.Instance.ConvertToTile(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    var path = GridManager.Instance.FindPath(m_SelectedTile, endTile);

                    if (m_SelectedTile.Product is SoldierProduct product &&
                        path != null &&
                        path.Any())
                    {
                        m_SelectedTile.Product = null;
                        m_SelectedTile.ClearStatus();
                        product.SetTarget(path);
                        path.ForEach(x => x.SetDebugStatus());
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

            var info = GameManager.Instance.ProductFactory.GetProductInfo(productType);
            m_ProductToBeCreate = Instantiate(info.ProductPrefab);
            m_ProductToBeCreate.Initialize(info);

            var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0;
            m_ProductToBeCreate.transform.position = worldPos;
        }
    }
}


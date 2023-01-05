using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts
{
    public class ProductBehaviour : MonoBehaviour
    {
        private List<TileBehaviour> m_ColoredTiles = new List<TileBehaviour>();
        private List<SpriteRenderer> m_Renderers;
        private bool m_HasProduction;
        private ProductType m_ProductionType;
        private bool m_CanPlaceable;

        private float m_Time;

        public ProductType Type;
        public float SpawnCooldown = 2;
        public Transform[] RayTransforms;
        public Transform SpawnTransform;

        //public int SecondsToSpawn => Mathf.CeilToInt(m_Time);

        private void Update()
        {
            if (m_HasProduction && m_Time < SpawnCooldown)
            {
                m_Time += Time.deltaTime;
            }
        }

        public void Init(ProductInfo info)
        {
            m_Renderers = GetComponentsInChildren<SpriteRenderer>().ToList();

            if (m_Renderers.Any())
            {
                var color = m_Renderers.FirstOrDefault().color;
                color.a = .5f;
                m_Renderers.ForEach(x => x.color = color);
            }

            Type = info.Type;
            m_HasProduction = info.HasProduction;
            if (m_HasProduction)
            {
                m_ProductionType = info.ProductionType;
            }

            m_Time = SpawnCooldown;
        }

        public void SendRay()
        {
            ClearTiles();

            RaycastHit2D hit;
            foreach (var ray in RayTransforms)
            {
                hit = Physics2D.Raycast(ray.transform.position, Vector2.zero);
                if (hit.collider)
                {
                    var tile = GridManager.Instance.ConvertToTile(hit.point);
                    if (tile)
                    {
                        m_ColoredTiles.Add(tile);
                    }
                }
            }

            if (m_ColoredTiles.Count == RayTransforms.Length)
            {
                m_CanPlaceable = m_ColoredTiles.All(x => x.Product == null);
                m_ColoredTiles.ForEach(x => x.SetStatus(m_CanPlaceable));
            }
            else
            {
                m_CanPlaceable = false;
            }
        }

        public bool TryPlace()
        {
            if (!m_CanPlaceable)
            {
                return false;
            }

            m_ColoredTiles.ForEach(x => x.Product = this);

            var x = (float)m_ColoredTiles.Average(x => x.transform.position.x);
            var y = (float)m_ColoredTiles.Average(x => x.transform.position.y);

            transform.position = new Vector3(x, y, 0);

            if (m_Renderers.Any())
            {
                var color = m_Renderers.FirstOrDefault().color;
                color.a = 1f;
                m_Renderers.ForEach(x => x.color = color);
            }

            return true;
        }

        public void ClearTiles()
        {
            m_ColoredTiles.ForEach(x =>
            {
                if (x)
                {
                    x.ClearStatus();
                }
            });
            m_ColoredTiles.Clear();
        }

        public bool TryCreateProduction()
        {
            if (m_HasProduction && m_Time >= SpawnCooldown)
            {
                m_Time = 0;
                Spawn();
                return true;
            }

            return false;
        }

        private void Spawn()
        {
            var tile = GridManager.Instance.ConvertToTile(SpawnTransform.position);

            var prefab = GameManager.Instance.ProductFactory.GetProduct(m_ProductionType);
            var production = Instantiate(prefab);

            tile.Product = production;
            production.transform.position = GridManager.Instance.ConvertToWorld(tile.PosX, tile.PosY);
        }
    }
}

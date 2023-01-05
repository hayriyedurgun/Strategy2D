using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts
{
    public class BarrackProduct : BaseProduct
    {
        private ProductType m_ProductionType;
        private float m_CurrentTime;
        private float m_CoolDown;
        private BaseProduct m_ProductPrefab;

        public Transform SpawnTransform;

        public int SecondsToSpawn => Mathf.CeilToInt(m_CurrentTime);

        public override void Initialize(ProductInfo info)
        {
            base.Initialize(info);

            m_ProductionType = info.ProductionType;
            m_CoolDown = info.Cooldown;
            m_ProductPrefab = info.ProductPrefab;

            //TODO: COOLDOWN LOGICS
            m_CurrentTime = m_CoolDown;
        }

        public override void Update()
        {
            base.Update();
            if (m_CurrentTime < m_CoolDown)
            {
                m_CurrentTime += Time.deltaTime;
            }
        }

        public void CreateProduction()
        {
            Spawn();
        }

        private void Spawn()
        {
            var tile = GridManager.Instance.ConvertToTile(SpawnTransform.position);

            if (!tile.IsAvailable)
            {
                tile = GridManager.Instance.GetNextAvailableNeigbour(tile);

                if (tile == null) return;
            }

            var production = Instantiate(m_ProductPrefab);
            tile.Product = production;

            production.transform.position = GridManager.Instance.ConvertToWorld(tile.PosX, tile.PosY);
            m_CurrentTime = 0;
        }
    }
}

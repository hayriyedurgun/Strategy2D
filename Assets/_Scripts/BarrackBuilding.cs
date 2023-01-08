using Assets._Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts
{
    public class BarrackBuilding : BaseBuilding
    {
        private float m_CurrentTime;
        private float m_CoolDown;
        private SoldierBuilding m_Unit;

        public Transform SpawnTransform;

        public int SecondsToSpawn => Mathf.CeilToInt(m_CurrentTime);

        public override void Initialize(Building model)
        {
            base.Initialize(model);

            var unitModel = GameManager.Instance.Units.FirstOrDefault(x=> x.Id == model.UnitId);

            m_Unit = new SoldierBuilding();
            m_Unit.Initialize(unitModel);

            m_CurrentTime = 0;
        }

        public override void Update()
        {
            base.Update();
            if (m_CurrentTime > 0)
            {
                m_CurrentTime -= Time.deltaTime;
                if (m_CurrentTime < 0)
                {
                    m_CurrentTime = 0;
                }
            }
        }

        public void CreateProduction()
        {
            Spawn();
        }

        private void Spawn()
        {
            var tile = GridManager.Instance.ConvertToTile(SpawnTransform.position);

            if (!tile.IsAvailable || !tile.TileRenderer.isVisible)
            {
                tile = GridManager.Instance.GetNextAvailableNeigbour(tile);

                if (tile == null) return;
            }

            var production = Instantiate(m_ProductionPrefab);
            tile.Product = production;
            production.transform.position = GridManager.Instance.ConvertToWorld(tile.PosX, tile.PosY);
            tile.ClearStatus();

            m_CurrentTime = m_CoolDown;
        }
    }
}

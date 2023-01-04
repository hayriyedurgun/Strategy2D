﻿using System;
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
        private bool m_CanPlaceable;

        public SpriteRenderer Renderer;
        public int Width;
        public int Height;
        public Transform[] RayTransforms;

        public Vector3 Bounds { get; private set; }

        public void Init()
        {
            var color = Renderer.color;
            color.a = .5f;
            Renderer.color = color;

            Bounds = new Vector3(Width, Height, 0) * GridManager.Instance.CellSize;
            transform.localScale = new Vector3(Width, Height, 1);
        }

        public void Place()
        {
            var color = Renderer.color;
            color.a = 1f;
            Renderer.color = color;
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
                    m_ColoredTiles.Add(GridManager.Instance.ConvertToTile(hit.point));
                }
            }

            if (m_ColoredTiles.Count == RayTransforms.Length)
            {
                m_CanPlaceable = m_ColoredTiles.All(x => !x.IsOccupied);
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

            m_ColoredTiles.ForEach(x => x.IsOccupied = true);

            var x =  (float)m_ColoredTiles.Average(x=> x.PosX);
            var y = (float)m_ColoredTiles.Average(x=> x.PosY);

            transform.position = new Vector3(x, y, 0);
            return true;
        }

        public void ClearTiles()
        {
            m_ColoredTiles.ForEach(x => x.ClearStatus());
            m_ColoredTiles.Clear();
        }
    }
}
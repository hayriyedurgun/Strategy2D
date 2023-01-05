using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts
{
    public class BaseProduct : MonoBehaviour
    {
        private List<SpriteRenderer> m_Renderers;
        private List<TileBehaviour> m_ColoredTiles = new List<TileBehaviour>();

        public ProductType Type;
        public Transform[] RayTransforms;

        public virtual void Awake()
        {
            m_Renderers = GetComponentsInChildren<SpriteRenderer>().ToList();
        }

        public virtual void Start()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Initialize(ProductInfo info)
        {
            Type = info.Type;
        }

        public void SetAlpha(float alpha)
        {
            if (m_Renderers.Any())
            {
                var color = m_Renderers.FirstOrDefault().color;
                color.a = alpha;
                m_Renderers.ForEach(x => x.color = color);
            }
        }

        public void SendRay()
        {
            ClearTiles();

            RaycastHit2D hit;
            foreach (var ray in RayTransforms)
            {
                //hit = Physics2D.Raycast(ray.transform.position, Vector2.zero);

                hit = Physics2D.Raycast(ray.transform.position, Vector2.zero, 10f, LayerHelper.Or(Layer.Tile));
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
                var isAvailable = m_ColoredTiles.All(x => x.Product == null);
                m_ColoredTiles.ForEach(x => x.SetStatus(isAvailable));
            }
        }

        public bool TryPlace()
        {
            if (m_ColoredTiles.Any(x=> !x.IsAvailable))
            {
                return false;
            }

            m_ColoredTiles.ForEach(x => x.Product = this);

            var x = (float)m_ColoredTiles.Average(x => x.transform.position.x);
            var y = (float)m_ColoredTiles.Average(x => x.transform.position.y);

            transform.position = new Vector3(x, y, 0);

            SetAlpha(1);
            return true;
        }

        public void ClearTiles()
        {
            m_ColoredTiles.ForEach(x => x.ClearStatus());
            m_ColoredTiles.Clear();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets._Scripts.UI
{
    [RequireComponent(typeof(ScrollController))]
    public class InfinityScroll : ScrollRect
    {
        //public List<ProductItem> ItemPrefabs;
        public ProductItemPool Pool;

        private float m_ScrollRatio;
        private float m_UpperLimit;
        private float m_LowerLimit;
        private float m_ViewPortHeight;
        private float m_ItemHeight;
        private List<ProductItem> m_Items = new List<ProductItem>();
        private bool m_IsHeightCalculated;
        private bool m_DraggingNow;

        private void Update()
        {
            if (!m_IsHeightCalculated && content.rect.height != 0)
            {
                m_ScrollRatio = m_ItemHeight / (content.rect.height - m_ViewPortHeight);

                m_UpperLimit = 1 - 3 * m_ScrollRatio;
                m_LowerLimit = 3 * m_ScrollRatio;

                verticalNormalizedPosition = m_UpperLimit;
                m_IsHeightCalculated = true;
            }


            if (!m_IsHeightCalculated) return;

            if (verticalNormalizedPosition > m_UpperLimit)
            {
                var floatVal = (verticalNormalizedPosition - m_UpperLimit) / m_ScrollRatio;
                if (floatVal > GameManager.Instance.GamePlaySettings.Threshold)
                {
                    var count = Mathf.CeilToInt(floatVal) * 2;

                    ProductItem item;
                    for (int i = 0; i < count; i++)
                    {
                        item = Pool.GetObject();
                        item.gameObject.SetActive(true);

                        item.transform.SetAsFirstSibling();
                        m_Items.Insert(0, item);
                    }

                    for (int i = 0; i < count; i++)
                    {
                        item = m_Items[m_Items.Count - 1];
                        m_Items.Remove(item);
                        Pool.ReleaseObject(item);
                    }

                    CustomSetVerticalNormalizedPosition(verticalNormalizedPosition - m_ScrollRatio * (count / 2));
                }
            }

            if (verticalNormalizedPosition < m_LowerLimit)
            {
                var floatVal = (m_LowerLimit - verticalNormalizedPosition) / m_ScrollRatio;
                if (floatVal > GameManager.Instance.GamePlaySettings.Threshold)
                {
                    var count = Mathf.CeilToInt(floatVal) * 2;

                    ProductItem item;
                    for (int i = 0; i < count; i++)
                    {
                        item = Pool.GetObject();
                        item.transform.SetAsLastSibling();
                        m_Items.Add(item);
                    }

                    for (int i = 0; i < count; i++)
                    {
                        item = m_Items.FirstOrDefault();
                        Pool.ReleaseObject(item);
                        m_Items.Remove(item);
                    }

                    CustomSetVerticalNormalizedPosition(verticalNormalizedPosition + m_ScrollRatio * (count / 2));
                }
            }
        }

        public void InitAwake()
        {
            Pool.Init(50);

            CreateItem(6);

            m_ViewPortHeight = viewport.GetComponent<RectTransform>().rect.height;

            var gridLayout = content.GetComponent<GridLayoutGroup>();
            m_ItemHeight = gridLayout.cellSize.y + gridLayout.spacing.y;
        }

        public void InitStart()
        {
            var height = viewport.rect.height;

            //since each row contains 2 items, width is 2.
            //var totalItemCount = Mathf.FloorToInt((height / m_ItemHeight) * CountIncrementer) * 2;

            CreateItem(18);
        }

        public void CustomSetVerticalNormalizedPosition(float value)
        {
            if (m_DraggingNow)
            {
                float anchoredYBeforeSet = content.anchoredPosition.y;
                SetNormalizedPosition(value, 1);
                m_ContentStartPosition += new Vector2(0f, content.anchoredPosition.y - anchoredYBeforeSet);
            }
            else
            {
                SetNormalizedPosition(value, 1);
            }
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            m_DraggingNow = true;
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            m_DraggingNow = false;
        }

        private void CreateItem(int count)
        {
            ProductItem item;
            for (int i = 0; i < count; i++)
            {
                item = Pool.GetObject();
                item.gameObject.SetActive(true);
                m_Items.Add(item);
            }
        }
    }
}

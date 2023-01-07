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
        public List<ProductItem> ItemPrefab;

        public float CountIncrementer = 1f;
        private float m_ScrollRatio;
        private float m_UpperLimit;
        private float m_LowerLimit;
        private float m_ViewPortHeight;
        private float m_ItemHeight;
        private List<ProductItem> m_Items = new List<ProductItem>();
        private bool m_Calculated;
        private int m_Counter;
        private bool m_Dragging;

        private void Update()
        {
            if (!m_Calculated && content.rect.height != 0)
            {
                m_ScrollRatio = m_ItemHeight / (content.rect.height - m_ViewPortHeight);

                m_UpperLimit = 1 - 3 * m_ScrollRatio;
                m_LowerLimit = 3 * m_ScrollRatio;

                verticalNormalizedPosition = m_UpperLimit;
                m_Calculated = true;
            }


            if (!m_Calculated) return;

            if (verticalNormalizedPosition > m_UpperLimit)
            {
                var floatVal = (verticalNormalizedPosition - m_UpperLimit) / m_ScrollRatio;
                if (floatVal > GameManager.Instance.GamePlaySettings.Threshold)
                {
                    var count = Mathf.CeilToInt(floatVal) * 2;
                    Debug.Log($"** count: {count}, norm: {verticalNormalizedPosition}, Next norm: {verticalNormalizedPosition - m_ScrollRatio * count}");

                    ProductItem item;
                    for (int i = 0; i < count; i++)
                    {
                        m_Counter++;

                        item = Instantiate(ItemPrefab[m_Counter % ItemPrefab.Count], content.transform);
                        item.name = m_Counter.ToString();
                        item.transform.SetAsFirstSibling();
                        m_Items.Insert(0, item);
                    }

                    for (int i = 0; i < count; i++)
                    {
                        item = m_Items[m_Items.Count - 1];
                        m_Items.Remove(item);
                        Destroy(item.gameObject);
                    }

                    CustomSetVerticalNormalizedPosition(verticalNormalizedPosition - m_ScrollRatio * (count / 2));
                    //verticalNormalizedPosition -= m_ScrollRatio * (count / 2);

                }
            }

            if (verticalNormalizedPosition < m_LowerLimit)
            {
                var floatVal = (m_LowerLimit - verticalNormalizedPosition) / m_ScrollRatio;
                if (floatVal > GameManager.Instance.GamePlaySettings.Threshold)
                {
                    var count = Mathf.CeilToInt(floatVal) * 2;
                    Debug.Log($"** count: {count}, norm: {verticalNormalizedPosition}, Next norm: {verticalNormalizedPosition + m_ScrollRatio * count}");

                    ProductItem item;
                    for (int i = 0; i < count; i++)
                    {
                        m_Counter++;

                        item = Instantiate(ItemPrefab[m_Counter % ItemPrefab.Count], content.transform);
                        item.name = m_Counter.ToString();
                        item.transform.SetAsLastSibling();
                        m_Items.Add(item);
                    }

                    for (int i = 0; i < count; i++)
                    {
                        item = m_Items.FirstOrDefault();
                        if (item)
                        {
                            m_Items.Remove(item);
                            Destroy(item.gameObject);
                        }
                    }
                    CustomSetVerticalNormalizedPosition(verticalNormalizedPosition + m_ScrollRatio * (count / 2));
                }
            }
        }

        private void CreateItem(int count)
        {
            ProductItem item;
            //Item item;
            for (int i = 0; i < count; i++)
            {
                m_Counter++;

                item = Instantiate(ItemPrefab[m_Counter % ItemPrefab.Count], content.transform);
                item.name = m_Counter.ToString();
                m_Items.Add(item);
            }
        }

        //public override void OnDrag(PointerEventData eventData)
        //{
        //    if (eventData.button != PointerEventData.InputButton.Left)
        //        return;

        //    if (!IsActive())
        //        return;

        //    Vector2 localCursor;
        //    if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(viewRect, eventData.position, eventData.pressEventCamera, out localCursor))
        //        return;

        //    m_ContentStartPosition = content.anchoredPosition;

        //    base.OnDrag(eventData);
        //}

        public void InitAwake()
        {
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
            if (m_Dragging)
            {
                float anchoredYBeforeSet = content.anchoredPosition.y;
                SetNormalizedPosition(value, 1);
                m_ContentStartPosition += new Vector2(0f, content.anchoredPosition.y - anchoredYBeforeSet);
            }
            else
                SetNormalizedPosition(value, 1);
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            m_Dragging = true;
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            m_Dragging = false;
        }
    }
}

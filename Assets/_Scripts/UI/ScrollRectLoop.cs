using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectLoop : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform content;
    public GameObject itemPrefab;
    public int poolSize;
    public float scrollSensitivity;

    private Queue<GameObject> itemPool;
    private List<GameObject> activeItems;

    void Start()
    {
        // Initialize the item pool with the specified pool size
        itemPool = new Queue<GameObject>(poolSize);
        for (int i = 0; i < poolSize; i++)
        {
            GameObject item = Instantiate(itemPrefab, content);
            item.name = i.ToString();
            item.SetActive(true);
            itemPool.Enqueue(item);
        }

        // Initialize the list of active items
        activeItems = itemPool.ToList();
    }

    void Update()
    {
        // Check if the content has moved past the top or bottom edge of the viewport
        if (scrollRect.normalizedPosition.y > 1 || scrollRect.normalizedPosition.y < 0)
        {
            // Calculate the current scroll position in terms of the number of items
            float scrollPos = scrollRect.normalizedPosition.y * (content.childCount - poolSize);

            // Calculate the index of the topmost item in the viewport
            int firstItemIndex = Mathf.FloorToInt(scrollPos);

            // Deactivate the bottommost item and add it to the pool
            GameObject lastItem = activeItems[activeItems.Count - 1];
            lastItem.SetActive(false);
            activeItems.RemoveAt(activeItems.Count - 1);
            itemPool.Enqueue(lastItem);

            // Activate the topmost item and add it to the list of active items
            GameObject firstItem = itemPool.Dequeue();
            firstItem.SetActive(true);
            activeItems.Insert(0, firstItem);

            // Calculate the position of the topmost item in the viewport
            float firstItemPos = -itemPrefab.GetComponent<RectTransform>().sizeDelta.y * firstItemIndex;

            // Reposition the items
            for (int i = 0; i < activeItems.Count; i++)
            {
                RectTransform itemRect = activeItems[i].GetComponent<RectTransform>();
                itemRect.anchoredPosition = new Vector2(0, firstItemPos + i * itemPrefab.GetComponent<RectTransform>().sizeDelta.y);
            }
        }
    }

    public void OnScroll(Vector2 scrollDelta)
    {
        Debug.Log(scrollDelta.y); 

        // Check if the scroll delta exceeds the specified sensitivity
        if (Mathf.Abs(scrollDelta.y) > scrollSensitivity)
        {
            // Calculate the current scroll position in terms of the number of items
            float scrollPos = scrollRect.normalizedPosition.y * (content.childCount - poolSize);

            // Calculate the index of the topmost item in the viewport
            int firstItemIndex = Mathf.FloorToInt(scrollPos);
            // Calculate the position of the topmost item in the viewport
            float firstItemPos = -itemPrefab.GetComponent<RectTransform>().sizeDelta.y * firstItemIndex;

            // Check if the content has been scrolled up or down
            if (scrollDelta.y > 0)
            {
                // Scrolling up

                // Check if the topmost item is no longer in the viewport
                if (firstItemPos > scrollRect.viewport.rect.yMax)
                {
                    // Deactivate the bottommost item and add it to the pool
                    GameObject lastItem = activeItems[activeItems.Count - 1];
                    lastItem.SetActive(false);
                    activeItems.RemoveAt(activeItems.Count - 1);
                    itemPool.Enqueue(lastItem);

                    // Activate the topmost item and add it to the list of active items
                    GameObject firstItem = itemPool.Dequeue();
                    firstItem.SetActive(true);
                    activeItems.Insert(0, firstItem);

                    // Recalculate the position of the topmost item in the viewport
                    firstItemPos = -itemPrefab.GetComponent<RectTransform>().sizeDelta.y * firstItemIndex;
                }
            }
            else if (scrollDelta.y < 0)
            {
                // Scrolling down

                // Check if the bottommost item is no longer in the viewport
                if (firstItemPos < scrollRect.viewport.rect.yMin)
                {
                    // Deactivate the topmost item and add it to the pool
                    GameObject firstItem = activeItems[0];
                    firstItem.SetActive(false);
                    activeItems.RemoveAt(0);
                    itemPool.Enqueue(firstItem);

                    // Activate the bottommost item and add it to the list of active items
                    GameObject lastItem = itemPool.Dequeue();
                    lastItem.SetActive(true);
                    activeItems.Add(lastItem);

                    // Recalculate the position of the topmost item in the viewport
                    firstItemPos = -itemPrefab.GetComponent<RectTransform>().sizeDelta.y * firstItemIndex;
                }
            }
        }
    }

}

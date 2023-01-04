using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts
{
    public class TileOverlay : MonoBehaviour
    {
        //public Image Image;

        //private TileStatus m_TileStatus;
        //public TileStatus Status
        //{
        //    get => m_TileStatus;
        //    set
        //    {
        //        m_TileStatus = value;
        //        if (m_TileStatus == TileStatus.None)
        //        {
        //            //Image.gameObject.SetActive(false);
        //            Image.gameObject.SetActive(true);
        //            Image.color = new Color(.5f, .5f, 0, .5f);

        //        }
        //        else if (m_TileStatus == TileStatus.Available)
        //        {
        //            Image.gameObject.SetActive(true);
        //            Image.color = new Color(0, 1, 0, .5f);
        //        }
        //        else if (m_TileStatus == TileStatus.NotAvailable)
        //        {
        //            Image.gameObject.SetActive(true);
        //            Image.color = new Color(1, 0, 0, .5f);
        //        }
        //    }
        //}

        //private void Update()
        //{
        //    var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        //    if (hit.collider)
        //    {
        //        var tile = GridManager.Instance.ConvertToTile(hit.point);
        //        Status = tile.IsOccupied ? TileStatus.NotAvailable : TileStatus.Available;
        //    }
        //    else
        //    {
        //        Status = TileStatus.None;
        //    }

        //}

    }


    //public enum TileStatus
    //{
    //    None,
    //    Available,
    //    NotAvailable
    //}
}

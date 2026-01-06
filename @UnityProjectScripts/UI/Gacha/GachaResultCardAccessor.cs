using BA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class GachaResultCardAccessor : MonoBehaviour
{
    public Sprite frontSprite;

    public Image portrait;
    public Image[] starImage;

    public void InitResultCard(Sprite img, int starNum)
    {
        InitPortrait(img);
        ResultStar(starNum);
    }

    public void ResultStar(int starNum)
    {
        for(int i = 0; i < starImage.Length; ++i)
            starImage[i].gameObject.SetActive(i < starNum);
    }

    public void InitPortrait(Sprite img)
    {
        portrait.sprite = img;
    }
}
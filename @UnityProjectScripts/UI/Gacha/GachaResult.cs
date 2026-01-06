using BA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class GachaResult : MonoBehaviour
{
    public Transform resultPlaceholder;
    public Sprite[] frontSprites;
    public GameObject cardPrefab;

    public GachaResultCardAccessor[] cardAccessors;

    [System.Serializable]
    public class PortWrapper
    {
        public int id;
        public Sprite port;
    }

    [SerializeField]
    public List<PortWrapper> portraits;

    public void PrintGachaResult(GachaManager.GachaResultInfo[] infos)
    {
        foreach (var i in GameResource.StudentPortraitSprites)
        {
            var p = new PortWrapper();
            p.id = i.Key;
            p.port = i.Value;
            portraits.Add(p);
        }

        gameObject.SetActive(true);
        for (int i = 0; i < infos.Length; ++i)
        {
            int starNum;
            Sprite img;

            switch (infos[i].rarity)
            {
                case GachaManager.Rarity.s1: starNum = 1; break;
                case GachaManager.Rarity.s2: starNum = 2; break;
                default: starNum = 3; break;
            }
                img = GameResource.StudentPortraitSprites[infos[i].id];

            cardAccessors[i].InitResultCard(img, starNum);
        }
    }
}
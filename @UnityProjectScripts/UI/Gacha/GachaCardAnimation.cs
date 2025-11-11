using System.Collections;
using UniRx.Operators;
using UnityEngine;
using UnityEngine.UI;
public class GachaCardAnimation : MonoBehaviour
{
    public GameObject cardsResultArea;
    public GachaResultTrain grt;
    public Transform[] cards;

    public float waitTime;
    public float waitToResult;

    public Sprite[] cardSprite;

    GachaManager.GachaResultInfo[] infos;

    void Start()
    {
        StopAllCoroutines();
        for (int i = 0; i < cards.Length; ++i)
        {
            cards[i].gameObject.SetActive(true);
            cards[i].GetComponentInChildren<Image>().gameObject.SetActive(false);
        }
    }

    public void SetCards(params GachaManager.GachaResultInfo[] infos)
    {
        this.infos = infos;
        cardsResultArea.SetActive(false);

        for (int i = 0; i < infos.Length; ++i)
        {
            var img = cards[i].gameObject.GetComponentInChildren<Image>();
            var ani = cards[i].GetComponentInChildren<UnityEngine.Animation>();

            switch (infos[i].rarity)
            {
                case GachaManager.Rarity.s1:
                    img.sprite = cardSprite[0];
                    ani.clip = ani.GetClip("Ani_CardUpperR1");
                    break;
                case GachaManager.Rarity.s2:
                    img.sprite = cardSprite[1];
                    ani.clip = ani.GetClip("Ani_CardUpperR2");
                    break;
                default:
                    img.sprite = cardSprite[2];
                    ani.clip = ani.GetClip("Ani_CardUpperR3");
                    break;
            }
            cards[i].gameObject.SetActive(true);
        }

        for (int i = infos.Length; i < cards.Length; ++i)
            cards[i].gameObject.SetActive(false);
    }

    public void PlayAnimation()
    {
        cardsResultArea.SetActive(true);
        StartCoroutine(CardAnimationCoroutine());
    }

    public void SkipAnimation()
    {
        cardsResultArea.SetActive(true);
        StopAllCoroutines();

        CardEnd();
    }

    void CardEnd()
    {
        StartCoroutine(DelayedStartResult());
    }

    IEnumerator DelayedStartResult()
    {
        for (int i = 0; i < infos.Length; ++i)
            cards[i].gameObject.SetActive(true);

        yield return new WaitForSeconds(waitToResult);

        cardsResultArea.SetActive(false);
        grt.gameObject.SetActive(true);
        grt.StartResult(infos);
    }

    IEnumerator CardAnimationCoroutine()
    {
        for (int i = 0; i < infos.Length; ++i)
        {
            yield return new WaitForSeconds(waitTime);
            var ani = cards[i].GetComponentInChildren<UnityEngine.Animation>();
            ani.Play();
        }
    }
}
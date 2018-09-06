using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyTextManager : MonoBehaviour
{
    [System.Serializable]
    public class Elements
    {
        public BuyElement seller;
        public TextMeshPro sellerText;
    }

    [SerializeField] Elements[] elements;
    [SerializeField] string[] differentAnswers;

    void Start()
    {
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].sellerText.enabled = false;
            elements[i].seller.OnRange.AddListener(OnRange);
            elements[i].seller.OnQuit.AddListener(OnQuit);
            elements[i].seller.OnInteract.AddListener(OnInteract);
            
        }
    }

    void OnRange()
    {
        for (int i = 0; i < elements.Length; i++)
        {
            if (elements[i].seller.IsOnRange())
                elements[i].sellerText.enabled = true;
        }
    }

    void OnQuit()
    {
        for (int i = 0; i < elements.Length; i++)
        {
            if (elements[i].sellerText.enabled)
                elements[i].sellerText.enabled = false;
        }
    }

    void OnInteract()
    {
        for (int i = 0; i < elements.Length; i++)
        {
            if (elements[i].seller.IsOnRange())
            {
                int newMessageIndex = Random.Range(0, differentAnswers.Length);
                elements[i].sellerText.text = differentAnswers[newMessageIndex];
            }
        }
    }
}

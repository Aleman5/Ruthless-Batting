using UnityEngine;
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
    //[SerializeField] string notEnoughMoneyText;

    //int indexOfText;
    //string savedText = "";

    void Start()
    {
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].sellerText.enabled = false;
            elements[i].seller.OnRange.AddListener(OnRange);
            elements[i].seller.OnQuit.AddListener(OnQuit);
            //elements[i].seller.OnInteract.AddListener(OnInteract);
            
            //elements[i].seller.OnNotEnoughMoney.AddListener(OnNotEnoughMoney);

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
                if(IsInvoking("NameBackToNormal"))
                    CancelInvoke("NameBackToNormal");

                int newMessageIndex = Random.Range(0, differentAnswers.Length);
                elements[i].sellerText.text = differentAnswers[newMessageIndex];
            }
        }
    }

    /*void OnNotEnoughMoney()
    {
        for (int i = 0; i < elements.Length; i++)
        {
            if (elements[i].seller.IsOnRange())
            {
                indexOfText = i;
                savedText = elements[i].sellerText.text;
                elements[i].sellerText.text = notEnoughMoneyText;
                Invoke("NameBackToNormal", 0.5f);
            }
        }
    }

    void NameBackToNormal()
    {
        elements[indexOfText].sellerText.text = savedText;
    }*/
}

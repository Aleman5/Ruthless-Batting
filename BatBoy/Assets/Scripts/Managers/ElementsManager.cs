using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsManager : MonoBehaviour
{
    [System.Serializable]
    public class Element
    {
        public string name;
    }

    [SerializeField] Element[] elements;

    

    void Start()
    {

    }

    void ActivateElement(int indexOfTheElement, int levelOfTheElement)
    {
        switch(indexOfTheElement)
        {
            case 0:
                if(levelOfTheElement == 1)
                {
                    GameObject go = GameObject.Find(elements[indexOfTheElement].name);

                    if (go)
                    {
                        if (!go.activeSelf)
                        {
                            go.SetActive(true);
                        }
                        
                    }
                    else
                        Debug.Log("Object: '" + elements[indexOfTheElement].name + "' wasn't found.");
                }
                break;
        }
    }
}

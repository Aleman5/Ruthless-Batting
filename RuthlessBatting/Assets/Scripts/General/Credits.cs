using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] Animator creditsAnimator;

    void Update()
    {
        if (InputManager.Instance.GetActionButton() && creditsAnimator.GetCurrentAnimatorStateInfo(0).IsName("Credits"))
        {
            SceneLoaderManager.Instance.ReturnMenu();
        }
    }
}

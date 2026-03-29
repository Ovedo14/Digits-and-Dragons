using UnityEngine;
using UnityEngine.EventSystems;

public class HoverAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Animator animator;

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetTrigger("Hover");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetTrigger("Idle");
    }
}
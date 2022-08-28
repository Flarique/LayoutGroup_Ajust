using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(VerticalLayoutGroup))]
public class VerticalGrupAjust : MonoBehaviour
{
    VerticalLayoutGroup vlg => GetComponent<VerticalLayoutGroup>();
    private RectTransform myrect => GetComponent<RectTransform>();
    public void Ajustar()
    {
        if (vlg.childControlHeight || vlg.childForceExpandHeight)
            throw new System.Exception("O código não pode ser execultado caso o VertialLayout esteja com a opção childControlHeight ativada");


		float num = (float)transform.childCount;
        float y = 0;
        float MaxX = 0;
        for (int i = 0; i < num; i++)
        {
            if(transform.GetChild(i).TryGetComponent(out RectTransform rc))
            {
                y += rc.rect.height;
                if(!vlg.childControlWidth)
                if(rc.rect.width > MaxX)
                {
                    MaxX = rc.rect.width;
                }
            }
        }
        y += vlg.spacing * (num-1);
        myrect.sizeDelta = new Vector2((!vlg.childControlWidth ? MaxX : myrect.sizeDelta.x), y + vlg.padding.top + vlg.padding.bottom);
	}
}

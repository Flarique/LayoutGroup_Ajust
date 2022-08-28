using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(HorizontalLayoutGroup))]
public class HorizontalGroupAjust : MonoBehaviour
{
    HorizontalLayoutGroup hlg => GetComponent<HorizontalLayoutGroup>();
    private RectTransform myrect => GetComponent<RectTransform>();
    public void Ajustar()
    {
        if (hlg.childControlWidth || hlg.childForceExpandWidth)
            throw new System.Exception("O código não pode ser execultado caso o HorizontalLayout esteja com a opção childControlWidth ativada");


        float num = (float)transform.childCount;
        float x = 0;
        float Maxy = 0;
        for (int i = 0; i < num; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out RectTransform rc))
            {
                x += rc.rect.width;
                if(!hlg.childControlHeight)
                if(rc.rect.height > Maxy)
                {
                    Maxy = rc.rect.height;
                }
            }
        }
        x += hlg.spacing * (num - 1);
        myrect.sizeDelta = new Vector2(x + hlg.padding.right + hlg.padding.left, (!hlg.childControlHeight ? Maxy: myrect.sizeDelta.y));
    }
}

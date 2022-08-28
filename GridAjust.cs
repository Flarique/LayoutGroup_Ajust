using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class GridAjust : MonoBehaviour
{
    GridLayoutGroup glg => GetComponent<GridLayoutGroup>();
    private RectTransform myrect => GetComponent<RectTransform>();
    public void Ajustar()
    {
        float num = (float)transform.childCount;
        Vector2 xy = Vector2.zero;
        float colums = 0;
        float rows = 0;
        switch (glg.constraint)
        {
            case GridLayoutGroup.Constraint.Flexible:
                num = Mathf.Sqrt(num);
                for (int i = 0; i < num; i++)
                {
                    xy += glg.cellSize;
                }
                colums = (xy.x / glg.cellSize.x);
                rows = (xy.y / glg.cellSize.y);
                xy.x += (colums - 1) * glg.spacing.x;
                xy.y += (rows - 1) * glg.spacing.y;
                break;
            case GridLayoutGroup.Constraint.FixedColumnCount:
                xy.y += glg.cellSize.y * ((num % 2 == 0 ? num:num+1) / glg.constraintCount);
                xy.x += glg.cellSize.x * glg.constraintCount;
                colums = (xy.x / glg.cellSize.x);
                rows = (xy.y / glg.cellSize.y);
                xy.x += glg.spacing.x * (colums - 1);
                xy.y += glg.spacing.y * (rows - 1);
                break;
            case GridLayoutGroup.Constraint.FixedRowCount:
                xy.x += glg.cellSize.x * ((num % 2 == 0 ? num : num + 1) / glg.constraintCount);
                xy.y += glg.cellSize.y * glg.constraintCount;
                colums = (xy.x / glg.cellSize.x);
                rows = (xy.y / glg.cellSize.y);
                xy.x += glg.spacing.x * (colums-1);
                xy.y += glg.spacing.y * (rows-1);
                break;
            default:
                break;
        }
        RectTransform PaiRect = transform.parent.GetComponent<RectTransform>();
        Vector2 relação = (myrect.anchorMin-myrect.anchorMax);
        relação.x = Mathf.Abs(relação.x) * PaiRect.rect.width;
        relação.y = Mathf.Abs(relação.y) * PaiRect.rect.height;
        myrect.sizeDelta = new Vector2(xy.x + glg.padding.right + glg.padding.left, xy.y + glg.padding.top + glg.padding.bottom)-relação;
        if (!float.IsNaN(myrect.sizeDelta.x) && !float.IsNaN(myrect.sizeDelta.y))
            Atualizar = false;
    }
    private void Update()
    {
        if (Atualizar)
        {
            Ajustar();
        }
    }
    bool Atualizar = false;
    private void OnRectTransformDimensionsChange()
    {
        Atualizar = true;
    }
}

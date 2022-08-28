using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyHorizontalGroup : MonoBehaviour
{
	public bool mostrarPanding;

	public Panding panding;
	[Header("Objetc Size")]
	[Range(0f, 1f)]
	public float horizontalAmount = 0.5f;

	public float horizontalSize = 0.5f;

	public bool fullHorizontal;

	public bool porcentHorizontal = true;

	[Space(10)]
	[Range(0f, 1f)]
	public float verticalAmount = 0.5f;

	public float verticalSize = 0.5f;

	public bool fullVertical;

	public bool porcentVertical = true;

	public float spaceAmount;
	public float SpaceSize;
	public bool PorcentSpace = true;
	public AlingTip alingTip;

	public RectTransform RefRect;
	private RectTransform myrect => GetComponent<RectTransform>();
	public void DefinirProprioTamanho()
	{
		float num = (float)transform.childCount;
		Vector2 relação = (myrect.anchorMax - myrect.anchorMin);
		relação.x = relação.x * RefRect.rect.width;
		relação.y = relação.y * RefRect.rect.height;
		if (!fullHorizontal)
		{
			if (!porcentHorizontal)
			{
				horizontalSize = horizontalAmount * RefRect.rect.width;
				horizontalAmount = horizontalSize / RefRect.rect.width;
			}
			else
			{
				horizontalSize = horizontalAmount * RefRect.rect.width;
			}
		}
		else
		{
			horizontalAmount = 1;
			horizontalSize = horizontalAmount * RefRect.rect.width;
		}
		if (!PorcentSpace)
		{
			spaceAmount = SpaceSize / RefRect.rect.width;
		}
		else
		{
			SpaceSize = spaceAmount * RefRect.rect.width;
		}
		float x = ((num * horizontalSize) + ((Mathf.Clamp(num - 1f, 0, Mathf.Infinity)) * SpaceSize));
		if (float.IsNaN(x))
			x = 0;
		myrect.sizeDelta = new Vector2(x + panding.Direita + panding.Esquerda, RefRect.rect.height + panding.Topo + panding.Baixo) - relação;
	}
	public void DefinirAmounts()
	{
		if (!fullHorizontal)
		{
			if (!porcentHorizontal)
			{
				horizontalSize = horizontalAmount * RefRect.rect.width;
				horizontalAmount = horizontalSize / RefRect.rect.width;
			}
			else
			{
				horizontalSize = horizontalAmount * RefRect.rect.width;
			}
		}
		else
		{
			horizontalAmount = 1;
			horizontalSize = horizontalAmount * RefRect.rect.width;
		}
		if (!PorcentSpace)
		{
			spaceAmount = SpaceSize / RefRect.rect.width;
		}
		else
		{
			SpaceSize = spaceAmount * RefRect.rect.width;
		}
	}

	public void DefinirTamanhoDosFilhos()
	{
		DefinirAmounts();
		float _relaçãoy = (verticalAmount * RefRect.rect.height)/myrect.rect.height;
		float _relaçãox = (horizontalAmount * RefRect.rect.width)/ myrect.rect.width;
		for (int i = 0; i < transform.childCount; i++)
		{
			RectTransform rectTransform;
			if (transform.GetChild(i).TryGetComponent(out rectTransform))
			{
				rectTransform.anchorMin = new Vector2(0, 0);
				rectTransform.anchorMax = new Vector2(0, 0);
				rectTransform.sizeDelta = new Vector2(0, 0);
				rectTransform.anchorMin = new Vector2(0, 0);
				rectTransform.anchorMax = new Vector2(_relaçãox, _relaçãoy);
				float y = ((i * spaceAmount) + (i * verticalAmount) + (verticalAmount / 2f)) * -RefRect.rect.height;
				float x = ((i * spaceAmount) + (i * horizontalAmount) + (horizontalAmount / 2f)) * RefRect.rect.width;
				if (float.IsNaN(y))
					y = 0;
				if (float.IsNaN(x))
					x = 0;
				switch (alingTip)
				{
					case AlingTip.Esquerda:
						rectTransform.position = new Vector3(x + panding.Esquerda, 0f - panding.Topo) + myrect.position;
						break;
					case AlingTip.Centro:
							rectTransform.position = new Vector3(x + panding.Esquerda, (float.IsNaN((-RefRect.rect.height / 2f)) ? 0 : (-RefRect.rect.height / 2f)) - panding.Topo) + myrect.position;
						break;
					case AlingTip.Direita:
						rectTransform.position = new Vector3(x + panding.Esquerda, -RefRect.rect.height - panding.Topo) + myrect.position;
						break;
				}
				//rectTransform.sizeDelta = new Vector2((float.IsNaN(horizontalAmount * RefRect.rect.width) ? 0 : horizontalAmount * RefRect.rect.width),
				//	(float.IsNaN(verticalAmount * RefRect.rect.height) ? 0 : verticalAmount * RefRect.rect.height));
				//if (!float.IsNaN(rectTransform.sizeDelta.x) && !float.IsNaN(rectTransform.sizeDelta.y))
				//	Atualizar = false;
				//else
				//{
				//	return;
				//}
			}
		}
	}
	//private void Update()
	//{
	//	if (Atualizar)
	//	{
	//		DefinirProprioTamanho();
	//		DefinirTamanhoDosFilhos();
	//	}
	//}
	//bool Atualizar = false;
	//private void OnRectTransformDimensionsChange()
	//{
	//	Atualizar = true;
	//}
}

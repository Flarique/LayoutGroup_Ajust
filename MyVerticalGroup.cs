using System;
using System.Collections.Generic;
using UnityEngine;

public enum AlingTip
{
	Esquerda,
	Centro,
	Direita
}
[Serializable]
public class Panding
{
	public float Esquerda;
	public float Direita;
	public float Topo;
	public float Baixo;
}
/// <summary>
/// Feito para Content de um vyew scrooll
/// </summary>
public class MyVerticalGroup : MonoBehaviour
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

    public List<T> Preencher<T>(T pref, int quantidade) where T : MonoBehaviour
	{
		LimparFilhos();
		List<T> list = new List<T>();
		for (int i = 0; i < quantidade; i++)
		{
			T item = Instantiate<T>(pref, transform);
			list.Add(item);
		}
		AtualizarFilhos();
		return list;
	}
	public void LimparFilhos()
	{
		foreach (Transform Child in transform)
		{
			Destroy(Child.gameObject);
		}
		transform.DetachChildren();
		DefinirProprioTamanho();
	}

	public void AtualizarFilhos()
	{
		if (RefRect.rect.height == 0f || transform.childCount == 0)
		{
			Debug.LogError("algo pode ter dado errado, talvez o tamanho do visor ou não foi encontrado nenhum save");
			return;
		}
		DefinirProprioTamanho();
		DefinirTamanhoDosFilhos();
	}

	public void DefinirProprioTamanho()
	{
		float num = (float)transform.childCount;
		Vector2 relação = (myrect.anchorMax - myrect.anchorMin);
		relação.x = relação.x * RefRect.rect.width;
		relação.y = relação.y * RefRect.rect.height;
		if (!fullVertical)
		{
			if (!porcentVertical)
			{
				verticalSize = verticalAmount * RefRect.rect.height;
				verticalAmount = verticalSize / RefRect.rect.height;
			}
			else
			{
				verticalSize = verticalAmount * RefRect.rect.height;
			}
		}
		else
		{
			verticalAmount = 1;
			verticalSize = verticalAmount * RefRect.rect.height;
		}
		if (!PorcentSpace)
		{
			spaceAmount = SpaceSize / RefRect.rect.height;
		}
		else
		{
			SpaceSize = spaceAmount * RefRect.rect.height;
		}
		float y = ((num * verticalSize) + ((Mathf.Clamp(num - 1f, 0, Mathf.Infinity)) * SpaceSize));
		if (float.IsNaN(y))
			y = 0;
		myrect.sizeDelta = new Vector2(RefRect.rect.width + panding.Direita + panding.Esquerda, y + panding.Topo + panding.Baixo) - relação;
	}
	void DefinirAmounts()
	{
		if (!fullVertical)
		{
			if (!porcentVertical)
			{
				verticalSize = verticalAmount * RefRect.rect.height;
				verticalAmount = verticalSize / RefRect.rect.height;
			}
			else
			{
				verticalSize = verticalAmount * RefRect.rect.height;
			}
		}
		else
		{
			verticalAmount = 1;
			verticalSize = verticalAmount * RefRect.rect.height;
		}
		if (!PorcentSpace)
		{
			spaceAmount = SpaceSize / RefRect.rect.height;
		}
		else
		{
			SpaceSize = spaceAmount * RefRect.rect.height;
		}
	}

	public void DefinirTamanhoDosFilhos()
	{
		DefinirAmounts();
		for (int i = 0; i < transform.childCount; i++)
		{
			RectTransform rectTransform;
			if (transform.GetChild(i).TryGetComponent(out rectTransform))
			{
				rectTransform.anchorMin = new Vector2(0, 0);
				rectTransform.anchorMax = new Vector2(0, 0);
				rectTransform.sizeDelta = new Vector2(0, 0);
				float y = ((i * spaceAmount) + (i * verticalAmount) + (verticalAmount / 2f)) * -RefRect.rect.height;
				float x = ((i * spaceAmount) + (i * horizontalAmount) + (horizontalAmount / 2f)) * RefRect.rect.width;
				if (float.IsNaN(y))
					y = 0;
				if (float.IsNaN(x))
					x = 0;
				switch (alingTip)
				{
					case AlingTip.Esquerda:
						rectTransform.position = new Vector3(0f + panding.Esquerda, y - panding.Topo) + myrect.position;
						break;
					case AlingTip.Centro:
						rectTransform.position = new Vector3((float.IsNaN((RefRect.rect.width / 2f)) ? 0 : (RefRect.rect.width / 2f)) + panding.Esquerda, y - panding.Topo) + myrect.position;
						break;
					case AlingTip.Direita:
						rectTransform.position = new Vector3(RefRect.rect.width + panding.Esquerda, y - panding.Topo) + myrect.position;
						break;
				}
				rectTransform.sizeDelta = new Vector2((float.IsNaN(horizontalAmount * RefRect.rect.width) ?0: horizontalAmount * RefRect.rect.width),
					(float.IsNaN(verticalAmount * RefRect.rect.height) ? 0 : verticalAmount * RefRect.rect.height));
				if (!float.IsNaN(rectTransform.sizeDelta.x) && !float.IsNaN(rectTransform.sizeDelta.y))
					Atualizar = false;
                else
                {
					return;
                }
			}
		}
	}
    private void Update()
    {
        if (Atualizar)
		{
			DefinirProprioTamanho();
			DefinirTamanhoDosFilhos();
		}
    }
	bool Atualizar = false;
	private void OnRectTransformDimensionsChange()
	{
		Atualizar = true;
	}
}

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(VerticalGrupAjust))]
public class VerticalCustonContent : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        // Create a new VisualElement to be the root of our inspector UI
        VisualElement myInspector = new VisualElement();

        VerticalGrupAjust a = (VerticalGrupAjust)target;

        // Add a simple label
        Button b = new Button(a.Ajustar);
        b.text = "Ajustar";
        myInspector.Add(b);

        // Return the finished inspector UI
        return myInspector;
    }
}

[CustomEditor(typeof(HorizontalGroupAjust))]
public class HorizontalCustonContent : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        // Create a new VisualElement to be the root of our inspector UI
        VisualElement myInspector = new VisualElement();

        HorizontalGroupAjust a = (HorizontalGroupAjust)target;

        // Add a simple label
        Button b = new Button(a.Ajustar);
        b.text = "Ajustar";
        myInspector.Add(b);

        // Return the finished inspector UI
        return myInspector;
    }
}

[CustomEditor(typeof(GridAjust))]
public class GridAjustCustonEditor : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        // Create a new VisualElement to be the root of our inspector UI
        VisualElement myInspector = new VisualElement();

        GridAjust a = (GridAjust)target;

        // Add a simple label
        Button b = new Button(a.Ajustar);
        b.text = "Ajustar";
        myInspector.Add(b);

        // Return the finished inspector UI
        return myInspector;
    }
}
[CustomEditor(typeof(MyVerticalGroup))]
public class MyVerticalGroupEditor : Editor
{
    SerializedProperty canvasType;
    public override void OnInspectorGUI()
    {
        MyVerticalGroup a = (MyVerticalGroup)target;

        a.RefRect = (RectTransform)EditorGUILayout.ObjectField("Rect Parametro",(a.RefRect == null ? null: a.RefRect),typeof(RectTransform));

        if (a.RefRect == null)
            return;

        a.alingTip = (AlingTip)EditorGUILayout.EnumPopup("Alinhamento", a.alingTip);

        a.mostrarPanding = EditorGUILayout.Foldout(a.mostrarPanding, "Bordas");
        if (a.mostrarPanding)
        {
            a.panding.Direita = EditorGUILayout.FloatField("Direita", a.panding.Direita);
            a.panding.Esquerda = EditorGUILayout.FloatField("Esquerda", a.panding.Esquerda);
            a.panding.Topo = EditorGUILayout.FloatField("Topo", a.panding.Topo);
            a.panding.Baixo = EditorGUILayout.FloatField("Baixo", a.panding.Baixo);
        }

        EditorGUILayout.Space(10);
        GUILayout.Label("Parametros Horizontais");

        a.fullHorizontal = EditorGUILayout.ToggleLeft("Tamanho Completo", a.fullHorizontal);

        if (!a.fullHorizontal)
        {
            a.porcentHorizontal = EditorGUILayout.Toggle("Tamanho em %", a.porcentHorizontal);

            if (a.porcentHorizontal)
            {
                a.horizontalAmount = EditorGUILayout.Slider("Tamanho %", a.horizontalAmount, 0, 1f);
            }
            else
            {
                a.horizontalSize = EditorGUILayout.FloatField("TamanhoUni", a.horizontalSize);
            }
        }
        else
        {
            a.horizontalAmount = 1f;
        }

        EditorGUILayout.Space(10);
        GUILayout.Label("Parametros Verticais");

        a.fullVertical = EditorGUILayout.ToggleLeft("Tamanho Completo",a.fullVertical);

        if (!a.fullVertical)
        {
            a.porcentVertical = EditorGUILayout.Toggle("Tamanho em %", a.porcentVertical);

            if (a.porcentVertical)
            {
                a.verticalAmount = EditorGUILayout.Slider("Tamanho %", a.verticalAmount, 0, 1f);
            }
            else
            {
                a.verticalSize = EditorGUILayout.FloatField("Tamanho", a.verticalSize);
            }
        }
        else
        {
            a.verticalAmount = 1f;
        }

        EditorGUILayout.Space(10);
        GUILayout.Label("Parametros Do espaçamento");

        a.PorcentSpace = EditorGUILayout.ToggleLeft("Espaçamento Por %",a.PorcentSpace);

        if (a.PorcentSpace)
        {
            a.spaceAmount = EditorGUILayout.Slider("Tamanho %", a.spaceAmount, 0, 1f);
        }
        else
        {
            a.SpaceSize = EditorGUILayout.FloatField("Tamanho", a.SpaceSize);
        }

        //Ajusta Os objetos e atualiza o tamanho do Content
        if (a.RefRect.rect.height == 0f || a.transform.childCount == 0)
        {
            return;
        }
        a.DefinirProprioTamanho();
        a.DefinirTamanhoDosFilhos();
    }
}
[CustomEditor(typeof(MyHorizontalGroup))]
public class MyHorizontalGroupEditor : Editor
{
    SerializedProperty canvasType;
    public override void OnInspectorGUI()
    {
        MyHorizontalGroup a = (MyHorizontalGroup)target;

        a.RefRect = (RectTransform)EditorGUILayout.ObjectField("Rect Parametro",(a.RefRect == null ? null: a.RefRect),typeof(RectTransform));

        if (a.RefRect == null)
            return;

        a.alingTip = (AlingTip)EditorGUILayout.EnumPopup("Alinhamento", a.alingTip);

        a.mostrarPanding = EditorGUILayout.Foldout(a.mostrarPanding, "Bordas");
        if (a.mostrarPanding)
        {
            a.panding.Direita = EditorGUILayout.FloatField("Direita", a.panding.Direita);
            a.panding.Esquerda = EditorGUILayout.FloatField("Esquerda", a.panding.Esquerda);
            a.panding.Topo = EditorGUILayout.FloatField("Topo", a.panding.Topo);
            a.panding.Baixo = EditorGUILayout.FloatField("Baixo", a.panding.Baixo);
        }

        EditorGUILayout.Space(10);
        GUILayout.Label("Parametros Horizontais");

        a.fullHorizontal = EditorGUILayout.ToggleLeft("Tamanho Completo", a.fullHorizontal);

        if (!a.fullHorizontal)
        {
            a.porcentHorizontal = EditorGUILayout.Toggle("Tamanho em %", a.porcentHorizontal);

            if (a.porcentHorizontal)
            {
                a.horizontalAmount = EditorGUILayout.Slider("Tamanho %", a.horizontalAmount, 0, 1f);
            }
            else
            {
                a.horizontalSize = EditorGUILayout.FloatField("TamanhoUni", a.horizontalSize);
            }
        }
        else
        {
            a.horizontalAmount = 1f;
        }

        EditorGUILayout.Space(10);
        GUILayout.Label("Parametros Verticais");

        a.fullVertical = EditorGUILayout.ToggleLeft("Tamanho Completo",a.fullVertical);

        if (!a.fullVertical)
        {
            a.porcentVertical = EditorGUILayout.Toggle("Tamanho em %", a.porcentVertical);

            if (a.porcentVertical)
            {
                a.verticalAmount = EditorGUILayout.Slider("Tamanho %", a.verticalAmount, 0, 1f);
            }
            else
            {
                a.verticalSize = EditorGUILayout.FloatField("Tamanho", a.verticalSize);
            }
        }
        else
        {
            a.verticalAmount = 1f;
        }

        EditorGUILayout.Space(10);
        GUILayout.Label("Parametros Do espaçamento");

        a.PorcentSpace = EditorGUILayout.ToggleLeft("Espaçamento Por %",a.PorcentSpace);

        if (a.PorcentSpace)
        {
            a.spaceAmount = EditorGUILayout.Slider("Tamanho %", a.spaceAmount, 0, 1f);
        }
        else
        {
            a.SpaceSize = EditorGUILayout.FloatField("Tamanho", a.SpaceSize);
        }

        //Ajusta Os objetos e atualiza o tamanho do Content
        if (a.RefRect.rect.height == 0f || a.transform.childCount == 0)
        {
            return;
        }
        a.DefinirProprioTamanho();
        a.DefinirTamanhoDosFilhos();
    }
}
#endif

using UnityEditor;
using UnityEngine;
using Utils.UI;

public class UI_MenuItem : MonoBehaviour
{
    static bool TryCheckCanvas(out Canvas canvas)
    {
        canvas = FindFirstObjectByType<Canvas>();
        return canvas != null;
    }

    static void Create<T>(GameObject prefab, string name = nameof(T)) where T : Object
    {
        GameObject asset;
        GameObject obj;

        if (!TryCheckCanvas(out var canvas)) {
            asset = Resources.Load<GameObject>("Prefabs/UI_Default_Canvas");
            canvas = Instantiate(asset).GetComponent<Canvas>();
            canvas.name = nameof(Canvas);
        }

        asset = prefab;
        if (Selection.activeGameObject != null && !Application.isPlaying) {
            obj = Instantiate(asset, Selection.activeGameObject.transform);
            obj.name = name;

            Undo.RegisterCreatedObjectUndo(obj, "Create" + obj.name);
            Undo.IncrementCurrentGroup();
            return;
        }

        obj = Instantiate(asset, canvas.transform);
        obj.name = "UI_Button";

        Undo.RegisterCreatedObjectUndo(obj, "Create" + obj.name);
        Undo.IncrementCurrentGroup();
    }

    [MenuItem("GameObject/UI/Utils/UI_Button")]
    public static void CreateButton() =>
        Create<UI_Button>(Resources.Load<GameObject>("Prefabs/UI_Button"), nameof(UI_Button));

    [MenuItem("GameObject/UI/Utils/Progress Bar")]
    static void CreateProgressBar() =>
        Create<UI_ProgressBar>(Resources.Load<GameObject>("Prefabs/UI_ProgressBar"), nameof(UI_ProgressBar));

    [MenuItem("GameObject/UI/Utils/Health Bar")]
    static void CreateHealthBar() =>
        Create<UI_HealthBar>(Resources.Load<GameObject>("Prefabs/UI_HealthBar"), nameof(UI_HealthBar));


    [MenuItem("GameObject/UI/Utils/UI_SkillButton")]
    static void CreateSkillButton() =>
        Create<UI_HealthBar>(Resources.Load<GameObject>("Prefabs/UI_SkillButton"), nameof(UI_HealthBar));
}
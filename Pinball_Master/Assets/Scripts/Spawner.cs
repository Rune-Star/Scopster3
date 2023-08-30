using Utils.Server;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    static T Spawn<T>(string id) where T : Component
    {
        var ins = DataManager.Instance.GetPrefab<T>(id);
        return Instantiate(ins).GetComponent<T>();
    }

    static T Spawn<T>() where T : Creature
    {
        //var obj = Instantiate(DataManager.Instance.prefabDict[nameof(T)]);
        return Instantiate(DataManager.Instance.GetPrefab<T>());
    }

    /// <summary> Summon GameObject attached Creature Component </summary>
    /// <param name="position">spawnPosition</param>
    /// <param name="parent">Set Object's parent</param>
    /// <typeparam name="T"> in Creature</typeparam>
    public static T Spawn<T>(Vector3 position, Transform parent) where T : Creature
    {
        var obj = Spawn<T>();
        Transform transform;
        (transform = obj.transform).SetParent(parent);
        transform.localPosition = position;

        return obj;
    }
}
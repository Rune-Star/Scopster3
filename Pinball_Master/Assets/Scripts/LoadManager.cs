using System.ComponentModel.Design;
using System.IO;
using UnityEngine;

namespace Utils.Server
{
    public interface IJsonData
    {
        
    }
    public class LoadManager : MonoBehaviour
    {
        static LoadManager instance;

        public static LoadManager Instance
        {
            get
            {
                if (instance == null) {
                    new GameObject(nameof(LoadManager)).AddComponent<LoadManager>();
                }

                return instance;
            }
        }

        void Awake()
        {
            if (instance == null) {
                instance = this;
                DontDestroyOnLoad(this);
            } else {
                Destroy(this);
            }
        }

        public static T LoadFromJson<T>(string path) where T : IJsonData
        {
            if (!File.Exists(path))
                throw CheckoutException.Canceled;
            
            var file = File.ReadAllText(path);
            var data = JsonUtility.FromJson<T>(file);

            return data;
        }
    }
}
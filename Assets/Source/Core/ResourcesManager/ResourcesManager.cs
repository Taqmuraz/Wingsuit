namespace Core
{
    using UnityEngine;

    public class ResourcesManager
    {
        public static GameObject LoadPrefab(string path)
        {
            return Resources.Load<GameObject>(path);
        }
        public static TObject LoadObject<TObject>(string path) where TObject : Object
        {
            return Resources.Load<TObject>(path);
        }
    }
}

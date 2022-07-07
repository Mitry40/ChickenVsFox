using System.Collections.Generic;
using UnityEngine;

namespace Uliger
{
    public class CPrefabContainer
    {
        private Dictionary<System.Type, GameObject> Prefabs;

        private void Load()
        {
            Prefabs = new Dictionary<System.Type, GameObject>();

            Prefabs.Add(typeof(OWall_Main), Resources.Load<GameObject>("Prefabs/OWall"));
            Prefabs.Add(typeof(OEnemy_Main), Resources.Load<GameObject>("Prefabs/OEnemy"));
            Prefabs.Add(typeof(OPlayer_Main), Resources.Load<GameObject>("Prefabs/OPlayer"));
            Prefabs.Add(typeof(OExitCell_Main), Resources.Load<GameObject>("Prefabs/OExitCell"));
        }
        private void Unload() => Resources.UnloadUnusedAssets();

        public GameObject GetInstance<T>() where T : MonoBehaviour
        {
            System.Type key = typeof(T);
            if (Prefabs == null) Load();
            if (Prefabs.ContainsKey(key) == true)
            {
                return Prefabs[key];
            }
            return null;
        }

    }
}
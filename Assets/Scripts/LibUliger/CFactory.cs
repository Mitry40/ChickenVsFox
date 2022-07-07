using System.Collections.Generic;
using UnityEngine;

namespace Uliger
{
    public static class CFactory
    {

        private static CPrefabContainer _PrefabContainer = null;
        private static CPrefabContainer PrefabContainer
        {
            get
            {
                if (_PrefabContainer == null)
                {
                    _PrefabContainer = new CPrefabContainer();
                }
                return _PrefabContainer;
            }
        }


        public static GameObject GetInstance<T>() where T : MonoBehaviour
        {
            GameObject obj = null;
            if (PrefabContainer != null) obj = PrefabContainer.GetInstance<T>();
            if (obj == null)
            {
                //CError.Add(CError.ErrorType.NullGameObject, string.Format("GetInstance<{0}>", typeof(T).Name));
            }
            return obj;
        }

        public static T CreateObject<T>() where T : MonoBehaviour
        {
            GameObject obj = GameObject.Instantiate(GetInstance<T>());
            T script = null;
            if (obj != null) script = obj.GetComponent<T>();
            if (script == null)
            {
                //CError.Add(CError.ErrorType.NullGameObject, string.Format("CreateObject<{0}>", typeof(T).Name));
            }
            return script;
        }



        public static OWall_Main CreateWall(Vector3 position, Transform parent)
        {
            OWall_Main script = CreateObject<OWall_Main>();
            if (script != null)
            {
                script.transform.position = position;
                script.transform.SetParent(parent, true);
            }
            return script;
        }

        public static OExitCell_Main CreateExitCell(Vector3 position)
        {
            OExitCell_Main script = CreateObject<OExitCell_Main>();
            if (script != null)
            {
                script.transform.position = position;
            }
            return script;
        }

        public static OEnemy_Main CreateEnemy(Vector3 position, List<Vector2Int> way)
        {
            OEnemy_Main script = CreateObject<OEnemy_Main>();
            if (script != null)
            {
                script.transform.position = position;
                script.Init(way);
            }
            return script;
        }

        public static OPlayer_Main CreatePlayer(Vector3 position)
        {
            OPlayer_Main script = CreateObject<OPlayer_Main>();
            if (script != null)
            {
                script.transform.position = position;
            }
            return script;
        }



        public static void Destroy<T>(T script) where T : MonoBehaviour => DestroyObject(script.gameObject);
        public static void DestroyObject(GameObject obj)
        {
            obj.SetActive(false);
            GameObject.Destroy(obj);
        }

    }
}
using UnityEngine;
using UnityEditor;

public class RemoveCollidersFromPrefab : EditorWindow
{
    [MenuItem("Tools/Remove Mesh and Box Colliders From Selected Prefab")]
    public static void RemoveCollidersFromSelectedPrefab()
    {
        if (Selection.activeGameObject != null)
        {
            GameObject selectedObject = Selection.activeGameObject;

            // MeshCollider 제거
            MeshCollider[] meshColliders = selectedObject.GetComponentsInChildren<MeshCollider>(true);
            foreach (MeshCollider meshCollider in meshColliders)
            {
                DestroyImmediate(meshCollider, true);
            }

        }
        else
        {
            Debug.LogWarning("프리팹 또는 오브젝트를 선택해주세요.");
        }
    }
}

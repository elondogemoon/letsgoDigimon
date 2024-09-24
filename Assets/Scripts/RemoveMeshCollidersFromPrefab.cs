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

            // MeshCollider ����
            MeshCollider[] meshColliders = selectedObject.GetComponentsInChildren<MeshCollider>(true);
            foreach (MeshCollider meshCollider in meshColliders)
            {
                DestroyImmediate(meshCollider, true);
            }

        }
        else
        {
            Debug.LogWarning("������ �Ǵ� ������Ʈ�� �������ּ���.");
        }
    }
}

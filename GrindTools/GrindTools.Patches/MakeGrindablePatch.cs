using GameManagement;
using HarmonyLib;
using MapEditor;
using UnityEngine;

namespace GrindTools.Patches
{
    [HarmonyPatch(typeof(MapEditorSplineObject))]
    [HarmonyPatch("MakeGrindable")]
    public class MakeGrindablePatch
    {
        [HarmonyPrefix]
        public static bool Prefix(ref bool useBoxCollider)
        {
            if (Main.settings.capColliders)
            {
                useBoxCollider = false;
            }
            return true; // Continue to the original method
        }

        [HarmonyPostfix]
        public static void Postfix(MapEditorSplineObject __instance, bool useBoxCollider)
        {
            if (!useBoxCollider && Main.settings.capColliders)
            {
                // remove existing mesh collider for spline if created from ogiginal method.
                MeshCollider meshCollider = __instance.GetComponent<MeshCollider>();
                if (meshCollider != null)
                {
                    Object.Destroy(meshCollider);
                }

                // Access private fields using Traverse
                float width = Traverse.Create(__instance).Field("width").GetValue<float>();
                float height = Traverse.Create(__instance).Field("height").GetValue<float>();

                // Your logic to add capsule colliders here
                for (int index = 0; index < __instance.nodes.Count - 1; ++index)
                {
                    string name = "Capsule Collider Node " + index + " -> " + (index + 1);
                    Transform transform;
                    if (__instance.transform.childCount > index)
                    {
                        transform = __instance.transform.GetChild(index);
                        transform.name = name;
                    }
                    else
                    {
                        transform = new GameObject(name).transform;
                        transform.SetParent(__instance.transform);
                    }

                    CapsuleCollider capCollider = transform.GetComponent<CapsuleCollider>();
                    if (capCollider == null)
                    {
                        capCollider = transform.gameObject.AddComponent<CapsuleCollider>();
                        Vector3 length = __instance.nodes[index + 1].center - __instance.nodes[index].center;
                        transform.position = Vector3.Lerp(__instance.nodes[index].center, __instance.nodes[index + 1].center, 0.5f);
                        transform.rotation = Quaternion.LookRotation(length, Vector3.Cross(length, __instance.nodes[index].widthDir + __instance.nodes[index + 1].widthDir));
                        transform.gameObject.layer = __instance.gameObject.layer;
                        capCollider.direction = 2;
                        capCollider.radius = width / 2f;
                        capCollider.height = length.magnitude;
                        capCollider.center = new Vector3(width / 2f, height / 2f, 0.0f);
                    }
                    for (int j = __instance.transform.childCount - 1; j > __instance.nodes.Count - 2; j--)
                    {
                        Object.Destroy(__instance.transform.GetChild(j));
                    }
                }
            }
        }
    }
}


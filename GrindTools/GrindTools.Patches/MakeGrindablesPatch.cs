using HarmonyLib;
using UnityEngine;
using MapEditor;
using System;
using SkaterXL.Core;
using Dreamteck.Splines;
using System.Linq;
using Object = UnityEngine.Object;

[HarmonyPatch(typeof(MapEditorSplineObject))]
[HarmonyPatch("MakeGrindable")]
public static class MakeGrindablePatch
{
    private static bool _useCapsuleCollider = false;
    public static bool useCapsuleCollider
    {
        get { return _useCapsuleCollider; }
        set { _useCapsuleCollider = value; }
    }
    public static bool Prefix(MapEditorSplineObject __instance, bool usecapCollider)
    {
        if (_useCapsuleCollider)
        {
            var width = Traverse.Create(__instance).Field("width").GetValue<float>();
            var height = Traverse.Create(__instance).Field("height").GetValue<float>();

            if (__instance.nodes.Count < 2)
            {
                Debug.LogError("Not enough Points");
                return false;
            }
            Debug.Log("minAngle: " + __instance.nodes.Aggregate(0.0f, (minAngle, edge) => Mathf.Min(minAngle, edge.GetAngle())) + "maxAngle: " + __instance.nodes.Aggregate(180f, (maxAngle, edge) => Mathf.Max(maxAngle, edge.GetAngle())));

            SplineComputer splineComputer = __instance.gameObject.GetComponent<SplineComputer>();
            if (splineComputer == null)
            {
                splineComputer = __instance.gameObject.AddComponent<SplineComputer>();
                splineComputer.type = Spline.Type.Linear;
                splineComputer.SetPoints(__instance.nodes.Select((node => new SplinePoint(node.center, node.edgeDir, Vector3.ProjectOnPlane(Vector3.up, node.edgeDir).normalized, 1f, Color.white))).ToArray());
            }        
            
            if (__instance.gameObject.layer != LayerUtility.Grindable && __instance.gameObject.layer != LayerUtility.Coping)
            {
                __instance.gameObject.layer = LayerUtility.Grindable;
                __instance.gameObject.GetComponent<Renderer>().enabled = false;
            }   

            for (int index = 0; index < __instance.nodes.Count - 1; ++index)
            {
                string name = "Collider Node " + index + " -> " + (index + 1);
                Transform transform = __instance.transform;
                if (__instance.transform.childCount > index)
                {
                    transform = transform.GetChild(index);
                    transform.name = name;
                }
                else
                {
                    transform = new GameObject(name, new Type[1]
                    {
              typeof (CapsuleCollider)
                    }).transform;
                    transform.transform.SetParent(transform);
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
            }
            for (int index = __instance.transform.childCount - 1; index > __instance.nodes.Count - 2; --index)
            {
                Object.Destroy(__instance.transform.GetChild(index));
            }

            // skip the original method.
            _useCapsuleCollider = false;
            return false;
        }

        // continue original method.
        return true;
    }
}
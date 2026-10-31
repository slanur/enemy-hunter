using System.Collections.Generic;
using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    private List<Transform> waypointler;

    public int WaypointSayisi => waypointler.Count;

    //oyun başlarken starttan önce çalışır
    private void Awake()
    {
        waypointler = new List<Transform>();

        foreach (Transform child in transform)
        {
            waypointler.Add(child);
        }
    }

    public Vector3 GetNokta(int index)
    {
        if (index < 0 || index >= waypointler.Count)
        {
            Debug.LogError($"[WaypointPath] Geçersiz index:{index}.Toplam waypoint sayısı: {waypointler.Count}");
            return transform.position;
        }

        return waypointler[index].position;
    }
}
using UnityEngine;
using UnityEngine.Tilemaps;

public class TimeManager : MonoBehaviour
{
    [Header("Worlds")]
    public GameObject presentWorld;
    public GameObject pastWorld;

    [Header("Settings")]
    public KeyCode switchKey = KeyCode.F; // Press F to Flashback
    public bool isPast = false; // Start in Present

    void Start()
    {
        UpdateWorldState();
    }

    void Update()
    {
        if (Input.GetKeyDown(switchKey))
        {
            ToggleTime();
        }
    }

    void ToggleTime()
    {
        isPast = !isPast;
        UpdateWorldState();
    }

    void UpdateWorldState()
    {
        if (isPast)
        {
            pastWorld.SetActive(true);
            presentWorld.SetActive(false);
        }
        else
        {
            pastWorld.SetActive(false);
            presentWorld.SetActive(true);
        }
    }
}
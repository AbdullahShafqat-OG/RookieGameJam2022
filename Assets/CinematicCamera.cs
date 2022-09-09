using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicCamera : MonoBehaviour
{
    [SerializeField]
    internal float slowMotionScale;

    void Start()
    {
        Time.timeScale = slowMotionScale;
    }
}

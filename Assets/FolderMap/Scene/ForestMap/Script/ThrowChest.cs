using Cainos.LucidEditor;
using Cainos.PixelArtPlatformer_VillageProps;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class ThrowChest : MonoBehaviour
{
    public GameObject[]? gameObject;

    [FoldoutGroup("Reference")]
    public Animator animator;

    [FoldoutGroup("Runtime"), ShowInInspector, DisableInEditMode]
    public bool IsOpened
    {
        get { return isOpened; }
        set
        {
            isOpened = value;
            animator.SetBool("IsOpened", isOpened);
        }
    }

    private bool hasBeenOpened = false;
    private bool isOpened;

    [FoldoutGroup("Runtime"), Button("Open"), HorizontalGroup("Runtime/Button")]
    public void Open()
    {
        if (!hasBeenOpened)
        {
            IsOpened = true;
            hasBeenOpened = true;
            Time.timeScale = 0;

            foreach (var item in gameObject)
            {
                item.GetComponent<ThrowItem>().Display();
            }

        }
    }

    [FoldoutGroup("Runtime"), Button("Close"), HorizontalGroup("Runtime/Button")]
    public void Close()
    {
        IsOpened = false;
    }

}

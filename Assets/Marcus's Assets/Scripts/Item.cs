using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class Item
{
    public GameObject item;
    public int cost = 1;

    public Color defaultColor;
    public Color selectedColor;

    public Image itemUIImage;
}

//Enum for attraction type

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractionTypeEnum : MonoBehaviour
{
    public AttractionType direction;
}

public enum AttractionType
{
    historicalSite,
    museum,
    gallery,
    recreation
}

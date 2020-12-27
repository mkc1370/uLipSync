﻿using UnityEngine;

namespace uLipSync
{

[CreateAssetMenu(menuName = "uLipSync/Profile")]
public class Profile : ScriptableObject
{
    public FormantPair formantA = new FormantPair(726f, 1171f);
    public FormantPair formantI = new FormantPair(281f, 2343f);
    public FormantPair formantU = new FormantPair(328f, 1570f);
    public FormantPair formantE = new FormantPair(468f, 2109f);
    public FormantPair formantO = new FormantPair(492f, 773f);
    public float maxError = 500f; // Hz
}

}

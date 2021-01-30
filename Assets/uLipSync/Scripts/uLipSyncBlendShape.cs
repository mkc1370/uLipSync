﻿using UnityEngine;
using System.Collections.Generic;

namespace uLipSync
{

[System.Serializable]
public class BlendShapeInfo
{
    public int index = -1;
    public float factor = 1f;
    public float blend { get; set; } = 0f;
    public float normalizedBlend { get; set; } = 0f;
}

public class uLipSyncBlendShape : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public List<BlendShapeInfo> blendShapeList = new List<BlendShapeInfo>()
    {
        new BlendShapeInfo(),
        new BlendShapeInfo(),
        new BlendShapeInfo(),
        new BlendShapeInfo(),
        new BlendShapeInfo(),
    };

    Vowel vowel = Vowel.A;
    float volume = 0f;
    bool lipSyncUpdated = false;

    public void OnLipSyncUpdate(LipSyncInfo lipSync)
    {
        vowel = lipSync.vowel;
        volume = Util.CalcNextValue(volume, lipSync.volume, 0.8f);
        lipSyncUpdated = true;
    }

    void Update()
    {
        float sum = 0f;

        for (int i = (int)Vowel.A; i <= (int)Vowel.O; ++i)
        {
            var vowel = (Vowel)i;
            var info = blendShapeList[i];
            bool isTargetVowel = vowel == this.vowel;
            info.blend = Util.CalcNextValue(info.blend, isTargetVowel ? 1f : 0f, 0.95f);
            sum += info.blend;
        }

        for (int i = (int)Vowel.A; i <= (int)Vowel.O; ++i)
        {
            var info = blendShapeList[i];
            info.normalizedBlend = info.blend / sum;
        }
    }

    void LateUpdate()
    {
        if (!skinnedMeshRenderer) return;

        foreach (var info in blendShapeList)
        {
            if (info.index < 0) continue;

            float blend = info.normalizedBlend * info.factor * volume * 100;
            skinnedMeshRenderer.SetBlendShapeWeight(info.index, blend);
        }

        if (!lipSyncUpdated)
        {
            volume = Util.CalcNextValue(volume, 0f, 0.9f);
        }
        lipSyncUpdated = false;
    }
}

}

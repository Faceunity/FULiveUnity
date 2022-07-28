using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class ExpressionPostprocessData
{
    public ExpressionPostprocessArray[] expression_postprocess;
}

[Serializable]
public class ExpressionPostprocessArray
{
    public string name;
    public int index;
    public float[] range;
    public BSList[] bs_list;
}

[Serializable]
public class BSList
{
    public string name;
    public int index;
    public float[] range;
    public bool enable_max;
}
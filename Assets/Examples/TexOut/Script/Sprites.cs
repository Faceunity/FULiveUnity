using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//public enum ItemType
//{
//    Undefine = -1,
//    Beauty = 0,
//    CommonFilter,
//    Makeup,
//    Animoji,
//    ItemSticker,
//    ARMask,
//    ChangeFace,
//    ExpressionRecognition,
//    MusicFilter,
//    BackgroundSegmentation,
//    GestureRecognition,
//    MagicMirror,
//    PortraitLightEffect,
//    PortraitDrive,
//    //...更多类型道具请咨询技术支持
//}

//Texout场景所需的UI合集，配合ItemConfig使用，这个组件只是为了方便，不具备任何实际编程价值，请谨慎参考

public class Sprites : MonoBehaviour {

    public Sprite[] BeautySkin;
    public Sprite[] BeautyShape;
    public Sprite[] BeautyFilter;
    Dictionary<BeautySkinType, Sprite[]> beautyspritelist = new Dictionary<BeautySkinType, Sprite[]>();

    public Sprite[] CommonFilter;
    public Sprite[] Makeup;
    public Sprite[] Animoji;
    public Sprite[] ItemSticker;
    public Sprite[] ARMask;
    public Sprite[] ChangeFace;
    public Sprite[] ExpressionRecognition;
    public Sprite[] MusicFilter;
    public Sprite[] BackgroundSegmentation;
    public Sprite[] GestureRecognition;
    public Sprite[] MagicMirror;
    public Sprite[] PortraitLightEffect;
    public Sprite[] PortraitDrive;
    Dictionary<ItemType,Sprite[]> spritelist = new Dictionary<ItemType,Sprite[]>();

    private void Awake()
    {
        beautyspritelist.Clear();
        beautyspritelist.Add(BeautySkinType.BeautySkin, BeautySkin);
        beautyspritelist.Add(BeautySkinType.BeautyShape, BeautyShape);
        beautyspritelist.Add(BeautySkinType.BeautyFilter, BeautyFilter);

        spritelist.Clear();
        
        spritelist.Add(ItemType.CommonFilter, CommonFilter);
        spritelist.Add(ItemType.Makeup, Makeup);
        spritelist.Add(ItemType.Animoji, Animoji);
        spritelist.Add(ItemType.ItemSticker, ItemSticker);
        spritelist.Add(ItemType.ARMask, ARMask);
        spritelist.Add(ItemType.ChangeFace, ChangeFace);
        spritelist.Add(ItemType.ExpressionRecognition, ExpressionRecognition);
        spritelist.Add(ItemType.MusicFilter, MusicFilter);
        spritelist.Add(ItemType.BackgroundSegmentation, BackgroundSegmentation);
        spritelist.Add(ItemType.GestureRecognition, GestureRecognition);
        spritelist.Add(ItemType.MagicMirror, MagicMirror);
        spritelist.Add(ItemType.PortraitLightEffect, PortraitLightEffect);
        spritelist.Add(ItemType.PortraitDrive, PortraitDrive);
    }


    /**\brief 获取Sprite\param type 道具类型\param id 道具ID\return Sprite    */
    public Sprite GetSprite(ItemType type,int id)
    {
        if (spritelist.ContainsKey(type))
        {
            var sprites = spritelist[type];
            if (id >= 0 && id < sprites.Length)
                return sprites[id];
            else
                return null;
        }
        else
            return null;
    }

    public Sprite GetSprite(BeautySkinType type, int id)
    {
        if (beautyspritelist.ContainsKey(type))
        {
            var sprites = beautyspritelist[type];
            if (id >= 0 && id < sprites.Length)
                return sprites[id];
            else
                return null;
        }
        else
            return null;
    }

}

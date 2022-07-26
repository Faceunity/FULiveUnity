using System.Collections;
using UnityEngine;

//道具种类
public enum ItemType
{
    Undefine = -1,
    Beauty = 0,
    CommonFilter,
    Makeup,
    Animoji,
    ItemSticker,
    ARMask,
    ChangeFace,
    ExpressionRecognition,
    MusicFilter,
    BackgroundSegmentation,
    GestureRecognition,
    MagicMirror,
    PortraitLightEffect,
    PortraitDrive,
    //...更多类型道具请咨询技术支持
}

//这个struct只是为了方便Texout场景的搭建，建议谨慎参考
//这个struct关联了一个道具的各种数据，包括名字，相对加载路径，UI的ID，道具提示信息，道具的种类
public struct Item
{
    public string name;//名字
    public string fullname; //相对加载路径
    public int iconid; //UI的ID
    public string tip;//道具提示信息
    public ItemType type;//道具的种类

    Item(string _name = "", string _fullname = "", int _iconid = -1, string _tip = "", ItemType _type = ItemType.Undefine)
    {
        name = _name;
        fullname = _fullname;
        iconid = _iconid;
        tip = _tip;
        type = _type;
    }

    public static Item Empty = new Item();

};

public class ItemConfig
{
    public static Item[] defaultItem ={
        new Item { name = "default", fullname = "default/default", iconid = -1 ,type=ItemType.Undefine},
    };
    public static Item[] beautySkin ={
        new Item { name = "beautification", fullname = "graphics/face_beautification", iconid = -1 ,type=ItemType.Beauty},
    };

    public static Item[] commonFilter ={
        new Item { name = "fuzzytoonfilter", fullname = "graphics/fuzzytoonfilter", iconid = -1 ,type=ItemType.CommonFilter},
        new Item { name = "fxaa", fullname = "graphics/fxaa", iconid = -1 ,type=ItemType.CommonFilter},
    };

    public static Item[] makeup ={
        new Item { name = "face_makeup", fullname = "graphics/face_makeup", iconid = -1 ,type=ItemType.Makeup},
    };

    //Animoji
    public static Item[] item_1 = {
        new Item { name = "cartoon_princess_Animoji", fullname = "items/Animoji/cartoon_princess_Animoji", iconid = 2 ,type=ItemType.Animoji},
        new Item { name = "kaola_Animoji", fullname = "items/Animoji/kaola_Animoji", iconid = 8 ,type=ItemType.Animoji},
        new Item { name = "wuxia_Animoji", fullname = "items/Animoji/wuxia_Animoji", iconid = 11 ,type=ItemType.Animoji},
        new Item { name = "baihu_Animoji", fullname = "items/Animoji/baihu_Animoji", iconid = 0 ,type=ItemType.Animoji},
        new Item { name = "qgirl_Animoji", fullname = "items/Animoji/qgirl_Animoji", iconid = 10 ,type=ItemType.Animoji},
        new Item { name = "frog_Animoji", fullname = "items/Animoji/frog_Animoji", iconid = 4 ,type=ItemType.Animoji},
        new Item { name = "huangya_Animoji", fullname = "items/Animoji/huangya_Animoji", iconid = 7 ,type=ItemType.Animoji},
        new Item { name = "hetun_Animoji", fullname = "items/Animoji/hetun_Animoji", iconid = 6 ,type=ItemType.Animoji},
        new Item { name = "douniuquan_Animoji", fullname = "items/Animoji/douniuquan_Animoji", iconid = 3 ,type=ItemType.Animoji},
        new Item { name = "hashiqi_Animoji", fullname = "items/Animoji/hashiqi_Animoji", iconid = 5 ,type=ItemType.Animoji},
        new Item { name = "baimao_Animoji", fullname = "items/Animoji/baimao_Animoji", iconid = 1 ,type=ItemType.Animoji},
        new Item { name = "kuloutou_Animoji", fullname = "items/Animoji/kuloutou_Animoji", iconid = 9 ,type=ItemType.Animoji},
    };

    //ItemSticker
    public static Item[] item_2 = {
        new Item { name = "fengya_ztt_fu", fullname = "items/ItemSticker/fengya_ztt_fu", iconid = 0 ,type=ItemType.ItemSticker},
        new Item { name = "hudie_lm_fu", fullname = "items/ItemSticker/hudie_lm_fu", iconid = 1 ,type=ItemType.ItemSticker},
        new Item { name = "juanhuzi_lm_fu", fullname = "items/ItemSticker/juanhuzi_lm_fu", iconid = 2 ,type=ItemType.ItemSticker},
        new Item { name = "mask_hat", fullname = "items/ItemSticker/mask_hat", iconid = 3 ,type=ItemType.ItemSticker},
        new Item { name = "touhua_ztt_fu", fullname = "items/ItemSticker/touhua_ztt_fu", iconid = 4 ,type=ItemType.ItemSticker},
        new Item { name = "yazui", fullname = "items/ItemSticker/yazui", iconid = 5 ,type=ItemType.ItemSticker},
        new Item { name = "yuguan", fullname = "items/ItemSticker/yuguan", iconid = 6 ,type=ItemType.ItemSticker},
    };

    //ARMask
    public static Item[] item_3 = {
        new Item { name = "armesh", fullname = "items/ARMask/armesh", iconid = 0 ,type=ItemType.ARMask},
        new Item { name = "baozi", fullname = "items/ARMask/baozi", iconid = 1 ,type=ItemType.ARMask},
        new Item { name = "tiger", fullname = "items/ARMask/tiger", iconid = 2 ,type=ItemType.ARMask},
        new Item { name = "xiongmao", fullname = "items/ARMask/xiongmao", iconid = 3 ,type=ItemType.ARMask},
    };

    //ChangeFace,deprecated
    public static Item[] item_4 = {
        //new Item { name = "mask_guocaijie", fullname = "items/ChangeFace/mask_guocaijie", iconid = 0 ,type=ItemType.ChangeFace},
        //new Item { name = "mask_huangxiaoming", fullname = "items/ChangeFace/mask_huangxiaoming", iconid = 1 ,type=ItemType.ChangeFace},
        //new Item { name = "mask_linzhiling", fullname = "items/ChangeFace/mask_linzhiling", iconid = 2 ,type=ItemType.ChangeFace},
        //new Item { name = "mask_liudehua", fullname = "items/ChangeFace/mask_liudehua", iconid = 3 ,type=ItemType.ChangeFace},
        //new Item { name = "mask_luhan", fullname = "items/ChangeFace/mask_luhan", iconid = 4 ,type=ItemType.ChangeFace},
        //new Item { name = "mask_matianyu", fullname = "items/ChangeFace/mask_matianyu", iconid = 5 ,type=ItemType.ChangeFace},
        //new Item { name = "mask_tongliya", fullname = "items/ChangeFace/mask_tongliya", iconid = 6 ,type=ItemType.ChangeFace},
    };

    //ExpressionRecognition
    public static Item[] item_5 = {
        new Item { name = "future_warrior", fullname = "items/ExpressionRecognition/future_warrior", iconid = 0 ,type=ItemType.ExpressionRecognition,tip ="张嘴试试"},
        new Item { name = "jet_mask", fullname = "items/ExpressionRecognition/jet_mask", iconid = 1 ,type=ItemType.ExpressionRecognition,tip ="鼓腮帮子"},
        new Item { name = "luhantongkuan_ztt_fu", fullname = "items/ExpressionRecognition/luhantongkuan_ztt_fu", iconid = 2 ,type=ItemType.ExpressionRecognition,tip ="眨一眨眼"},
        new Item { name = "qingqing_ztt_fu", fullname = "items/ExpressionRecognition/qingqing_ztt_fu", iconid = 3 ,type=ItemType.ExpressionRecognition,tip ="嘟嘴试试"},
        new Item { name = "sdx2", fullname = "items/ExpressionRecognition/sdx2", iconid = 4 ,type=ItemType.ExpressionRecognition,tip ="皱眉试试"},
        new Item { name = "xiaobianzi_zh_fu", fullname = "items/ExpressionRecognition/xiaobianzi_zh_fu", iconid = 5 ,type=ItemType.ExpressionRecognition,tip ="微笑触发"},
        new Item { name = "xiaoxueshen_ztt_fu", fullname = "items/ExpressionRecognition/xiaoxueshen_ztt_fu", iconid = 6 ,type=ItemType.ExpressionRecognition,tip ="吹气触发"},
    };

    //MusicFilter
    public static Item[] item_6 = {
        new Item { name = "douyin_01", fullname = "items/MusicFilter/douyin_01", iconid = 0 ,type=ItemType.MusicFilter},
        new Item { name = "douyin_02", fullname = "items/MusicFilter/douyin_02", iconid = 1 ,type=ItemType.MusicFilter},
    };

    //BackgroundSegmentation
    public static Item[] item_7 = {
        new Item { name = "hez_ztt_fu", fullname = "items/BackgroundSegmentation/hez_ztt_fu", iconid = 1 ,type=ItemType.BackgroundSegmentation},
        new Item { name = "gufeng_zh_fu", fullname = "items/BackgroundSegmentation/gufeng_zh_fu", iconid = 0 ,type=ItemType.BackgroundSegmentation},
        new Item { name = "xiandai_ztt_fu", fullname = "items/BackgroundSegmentation/xiandai_ztt_fu", iconid = 4 ,type=ItemType.BackgroundSegmentation},
        new Item { name = "sea_lm_fu", fullname = "items/BackgroundSegmentation/sea_lm_fu", iconid = 3 ,type=ItemType.BackgroundSegmentation},
        new Item { name = "ice_lm_fu", fullname = "items/BackgroundSegmentation/ice_lm_fu", iconid = 2 ,type=ItemType.BackgroundSegmentation},
    };

    //GestureRecognition
    public static Item[] item_8 = {
         new Item { name = "ctrl_flower", fullname = "items/GestureRecognition/ctrl_flower", iconid = 0 ,type=ItemType.GestureRecognition,tip ="推出手掌"},
         new Item { name = "ctrl_rain", fullname = "items/GestureRecognition/ctrl_rain", iconid = 1 ,type=ItemType.GestureRecognition,tip ="推出手掌"},
         new Item { name = "ctrl_snow", fullname = "items/GestureRecognition/ctrl_snow", iconid = 2 ,type=ItemType.GestureRecognition,tip ="推出手掌"},
         new Item { name = "ssd_thread_cute", fullname = "items/GestureRecognition/ssd_thread_cute", iconid = 3 ,type=ItemType.GestureRecognition,tip ="双拳靠近脸颊卖萌"},
         new Item { name = "ssd_thread_korheart", fullname = "items/GestureRecognition/ssd_thread_korheart", iconid = 4 ,type=ItemType.GestureRecognition,tip ="单手手指比心"},
         new Item { name = "ssd_thread_six", fullname = "items/GestureRecognition/ssd_thread_six", iconid = 5 ,type=ItemType.GestureRecognition,tip ="比个六"},
    };

    //MagicMirror
    public static Item[] item_9 = {
        new Item { name = "facewarp2", fullname = "items/MagicMirror/facewarp2", iconid = 0 ,type=ItemType.MagicMirror},
        new Item { name = "facewarp3", fullname = "items/MagicMirror/facewarp3", iconid = 1 ,type=ItemType.MagicMirror},
        new Item { name = "facewarp4", fullname = "items/MagicMirror/facewarp4", iconid = 2 ,type=ItemType.MagicMirror},
        new Item { name = "facewarp5", fullname = "items/MagicMirror/facewarp5", iconid = 3 ,type=ItemType.MagicMirror},
        new Item { name = "facewarp6", fullname = "items/MagicMirror/facewarp6", iconid = 4 ,type=ItemType.MagicMirror},
    };

    //PortraitLightEffect,deprecated
    public static Item[] item_10 ={
        //new Item { name = "PortraitLighting_effect_0", fullname = "items/PortraitLightEffect/PortraitLighting_effect_0", iconid = 0 ,type=ItemType.PortraitLightEffect},
        //new Item { name = "PortraitLighting_effect_1", fullname = "items/PortraitLightEffect/PortraitLighting_effect_1", iconid = 1 ,type=ItemType.PortraitLightEffect},
        //new Item { name = "PortraitLighting_effect_2", fullname = "items/PortraitLightEffect/PortraitLighting_effect_2", iconid = 2 ,type=ItemType.PortraitLightEffect},
        //new Item { name = "PortraitLighting_effect_3", fullname = "items/PortraitLightEffect/PortraitLighting_effect_3", iconid = 3 ,type=ItemType.PortraitLightEffect},
        //new Item { name = "PortraitLighting_X_rim", fullname = "items/PortraitLightEffect/PortraitLighting_X_rim", iconid = 4 ,type=ItemType.PortraitLightEffect},
        //new Item { name = "PortraitLighting_X_studio", fullname = "items/PortraitLightEffect/PortraitLighting_X_studio", iconid = 5 ,type=ItemType.PortraitLightEffect},
    };

    //PortraitDrive,deprecated
    public static Item[] item_11 ={
        //new Item { name = "picasso_e1", fullname = "items/PortraitDrive/picasso_e1", iconid = 0,type=ItemType.PortraitDrive},
        //new Item { name = "picasso_e2", fullname = "items/PortraitDrive/picasso_e2", iconid = 1 ,type=ItemType.PortraitDrive},
        //new Item { name = "picasso_e3", fullname = "items/PortraitDrive/picasso_e3", iconid = 2 ,type=ItemType.PortraitDrive},
    };

}


//这个类只是为了方便Texout场景的搭建，建议谨慎参考
//这个类关联了一个美颜参数
public class Beauty
{
    public float currentvalue = 0;  //当前值

    public string name = "";    //参数名字
    public string paramword = "";   //setparam接口所需的关键词
    public float maxvalue = 1;  //最大值
    public float defaultvalue = 0;  //默认值
    public float disablevalue = 0;  //关闭值，用于UI关闭
    public int iconid_0 = -1;   //未选中UI的ID
    public int iconid_1 = -1;   //选中UI的ID

    public BeautySkinType type;
};

//这个类关联了一个质感美颜参数（美妆加美颜）
public class Makeup
{
    public string name = "";    //参数名字
    public float intensity = 1.0f;  //整体强度0~1
    
    public Item item;
}

//美颜类型，在这个场景里质感美颜是美颜的一个子集，实际产品划分上美妆和美颜是平级的
public enum BeautySkinType
{
    None = 0,
    BeautySkin = 1,
    BeautyShape,
    BeautyFilter,
    MakeupGroup,
}

//美妆类型
public enum MakeupType
{
    Lipstick = 0,
    Pupil = 1,
    Eyeshadow,
    Eyeliner,
    Eyelash,
    Eyebrow,
    Blush,
}

public class BeautyConfig
{
    //全局开关 is_beauty_on: 1, 美颜道具开关 0关闭 1开启
    public static Beauty[] beautySkin_1 = {
        new Beauty { name = "精准美肤", paramword = "skin_detect", maxvalue=1, defaultvalue=1, disablevalue=0, iconid_0 = 0,iconid_1=1 ,type = BeautySkinType.BeautySkin},//0-1 int,d=0
        new Beauty { name = "精细磨皮", paramword = "blur_type", maxvalue=2, defaultvalue=2, disablevalue=0, iconid_0 = 2,iconid_1=3 ,type = BeautySkinType.BeautySkin},//0-2 int,d=0 清晰磨皮,d=1 重度磨皮,d=2 精细磨皮 d=3均匀磨皮
        new Beauty { name = "磨皮", paramword = "blur_level", maxvalue=6, defaultvalue=4.2f, disablevalue=0, iconid_0 = 2,iconid_1=3 ,type = BeautySkinType.BeautySkin},//0-6f,
        new Beauty { name = "美白", paramword = "color_level_mode2", maxvalue=1, defaultvalue=0.3f, disablevalue=0, iconid_0 = 4,iconid_1=5 ,type = BeautySkinType.BeautySkin},//0-2f
        new Beauty { name = "红润", paramword = "red_level", maxvalue=1, defaultvalue=0.3f, disablevalue=0, iconid_0 = 6,iconid_1=7 ,type = BeautySkinType.BeautySkin},//0-2f
        new Beauty { name = "亮眼", paramword = "eye_bright", maxvalue=1, defaultvalue=0.0f, disablevalue=0, iconid_0 = 8,iconid_1=9 ,type = BeautySkinType.BeautySkin},//0-1f
        new Beauty { name = "美牙", paramword = "tooth_whiten", maxvalue=1, defaultvalue=0.0f, disablevalue=0, iconid_0 = 10,iconid_1=11 ,type = BeautySkinType.BeautySkin},//0-1f
        new Beauty { name = "去黑眼圈", paramword = "remove_pouch_strength_mode2", maxvalue=1, defaultvalue=0.0f, disablevalue=0, iconid_0 = 12,iconid_1=13 ,type = BeautySkinType.BeautySkin},//0-1f
        new Beauty { name = "去法令纹", paramword = "remove_nasolabial_folds_strength_mode2", maxvalue=1, defaultvalue=0.0f, disablevalue=0, iconid_0 = 14,iconid_1=15 ,type = BeautySkinType.BeautySkin},//0-1f
    };

    public static Beauty[] beautySkin_2 = {
        new Beauty { name = "脸型", paramword = "face_shape", maxvalue=4, defaultvalue=4, disablevalue=-1, iconid_0 = 0,iconid_1=1 ,type = BeautySkinType.BeautyShape},//0-4 int ,d=4
        new Beauty { name = "瘦脸", paramword = "cheek_thinning_mode2", maxvalue=1, defaultvalue=0.0f, disablevalue=0, iconid_0 = 2,iconid_1=3 ,type = BeautySkinType.BeautyShape},//0-1f
        new Beauty { name = "V脸", paramword = "cheek_v", maxvalue=1, defaultvalue=0.5f, disablevalue=0, iconid_0 = 4,iconid_1=5 ,type = BeautySkinType.BeautyShape},//0-1f
        new Beauty { name = "窄脸", paramword = "cheek_narrow_mode2", maxvalue=1, defaultvalue=0.0f, disablevalue=0, iconid_0 = 6,iconid_1=7 ,type = BeautySkinType.BeautyShape},//0-1f
        new Beauty { name = "短脸", paramword = "cheek_short", maxvalue=1, defaultvalue=0.0f, disablevalue=0, iconid_0 = 8,iconid_1=9 ,type = BeautySkinType.BeautyShape},//0-1f
        new Beauty { name = "小脸", paramword = "cheek_small_mode2", maxvalue=1, defaultvalue=0.0f, disablevalue=0, iconid_0 = 32,iconid_1=33 ,type = BeautySkinType.BeautyShape},//0-1f
        new Beauty { name = "大眼", paramword = "eye_enlarging_mode3", maxvalue=1, defaultvalue=0.4f, disablevalue=0, iconid_0 = 10,iconid_1=11 ,type = BeautySkinType.BeautyShape},//0-1f
        new Beauty { name = "下巴", paramword = "intensity_chin", maxvalue=1, defaultvalue=0.3f, disablevalue=0.5f, iconid_0 = 12,iconid_1=13 ,type = BeautySkinType.BeautyShape},//0-1f
        new Beauty { name = "额头", paramword = "intensity_forehead_mode2", maxvalue=1, defaultvalue=0.3f, disablevalue=0.5f, iconid_0 = 14,iconid_1=15 ,type = BeautySkinType.BeautyShape},//0-1f
        new Beauty { name = "瘦鼻", paramword = "intensity_nose_mode2", maxvalue=1, defaultvalue=0.5f, disablevalue=0, iconid_0 = 16,iconid_1=17 ,type = BeautySkinType.BeautyShape},//0-1f
        new Beauty { name = "嘴型", paramword = "intensity_mouth_mode3", maxvalue=1, defaultvalue=0.4f, disablevalue=0.5f, iconid_0 = 18,iconid_1=19 ,type = BeautySkinType.BeautyShape},//0-1f
        new Beauty { name = "开眼角", paramword = "intensity_canthus", maxvalue=1, defaultvalue=0.0f, disablevalue=0.0f, iconid_0 = 20,iconid_1=21 ,type = BeautySkinType.BeautyShape},//0-1f
        new Beauty { name = "眼距", paramword = "intensity_eye_space", maxvalue=1, defaultvalue=0.5f, disablevalue=0.5f, iconid_0 = 22,iconid_1=23 ,type = BeautySkinType.BeautyShape},//0-1f
        new Beauty { name = "眼睛角度", paramword = "intensity_eye_rotate", maxvalue=1, defaultvalue=0.5f, disablevalue=0.5f, iconid_0 = 24,iconid_1=25 ,type = BeautySkinType.BeautyShape},//0-1f
        new Beauty { name = "长鼻", paramword = "intensity_long_nose", maxvalue=1, defaultvalue=0.5f, disablevalue=0.5f, iconid_0 = 26,iconid_1=27 ,type = BeautySkinType.BeautyShape},//0-1f
        new Beauty { name = "缩人中", paramword = "intensity_philtrum", maxvalue=1, defaultvalue=0.5f, disablevalue=0.5f, iconid_0 = 28,iconid_1=29 ,type = BeautySkinType.BeautyShape},//0-1f
        new Beauty { name = "微笑嘴角", paramword = "intensity_smile", maxvalue=1, defaultvalue=0.0f, disablevalue=0.0f, iconid_0 = 30,iconid_1=31 ,type = BeautySkinType.BeautyShape},//0-1f
    };

    public static Beauty[] beautySkin_3 = {
        new Beauty { name = "原图", paramword = "origin", maxvalue=1, defaultvalue=0.7f, iconid_0 = 0,type = BeautySkinType.BeautyFilter},//0-1f
        new Beauty { name = "白亮", paramword = "bailiang2", maxvalue=1, defaultvalue=0.7f, iconid_0 = 1,type = BeautySkinType.BeautyFilter},
        new Beauty { name = "粉嫩", paramword = "fennen1", maxvalue=1, defaultvalue=0.4f, iconid_0 = 2,type = BeautySkinType.BeautyFilter},
        new Beauty { name = "小清新", paramword = "xiaoqingxin6", maxvalue=1, defaultvalue=0.7f, iconid_0 = 3,type = BeautySkinType.BeautyFilter},
        new Beauty { name = "冷色调", paramword = "lengsediao1", maxvalue=1, defaultvalue=0.7f, iconid_0 = 4,type = BeautySkinType.BeautyFilter},
        new Beauty { name = "暖色调", paramword = "nuansediao1", maxvalue=1, defaultvalue=0.7f, iconid_0 = 5,type = BeautySkinType.BeautyFilter},
    };
 
    public static Makeup[] makeupGroup_1 =
    {
        new Makeup { name="卸妆", intensity = 0.0f,
            item = new Item { name = "", fullname = "", iconid = 0 ,type = ItemType.Makeup}},
        //new Makeup { name="嗲嗲兔", intensity = 1.0f,
        //    item = new Item { name = "diadiatu", fullname = "items/Makeup/diadiatu", iconid = 14 ,type = ItemType.Makeup}},
        //new Makeup { name="冻龄", intensity = 1.0f,
        //    item = new Item { name = "dongling", fullname = "items/Makeup/dongling", iconid = 15 ,type = ItemType.Makeup}},
        // new Makeup { name="国风", intensity = 1.0f,
        //    item = new Item { name = "guofeng", fullname = "items/Makeup/guofeng", iconid = 16 ,type = ItemType.Makeup}},
        //new Makeup { name="混血", intensity = 1.0f,
        //    item = new Item { name = "hunxue", fullname = "items/Makeup/hunxue", iconid = 17 ,type = ItemType.Makeup}},
        new Makeup { name="减龄", intensity = 1.0f,
            item = new Item { name = "jianling", fullname = "items/Makeup/jianling", iconid = 1, type = ItemType.Makeup}},
        new Makeup { name="暖冬", intensity = 1.0f,
            item = new Item { name = "nuandong", fullname = "items/Makeup/nuandong", iconid = 2, type = ItemType.Makeup}},
        new Makeup { name="红枫", intensity = 1.0f,
            item = new Item { name = "hongfeng", fullname = "items/Makeup/hongfeng", iconid = 3, type = ItemType.Makeup}},
        new Makeup { name="Rose", intensity = 1.0f,
            item = new Item { name = "rose", fullname = "items/Makeup/rose", iconid = 4, type = ItemType.Makeup}},
        new Makeup { name="少女", intensity = 1.0f,
            item = new Item { name = "shaonv", fullname = "items/Makeup/shaonv", iconid = 5, type = ItemType.Makeup}},
        new Makeup { name="紫韵", intensity = 1.0f,
            item = new Item { name = "ziyun", fullname = "items/Makeup/ziyun", iconid = 6, type = ItemType.Makeup}},
        new Makeup { name="厌世猫", intensity = 1.0f,
            item = new Item { name = "yanshimao", fullname = "items/Makeup/yanshimao", iconid = 7, type = ItemType.Makeup}},
        new Makeup { name="人鱼", intensity = 1.0f,
            item = new Item { name = "renyu", fullname = "items/Makeup/renyu", iconid = 8, type = ItemType.Makeup}},
        new Makeup { name="初秋", intensity = 1.0f,
            item = new Item { name = "chuqiu", fullname = "items/Makeup/chuqiu", iconid = 9, type = ItemType.Makeup}},
        new Makeup { name="千纸鹤", intensity = 1.0f,
            item = new Item { name = "qianzhihe", fullname = "items/Makeup/qianzhihe", iconid = 10, type = ItemType.Makeup}},
        new Makeup { name="超模", intensity = 1.0f,
            item = new Item { name = "chaomo", fullname = "items/Makeup/chaomo", iconid = 11, type = ItemType.Makeup}},
        new Makeup { name="雏菊", intensity = 1.0f,
            item = new Item { name = "chuju", fullname = "items/Makeup/chuju", iconid = 12, type = ItemType.Makeup}},
        new Makeup { name="港风", intensity = 1.0f,
            item = new Item { name = "gangfeng", fullname = "items/Makeup/gangfeng", iconid = 13, type = ItemType.Makeup}},
    };
}
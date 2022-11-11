using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class UIManagerForTexOut : MonoBehaviour
{
    //这个组件是Texout场景的UI控制器
    RenderToTexture rtt;

    public GameObject Canvas_BackUI;                                              //后景UI，包括rtt.RawImg_BackGroud
    public Camera Camera_BackUI;
    public GameObject RawImage_Pic;
    public Button Btn_Cancel;
    public Button Btn_SavePic;

    public GameObject Canvas_TopUI;                                               //主界面UI
    public Camera Camera_TopUI;
    public GameObject[] ItemSelecters;

    public GameObject Canvas_FrontUI; //前景UI
    public Camera Camera_FrontUI;
    public Button Btn_Switch;
    public Button Btn_Back;
    public GameObject Image_FaceDetect;

    public Button Btn_TakePic_mini_1;   //美颜相关UI
    public GameObject BeautySkinSelecter;
    public GameObject[] BeautySkinSelecterOptions;
    public GameObject BeautySkinContent;
    public GameObject[] BeautySkinContentPanels;
    public GameObject BeautySkin_UIExample;
    public GameObject BeautyFilter_UIExample;
    public Tips tips;    //一些功能点击后的提示UI
    
    public Slider BeautySkin_Slider;
    public RectTransform BeautyOptionContentTrans;

    public Button Btn_TakePic_mini_2;                                             //道具相关UI
    public GameObject Item_Content;
    public ToggleGroup Item_ToggleGroup;
    public RectTransform ItemOptionContentTrans;
    public GameObject Item_UIExample;
    public GameObject Item_Disable;
    public Text Item_Tip;

    Coroutine musiccor = null;
    AudioSource audios;

    Dictionary<Beauty, GameObject> BeautyGOs = new Dictionary<Beauty, GameObject>();
    Dictionary<Makeup, GameObject> MakeupGOs = new Dictionary<Makeup, GameObject>();
    GameObject currentSelected;
    string currentSelectedMakeupName = BeautyConfig.makeupGroup_1[0].name;
    string BeautySkinItemName;
    string MakeupItemName;

    BeautySkinType currentBeautySkinType = BeautySkinType.None;

    ItemType currentItemType = ItemType.Undefine;
    //权限码
    //fu_GetModuleCode返回的值是以下code的集合，与一下就能知道是否有相应权限
    private static int[] permissions_code = {
            0x1,                    //0     //美颜
            0x10,                   //1     //Animoji
            0x2 | 0x4,              //2     //道具贴纸
            0x20 | 0x40,            //3     //AR面具
            0x80,                   //4     //换脸
            0x800,                  //5     //表情识别
            0x20000,                //6     //音乐滤镜
            0x100,                  //7     //背景分割
            0x200,                  //8     //手势识别
            0x10000,                //9     //哈哈镜
            0x4000,                 //10    //人像光效
            0x8000,                 //11    //人像驱动
            0x80000,                //12    //美妆
            0x40000,                //13    //情绪识别
            0x100000,               //14    //美发
            0x200000,               //15    //动漫滤镜
            0x400000,               //16    //手势跟踪
            0x800000,               //17    //海报换脸
    };
    private static bool[] permissions;
    Sprites uisprites;

    enum SlotForItems   //道具的槽位
    {
        Beauty = 0,
        Item = 1,
        CommonFilter,
        Makeup,
        Makeup_bundle
    };

    void Awake()
    {
        rtt = GetComponent<RenderToTexture>();
        audios = GetComponent<AudioSource>();
        uisprites = GetComponent<Sprites>();
        FaceunityWorker.Instance.OnInitOK += InitApplication;
    }

    void Start()
    {
        Canvas_TopUI.SetActive(true);
        Canvas_FrontUI.SetActive(true);
        Canvas_BackUI.SetActive(true);
        CloseBeautySkinUI();
        CloseItemUI();
    }

    void InitApplication(object source, EventArgs e)
    {
        StartCoroutine(Authentication());
    }

    void Update()
    {
        //TODO:根据场景处理
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    //获取License授权信息，这里只是根据SDK用License初始化后的结果，来控制不同类型道具的UI的开启关闭，具体权限分类请咨询技术支持
    //顺便根据授权信息初始化相关道具
    IEnumerator Authentication()
    {
        while (FaceunityWorker.fuIsLibraryInit() == 0)
            yield return Util.end_of_frame;
        int code = FaceunityWorker.fuGetModuleCode(0);
        Debug.Log("fu_GetModuleCode:" + code);
        permissions = new bool[permissions_code.Length];
        for (int i = 0; i < permissions_code.Length; i++)
        {
            if ((code & permissions_code[i]) == permissions_code[i])
            {
                permissions[i] = true;
                SetItemTypeEnable(i, true);
            }
            else
            {
                permissions[i] = false;
                Debug.Log("权限未获取:" + permissions_code[i]);
                SetItemTypeEnable(i, false);
            }
        }
        if (permissions[0])
        {
            //美颜
            BeautySkinItemName = ItemConfig.beautySkin[0].name;
            //yield return rtt.LoadItem(ItemConfig.beautySkin[0], (int)SlotForItems.Beauty);
            //for (int i = 0; i < BeautyConfig.beautySkin_1.Length; i++)
            //{
            //    rtt.SetItemParamd(BeautySkinItemName, BeautyConfig.beautySkin_1[i].paramword, BeautyConfig.beautySkin_1[i].defaultvalue);
            //}
            //for (int i = 0; i < BeautyConfig.beautySkin_2.Length; i++)
            //{
            //    rtt.SetItemParamd(BeautySkinItemName, BeautyConfig.beautySkin_2[i].paramword, BeautyConfig.beautySkin_2[i].defaultvalue);
            //}
        }
        if (permissions[12])
        {
            //新版美妆
            //yield return rtt.LoadItem(ItemConfig.makeup[0], (int)SlotForItems.Makeup);
            MakeupItemName = ItemConfig.makeup[0].name;
        }
        if (permissions[15])
        {
            //动漫滤镜
            //yield return rtt.LoadItem(ItemConfig.commonFilter[0], (int)SlotForItems.CommonFilter);
            //rtt.SetItemParamd((int)SlotForItems.CommonFilter, "style", 0);
        }
        //FXAA
        //yield return rtt.LoadItem(ItemConfig.commonFilter[1], (int)SlotForItems.CommonFilter);
        RegisterUIFunc();
    }

    IEnumerator LoadBeautyBundle()
    {
        var tempslot = rtt.GetSlotIDbyName(ItemConfig.beautySkin[0].name);

        yield return rtt.LoadItem(ItemConfig.beautySkin[0], (int)SlotForItems.Beauty);

        if (tempslot < 0)
        {
            for (int i = 0; i < BeautyConfig.beautySkin_1.Length; i++)
            {
                rtt.SetItemParamd(BeautySkinItemName, BeautyConfig.beautySkin_1[i].paramword, BeautyConfig.beautySkin_1[i].defaultvalue);
            }
            for (int i = 0; i < BeautyConfig.beautySkin_2.Length; i++)
            {
                rtt.SetItemParamd(BeautySkinItemName, BeautyConfig.beautySkin_2[i].paramword, BeautyConfig.beautySkin_2[i].defaultvalue);
            }
            rtt.SetItemParams(BeautySkinItemName, "filter_name", BeautyConfig.beautySkin_3[0].paramword);
            rtt.SetItemParamd(BeautySkinItemName, "filter_level", BeautyConfig.beautySkin_3[0].defaultvalue);
        }
    }

    IEnumerator LoadMakeupBundle()
    {
        yield return rtt.LoadItem(ItemConfig.makeup[0], (int)SlotForItems.Makeup);
    }


    void SetItemTypeEnable(int i, bool ifenable)
    {
        if (i < ItemSelecters.Length && ItemSelecters[i])
        {
            ItemSelecters[i].transform.Find("Image_On").gameObject.SetActive(ifenable);
            ItemSelecters[i].transform.Find("Image_Off").gameObject.SetActive(!ifenable);
            ItemSelecters[i].transform.Find("Image_bg").GetComponent<Image>().raycastTarget = ifenable;
        }
    }

    //for循环配合delegate是个坑，小心。
    void RegisterUIFunc()
    {
        Btn_Switch.onClick.AddListener(delegate
        {
            CameraManager.Instance.SwitchCamera();
        });
        Btn_Back.onClick.AddListener(delegate
        {
            if (musiccor != null)
            {
                StopCoroutine(musiccor);
                musiccor = null;
                audios.Stop();
            }
            Canvas_TopUI.SetActive(true);
            for (int i = 0; i < rtt.slot_items.Length; i++)
            {
                if (i == (int)SlotForItems.CommonFilter)
                    continue;
                rtt.UnLoadItem(i);
            }
            CloseBeautySkinUI();
            CloseItemUI();
        });
        Btn_Cancel.onClick.AddListener(OnCancelTakePicture);
        Btn_SavePic.onClick.AddListener(OnSavePicture);
        Btn_TakePic_mini_1.onClick.AddListener(TakePicture);
        Btn_TakePic_mini_2.onClick.AddListener(TakePicture);

        for (int i = 0; i < ItemSelecters.Length; i++)
        {
            if (ItemSelecters[i].activeSelf)
                ItemSelecters[i].GetComponent<AddClickEvent>().AddListener(delegate (GameObject go)
                {
                    if (go.name.CompareTo(ItemType.Beauty.ToString()) == 0)
                    {
                        StartCoroutine(OpenBeautySkinUI(BeautySkinType.None));
                    }
                    else
                    {
                        OpenItemsUI(go.name);
                    }
                    Canvas_TopUI.SetActive(false);
                });
        }

        rtt.rawimg_backgroud.GetComponent<AddClickEvent>().AddListener(delegate
        {
            if (currentItemType == ItemType.Beauty)
            {
                CloseAllBeautySkinContent();
                BeautySkinContentPanels[2].SetActive(true);
            }
        });

        BeautySkinSelecterOptions[0].GetComponent<AddClickEvent>().AddListener(delegate { StartCoroutine(OpenBeautySkinUI(BeautySkinType.BeautySkin)); });
        BeautySkinSelecterOptions[1].GetComponent<AddClickEvent>().AddListener(delegate { StartCoroutine(OpenBeautySkinUI(BeautySkinType.BeautyShape)); });
        BeautySkinSelecterOptions[2].GetComponent<AddClickEvent>().AddListener(delegate { StartCoroutine(OpenBeautySkinUI(BeautySkinType.BeautyFilter)); });
        BeautySkinSelecterOptions[3].GetComponent<AddClickEvent>().AddListener(delegate { StartCoroutine(OpenBeautySkinUI(BeautySkinType.MakeupGroup)); });
    }

    void UnRegisterUIFunc()
    {
        Btn_Switch.onClick.RemoveAllListeners();
        Btn_Back.onClick.RemoveAllListeners();
        Btn_Cancel.onClick.RemoveAllListeners();
        Btn_SavePic.onClick.RemoveAllListeners();
        Btn_TakePic_mini_1.onClick.RemoveAllListeners();
        Btn_TakePic_mini_2.onClick.RemoveAllListeners();
        for (int i = 0; i < ItemSelecters.Length; i++)
        {
            ItemSelecters[i].GetComponent<AddClickEvent>().RemoveAllListener();
        }

        rtt.rawimg_backgroud.GetComponent<AddClickEvent>().RemoveAllListener();
        BeautySkinSelecterOptions[0].GetComponent<AddClickEvent>().RemoveAllListener();
        BeautySkinSelecterOptions[1].GetComponent<AddClickEvent>().RemoveAllListener();
        BeautySkinSelecterOptions[2].GetComponent<AddClickEvent>().RemoveAllListener();
        BeautySkinSelecterOptions[3].GetComponent<AddClickEvent>().RemoveAllListener();
    }

    void SwitchPicGos(bool ifenable)
    {
        RawImage_Pic.SetActive(ifenable);
        Btn_Cancel.gameObject.SetActive(ifenable);
        Btn_SavePic.gameObject.SetActive(ifenable);
    }

    void TakePicture()
    {
        RawImage_Pic.GetComponent<RawImage>().texture = Util.CaptureCamera(new Camera[] { Camera_BackUI }, new Rect(0, 0, Screen.width, Screen.height));
        Canvas_FrontUI.SetActive(false);
        SwitchPicGos(true);
    }

    void OnCancelTakePicture()
    {
        SwitchPicGos(false);
        Canvas_FrontUI.SetActive(true);
    }

    public void OnSavePicture()
    {
        Util.SaveTex2D((Texture2D)RawImage_Pic.GetComponent<RawImage>().texture);
        OnCancelTakePicture();
    }

    #region BeautySkinUI

    IEnumerator OpenBeautySkinUI(BeautySkinType type)
    {
        if (type == BeautySkinType.None)
        {
            yield return LoadBeautyBundle();
            yield return LoadMakeupBundle();
        }

        currentBeautySkinType = type;
        currentItemType = ItemType.Beauty;

        CloseAllBeautySkinContent();
        GameObject panel = BeautySkinContentPanels[0];
        BeautyOptionContentTrans.localPosition = Vector3.zero;
        var layout = BeautyOptionContentTrans.GetComponent<HorizontalLayoutGroup>();
        layout.padding.left = 20;
        layout.padding.right = 20;
        BeautySkinContentPanels[2].SetActive(true);
        ClearBeautySkinOptions();

        if (type == BeautySkinType.BeautySkin)
        {
            BeautySkinSelecterOptions[0].GetComponent<Text>().color = FuConst.HIGHT_LIGHT_COLOR;

            AddBeautySkinOptions(0, BeautyConfig.beautySkin_1[0]).GetComponent<AddClickEvent>().AddListener(delegate (GameObject go)
            {
                Beauty bi = BeautyConfig.beautySkin_1[0];
                if (currentSelected != go)
                {
                    currentSelected = go;
                    UnSelectAllBeautySkinOptions();
                    go.GetComponentInChildren<Image>().sprite = uisprites.GetSprite(bi.type, bi.iconid_1);
                    go.GetComponentInChildren<Text>().color = FuConst.HIGHT_LIGHT_COLOR;
                    BeautySkinContentPanels[1].SetActive(false);
                }
                else
                {
                    bi.currentvalue = bi.currentvalue == bi.disablevalue ? bi.maxvalue : bi.disablevalue;
                    rtt.SetItemParamd(BeautySkinItemName, BeautyConfig.beautySkin_1[0].paramword, bi.currentvalue);
                    SwitchBeautyOptionUIState(bi, go);
                }
            });

            GameObject bgo1 = AddBeautySkinOptions(1, BeautyConfig.beautySkin_1[2]);
            bgo1.GetComponent<AddClickEvent>().AddListener(delegate (GameObject go)
            {
                Beauty bi1 = BeautyConfig.beautySkin_1[1];
                Beauty bi2 = BeautyConfig.beautySkin_1[2];

                if (currentSelected != go)
                {
                    currentSelected = go;
                    UnSelectAllBeautySkinOptions();
                    go.GetComponentInChildren<Image>().sprite = uisprites.GetSprite(bi2.type, bi2.iconid_1);
                    go.GetComponentInChildren<Text>().color = FuConst.HIGHT_LIGHT_COLOR;
                    BeautySkin_Slider.onValueChanged.RemoveAllListeners();
                    BeautySkin_Slider.minValue = 0;
                    BeautySkin_Slider.maxValue = bi2.maxvalue;
                    BeautySkin_Slider.value = bi2.currentvalue;
                    BeautySkin_Slider.onValueChanged.AddListener(delegate
                    {
                        bi2.currentvalue = BeautySkin_Slider.value;
                        rtt.SetItemParamd(BeautySkinItemName, bi2.paramword, bi2.currentvalue);
                        SwitchBeautyOptionUIState(bi2, go);
                    });
                    BeautySkinContentPanels[1].SetActive(true);
                }
                else
                {
                    bi1.currentvalue++;
                    bi1.currentvalue = bi1.currentvalue % (bi1.maxvalue + 2); //0~2 循环
                    //bi1.currentvalue = bi1.currentvalue == bi1.disablevalue ? bi1.maxvalue : bi1.disablevalue;
                    if (bi1.currentvalue == 0)
                    {
                        go.GetComponentInChildren<Text>().text = FuConst.CLEAR_GRINDING;         //"清晰磨皮";
                    }
                    else if (bi1.currentvalue == 1)
                    {
                        go.GetComponentInChildren<Text>().text = FuConst.SEVERE_GRINDING;         // "重度磨皮";
                    }
                    else if (bi1.currentvalue == 2)
                    {
                        go.GetComponentInChildren<Text>().text = FuConst.FINE_GRINDING;            //"精细磨皮";
                    }
                    else if (bi1.currentvalue == 3)
                    {
                        go.GetComponentInChildren<Text>().text = FuConst.UNIFORM_GRINDING;            //"均匀磨皮";
                    }
                    rtt.SetItemParamd(BeautySkinItemName, BeautyConfig.beautySkin_1[1].paramword, bi1.currentvalue);
                }
            });
            if (BeautyConfig.beautySkin_1[1].currentvalue == 0)
            {
                bgo1.GetComponentInChildren<Text>().text = FuConst.CLEAR_GRINDING;                 //"清晰磨皮";
            }
            else if (BeautyConfig.beautySkin_1[1].currentvalue == 1)
            {
                bgo1.GetComponentInChildren<Text>().text = FuConst.SEVERE_GRINDING;                //"重度磨皮";
            }
            else if (BeautyConfig.beautySkin_1[1].currentvalue == 2)
            {
                bgo1.GetComponentInChildren<Text>().text = FuConst.FINE_GRINDING;                   //"精细磨皮";
            }
            else if (BeautyConfig.beautySkin_1[1].currentvalue == 3)
            {
                bgo1.GetComponentInChildren<Text>().text = FuConst.UNIFORM_GRINDING;            //"均匀磨皮";
            }
            for (int i = 3; i < BeautyConfig.beautySkin_1.Length; i++)
            {
                AddBeautySkinOptions(i, BeautyConfig.beautySkin_1[i]).GetComponent<AddClickEvent>().AddListener(delegate (GameObject go)
                 {
                     if (currentSelected != go)
                     {
                         Beauty bi = null;
                         foreach (var bgo in BeautyGOs)
                         {
                             if (bgo.Value == go)
                                 bi = bgo.Key;
                         }
                         if (bi == null)
                         {
                             Debug.Log("Undefined BeautyGO!!! name=" + go.name);
                             return;
                         }
                         currentSelected = go;
                         UnSelectAllBeautySkinOptions();
                         go.GetComponentInChildren<Image>().sprite = uisprites.GetSprite(bi.type, bi.iconid_1);
                         go.GetComponentInChildren<Text>().color = FuConst.HIGHT_LIGHT_COLOR;
                         BeautySkin_Slider.onValueChanged.RemoveAllListeners();
                         BeautySkin_Slider.minValue = 0;
                         BeautySkin_Slider.maxValue = bi.maxvalue;
                         BeautySkin_Slider.value = bi.currentvalue;
                         BeautySkin_Slider.onValueChanged.AddListener(delegate
                             {
                                 bi.currentvalue = BeautySkin_Slider.value;
                                 rtt.SetItemParamd(BeautySkinItemName, bi.paramword, bi.currentvalue);
                                 SwitchBeautyOptionUIState(bi, go);
                             });
                         BeautySkinContentPanels[1].SetActive(true);
                     }
                 });
            }

            panel.SetActive(true);
            BeautySkinContentPanels[3].SetActive(true);
        }
        else if (type == BeautySkinType.BeautyShape)
        {
            BeautySkinSelecterOptions[1].GetComponent<Text>().color = FuConst.HIGHT_LIGHT_COLOR;

            for (int i = 1; i < BeautyConfig.beautySkin_2.Length; i++)
            {
                AddBeautySkinOptions(i, BeautyConfig.beautySkin_2[i]).GetComponent<AddClickEvent>().AddListener(delegate (GameObject go)
                {
                    if (currentSelected != go)
                    {
                        Beauty bi = null;
                        foreach (var bgo in BeautyGOs)
                        {
                            if (bgo.Value == go)
                                bi = bgo.Key;
                        }
                        if (bi == null)
                        {
                            Debug.Log("Undefined BeautyGO!!! name=" + go.name);
                            return;
                        }
                        if(!bi.enable)
                        {
                            Debug.Log(bi.tipsStr);
                            if(tips != null)
                                tips.ShowAndSetText(bi.tipsStr);
                            return;
                        }
                        currentSelected = go;
                        UnSelectAllBeautySkinOptions();
                        go.GetComponentInChildren<Image>().sprite = uisprites.GetSprite(bi.type, bi.iconid_1);
                        go.GetComponentInChildren<Text>().color = FuConst.HIGHT_LIGHT_COLOR;
                        BeautySkin_Slider.onValueChanged.RemoveAllListeners();
                        BeautySkin_Slider.minValue = 0;
                        BeautySkin_Slider.maxValue = bi.maxvalue;
                        BeautySkin_Slider.value = bi.currentvalue;
                        BeautySkin_Slider.onValueChanged.AddListener(delegate
                        {
                            bi.currentvalue = BeautySkin_Slider.value;
                            rtt.SetItemParamd(BeautySkinItemName, bi.paramword, bi.currentvalue);
                            SwitchBeautyOptionUIState(bi, go);
                        });
                        BeautySkinContentPanels[1].SetActive(true);
                    }
                });
            }
            OpenBeautyShapeUI();

            panel.SetActive(true);
            BeautySkinContentPanels[1].SetActive(false);
            BeautySkinContentPanels[3].SetActive(true);
        }
        else if (type == BeautySkinType.BeautyFilter)
        {
            BeautySkinSelecterOptions[2].GetComponent<Text>().color = FuConst.HIGHT_LIGHT_COLOR;
            string currentfiltername = rtt.GetItemParams(BeautySkinItemName, "filter_name");
            foreach (var bi in BeautyConfig.beautySkin_3)
            {
                GameObject go = AddBeautyFilterOptions(bi);
                if (string.Compare(bi.paramword, currentfiltername, true) == 0)
                {
                    currentSelected = go;
                    go.transform.Find("Image_bg").gameObject.SetActive(true);
                }
            }

            BeautySkin_Slider.onValueChanged.RemoveAllListeners();
            BeautySkin_Slider.minValue = 0;
            BeautySkin_Slider.maxValue = 1;
            BeautySkin_Slider.value = (float)rtt.GetItemParamd(BeautySkinItemName, "filter_level");
            BeautySkin_Slider.onValueChanged.AddListener(delegate
            {
                rtt.SetItemParamd(BeautySkinItemName, "filter_level", BeautySkin_Slider.value);
            });
            panel.SetActive(true);
            BeautySkinContentPanels[1].SetActive(true);
            BeautySkinContentPanels[2].SetActive(false);
            BeautySkinContentPanels[3].SetActive(true);
        }
        else if (type == BeautySkinType.MakeupGroup)
        {
            BeautySkinSelecterOptions[3].GetComponent<Text>().color = FuConst.HIGHT_LIGHT_COLOR;

            foreach (var mg in BeautyConfig.makeupGroup_1)
            {
                GameObject go = AddMakeupOptions(mg);
                if (string.Compare(mg.name, currentSelectedMakeupName, true) == 0)
                {
                    //currentSelected = go;
                    //go.transform.Find("Image_bg").gameObject.SetActive(true);
                    go.GetComponent<AddClickEvent>().onClick(go);
                }
            }
            if (string.Compare(BeautyConfig.makeupGroup_1[0].name, currentSelectedMakeupName, true) != 0)
            {
                BeautySkin_Slider.onValueChanged.RemoveAllListeners();
                BeautySkin_Slider.minValue = 0;
                BeautySkin_Slider.maxValue = 1;
                BeautySkin_Slider.value = (float)rtt.GetItemParamd(MakeupItemName, "makeup_intensity");
                BeautySkin_Slider.onValueChanged.AddListener(delegate
                {
                    rtt.SetItemParamd(MakeupItemName, "makeup_intensity", BeautySkin_Slider.value);
                });
                BeautySkinContentPanels[1].SetActive(true);
            }
            else
            {
                BeautySkin_Slider.onValueChanged.RemoveAllListeners();
                BeautySkinContentPanels[1].SetActive(false);
            }

            panel.SetActive(true);
            BeautySkinContentPanels[3].SetActive(true);
        }

        BeautySkinContent.SetActive(true);
        BeautySkinSelecter.SetActive(true);
    }

    GameObject AddBeautySkinOptions(int name, Beauty beautyitem)
    {
        beautyitem.currentvalue = (float)rtt.GetItemParamd(BeautySkinItemName, beautyitem.paramword);
        GameObject option = Instantiate(BeautySkin_UIExample);
        option.transform.SetParent(BeautyOptionContentTrans, false);
        option.transform.localScale = Vector3.one;
        option.transform.localPosition = Vector3.zero;
        option.name = name.ToString();
        option.GetComponentInChildren<Text>().text = beautyitem.name;
        option.GetComponentInChildren<Image>().sprite = uisprites.GetSprite(beautyitem.type, beautyitem.iconid_0);

        if (BeautyGOs.ContainsKey(beautyitem))
            BeautyGOs.Remove(beautyitem);
        BeautyGOs.Add(beautyitem, option);
        SwitchBeautyOptionUIState(beautyitem, option);
        return option;
    }

    void OpenBeautyShapeUI()
    {
        var layout = BeautyOptionContentTrans.GetComponent<HorizontalLayoutGroup>();
        layout.padding.left = 20;
        layout.padding.right = 20;
        for (int i = 1; i < BeautyConfig.beautySkin_2.Length; i++)
        {
            BeautyGOs[BeautyConfig.beautySkin_2[i]].SetActive(true);
        }
    }

    GameObject AddBeautyFilterOptions(Beauty beautyitem)
    {
        GameObject option = Instantiate(BeautyFilter_UIExample);
        option.transform.SetParent(BeautyOptionContentTrans, false);
        option.transform.localScale = Vector3.one;
        option.transform.localPosition = Vector3.zero;
        option.name = beautyitem.name;
        option.GetComponentInChildren<Text>().text = beautyitem.name;
        option.GetComponentInChildren<Image>().sprite = uisprites.GetSprite(beautyitem.type, beautyitem.iconid_0);

        if (BeautyGOs.ContainsKey(beautyitem))
            BeautyGOs.Remove(beautyitem);
        BeautyGOs.Add(beautyitem, option);
        option.GetComponent<AddClickEvent>().AddListener(delegate (GameObject go)
        {
            if (go != currentSelected)
            {
                currentSelected = go;
                foreach (var bgo in BeautyGOs)
                {
                    bgo.Value.transform.Find("Image_bg").gameObject.SetActive(false);
                }
                go.transform.Find("Image_bg").gameObject.SetActive(true);
            }
            rtt.SetItemParams(BeautySkinItemName, "filter_name", beautyitem.paramword);
            rtt.SetItemParamd(BeautySkinItemName, "filter_level", beautyitem.defaultvalue);
            BeautySkin_Slider.value = beautyitem.defaultvalue;
        });
        return option;
    }

    GameObject AddMakeupOptions(Makeup makeupitem)
    {
        GameObject option = Instantiate(BeautyFilter_UIExample);
        option.transform.SetParent(BeautyOptionContentTrans, false);
        option.transform.localScale = Vector3.one;
        option.transform.localPosition = Vector3.zero;
        option.name = makeupitem.name;
        option.GetComponentInChildren<Text>().text = makeupitem.name;
        option.GetComponentInChildren<Image>().sprite = uisprites.GetSprite(ItemType.Makeup, makeupitem.item.iconid);

        if (MakeupGOs.ContainsKey(makeupitem))
            MakeupGOs.Remove(makeupitem);
        MakeupGOs.Add(makeupitem, option);
        option.GetComponent<AddClickEvent>().AddListener(delegate (GameObject go)
        {
            if (go != currentSelected)
            {
                currentSelected = go;
                foreach (var bgo in MakeupGOs)
                {
                    bgo.Value.transform.Find("Image_bg").gameObject.SetActive(false);
                }
                go.transform.Find("Image_bg").gameObject.SetActive(true);
            }

            currentSelectedMakeupName = makeupitem.name;

            if (makeupitem.intensity <= 0)
            {
                rtt.SetItemParamd((int)SlotForItems.Makeup, "is_makeup_on", 0);
            }
            else
            {
                rtt.SetItemParamd((int)SlotForItems.Makeup, "is_makeup_on", 1);
                rtt.SetItemParamd((int)SlotForItems.Makeup, "makeup_intensity", makeupitem.intensity);
            }

            if (string.Compare(BeautyConfig.makeupGroup_1[0].name, makeupitem.name, true) != 0)
            {
                StartCoroutine(LoadMakeupBundle(makeupitem));

                BeautySkin_Slider.onValueChanged.RemoveAllListeners();
                BeautySkin_Slider.minValue = 0;
                BeautySkin_Slider.maxValue = 1;
                BeautySkin_Slider.value = (float)rtt.GetItemParamd(MakeupItemName, "makeup_intensity");
                BeautySkin_Slider.onValueChanged.AddListener(delegate
                {
                    rtt.SetItemParamd(MakeupItemName, "makeup_intensity", BeautySkin_Slider.value);
                });
                BeautySkinContentPanels[1].SetActive(true);
            }
            else
            {
                UnLoadMakeupBundle();

                BeautySkin_Slider.onValueChanged.RemoveAllListeners();
                BeautySkinContentPanels[1].SetActive(false);
            }
        });
        return option;
    }

    IEnumerator LoadMakeupBundle(Makeup makeupitem)
    {
        rtt.UnBindItem((int)SlotForItems.Makeup, (int)SlotForItems.Makeup_bundle);
        yield return rtt.LoadItem(makeupitem.item, (int)SlotForItems.Makeup_bundle);
        rtt.BindItem((int)SlotForItems.Makeup, (int)SlotForItems.Makeup_bundle);
    }

    void UnLoadMakeupBundle()
    {
        rtt.UnBindItem((int)SlotForItems.Makeup, (int)SlotForItems.Makeup_bundle);
        rtt.UnLoadItem((int)SlotForItems.Makeup_bundle);
    }

    void SwitchBeautyOptionUIState(Beauty bi, GameObject go)
    {
        var bg = go.transform.Find("Image_bg");
        if (bg)
            if (Math.Abs(bi.currentvalue - bi.disablevalue) < 0.01)
            {
                bg.gameObject.SetActive(false);
            }
            else
            {
                bg.gameObject.SetActive(true);
            }
    }

    void ClearBeautySkinOptions()
    {
        foreach (Transform childTr in BeautyOptionContentTrans)
        {
            childTr.GetComponent<AddClickEvent>().RemoveAllListener();
            Destroy(childTr.gameObject);
        }
        BeautyGOs.Clear();
        MakeupGOs.Clear();
    }

    void UnSelectAllBeautySkinOptions()
    {
        foreach (var bgo in BeautyGOs)
        {
            bgo.Value.GetComponentInChildren<Image>().sprite = uisprites.GetSprite(bgo.Key.type, bgo.Key.iconid_0);
            bgo.Value.GetComponentInChildren<Text>().color = FuConst.NORMAL_COLOR;
        }
    }

    void CloseAllBeautySkinContent()
    {
        foreach (var go in BeautySkinContentPanels)
        {
            go.SetActive(false);
        }
        foreach (var go in BeautySkinSelecterOptions)
        {
            go.GetComponent<Text>().color = FuConst.NORMAL_COLOR;
        }
    }

    void CloseBeautySkinUI()
    {
        BeautySkinContent.SetActive(false);
        BeautySkinSelecter.SetActive(false);
    }
    #endregion

    #region ItemsUI

    void OpenItemsUI(string it)
    {
        if (it.CompareTo(ItemType.Animoji.ToString()) == 0)
            OpenItemsUI(ItemType.Animoji);
        else if (it.CompareTo(ItemType.ItemSticker.ToString()) == 0)
            OpenItemsUI(ItemType.ItemSticker);
        else if (it.CompareTo(ItemType.ARMask.ToString()) == 0)
            OpenItemsUI(ItemType.ARMask);
        else if (it.CompareTo(ItemType.ChangeFace.ToString()) == 0)
            OpenItemsUI(ItemType.ChangeFace);
        else if (it.CompareTo(ItemType.ExpressionRecognition.ToString()) == 0)
            OpenItemsUI(ItemType.ExpressionRecognition);
        else if (it.CompareTo(ItemType.MusicFilter.ToString()) == 0)
            OpenItemsUI(ItemType.MusicFilter);
        else if (it.CompareTo(ItemType.BackgroundSegmentation.ToString()) == 0)
            OpenItemsUI(ItemType.BackgroundSegmentation);
        else if (it.CompareTo(ItemType.GestureRecognition.ToString()) == 0)
            OpenItemsUI(ItemType.GestureRecognition);
        else if (it.CompareTo(ItemType.MagicMirror.ToString()) == 0)
            OpenItemsUI(ItemType.MagicMirror);
        else if (it.CompareTo(ItemType.PortraitLightEffect.ToString()) == 0)
            OpenItemsUI(ItemType.PortraitLightEffect);
        else if (it.CompareTo(ItemType.PortraitDrive.ToString()) == 0)
            OpenItemsUI(ItemType.PortraitDrive);
    }

    void OpenItemsUI(ItemType it)
    {
        currentItemType = it;
        ItemOptionContentTrans.localPosition = Vector3.zero;
        ClearItemsOptions();
        switch (it)
        {
            case ItemType.Animoji:
                AddItemOptions(ItemConfig.item_1);
                break;
            case ItemType.ItemSticker:
                AddItemOptions(ItemConfig.item_2);
                break;
            case ItemType.ARMask:
                AddItemOptions(ItemConfig.item_3);
                break;
            case ItemType.ChangeFace:
                AddItemOptions(ItemConfig.item_4);
                break;
            case ItemType.ExpressionRecognition:
                AddItemOptions(ItemConfig.item_5);
                break;
            case ItemType.MusicFilter:
                AddItemOptions(ItemConfig.item_6);
                break;
            case ItemType.BackgroundSegmentation:
                AddItemOptions(ItemConfig.item_7);
                break;
            case ItemType.GestureRecognition:
                AddItemOptions(ItemConfig.item_8);
                break;
            case ItemType.MagicMirror:
                AddItemOptions(ItemConfig.item_9);
                break;
            case ItemType.PortraitLightEffect:
                AddItemOptions(ItemConfig.item_10);
                break;
            case ItemType.PortraitDrive:
                AddItemOptions(ItemConfig.item_11);
                break;
            default:
                break;
        }

        Item_Content.SetActive(true);
    }

    void AddItemOptions(Item[] items)
    {
        GameObject disableoption = Instantiate(Item_Disable);
        disableoption.transform.SetParent(ItemOptionContentTrans, false);
        disableoption.transform.localScale = Vector3.one;
        disableoption.transform.localPosition = Vector3.zero;
        disableoption.name = "Item_Disable";
        var disabletoggle = disableoption.GetComponent<Toggle>();
        disabletoggle.isOn = true;
        disabletoggle.group = Item_ToggleGroup;
        Item_ToggleGroup.RegisterToggle(disabletoggle);

        disabletoggle.onValueChanged.AddListener(delegate
        {
            if (disabletoggle.isOn)
            {
                if (musiccor != null)
                {
                    StopCoroutine(musiccor);
                    musiccor = null;
                    audios.Stop();
                }
                rtt.UnLoadItem((int)SlotForItems.Item);
                Item_Tip.text = "";
            }
        });

        for (int i = 0; i < items.Length; i++)
        {
            var itemobj = AddItemOption(items[i]);
            if (i == 0)
            {
                itemobj.GetComponent<Toggle>().isOn = true;
                Item_ToggleGroup.NotifyToggleOn(itemobj.GetComponent<Toggle>());
            }
        }
    }

    GameObject AddItemOption(Item item)
    {
        GameObject option = Instantiate(Item_UIExample);
        option.transform.SetParent(ItemOptionContentTrans, false);
        option.transform.localScale = Vector3.one;
        option.transform.localPosition = Vector3.zero;
        option.name = item.name;
        option.GetComponentInChildren<Image>().sprite = uisprites.GetSprite(item.type, item.iconid);
        var toggle = option.GetComponent<Toggle>();
        toggle.isOn = false;
        toggle.group = Item_ToggleGroup;
        Item_ToggleGroup.RegisterToggle(toggle);


        toggle.onValueChanged.AddListener(delegate
        {
            if (toggle.isOn)
            {
                if (musiccor != null)
                {
                    StopCoroutine(musiccor);
                    musiccor = null;
                    audios.Stop();
                }
                StartCoroutine(rtt.LoadItem(item, (int)SlotForItems.Item, new RenderToTexture.LoadItemCallback(OnItemLoaded)));
            }
        });
        return option;
    }

    IEnumerator PlayMusic(string name)
    {
        bool isMusicFilter = false;
        foreach (Item item in ItemConfig.item_6)
        {
            if (string.Equals(name, item.name))
            {
                isMusicFilter = true;
                break;
            }
        }
        if (isMusicFilter)
        {
            var audiodata = Resources.LoadAsync<AudioClip>("douyin");
            yield return audiodata;
            audios.clip = audiodata.asset as AudioClip;
            audios.loop = true;
            audios.Play();
            while (true)
            {
                rtt.SetItemParamd(name, "music_time", audios.time * 1000);
                //Debug.Log(audios.time);
                yield return Util.end_of_frame;
            }
        }
        musiccor = null;
        audios.Stop();
    }

    void ClearItemsOptions()
    {
        foreach (Transform childTr in ItemOptionContentTrans)
        {
            var toggle = childTr.GetComponent<Toggle>();
            toggle.onValueChanged.RemoveAllListeners();
            Item_ToggleGroup.UnregisterToggle(toggle);
            Destroy(childTr.gameObject);
        }
    }

    void OnItemLoaded(Item item)
    {
        if (item.type == ItemType.Animoji)
        {
            rtt.SetItemParamd(item.name, "{\"thing\":\"<global>\",\"param\":\"follow\"}", 1);
        }
        else if (item.type == ItemType.MusicFilter)
        {
            musiccor = StartCoroutine(PlayMusic(item.name));
        }
        Item_Tip.text = item.tip;
    }

    void CloseItemUI()
    {
        Item_Content.SetActive(false);
    }
    #endregion

    void OnApplicationQuit()
    {
        UnRegisterUIFunc();
    }
}

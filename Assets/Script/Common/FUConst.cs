using UnityEngine;

public class FuConst 
{
    public const int CAMERA_WIDTH = 1280;
    public const int CAMERA_HEIGHT = 720;
    public const int CAMERA_FRAME_RATE = 30;
    public const int SLOTLENGTH = 1;
    public const int SLOTLENGTH_TEN = 10;
    public const float RENERDER_TO_TEXTURE_FOV = 25f;
    public const float SIMPLE_RENERDER_FOV = 20f;

    public const string BUNDLE_AI_FACE = "ai_face_processor.bundle";
    public const string BUNDLE_AI_HAND = "ai_hand_processor.bundle";
    public const string BUNDLE_AI_HUMAN="ai_human_processor.bundle";

    public const string BUNDLE_AI_FACE_PC = "ai_face_processor_pc.bundle";
    public const string BUNDLE_AI_HAND_PC = "ai_hand_processor_pc.bundle";
    public const string BUNDLE_AI_HUMAN_PC = "ai_human_processor_pc.bundle";

    public const string BUNDLE_TONGUE = "tongue.bundle";

    public const string AITYPE  ="aitype";
    public const string AITYPE_PATH_ROOT = "/faceunity/";
    public const string AITYPE_PATH= "/faceunity/graphics/aitype.bundle";
    public const string MODEL_PATH = "/faceunity/model/";

    public const string  PHOTOES_PATH="/Photoes/";
    public const string YYYYMMDDHHMMSSFFFF="yyyyMMddHHmmssffff";

    public const string CLEAR_GRINDING = "清晰磨皮";
    public const string SEVERE_GRINDING = "重度磨皮";
    public const string FINE_GRINDING = "精细磨皮";
    public const string UNIFORM_GRINDING = "均匀磨皮";
    public static Color HIGHT_LIGHT_COLOR = new Color(0.337f, 0.792f, 0.957f, 1);
    public static Color NORMAL_COLOR = Color.white;
}
//五种数据输入格式，详见文档
public enum UpdateDataMode
{
    None,
    RGBABuffer,
    BGRABuffer,
    TexID,
    NV21Buffer,
    NV21BufferAndTexID,
    YUV420Buffer
}
public enum InputSource
{
    None = 0,
    Camera,
    Image,
    Video
}
public enum FUAITYPE
{
    FUAITYPE_NONE = 0,
    FUAITYPE_BACKGROUNDSEGMENTATION = 1 << 1,
    FUAITYPE_HAIRSEGMENTATION = 1 << 2,
    FUAITYPE_HANDGESTURE = 1 << 3,
    FUAITYPE_HANDPROCESSOR = 1 << 4,
    FUAITYPE_TONGUETRACKING = 1 << 5,
    FUAITYPE_FACELANDMARKS75 = 1 << 13,
    FUAITYPE_FACELANDMARKS209 = 1 << 14,
    FUAITYPE_FACELANDMARKS239 = 1 << 15,
    FUAITYPE_HUMANPOSE2D = 1 << 6,
    FUAITYPE_BACKGROUNDSEGMENTATION_GREEN = 1 << 7,
    FUAITYPE_FACEPROCESSOR = 1 << 8,
    FUAITYPE_FACEPROCESSOR_FACECAPTURE = 1 << 20,
    FUAITYPE_FACEPROCESSOR_FACECAPTURE_TONGUETRACKING = 1 << 21,
    FUAITYPE_FACEPROCESSOR_HAIRSEGMENTATION = 1 << 22,
    FUAITYPE_FACEPROCESSOR_HEADSEGMENTATION = 1 << 23,
    FUAITYPE_FACEPROCESSOR_EXPRESSION_RECOGNIZER = 1 << 24,
    FUAITYPE_FACEPROCESSOR_EMOTION_RECOGNIZER = 1 << 25,
    FUAITYPE_FACEPROCESSOR_DISNEYGAN = 1 << 26,
    FUAITYPE_FACEPROCESSOR_FACEID = 1 << 27,
    FUAITYPE_HUMAN_PROCESSOR = 1 << 9,
    FUAITYPE_HUMAN_PROCESSOR_DETECT = 1 << 28,
    FUAITYPE_HUMAN_PROCESSOR_2D_SELFIE = 1 << 30,
    FUAITYPE_HUMAN_PROCESSOR_2D_DANCE = 1 << 31,
    FUAITYPE_HUMAN_PROCESSOR_2D_SLIM = 1 << 32,
    FUAITYPE_HUMAN_PROCESSOR_3D_SELFIE = 1 << 33,
    FUAITYPE_HUMAN_PROCESSOR_3D_DANCE = 1 << 34,
    FUAITYPE_HUMAN_PROCESSOR_SEGMENTATION = 1 << 29,
    FUAITYPE_FACE_RECOGNIZER = 1 << 10
}
public enum FUAIGESTURETYPE
{
    FUAIGESTURE_NO_HAND = -1,
    FUAIGESTURE_UNKNOWN = 0,
    FUAIGESTURE_THUMB = 1,
    FUAIGESTURE_KORHEART = 2,
    FUAIGESTURE_SIX = 3,
    FUAIGESTURE_FIST = 4,
    FUAIGESTURE_PALM = 5,
    FUAIGESTURE_ONE = 6,
    FUAIGESTURE_TWO = 7,
    FUAIGESTURE_OK = 8,
    FUAIGESTURE_ROCK = 9,
    FUAIGESTURE_CROSS = 10,
    FUAIGESTURE_HOLD = 11,
    FUAIGESTURE_GREET = 12,
    FUAIGESTURE_PHOTO = 13,
    FUAIGESTURE_HEART = 14,
    FUAIGESTURE_MERGE = 15,
    FUAIGESTURE_EIGHT = 16,
    FUAIGESTURE_HALFFIST = 17,
    FUAIGESTURE_GUN = 18,
}
public enum FULOGLEVEL
{
    FU_LOG_LEVEL_TRACE = 0,     //调试日志，每帧多次
    FU_LOG_LEVEL_DEBUG = 1,     //调试日志，每帧一次或多次信息
    FU_LOG_LEVEL_INFO = 2,      //正常信息日志，程序运行过程中出现一次的信息，系统信息等
    FU_LOG_LEVEL_WARN = 3,      //警告级日志
    FU_LOG_LEVEL_ERROR = 4,     //错误级日志
    FU_LOG_LEVEL_CRITICAL = 5,  //错误且影响程序正常运行日志
    FU_LOG_LEVEL_OFF = 6        //关闭日志输出
}
public enum FUAIEXPRESSIONTYPE
{
    FUAIEXPRESSION_UNKNOWN = 0,
    FUAIEXPRESSION_BROW_UP = 1 << 1,
    FUAIEXPRESSION_BROW_FROWN = 1 << 2,
    FUAIEXPRESSION_LEFT_EYE_CLOSE = 1 << 3,
    FUAIEXPRESSION_RIGHT_EYE_CLOSE = 1 << 4,
    FUAIEXPRESSION_EYE_WIDE = 1 << 5,
    FUAIEXPRESSION_MOUTH_SMILE_LEFT = 1 << 6,
    FUAIEXPRESSION_MOUTH_SMILE_RIGHT = 1 << 7,
    FUAIEXPRESSION_MOUTH_FUNNEL = 1 << 8,
    FUAIEXPRESSION_MOUTH_OPEN = 1 << 9,
    FUAIEXPRESSION_MOUTH_PUCKER = 1 << 10,
    FUAIEXPRESSION_MOUTH_ROLL = 1 << 11,
    FUAIEXPRESSION_MOUTH_PUFF = 1 << 12,
    FUAIEXPRESSION_MOUTH_SMILE = 1 << 13,
    FUAIEXPRESSION_MOUTH_FROWN = 1 << 14,
    FUAIEXPRESSION_HEAD_LEFT = 1 << 15,
    FUAIEXPRESSION_HEAD_RIGHT = 1 << 16,
    FUAIEXPRESSION_HEAD_NOD = 1 << 17,
}
public enum FUAITONGUETYPE
{
    FUAITONGUE_UNKNOWN = 0,
    FUAITONGUE_UP = 1 << 1,
    FUAITONGUE_DOWN = 1 << 2,
    FUAITONGUE_LEFT = 1 << 3,
    FUAITONGUE_RIGHT = 1 << 4,
    FUAITONGUE_LEFT_UP = 1 << 5,
    FUAITONGUE_LEFT_DOWN = 1 << 6,
    FUAITONGUE_RIGHT_UP = 1 << 7,
    FUAITONGUE_RIGHT_DOWN = 1 << 8,
}

public enum FUITEMTRIGGERTYPE
{
    FUITEMTRIGGER_UNKNOWN = 0,
    FUITEMTRIGGER_CAI_DAN = 1,
}

public enum FUAIEMOTIONTYPE
{
    FUAIEMOTION_UNKNOWN = 0,
    FUAIEMOTION_HAPPY = 1 << 1,
    FUAIEMOTION_SAD = 1 << 2,
    FUAIEMOTION_ANGRY = 1 << 3,
    FUAIEMOTION_SURPRISE = 1 << 4,
    FUAIEMOTION_FEAR = 1 << 5,
    FUAIEMOTION_DISGUST = 1 << 6,
    FUAIEMOTION_NEUTRAL = 1 << 7,
    FUAIEMOTION_CONFUSE = 1 << 8,
}
public enum TRANSFORM_MATRIX
{
    /*
     * 8 base orientation cases, first do counter-clockwise rotation in degree,
     * then do flip
     */
    DEFAULT = 0,             // no rotation, no flip
    CCROT0 = DEFAULT,        // no rotation, no flip
    CCROT90,                 // counter-clockwise rotate 90 degree
    CCROT180,                // counter-clockwise rotate 180 degree
    CCROT270,                // counter-clockwise rotate 270 degree
    CCROT0_FLIPVERTICAL,     // vertical flip
    CCROT0_FLIPHORIZONTAL,   // horizontal flip
    CCROT90_FLIPVERTICAL,    // first counter-clockwise rotate 90 degree，then
                             // vertical flip
    CCROT90_FLIPHORIZONTAL,  // first counter-clockwise rotate 90 degree，then
                             // horizontal flip
    /*
     * enums below is alias to above enums, there are only 8 orientation cases
     */
    CCROT0_FLIPVERTICAL_FLIPHORIZONTAL = CCROT180,
    CCROT90_FLIPVERTICAL_FLIPHORIZONTAL = CCROT270,
    CCROT180_FLIPVERTICAL = CCROT0_FLIPHORIZONTAL,
    CCROT180_FLIPHORIZONTAL = CCROT0_FLIPVERTICAL,
    CCROT180_FLIPVERTICAL_FLIPHORIZONTAL = DEFAULT,
    CCROT270_FLIPVERTICAL = CCROT90_FLIPHORIZONTAL,
    CCROT270_FLIPHORIZONTAL = CCROT90_FLIPVERTICAL,
    CCROT270_FLIPVERTICAL_FLIPHORIZONTAL = CCROT90,
}
public enum FU_RUNNING_MODE
{
    FU_RUNNING_MODE_UNKNOWN = 0,
    FU_RUNNING_MODE_RENDERITEMS, //face tracking and render item (beautify is one type of item) ,item means '道具'
    FU_RUNNING_MODE_BEAUTIFICATION,//non face tracking, beautification only.
    FU_RUNNING_MODE_TRACK//tracking face only then get face infomation, without render item. it's very fast.
};
public enum FU_ROTATION_MODE
{
    ROT_0 = 0,
    ROT_90 = 1,
    ROT_180 = 2,
    ROT_270 = 3,
}
public enum Nama_GL_Event_ID
{
    FuSetup = 0,
    NormalRender = 1,
    ReleaseGLResources = 2,
    FuDestroyAllItems = 3,
}
public enum SETUPMODE
{
    Normal,
    Local,
}
public struct slot_item
{
    public string name;
    public int id;
    public Item item;

    public void Reset()
    {
        id = 0;
        name = "";
        item = Item.Empty;
    }
};

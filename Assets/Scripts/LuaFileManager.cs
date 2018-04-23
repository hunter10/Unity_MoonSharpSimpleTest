using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AssetBundles;

using MoonSharp.Interpreter;

using System.IO;

public class LuaFileInfo
{
	public DynValue className = null;      // 불러올 클래스 이름
    public DynValue classInst = null;     // 클래스 인스턴스
}

public class LuaFileManager : MonoSingleton<LuaFileManager> 
{
	[HideInInspector]
	public Script script = null;

	public Dictionary<string, LuaFileInfo> dicLuaFileInfo = new Dictionary<string, LuaFileInfo>();

	public void Init()
    {
        script = MoonSharpScript.Instance.script;

        /*
        AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetFromCachedAssetBundle("promotion", "Global", typeof(TextAsset));
        if (request != null && request.IsDone())
        {
            TextAsset luaSrc = request.GetAsset<TextAsset>();
            script.DoString(luaSrc.text);
        }
        else
        {
            Debug.LogError("Global.lua load Failed");
        }

        request = AssetBundleManager.LoadAssetFromCachedAssetBundle("promotion", "PGlobal", typeof(TextAsset));
        if (request != null && request.IsDone())
        {
            TextAsset luaSrc = request.GetAsset<TextAsset>();
            script.DoString(luaSrc.text);
        }
        else
        {
            Debug.LogError("PGlobal.lua load Failed");
        }
        ㄴ*/
        //MoonSharpScript.Instance.OnPromotionManagerInit();
    }

    public LuaFileInfo SingleGetInfo(string bundleName, string className)
    {
        string path = Application.dataPath + "/Resources/Scripts/LuaTest2.lua";

        //string luaCode = File.ReadAllText(Path.Combine(path, "LuaTest2.Lua"));
        //Script script = new Script();
        //script.DoString(luaCode);
        //this.script = script;

        string scriptData = File.ReadAllText(path);

        //DynValue table = Script.RunString(scriptData);
        script.DoString(scriptData);

        LuaFileInfo info = new LuaFileInfo();
        info.className = script.Globals.Get(className);

         DynValue newFunction = info.className.Table.Get("new");
        if (newFunction.IsNotNil())
        {
            info.classInst = script.Call(newFunction, info.className);
        }
        else
        {
            Debug.LogError(className + "new not found");
            return null;
        }

        return info;
		
        //print(LuaFileManager.Instance.script.DoFile(path));
    }

	public LuaFileInfo GetInfo(string bundleName, string className)
    {
        LuaFileInfo info = null;
        dicLuaFileInfo.TryGetValue(className, out info);
        if (info == null && className != "none")
        {
            info = ActivatePromotion(bundleName, className);
        }
        return info;
    }

	 // 없으면 새로 생성하고 맵에 등록후 OnActivatePromotion, OnSceneReady 함수들 실행
    public LuaFileInfo ActivatePromotion(string bundleName, string className)
    {
        if (script == null && className == "none")
        {
            Debug.Log("PromotionManager not init()!! ignore packet!");
            return null;
        }

        LuaFileInfo info = null;
        dicLuaFileInfo.TryGetValue(className, out info);
        if (info != null)
        {
            Debug.LogError(className + " is already active!");
            return null;
        }


        info = CreatePromtionObject(bundleName, className);
        dicLuaFileInfo[className] = info;

        return info;
    }

	LuaFileInfo CreatePromtionObject(string bundleName, string className)
    {
        if (className == "none")
        {
            return null;
        }

        AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetFromCachedAssetBundle(bundleName, className, typeof(TextAsset));
        if (request != null && request.IsDone())
        {
            TextAsset luaSrc = request.GetAsset<TextAsset>();
            script.DoString(luaSrc.text);
        }
        else
        {
            Debug.LogError(className + ".lua load Failed");
            return null;
        }

        LuaFileInfo info = new LuaFileInfo();
        info.className = script.Globals.Get(className);

        DynValue newFunction = info.className.Table.Get("new");
        if (newFunction.IsNotNil())
        {
            info.classInst = script.Call(newFunction, info.className);
        }
        else
        {
            Debug.LogError(className + "new not found");
            return null;
        }

        return info;
    }
}

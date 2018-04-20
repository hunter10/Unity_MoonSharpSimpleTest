using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MoonSharp.Interpreter;

public class GameManager : MonoBehaviour {


	public void Start()
	{
		LuaFileManager.Instance.Init();
	}

	public void OnClickInit()
	{
		Debug.Log("1");
	}

	public void OnClickMoveTest()
	{
		Debug.Log("2");

        //var go = GameObject.Find("Image-LuaTest");
        //go.transform.position = new Vector3(-1, 0, 0);

		//MoonSharpVsCodeDebugServer server = new MoonSharpVsCodeDebugServer();
		//server.Start();
		//server.AttachToScript(script, "DebugScript");

        LuaFileInfo info = LuaFileManager.Instance.GetInfo("luascripts", "LuaTest");
		DynValue function = LuaFileManager.Instance.script.Globals.Get("LuaTest").Table.Get("OnClickDoLocalMove");
		LuaFileManager.Instance.script.Call(function, info.classInst);
	}

}

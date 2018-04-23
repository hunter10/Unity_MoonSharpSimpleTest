using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MoonSharp.Interpreter;
using MoonSharp.VsCodeDebugger;

using System.Reflection;
using System.Diagnostics;

using System.IO;


public class GameManager : MonoBehaviour {


	public void Start()
	{
		LuaFileManager.Instance.Init();
	}

	public void OnClickInit()
	{
		UnityEngine.Debug.Log("1");
	}

	public void OnClickMoveTest()
	{
		UnityEngine.Debug.Log("2");

        //var go = GameObject.Find("Image-LuaTest");
        //go.transform.position = new Vector3(-1, 0, 0);



		//LuaFileInfo info = LuaFileManager.Instance.GetInfo("luascripts", "LuaTest");



		//MoonSharpVsCodeDebugServer server = new MoonSharpVsCodeDebugServer();
		//server.Start();
		//server.AttachToScript(script, "DebugScript");
		///server.AttachToScript(MoonSharpScript.Instance.script, "DebugScript");



		//StartCoroutine(AwaitDebuggerAttach(server));
		// wait for debugger to attach
		// bool attached = AwaitDebuggerAttach(server);
		// if (!attached) 
		// {
		// 	UnityEngine.Debug.Log("VS Code debugger did not attach. Running the script.");
		// }

		
		
		
		//LuaFileInfo info = LuaFileManager.Instance.GetInfo("luascripts", "LuaTest");
		//DynValue function = LuaFileManager.Instance.script.Globals.Get("LuaTest").Table.Get("OnClickDoLocalMove");
		//LuaFileManager.Instance.script.Call(function, info.classInst);
        


		LuaFileInfo info = LuaFileManager.Instance.SingleGetInfo("test", "LuaTest2");
        DynValue function = LuaFileManager.Instance.script.Globals.Get("LuaTest").Table.Get("OnClickDoLocalMove");
        LuaFileManager.Instance.script.Call(function, info.classInst);
    }

    LuaFileInfo info;
	void Update ()
	{
		//If we press F5, reload the script and run it.
		if (Input.GetKeyDown(KeyCode.F5))
		{
            /*/
			string path = Application.dataPath + "/Resources/Scripts/LuaTest2.lua";
			StreamReader reader = new StreamReader(path);
			string content = reader.ReadToEnd();
			print(LuaFileManager.Instance.script.DoFile(path));
			*/
            LuaFileManager.Instance.Init();

            LuaFileInfo info = LuaFileManager.Instance.SingleGetInfo("test", "LuaTest2");
            DynValue function = LuaFileManager.Instance.script.Globals.Get("LuaTest").Table.Get("OnClickDoLocalMove");
            LuaFileManager.Instance.script.Call(function, info.classInst);
        }
	}

    /*
	private IEnumerator AwaitDebuggerAttach(this MoonSharpVsCodeDebugServer server)
	{
		// as soon as a client has attached, 'm_Client__' field of 'm_Current' isn't null anymore
		// 
		// we wait for ~60 seconds for a client to attach
		BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.NonPublic;
		FieldInfo field = server.GetType().GetField("m_Current", bindFlags);
		object current = field.GetValue(server);
		
		FieldInfo property = current.GetType().GetField("m_Client__", bindFlags);
		
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		UnityEngine.Debug.Log("Waiting for VS Code debugger to attach");
		while (property.GetValue(current) == null)
		{
			//Thread.Sleep(500);
			yield return new WaitForSeconds(0.5f);
			if (stopwatch.Elapsed.TotalSeconds > 60) 
				yield return null;
		}
		stopwatch.Stop();
		UnityEngine.Debug.Log("VS Code debugger attached");
		yield return null;
	}
    */
}

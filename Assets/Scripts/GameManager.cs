using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MoonSharp.Interpreter;
using MoonSharp.VsCodeDebugger;

using System.Reflection;
using System.Diagnostics;
using System.Threading;

using System;
using System.IO;
using System.Reflection;

public class GameManager : MonoBehaviour {

	//MoonSharpVsCodeDebugServer server;

	public void Start()
	{
		LuaFileManager.Instance.Init();
	}

	public void OnClickInit()
	{
		UnityEngine.Debug.Log("1");

		LuaFileManager.Instance.Init();
	}

	public void OnClickMoveTest()
	{
		UnityEngine.Debug.Log("2");

		LuaFileInfo info = LuaFileManager.Instance.SingleGetInfo("test", "LuaTest2");
        DynValue function = LuaFileManager.Instance.script.Globals.Get("LuaTest2").Table.Get("OnClickDoLocalMove");
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
            

            LuaFileInfo info = LuaFileManager.Instance.SingleGetInfo("test", "LuaTest2");
            DynValue function = LuaFileManager.Instance.script.Globals.Get("LuaTest").Table.Get("OnClickDoLocalMove");
            LuaFileManager.Instance.script.Call(function, info.classInst);
        }
	}

    private static bool AwaitDebuggerAttach(MoonSharpVsCodeDebugServer server)
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
         //Console.WriteLine("Waiting for VS Code debugger to attach");
		 UnityEngine.Debug.Log("Waiting for VS Code debugger to attach");
         while (property.GetValue(current) == null)
         {
            Thread.Sleep(500);
            if (stopwatch.Elapsed.TotalSeconds > 60) return false;
         }
         stopwatch.Stop();
         //Console.WriteLine("VS Code debugger attached");
		 UnityEngine.Debug.Log("VS Code debugger attached");
         return true;
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

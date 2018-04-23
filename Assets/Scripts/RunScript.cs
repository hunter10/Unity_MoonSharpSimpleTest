//Only including the base requirements.
using UnityEngine;
using MoonSharp.Interpreter;
using MoonSharp.VsCodeDebugger;
using MoonSharp.Interpreter.Loaders;
using UnityEditor;
using System.IO;

public class RunScript : MonoBehaviour {

	MoonSharpVsCodeDebugServer server;

	Script script;

	// Use this for initialization
	void Start ()
	{
		UserData.RegisterAssembly();

		script = new Script();
		server = new MoonSharpVsCodeDebugServer().Start();

		//I'm loading from file, so this is a requirement.
		script.Options.ScriptLoader = new FileSystemScriptLoader();
		
		//For debugging purposes on my end.
		server.AttachToScript(script, "SCRIPT_NAME_HERE_LOL");

		//This here is the function we need to set the type to be able to instantiate. I know, it's global, but it lets us use
		//the methods and UnityEngine functions with ease from Lua.
		//script.Globals.Set("CustomType", UserData.Create(ScriptableObject.CreateInstance(typeof(CustomType))));
	}
	
	// Update is called once per frame
	void Update ()
	{
		//If we press F5, reload the script and run it.
		if (Input.GetKeyDown(KeyCode.F5))
		{
            //string path = Application.dataPath + "/Resources/Scripts/LuaTest2.lua";

            //StreamReader reader = new StreamReader(path);
            //string content = reader.ReadToEnd();

            //string scriptData = File.ReadAllText(path);
            //script.DoFile(scriptData);

            LuaFileManager.Instance.Init();

            LuaFileInfo info = LuaFileManager.Instance.SingleGetInfo("test", "LuaTest2");
            DynValue function = LuaFileManager.Instance.script.Globals.Get("LuaTest2").Table.Get("OnClickDoLocalMove");
            LuaFileManager.Instance.script.Call(function, info.classInst);
        }

		if (Input.GetKeyDown(KeyCode.F2))
		{
			DynValue classInst = script.Globals.Get("LuaTest2").Table.Get("new");
			DynValue function = script.Globals.Get("LuaTes2").Table.Get("OnClickDoLocalMove");
			script.Call(function, classInst);
		}
	}
}
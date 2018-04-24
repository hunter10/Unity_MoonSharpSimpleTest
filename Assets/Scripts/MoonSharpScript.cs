using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MoonSharp.Interpreter;

public class MoonSharpScript : MonoSingleton<MoonSharpScript>
{
	Script _script = null;
	public Script script{
		get{
			if(_script != null){
				return _script;
			}

			Script.DefaultOptions.DebugPrint = Debug.Log;
			_script = new Script();
			
			RegisterTypes2Script();
			RegisterConverter2Script();

			return _script;
		}
	}

	void RegisterTypes2Script()
    {
		UserData.RegisterType<UnityEngine.GameObject>();
        UserData.RegisterType<UnityEngine.Transform>();
        UserData.RegisterType<UnityEngine.Vector3>();
		UserData.RegisterType<UnityEngine.RectTransform>();

		script.Globals["UnityEngine"] = DynValue.NewTable(script);
		
		script.Globals["UnityEngine", "GameObject"] = UserData.CreateStatic(typeof(GameObject));
        script.Globals["UnityEngine", "Transform"] = UserData.CreateStatic(typeof(Transform));
        script.Globals["UnityEngine", "Vector3"] = UserData.CreateStatic(typeof(UnityEngine.Vector3));
		script.Globals["UnityEngine", "RectTransform"] = UserData.CreateStatic(typeof(UnityEngine.RectTransform));
    }

	void RegisterConverter2Script()
    {
        
	}
}

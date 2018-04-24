using System;

using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using MoonSharp.Interpreter;
using MoonSharp.VsCodeDebugger;

using UnityEngine;

namespace ConsoleProj
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Path to lua script file: ");
            string path = Console.ReadLine();

            if (!File.Exists(path))
            {
                Console.WriteLine("File not found.");
                Console.ReadKey();
                return;
            }

            var script = new Script();
            
            script.Globals["WriteToConsole"] = new Func<string, int>(text =>
                {
                    Console.WriteLine(text);
                    return text.Length;
                });

            


            MoonSharpVsCodeDebugServer server = new MoonSharpVsCodeDebugServer();
            server.Start();
            server.AttachToScript(script, "DebugScript");
            //server.AttachToScript(LuaFileManager.Instance.script, "DebugScript");

            // read script
            string scriptData = File.ReadAllText(path);
            script.DoString(scriptData, null, path);
            //LuaFileManager.Instance.script.DoString(scriptData, null, path);

            // wait for debugger to attach
            bool attached = AwaitDebuggerAttach(server);
            if (!attached)
            {
                Console.WriteLine("VS Code debugger did not attach. Running the script.");
            }

            DynValue className = script.Globals.Get("LuaTest2");
            DynValue newFunc = className.Table.Get("new");
            DynValue classInst = script.Call(newFunc, className);

            //LuaFileInfo info = LuaFileManager.Instance.SingleGetInfo("test", "LuaTest2");
            //DynValue function = LuaFileManager.Instance.script.Globals.Get("LuaTest2").Table.Get("OnClickDoLocalMove");
            //LuaFileManager.Instance.script.Call(function, info.classInst);


            // run script's main function
            //script.Call(script.Globals["main"]);
            //script.Call(script.Globals["OnClickDoLocalMove"]);
            
            Console.ReadKey();
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
            Console.WriteLine("Waiting for VS Code debugger to attach");
            while (property.GetValue(current) == null)
            {
                Thread.Sleep(500);
                if (stopwatch.Elapsed.TotalSeconds > 60) return false;
            }
            stopwatch.Stop();
            Console.WriteLine("VS Code debugger attached");
            return true;
        }
    }
}

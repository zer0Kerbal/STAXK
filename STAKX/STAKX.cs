// 




using System;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using KSP.IO;
using UnityEngine;


namespace STAKX
{

    using ResourceRequestFunc = Func<Vessel, float, string, float>;
    using DebugLevel = STAKXConfig.DebugLevel;

    public delegate void BackgroundUpdateResourceFunc(Vessel v, uint partFlightId, ResourceRequestFunc resourceFunc, ref System.Object data);
    public delegate void BackgroundUpdateFunc(Vessel v, uint partFlightId, ref System.Object data);

    public delegate void BackgroundSaveFunc(Vessel v, uint partFlightId, System.Object data);
    public delegate void BackgroundLoadFunc(Vessel v, uint partFlightId, ref System.Object data);

    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class STAKX : MonoBehaviour
    {


        private static HashSet<ProtoPartResourceSnapshot> modifiedResources = new HashSet<ProtoPartResourceSnapshot>();
        public static void Debug(String s, DebugLevel l)
        {
            if (config.debugLevel >= l)
            {
                switch (l)
                {
                    case DebugLevel.ERROR: UnityEngine.Debug.LogError(s); break;
                    case DebugLevel.WARNING: UnityEngine.Debug.LogWarning(s); break;
                    default: UnityEngine.Debug.Log(s); break;
                }
            }
        }

        static public STAKXConfig config = null;

        public bool IsMostRecentAssembly()
        {
            Assembly me = Assembly.GetExecutingAssembly();

            foreach (AssemblyLoader.LoadedAssembly la in AssemblyLoader.loadedAssemblies)
            {
                if (la.assembly.GetName().Name != me.GetName().Name) continue;

                if (la.assembly.GetName().Version > me.GetName().Version) { return false; }
            }

            return true;
        }

        public void LoadConfigFile()
        {
            PluginConfiguration pc = PluginConfiguration.CreateForType<STAKX>();
            if (pc == null) { return; }

            pc.load();
            config = new STAKXConfig(pc);
            pc.save();
        }

        public void Awake()
        {
            LoadConfigFile();
            Debug("STAKX: Running assembly at " + Assembly.GetExecutingAssembly().Location + " (" + Assembly.GetExecutingAssembly().GetName().Version + ")", DebugLevel.WARNING);
        }

        private void HandleResources(Vessel v)
        {

        }

        public void FixedUpdate()
        {
        }

    }


} // STAKX

/*{
	"NAME" : "STatstical Analysis for Kerbal eXploration",
	"URL"      : "https://raw.githubusercontent.com/zer0Kerbal/STAKX/master/GameData/STAKX/STAKX.version",
	"DOWNLOAD" : "https://github.com/zer0Kerbal/STAKX/releases/latest",
	"GITHUB" :
	{
		"USERNAME" : "zer0Kerbal",
		"REPOSITORY" : STAKX
		"ALLOW_PRE_RELEASE": false
	},
	"VERSION" : 
	{
		"MAJOR" : 1,
		"MINOR" : 0,
		"PATCH" : 0,
		"BUILD" : 0
	},
	"KSP_VERSION":
	{
		"MAJOR":1,
		"MINOR":7,
		"PATCH":3
	},
	"KSP_VERSION_MIN":
	{
		"MAJOR":1,
		"MINOR":4,
		"PATCH":1
	},
	"KSP_VERSION_MAX":
	{
		"MAJOR":1,
		"MINOR":8,
		"PATCH":9999
	}
}*/

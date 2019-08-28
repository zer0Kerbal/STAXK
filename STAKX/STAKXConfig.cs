using KSP.IO;
using System;

namespace STAKX {
	public class STAKXConfig {
		public enum DebugLevel {
			SILENT = 0,
			ERROR = 1,
			WARNING = 2,
			ALL = 3
		};

		public DebugLevel debugLevel { get; private set; }
		public bool testconfig1 { get; private set; }
		public bool testconfig2 { get; private set; }

		public STAKXConfig(PluginConfiguration config) {
			try {
				string tmp = config.GetValue<string>("DebugLevel", Enum.GetName(typeof(DebugLevel), DebugLevel.ERROR));
				debugLevel = (DebugLevel)Enum.Parse(typeof(DebugLevel), tmp);
			}
			catch (Exception) {
				debugLevel = DebugLevel.ERROR;
			}

            testconfig1 = config.GetValue<bool>("testconfig1", true);
            testconfig2 = config.GetValue<bool>("testconfig2", true);
		}
	}
}
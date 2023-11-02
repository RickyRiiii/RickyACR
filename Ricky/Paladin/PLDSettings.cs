using CombatRoutine.View.JobView;
using Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Ricky.Paladin
{
    public class PLDSettings
    {
        public static PLDSettings Instance;
        private static string path;

        public JobViewSave JobViewSave = new() { MainColor = new Vector4(0 / 255f, 153 / 255f, 255 / 255f, 0.8f) };

        public static void Build(string settingPath)
        {
            path = Path.Combine(settingPath, "PLDSettings.json");
            if (!File.Exists(path))
            {
                Instance = new PLDSettings();
                Instance.Save();
                return;
            }

            try
            {
                Instance = JsonHelper.FromJson<PLDSettings>(File.ReadAllText(path));
            }
            catch (Exception e)
            {
                Instance = new();
                LogHelper.Error(e.ToString());
            }
        }

        public void Save()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, JsonHelper.ToJson(this));
        }
        public int ClemencyPercent = 27;
        public int RCClemencyPercent = 0;
        public int Kaiguai = 200;
        public int Opener = 0;
        public Dictionary<string, object> StyleSetting = new();
        public bool AutoReset = true;
    }
}

using CombatRoutine.View.JobView;
using Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Ricky.Reaper
{
    public class RPRSettings
    {
        public static RPRSettings Instance;
        private static string path;

        public JobViewSave JobViewSave = new() { MainColor = new Vector4(168 / 255f, 20 / 255f, 20 / 255f, 0.8f) };

        public static void Build(string settingPath)
        {
            path = Path.Combine(settingPath, "ReaperSettings.json");
            if (!File.Exists(path))
            {
                Instance = new RPRSettings();
                Instance.Save();
                return;
            }

            try
            {
                Instance = JsonHelper.FromJson<RPRSettings>(File.ReadAllText(path));
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
        public int BooldBathPercent = 30;
        public int SecondWindPercent = 20;
        public int Kaiguai = 1600;
        public int Opener = 0;
        public Dictionary<string, object> StyleSetting = new();
        public bool AutoReset = true;
    }
}

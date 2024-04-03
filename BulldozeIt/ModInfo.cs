using ICities;
using System;
using System.Reflection;
/*
中文翻译支持
2024.04.03 by`TacKana
*/
namespace BulldozeIt
{
    public class ModInfo : IUserMod
    {
        public string Name => "自动化推土机!";
        public string Description => "自动化推土机!增高自动推平废弃建筑物功能。";

        private static readonly string[] IntervalLabels =
        {
            "一天一次",
            "一月一次",
            "一年一次",
            "5秒一次",
            "10秒一次",
            "30秒/半分钟一次"
        };

        private static readonly int[] IntervalValues =
        {
            1,
            2,
            3,
            4,
            5,
            6
        };

        public void OnSettingsUI(UIHelperBase helper)
        {
            UIHelperBase group;

            AssemblyName assemblyName = Assembly.GetExecutingAssembly().GetName();

            group = helper.AddGroup(Name + " - " + assemblyName.Version.Major + "." + assemblyName.Version.Minor);

            bool selected;
            int selectedIndex;
            int selectedValue;
            int result;

            selectedIndex = GetSelectedOptionIndex(IntervalValues, ModConfig.Instance.Interval);
            group.AddDropdown("自动化推土机工作时间", IntervalLabels, selectedIndex, sel =>
            {
                ModConfig.Instance.Interval = IntervalValues[sel];
                ModConfig.Instance.Save();
            });

            selectedValue = ModConfig.Instance.MaxBuildingsPerInterval;
            group.AddTextfield("每次最大铲除多少废弃建筑物？", selectedValue.ToString(), sel =>
            {
                int.TryParse(sel, out result);
                ModConfig.Instance.MaxBuildingsPerInterval = result;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.PreserveHistoricalBuildings;
            group.AddCheckbox("是否排除历史建筑物？", selected, sel =>
            {
                ModConfig.Instance.PreserveHistoricalBuildings = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.IgnoreSearchingForSurvivors;
            group.AddCheckbox("忽略搜索幸存者（机翻的不太理解意思）", selected, sel =>
            {
                ModConfig.Instance.IgnoreSearchingForSurvivors = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.ShowCounters;
            group.AddCheckbox("是否显示已经铲除建筑数量？", selected, sel =>
            {
                ModConfig.Instance.ShowCounters = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.ShowStatistics;
            group.AddCheckbox("是否在日志面板中显示统计信息？", selected, sel =>
            {
                ModConfig.Instance.ShowStatistics = sel;
                ModConfig.Instance.Save();
            });
        }

        private int GetSelectedOptionIndex(int[] option, int value)
        {
            int index = Array.IndexOf(option, value);
            if (index < 0) index = 0;

            return index;
        }
    }
}

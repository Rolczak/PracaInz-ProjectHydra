using System;

namespace ProjectHydraMobile.Navigation
{

    public class MainNavigationPageMasterMenuItem
    {
        public MainNavigationPageMasterMenuItem()
        {
            TargetType = typeof(MainNavigationPageMasterMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplePrism.Models
{
    public class DomainMenuItem
    {
        public DomainMenuItem(int id, string title, string viewName, PackIconKind selectedIcon, PackIconKind unSelectedIcon)
        {
            Id = id;
            Title = title;
            ViewName = viewName;
            SelectedIcon = selectedIcon;
            UnSelectedIcon = unSelectedIcon;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string ViewName { get; set; }

        public PackIconKind SelectedIcon { get; set; }

        public PackIconKind UnSelectedIcon { get; set; }

    }
}

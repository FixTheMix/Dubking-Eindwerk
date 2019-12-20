using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Model
{
    public enum ProjectType
    {
        LiveActionFilm,
        LiveActionSeries,
        AnimationFilm,
        AnimationSeries,
        Other
    }
    public enum ProjectModuleAccess { ReadOnly, ReadWrite }
    public enum ModuleAccess { ReadOnly, ReadWrite, NoAccess }
    public enum SettingsModuleAccess { ReadWrite, NoAccess }
    public enum SortingOptions { Character, Actor, Quantity}
    public enum DisplayOptions {Avg, Ewl, Tc}
}

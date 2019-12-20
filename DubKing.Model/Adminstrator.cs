using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Model
{
    public class Adminstrator:User
    {
        public override bool IsAdmin { get { return true; } }
        public override ProjectModuleAccess ProjectAccess { get { return _projectAccess; } }
        public override ModuleAccess VoiceLibraryAccess { get { return _voiceLibraryAccess; } }
        public override ModuleAccess ScheduleAccess { get { return _scheduleAccess; } }
        public override SettingsModuleAccess SettingsAccess { get { return _settingAccess; } }

        public Adminstrator():base(0, "Admin", "Admin", ProjectModuleAccess.ReadWrite,ModuleAccess.ReadWrite,ModuleAccess.ReadWrite,SettingsModuleAccess.ReadWrite)
        {

        }
    }
}

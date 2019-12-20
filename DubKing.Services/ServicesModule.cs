using DubKing.Repositories;
using DubKing.Services.Contracts;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Services
{
    public class ServicesModule
    {
        public static void RegisterTypes()
        {
            RepositoriesModule.RegisterTypes();
            SimpleIoc.Default.Register<IUserService, UserService>();
            SimpleIoc.Default.Register<IProjectService, ProjectService>();
            SimpleIoc.Default.Register<IVoiceTalentService, VoiceTalentService>();
            SimpleIoc.Default.Register<IEpisodeService, EpisodeService>();
            SimpleIoc.Default.Register<ICharacterService, CharacterService>();
            SimpleIoc.Default.Register<ILineService, LineService>();
            SimpleIoc.Default.Register<ILanguageService, LanguageService>();
            SimpleIoc.Default.Register<IEpisodeService, EpisodeService>();
            SimpleIoc.Default.Register<IProjectTableService, ProjectTableService>();
            SimpleIoc.Default.Register<IGlossaryService, GlossaryService>();
            SimpleIoc.Default.Register<IActiveActorService, ActiveActorService>();
        }
    }
}

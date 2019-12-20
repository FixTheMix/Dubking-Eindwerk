using DubKing.Model;
using GalaSoft.MvvmLight.Ioc;
using DubKing.Repositories.Contracts;

namespace DubKing.Repositories
{
    public class RepositoriesModule
    {
        public static void RegisterTypes()
        {
            DialogueService.DialogueModule.RegisterTypes();
            SimpleIoc.Default.Register<IRepository<Project>, ProjectRepository>();
            SimpleIoc.Default.Register<IRepository<User>, UserRepositry>();
            SimpleIoc.Default.Register<ILanguageRepository, LanguageRepository>();
            SimpleIoc.Default.Register<IEpisodeRepository, EpisodeRepository>();
            SimpleIoc.Default.Register<ILineRepository<Line, Episode>, LineRepository>();
            SimpleIoc.Default.Register<IRemoveUnused, CharacterRepository>();
            SimpleIoc.Default.Register<IRepository<VoiceTalent>, VoiceTalentRepository>();
            SimpleIoc.Default.Register<IProjectTableRepository, ProjectTableRepository>();
            SimpleIoc.Default.Register<IRememberUserRepository, RememberUserRepository>();
            SimpleIoc.Default.Register<IRepository<VLKeyword>, VlKeywordRepository>();
            SimpleIoc.Default.Register<IGlossaryRepository, GlossaryRepository>();
            SimpleIoc.Default.Register<ITranslatorRepository, TranslatorRepository>();
            SimpleIoc.Default.Register<IReadAllRepository<ActiveActor>, ActiveActorRepository>();
            SimpleIoc.Default.Register<IReadActiveProjects, ActiveProjectRepository>();
            SimpleIoc.Default.Register<IConnectionStringReader, ConnectionStringReader>();
            SimpleIoc.Default.Register<IVoiceTalentDbOperations, VoiceTalentDbOperations>();
        }
    }
}

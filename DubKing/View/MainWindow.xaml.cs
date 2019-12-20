using DubKing.Helpers;
using DubKing.Messages;
using DubKing.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using DubKing.View.VoiceLibrary;
using System;
using DubKing.View.ProjectTable;
using DubKing.View.Episode;
using DubKing.View.Scenario;
using DubKing.View.VoiceLibrary.Keywords;

namespace DubKing.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private ProjectTable.SelectActor _selectActorWindow;
        public MainWindow()
        {
            InitializeComponent();
            var data = (MainViewModel)DataContext;
            data.ShutDown += CloseApp;

            Messenger.Default.Register<MessageOpenNewUserWindow>(this, OpenNewUser);
            Messenger.Default.Register<OpenNewProjectWindow>(this, OpenNewProject);
            Messenger.Default.Register<OpenNewVoiceTalentWindow>(this, OpenNewVoiceTalent);
            Messenger.Default.Register<Messages.OpenSelectActorWindow>(this, this.OpenSelectActor);
            Messenger.Default.Register<OpenEditCharacterWindow>(this, OpenEditCharacter);
            Messenger.Default.Register<OpenImportEpisodeWindow>(this, OpenImportEpisode);
            Messenger.Default.Register<OpenScenarioWindow>(this, OpenScenario);
            Messenger.Default.Register<AskConfirmationDeleteLine>(this, AskConfirmation);
            Messenger.Default.Register<ConfirmDeleteMessage>(this, ConfirmVoiceDelete);
            Messenger.Default.Register<NewKeywordMessage>(this, OnNewKeywordMessage);
            Messenger.Default.Register<DeleteKeywordMessage>(this, OnDeleteKeywordMessage);
        }

        private void OnDeleteKeywordMessage(DeleteKeywordMessage obj)
        {
            var deleteKeywordWindow = new DeleteKeywordWindow(obj);
            deleteKeywordWindow.GetKeyword();
        }

        private void OnNewKeywordMessage(NewKeywordMessage obj)
        {
            var newKeywordWindow = new NewKeywordWindow(obj);
            newKeywordWindow.GetNewKeyword();
        }

        private void ConfirmVoiceDelete(ConfirmDeleteMessage obj)
        {
            var confirm = new ConfirmDelete();
            obj.CanDelete = confirm.GetConfirmation(obj.VTName, "");
        }

        private void AskConfirmation(AskConfirmationDeleteLine obj)
        {
            var confirm = new ConfirmDelete();
            obj.IsConfirmed = confirm.GetConfirmation("You're about to delete a line", "");
        }

        private void OpenScenario(OpenScenarioWindow obj)
        {
            var scenarioWindow = new ScenarioView();
            Messenger.Default.Send<LoadScenarioMessage>(new LoadScenarioMessage(obj.Episode, obj.Character));
            scenarioWindow.ShowDialog();
            Messenger.Default.Send<ClosingScenarioMessage>(new ClosingScenarioMessage());
        }
        private void OnCloseScenarioWindow(object sender, EventArgs e)
        {
            
            Messenger.Default.Send<ClosingScenarioMessage>(new ClosingScenarioMessage());
        }

        private void OpenImportEpisode(OpenImportEpisodeWindow obj)
        {
            var importEpisodeWindow = new ImportEpisode();
            Messenger.Default.Send<SetCurrentProject>(new SetCurrentProject(obj.CurrentProject));
            importEpisodeWindow.ShowDialog();
        }

        private void OpenEditCharacter(OpenEditCharacterWindow obj)
        {
            var editCharacterWindow = new EditCharacterWindow();
            //Messenger.Default.Send<SendCharacterToEdit>(new SendCharacterToEdit(obj.Character));
            editCharacterWindow.ShowDialog();
        }

        private void OpenSelectActor(Messages.OpenSelectActorWindow obj)
        {
            if(_selectActorWindow == null)_selectActorWindow = new ProjectTable.SelectActor();
            _selectActorWindow.ShowDialog();
        }
        
        private void OpenNewVoiceTalent(OpenNewVoiceTalentWindow obj)
        {
            var newVoiceTalent = new NewVoiceTalent();
            newVoiceTalent.ShowDialog();
        }

        private void CloseApp()
        {
            this.Close();
        }
        private void OpenNewUser(MessageOpenNewUserWindow message)
        {
            var newUserWindow = new NewUser();
            newUserWindow.ShowDialog();
        }
        private void OpenNewProject(OpenNewProjectWindow message)
        {
            var newProjectWindow = new NewProject();
            newProjectWindow.ShowDialog();
        }
    }
}

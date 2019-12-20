using DubKing.Messages;
using DubKing.View.Scenario.KeywordComment;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace DubKing.View.Scenario
{
    /// <summary>
    /// Interaction logic for ScenarioView.xaml
    /// </summary>
    public partial class ScenarioView : Window
    {

      
        private CalculateOffsetWindow CalculateOffset
        {
            get
            {
                return new CalculateOffsetWindow();
            }
        }
        public ScenarioView()
        {
            InitializeComponent();
            Messenger.Default.Register<OpenCalculateOffsetWindow>(this, OpenCalculateOffset);
            Messenger.Default.Register<CloseScenarioMessage>(this, OnCloseMessage);
            Messenger.Default.Register<OpenNewKeywordMessage>(this, OnOpenNewKeywordWindow);
            Messenger.Default.Register<ConfirmGlossaryKeywordDeleteMessage>(this, OnConfirmDeleteKeywordMessage);
            this.Closing += UnRegister;
        }

        private void OnConfirmDeleteKeywordMessage(ConfirmGlossaryKeywordDeleteMessage obj)
        {
            var confirm = new ConfirmDeleteKeywordCommentDialogue();
            confirm.GetConfirmation(obj);
        }

        private void OnCloseMessage(CloseScenarioMessage obj)
        {
            this.Close();
        }
        private void OnOpenNewKeywordWindow(OpenNewKeywordMessage obj)
        {
            var newKeywordWindow = new NewKeywordCommentwindow();
            newKeywordWindow.DataContext = obj;
            newKeywordWindow.ShowDialog();
        }
        private void UnRegister(object sender, CancelEventArgs e)
        {
            Messenger.Default.Unregister(this);
        }
        private void OpenCalculateOffset(OpenCalculateOffsetWindow obj)
        {
            CalculateOffset.ShowDialog();
        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var input = (ListBox)sender;
            input.ScrollIntoView(input.SelectedItem);
        }
        public string SelectedText
        {
            get { return (string)this.GetValue(SelectedTextProperty); }
            set { this.SetValue(SelectedTextProperty, value); }
        }
        public static DependencyProperty SelectedTextProperty = DependencyProperty.Register(
          "SelectedText", typeof(string), typeof(ScenarioView), new PropertyMetadata(""));
    }
}

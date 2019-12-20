using DubKing.Messages;
using DubKing.Model;
using DubKing.Services.Contracts;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DubKing.ViewModel.VoiceLibrary
{
    public class NewVoiceTalentViewModel : ViewModelBase
    {
        #region Fields
        private ILanguageService _languageService;
        private IVoiceTalentService _voiceTalentService;
        private VoiceTalent _newVoice;
        private ObservableCollection<string> _posibleLanguages;
        private ICommand _NewKeyword;
        private ICommand _deleteKeyword;

        ICommand _saveCommand;
        ICommand _cancelCommand;
        #endregion

        #region Properties
        public VoiceTalent Object
        {
            get { return _newVoice; }
            set { _newVoice = value; }
        }
        public IList<VLKeyword> VlKeywords { get => _vlKeywords; set => Set(ref _vlKeywords , value); }
        public ICommand SaveCommand
        {
            get { return _saveCommand ?? (new RelayCommand(OnSave, OnCanSave)); }
        }
        public ICommand CancelCommand
        {
            get { return _saveCommand ?? (new RelayCommand(OnCancel)); }
        }

        public ObservableCollection<string> PosibleLanguages
        {
            get { return _posibleLanguages; }
            set { Set(ref _posibleLanguages, value); }
        }
        private ICommand _dragEnter;
        public ICommand DragEnter { get => _dragEnter ?? (_dragEnter = new RelayCommand<DragEventArgs>(OnDragEnter)); }

        private ICommand _PhotoDrop;
        private IList<VLKeyword> _vlKeywords;

        public ICommand PhotoDrop { get => _PhotoDrop ?? (_PhotoDrop = new RelayCommand<DragEventArgs>(OnPhotoDrop)); }
        public ICommand NewKeyword { get => _NewKeyword ?? (_NewKeyword = new RelayCommand(OnNewKeyword)); }
        public ICommand DeleteKeyword { get => _deleteKeyword ?? (_deleteKeyword = new RelayCommand(OnDeleteKeyword)); }

        private void OnDragEnter(DragEventArgs args)
        {
            if (IsValidDropFile(args))
            {
                args.Effects = DragDropEffects.Copy;
            }
            else
            {
                args.Effects = DragDropEffects.None;
            }

            args.Handled = true;
        }
        private void OnPhotoDrop(DragEventArgs args)
        {
            if (IsValidDropFile(args))
            {
                var filenames = args.Data.GetData(DataFormats.FileDrop, true) as string[];
                var originalSource = args.OriginalSource as Image;
                var vt = originalSource.Tag as VoiceTalent;
                Object.Picture = filenames[0];
            }
        }

        private static bool IsValidDropFile(DragEventArgs args)
        {
            if (args.Data.GetDataPresent(DataFormats.FileDrop, true))
            {
                var fileNames = args.Data.GetData(DataFormats.FileDrop, true) as string[];
                // Check for a single file or folder.
                if (fileNames?.Length is 1)
                {
                    // Check for a file (a directory will return false).
                    if (File.Exists(fileNames[0]))
                    {
                        // At this point we know there is a single file.
                        //MessageBox.Show(fileNames[0]);
                        if (Path.GetExtension(fileNames[0]) == ".bmp" || Path.GetExtension(fileNames[0]) == ".png" || Path.GetExtension(fileNames[0]) == ".jpg")
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        #endregion

        #region Commands
        #region Save
        private bool OnCanSave()
        {
            return !string.IsNullOrEmpty(Object.FirstName) && !string.IsNullOrEmpty(Object.SurName);
        }

        private void OnSave()
        {
            MessengerInstance.Send<CloseNewVoiceTalentWindow>(new CloseNewVoiceTalentWindow(true, Object));
            Object = new VoiceTalent();
        }
        #endregion
        #region Cancel
        private void OnCancel()
        {
            MessengerInstance.Send<CloseNewVoiceTalentWindow>(new CloseNewVoiceTalentWindow(false, Object));
            Object = new VoiceTalent();
        }
        #endregion
        private void OnNewKeyword()
        {
            var msg = new NewKeywordMessage(VlKeywords);
            MessengerInstance.Send<NewKeywordMessage>(msg);
            if (msg.IsSaved && msg.IsValid)
            {
                _voiceTalentService.CreateKeyword(msg.Keyword);
            }
            VlKeywords = _voiceTalentService.GetVLKeywords();
        }
        
        private void OnDeleteKeyword()
        {
            var msg = new DeleteKeywordMessage(VlKeywords);
            MessengerInstance.Send(msg);
            if (!msg.IsCanceled && msg.SelectedKeyword != null)
            {
                _voiceTalentService.DeleteVlKeyword(msg.SelectedKeyword);
            }
            VlKeywords = _voiceTalentService.GetVLKeywords(); 
        }
        #endregion

        #region Constructors
        public NewVoiceTalentViewModel(ILanguageService languageService, IVoiceTalentService voiceTalentService)
        {
            _languageService = languageService;
            _voiceTalentService = voiceTalentService;
            VlKeywords = _voiceTalentService.GetVLKeywords();
            PosibleLanguages = new ObservableCollection<string>(_languageService.GetLanguages());
            Object = new VoiceTalent();

        }
        #endregion
    }
}

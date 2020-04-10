using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ICSharpCode.AvalonEdit;
using Microsoft.Win32;
using Troublemaker.Editor.Framework;
using Troublemaker.Editor.Settings;
using Troublemaker.Editor.ViewModels;
using Troublemaker.Xml;
using Troublemaker.Xml.Dialogs;
using Localization = Troublemaker.Xml.Localization;

namespace Troublemaker.Editor.Pages
{
    public sealed class StageController : DependencyObject
    {
        public static StageController Instance { get; } = new StageController();

        public StageController()
        {
        }
        
        private IEnumerable<StageViewModel> LoadStages()
        {
            ProgressWindow wnd = ProgressWindow.ShowBackground("Loading stages...");
            try
            {
                IEnumerable<XmlMission> missions = DB.Missions.Missions;
                missions = missions.Where(m => m.Slot != "None");
                missions = missions.OrderBy(m => m.Lv).ThenBy(m => m.ProgressOrder);

                var list = missions.ToArray();
                wnd.SetTotal(list.Length);

                foreach (XmlMission mission in list)
                {
                    String filePath = Path.Combine(Path.GetFullPath(@"Data\stage"), mission.Stage);
                    if (!File.Exists((filePath)))
                    {
                        wnd.Increment(1);
                        continue;
                    }

                    Stage stage = XmlDeserializerFactory.Default.Deserialize<Stage>(filePath);
                    var vm = new StageViewModel(stage.Map, stage);

                    wnd.Increment(1);
                    yield return vm;
                }
            }
            finally
            {
                wnd.Close();
            }
        }
        
        private IEnumerable<StageViewModel> LoadDialogs()
        {
            ProgressWindow wnd = ProgressWindow.ShowBackground("Dialogs loading");
            try
            {
                String[] dialogs = Directory.GetFiles(@"Data/xml/Dialog", "*.xml", SearchOption.TopDirectoryOnly);
                wnd.SetTotal(dialogs.Length);
                foreach (var dlgPath in dialogs)
                 {
                     Dialog dlg = XmlDeserializerFactory.Default.Deserialize<Dialog>(dlgPath);
                     dlg.Translate(LocalizationMap.Instance.Tree);

                     String fileName = Path.GetFileNameWithoutExtension(dlgPath);
                     wnd.Increment(1);

                     yield return new StageViewModel(fileName, dlg);
                 }
            }
            finally
            {
                wnd.Close();
            }
        }

        public static readonly DependencyProperty StagesProperty = DependencyProperty.Register("Stages", typeof(StageViewModel[]), typeof(StageController), new PropertyMetadata(Array.Empty<StageViewModel>()));
        public static readonly DependencyProperty SelectedStageProperty = DependencyProperty.Register("SelectedStage", typeof(StageViewModel), typeof(StageController), new PropertyMetadata(default(StageViewModel)));
        public static readonly DependencyProperty SelectedComponentProperty = DependencyProperty.Register("SelectedComponent", typeof(StageExpandableViewModel), typeof(StageController), new PropertyMetadata(default(StageExpandableViewModel), OnSelectedComponentChanged));
        public static readonly DependencyProperty SelectedMessageProperty = DependencyProperty.Register("SelectedMessage", typeof(StageMessage), typeof(StageController), new PropertyMetadata(default(StageMessage), OnSelectedMessageChanged));
        public static readonly DependencyProperty SelectedSpeakerProperty = DependencyProperty.Register("SelectedSpeaker", typeof(ImageSource), typeof(StageController), new PropertyMetadata(default(ImageSource)));
        public static readonly DependencyProperty SelectedHistoryProperty = DependencyProperty.Register("SelectedHistory", typeof(TranslationHistory), typeof(StageController), new PropertyMetadata(default(TranslationHistory), OnSelectedHistoryChanged));

        public StageViewModel[] Stages
        {
            get => (StageViewModel[]) GetValue(StagesProperty);
            set => SetValue(StagesProperty, value);
        }

        public StageViewModel? SelectedStage
        {
            get => (StageViewModel?) GetValue(SelectedStageProperty);
            set => SetValue(SelectedStageProperty, value);
        }

        public StageExpandableViewModel? SelectedComponent
        {
            get => (StageExpandableViewModel?) GetValue(SelectedComponentProperty);
            set => SetValue(SelectedComponentProperty, value);
        }

        public StageMessage? SelectedMessage
        {
            get => (StageMessage?) GetValue(SelectedMessageProperty);
            set => SetValue(SelectedMessageProperty, value);
        }

        public ImageSource? SelectedSpeaker
        {
            get => (ImageSource?) GetValue(SelectedSpeakerProperty);
            set => SetValue(SelectedSpeakerProperty, value);
        }

        public TranslationHistory? SelectedHistory
        {
            get => (TranslationHistory?) GetValue(SelectedHistoryProperty);
            set => SetValue(SelectedHistoryProperty, value);
        }

        private TextEditor _selectedControl;

        private static Brush DefaultBrush;
        private static readonly Brush SelectedBrush = Brushes.CornflowerBlue;

        public TextEditor? SelectedControl
        {
            get => _selectedControl;
            set
            {
                if (_selectedControl != null)
                    _selectedControl.BorderBrush = DefaultBrush;

                if (value != null)
                {
                    DefaultBrush = value.BorderBrush;
                    value.BorderBrush = SelectedBrush;
                }

                _selectedControl = value;
            }
        }

        public event Action<(TranslationHistory oldValue, TranslationHistory newValue)> SelectedHistoryChanged;

        private static void OnSelectedHistoryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            StageController controller = (StageController) d;
            var h = controller.SelectedHistoryChanged;
            if (h == null)
                return;

            var oldValue = (TranslationHistory) e.OldValue;
            var newValue = (TranslationHistory) e.NewValue;

            h((oldValue, newValue));
        }

        private static void OnSelectedComponentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            StageController control = (StageController) d;
            control.SelectedMessage = null;
            control.SelectedControl = null;
        }

        private static void OnSelectedMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            StageController control = (StageController) d;
            StageMessage? message = (StageMessage?) e.NewValue;

            if (message == null)
            {
                control.SelectedHistory = null;
                control.SelectedSpeaker = null;
                return;
            }

            String language = TranslateLanguages.Instance.CurrentLanguage;
            TranslationFile file = TranslationFile.Get(language);
            control.SelectedHistory = file.GetHistory(message.Key);

            RefreshSelectedSpeaker(message.Speaker, control);
        }

        private static void RefreshSelectedSpeaker(StageSpeakerInfo? speaker, StageController control)
        {
            if (speaker == null)
            {
                control.SelectedSpeaker = null;
                return;
            }

            var name = speaker.Name;
            var emotion = speaker.Emotion;

            var fullImage = PortraitSet.Instance.FindImage(name, emotion);
            if (fullImage == null)
            {
                control.SelectedSpeaker = fullImage;
                return;
            }

            var width = fullImage.PixelWidth;
            if (width == 1024)
            {
                width = fullImage.PixelWidth - 200 * 2;
                var height = fullImage.PixelHeight - 100;
                var sourceRect = new Int32Rect(200, 100, width, height);
                fullImage = new CroppedBitmap(fullImage, sourceRect);
            }

            control.SelectedSpeaker = fullImage;
        }

        public ICommand GoToNextLanguage { get; } = new GoToLanguageCommand(true);
        public ICommand GoToPreviousLanguage { get; } = new GoToLanguageCommand(false);

        private sealed class GoToLanguageCommand : ICommand
        {
            private readonly Boolean _isForward;

            public GoToLanguageCommand(Boolean isForward)
            {
                _isForward = isForward;
            }

            public Boolean CanExecute(Object parameter)
            {
                return true;
            }

            public void Execute(Object parameter)
            {
                RadioButton? first = null;
                RadioButton? last = null;
                foreach (RadioButton button in TranslateLanguages.Instance.EnumerateRadioButtons())
                {
                    if (first is null)
                        first = button;

                    if (last != null)
                    {
                        if (_isForward)
                        {
                            if (last.IsChecked == true)
                            {
                                button.IsChecked = true;
                                return;
                            }
                        }
                        else if (button.IsChecked == true)
                        {
                            last.IsChecked = true;
                            return;
                        }
                    }

                    last = button;
                }

                if (_isForward)
                {
                    if (last != null && last.IsChecked == true)
                        first.IsChecked = true;
                }
                else if (first != null && first.IsChecked == true)
                {
                    last.IsChecked = true;
                }
            }

            public event EventHandler CanExecuteChanged;
        }

        public ICommand SelectStageList { get; } = new GoToComponentCommand(0);
        public ICommand SelectComponentList { get; } = new GoToComponentCommand(1);
        public ICommand SelectEditorControl { get; } = new GoToComponentCommand(2);
        public ICommand ChangeUserName { get; } = new ChangeUserNameCommand();
        public ICommand SaveDictionary { get; } = new SaveDictionaryCommand();

        private class GoToComponentCommand : BaseCommand
        {
            private readonly Int32 _number;

            public GoToComponentCommand(Int32 number)
            {
                _number = number;
            }

            public override void Execute(Object parameter)
            {
                UIElement element;

                switch (_number)
                {
                    case 0:
                        if (StageTab.Instance?.StageList?.SelectedItem == null)
                            return;
                        element = (ListBoxItem) StageTab.Instance.StageList.ItemContainerGenerator.ContainerFromItem(StageTab.Instance.StageList.SelectedItem);
                        break;
                    case 1:
                        element = StageTab.Instance.ComponentsTree;
                        break;
                    case 2:
                        element = ((StageController) StageTab.Instance.DataContext).SelectedControl;
                        break;
                    default:
                        throw new NotSupportedException(_number.ToString());
                }

                element?.Focus();
            }
        }

        private class ChangeUserNameCommand : BaseCommand
        {
            public override void Execute(Object parameter)
            {
                String language = Instance.SelectedHistory.Language;
                LoginWindow wnd = new LoginWindow(TranslationFile.Get(language));
                wnd.ShowDialog();
            }
        }
        
        private class SaveDictionaryCommand : BaseCommand
        {
            public override void Execute(Object parameter)
            {
                SaveFileDialog dlg = new SaveFileDialog
                {
                    FileName = "dic_text.dic",
                    Filter = "Troubleshooter (*.dic)|*.dic"
                };
                
                if (dlg.ShowDialog() != true)
                    return;

                String dictionaryPath = dlg.FileName;

                SaveDictionaryMode mode = SaveDictionaryMode.Latest;

                String language = Instance.SelectedHistory.Language;
                TranslationFile translationFile = TranslationFile.Get(language);
                Localization dic = Localize.GetDic(language);

                Regex tags = new Regex(@"\{([^}]+?)\}", RegexOptions.Compiled);

                var result = new Dictionary<TextId, (String comment, String text)>();
                foreach (LocalizeString str in dic.Enumerate().OrderBy(s=>s.Key))
                {
                    if (str.Key.Type != "Text")
                        continue;

                    String comment = str.Comment;
                    String text = str.Text;

                    TranslationHistory? history = translationFile.FindHistory(str.Key);
                    if (history != null)
                    {
                        TranslationInfo latest = history.Last;

                        if (mode == SaveDictionaryMode.Latest || latest.Approved != default)
                            text = latest.Text;
                    }
                    
                    var m = tags.Match(text);
                    while (m.Success)
                    {
                        String word = m.Groups[0].Value;
                        String tag = m.Groups[1].Value;
                        String? rep = translationFile.Tags.FindSingleValue(tag);
                        if (rep != null)
                        {
                            text = text.Replace(word, rep);
                        }
                        
                        m = m.NextMatch();
                    }

                    result.Add(str.Key, (comment, text));
                }

                using (var sw = File.CreateText(dictionaryPath))
                {
                    foreach (var pair in result.OrderBy(kv => kv.Key))
                    {
                        String key = $"#{pair.Key.Index}";
                        String comment = pair.Value.comment;
                        String text = pair.Value.text;

                        sw.Write(key);
                        sw.Write('\t');
                        
                        sw.Write(comment);
                        sw.Write('\t');
                        
                        sw.WriteLine(text);
                    }
                }
            }
        }

        public void LoadFiles(String type)
        {
            switch (type)
            {
                case "Stages":
                    Stages = LoadStages().ToArray();
                    break;
                case "Dialogs":
                    Stages = LoadDialogs().ToArray();
                    break;
                default:
                    throw new NotSupportedException(type);
            }
        }
    }

    public enum SaveDictionaryMode
    {
        Latest = 1,
        Approved
    }
}
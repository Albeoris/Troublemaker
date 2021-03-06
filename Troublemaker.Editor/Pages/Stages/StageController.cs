﻿using System;
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
using Troublemaker.Framework;
using Troublemaker.Xml;
using Troublemaker.Xml.Dialogs;
using Troublemaker.Xml.Quests;
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
            ProgressWindow wnd = ProgressWindow.ShowBackground("Loading dialogs...");
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

                    var vm = new StageViewModel(fileName, dlg);
                    if (vm.EnumerateExpandable.Count > 0)
                        yield return vm;
                    else
                        continue;
                }
            }
            finally
            {
                wnd.Close();
            }
        }
        
        private IEnumerable<StageViewModel> LoadQuests()
        {
            ProgressWindow wnd = ProgressWindow.ShowBackground("Loading quests...");
            try
            {
                String[] dialogs = Directory.GetFiles(@"Data/xml/Quest", "*.xml", SearchOption.TopDirectoryOnly);
                wnd.SetTotal(dialogs.Length);
                foreach (var dlgPath in dialogs)
                {
                    DialogQuests dlg = XmlDeserializerFactory.Default.Deserialize<DialogQuests>(dlgPath);
                    dlg.Translate(LocalizationMap.Instance.Tree);

                    String fileName = Path.GetFileNameWithoutExtension(dlgPath);
                    wnd.Increment(1);

                    var vm = new StageViewModel(fileName, dlg);
                    if (vm.EnumerateExpandable.Count > 0)
                        yield return vm;
                    else
                        continue;
                }
            }
            finally
            {
                wnd.Close();
            }
        }
        
        // private IEnumerable<StageViewModel> LoadMasteries()
        // {
        //     ProgressWindow wnd = ProgressWindow.ShowBackground("Loading masteries...");
        //     try
        //     {
        //         Map<XmlMastery> masteries = DB.Masteries.Entries;
        //         wnd.SetTotal(masteries.Count);
        //
        //         LocalizationTree tree = LocalizationMap.Instance.Tree["Mastery"];
        //         foreach (XmlMastery mastery in masteries)
        //         {
        //             mastery.Translate(tree);
        //             wnd.Increment(1);
        //         }
        //
        //         IEnumerable<IGrouping<String, XmlMastery>> grouped = masteries.Values
        //             .OrderBy(g => g.Category)
        //             .ThenBy(g => g.Name)
        //             .GroupBy(m => m.Category);
        //
        //         List<ExpandableCollection> collections = new List<ExpandableCollection>();
        //         foreach (IGrouping<String, XmlMastery> g in grouped)
        //         {
        //             String category = String.IsNullOrEmpty(g.Key) ? "Unknown" : g.Key;
        //             collections.Add(new ExpandableCollection(category, g));
        //         }
        //
        //         var vm = new StageViewModel("Masteries", new StageWrapper(collections));
        //         yield return vm;
        //     }
        //     finally
        //     {
        //         wnd.Close();
        //     }
        // }

        public static readonly DependencyProperty StagesProperty = DependencyProperty.Register("Stages", typeof(StageViewModel[]), typeof(StageController), new PropertyMetadata(Array.Empty<StageViewModel>()));
        public static readonly DependencyProperty SelectedStageProperty = DependencyProperty.Register("SelectedStage", typeof(StageViewModel), typeof(StageController), new PropertyMetadata(default(StageViewModel)));
        public static readonly DependencyProperty SelectedComponentProperty = DependencyProperty.Register("SelectedComponent", typeof(StageExpandableViewModel), typeof(StageController), new PropertyMetadata(default(StageExpandableViewModel), OnSelectedComponentChanged));
        public static readonly DependencyProperty SelectedMessageProperty = DependencyProperty.Register("SelectedMessage", typeof(StageMessage), typeof(StageController), new PropertyMetadata(default(StageMessage), OnSelectedMessageChanged));
        public static readonly DependencyProperty SelectedSpeakerProperty = DependencyProperty.Register("SelectedSpeaker", typeof(ImageSource), typeof(StageController), new PropertyMetadata(default(ImageSource)));
        public static readonly DependencyProperty SelectedPreviewProperty = DependencyProperty.Register("SelectedPreview", typeof(Object), typeof(StageController), new PropertyMetadata(default(Object)));
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
        
        public Object? SelectedPreview
        {
            get => (Object?) GetValue(SelectedPreviewProperty);
            set => SetValue(SelectedPreviewProperty, value);
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
            // control.SelectedPreview = new TranslateMasteryPreview();
                
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
                control.SelectedSpeaker = PortraitSet.Instance.FindIcon(name, emotion);
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

        public ICommand GoToNextLine { get; } = new GoToNextLineCommand();
        public ICommand GoToPreviousLine { get; } = new GoToPreviousLineCommand();

        private sealed class GoToNextLineCommand : ICommand
        {
            public GoToNextLineCommand()
            {
            }

            public Boolean CanExecute(Object parameter)
            {
                return true;
            }

            public void Execute(Object parameter)
            {
                TextEditor ctrl = StageController.Instance.SelectedControl;
                if (ctrl == null)
                    return;

                Boolean found = false;
                foreach (var item in MainWindow.Instance.TranslationControl.FindVisualChildren<TranslateControl>())
                {
                    if (found)
                    {
                        item.TextBox.Focus();
                        return;
                    }

                    if (item.TextBox == ctrl)
                    {
                        found = true;
                    }
                }
            }

            public event EventHandler CanExecuteChanged;
        }

        private sealed class GoToPreviousLineCommand : ICommand
        {
            public GoToPreviousLineCommand()
            {
            }

            public Boolean CanExecute(Object parameter)
            {
                return true;
            }

            public void Execute(Object parameter)
            {
                TextEditor ctrl = Instance.SelectedControl;
                if (ctrl == null)
                    return;

                TranslateControl? previous = null;
                foreach (var item in MainWindow.Instance.TranslationControl.FindVisualChildren<TranslateControl>())
                {
                    if (item.TextBox == ctrl)
                    {
                        if (previous != null)
                            previous.TextBox.Focus();
                        return;
                    }

                    previous = item;
                }
            }

            public event EventHandler CanExecuteChanged;
        }
        
        public ICommand SaveAll { get; } = new SaveAllCommand();

        private sealed class SaveAllCommand : ICommand
        {
            public SaveAllCommand()
            {
            }

            public Boolean CanExecute(Object parameter)
            {
                return true;
            }

            public void Execute(Object parameter)
            {
                foreach (var item in MainWindow.Instance.TranslationControl.FindVisualChildren<TranslateControl>())
                {
                    if (item.SaveClick.CanExecute(null))
                        item.SaveClick.Execute(null);
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
                        if (MainWindow.Instance?.FileList?.SelectedItem == null)
                            return;
                        element = (ListBoxItem) MainWindow.Instance.FileList.ItemContainerGenerator.ContainerFromItem(MainWindow.Instance.FileList.SelectedItem);
                        break;
                    case 1:
                        element = MainWindow.Instance.ComponentsTree;
                        break;
                    case 2:
                        element = ((StageController) MainWindow.Instance.DataContext).SelectedControl;
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

                String language = TranslateLanguages.Instance.CurrentLanguage;
                TranslationFile translationFile = TranslationFile.Get(language);
                Localization dic = Localize.GetDic(language);

                Regex tags = new Regex(@"\{([^}]+?)\}", RegexOptions.Compiled);

                var result = new Dictionary<TextId, (String comment, String text)>();

                LocalizeString[] items = dic.Enumerate().OrderBy(s => s.Key).ToArray();
                translationFile.EnsureLoaded(items.Select(i => i.Key).ToArray());

                // Internal
                // MakeHistoryFromLocalizedDictionary(items, translationFile);
                
                foreach (LocalizeString str in items)
                {
                    if (str.Key.Type != "Text")
                        continue;
                    
                    String comment = str.Comment;
                    String text = str.Text;

                    TranslationHistory? history = translationFile.FindLoadedHistory(str.Key);
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

            private static void MakeHistoryFromLocalizedDictionary(LocalizeString[] items, TranslationFile translationFile)
            {
                Regex regex = new Regex(@"[А-Яа-я]+");
                Localize.Read("tmp", "Text", @"C:\Git\C#\Troublemaker\Output\netcoreapp3.0\Dictionary\rus\dic_text.dic");
                Localization tmp = Localize.GetDic("tmp");
                Localization rus = Localize.GetDic("rus");
                Localization eng = Localize.GetDic("eng");
                
                //
                tmp = rus;
                
                foreach (LocalizeString str in items)
                {
                    if (str.Key.Type != "Text")
                        continue;

                    TranslationHistory? history = translationFile.FindLoadedHistory(str.Key);
                    if (history == null)
                    {
                        if (tmp.TryGetValue(str.Key, out var russian) && regex.IsMatch(russian.Text))
                        {
                            String russianText = russian.Text;
                            rus[str.Key].Text = eng[str.Key].Text;
                            
                            history = translationFile.GetHistory(str.Key);
                            history.CurrentText = russianText;
                            history.SaveChanges();
                        }
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
                case "Quests":
                    Stages = LoadQuests().ToArray();
                    break;
                // case "Masteries":
                //     Stages = LoadMasteries().ToArray();
                //     break;
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
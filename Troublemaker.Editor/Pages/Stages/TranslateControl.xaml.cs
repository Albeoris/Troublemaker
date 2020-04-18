using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;
using Troublemaker.Editor.Annotations;
using Troublemaker.Editor.Framework;
using Troublemaker.Editor.Settings;
using Troublemaker.Editor.ViewModels;
using Troublemaker.Xml;

namespace Troublemaker.Editor.Pages
{
    /// <summary>
    /// Логика взаимодействия для TranslateControl.xaml
    /// </summary>
    public partial class TranslateControl : UserControl
    {
        public TranslateControl()
        {
            SaveClick = new SaveCommand(this);
            InitializeComponent();

            Loaded += TranslateControl_Loaded;
            Unloaded += TranslateControl_Unloaded;

            TextBox.TextChanged += TextBoxOnTextChanged;
            TextBox.TextArea.TextEntering += TextAreaOnTextEntering;
            TextBox.TextArea.TextEntered += TextAreaOnTextEntered;
            TextBox.KeyUp += TextBoxOnKeyUp;
            TextBox.PreviewMouseDoubleClick += TextBoxOnPreviewMouseDoubleClick;
        }

        private void TextBoxOnPreviewMouseDoubleClick(Object sender, MouseButtonEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt)
            {
                AutoComplete();
            }
        }

        private void TextBoxOnKeyUp(Object sender, KeyEventArgs e)
        {
            if (e.Key == Key.System)
            {
                if (e.SystemKey != Key.Enter)
                    return;
            }
            else if (e.Key != Key.Enter)
            {
                return;
            }

            if ((e.KeyboardDevice.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt)
            {
                AutoComplete();
            }
            else if ((e.KeyboardDevice.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                OnCtrlEnter();
            }
        }

        private void OnCtrlEnter()
        {
            if (SaveClick.CanExecute(null))
                SaveClick.Execute(null);
        }

        private void AutoComplete()
        {
            String text = TextBox.Text;
            if (text.Length < 1)
                return;

            Int32 offset = Math.Min(TextBox.TextArea.Caret.Offset, text.Length - 1);
            Int32 min = offset;
            Int32 max = offset;
            Boolean leftBracket = false;
            Boolean rightBracket = false;

            while (min > 0)
            {
                var ch = text[min - 1];
                if (Char.IsLetter(ch))
                    min--;
                else
                {
                    if (ch == '{')
                        leftBracket = true;
                    break;
                }
            }

            while (max < text.Length - 1)
            {
                var ch = text[max + 1];
                if (Char.IsLetter(ch))
                    max++;
                else
                {
                    if (ch == '}')
                        rightBracket = true;
                    break;
                }
            }

            if (min == max)
                return;

            if (rightBracket)
            {
                TextBox.Document.Replace(max + 1, 1, String.Empty);
                if (leftBracket)
                    TextBox.Document.Replace(min - 1, 1, String.Empty);
                return;
            }

            if (leftBracket)
            {
                TextBox.Document.Replace(min - 1, 1, String.Empty);
                return;
            }

            max = offset;

            if (Char.IsLetter(text[max]))
            {
                while (max < text.Length - 1)
                {
                    var ch = text[max + 1];
                    if (Char.IsLetter(ch))
                        max++;
                    else
                        break;
                }
            }

            // Open code completion after the user has pressed {:
            _completionWindow = new CompletionWindow(TextBox.TextArea);
            _completionWindow.StartOffset = min;
            _completionWindow.EndOffset = max + 1;
            IList<ICompletionData> data = _completionWindow.CompletionList.CompletionData;

            String language = TranslateLanguages.Instance.CurrentLanguage;
            TranslationFile file = TranslationFile.Get(language);
            var completionData = TagsHighlighting.Ensure(file.Tags).CompletionData;
            foreach (var item in completionData)
                data.Add(item);

            _completionWindow.Show();
            _completionWindow.Closed += OnCompletionWindowOnClosed;

            TextDocument document = TextBox.TextArea.Document;
            if (document != null)
                _completionWindow.CompletionList.SelectItem(document.GetText(min, max - min /* + 1*/));
        }

        private void TextBoxOnTextChanged(Object? sender, EventArgs e)
        {
            if (_history != null)
                _history.CurrentText = TextBox.Text;
            SaveClick.RaiseCanExecuteChanged();
            UpdateFolding();
        }

        private static readonly Regex FoldingRegex = new Regex(@"(\[[^]]+\]+)+", RegexOptions.Compiled);

        private FoldingManager _foldingManager;

        private void UpdateFolding()
        {
            MatchCollection matches = FoldingRegex.Matches(_history.CurrentText);
            if (_foldingManager is null)
            {
                if (matches.Count > 0)
                    _foldingManager = FoldingManager.Install(TextBox.TextArea);
                else
                    return;
            }

            List<NewFolding> result = new List<NewFolding>(matches.Count);
            foreach (Match m in matches)
            {
                var folding = new NewFolding
                {
                    Name = "Tags",
                    DefaultClosed = true,
                    StartOffset = m.Index,
                    EndOffset = m.Index + m.Length
                };
                result.Add(folding);
            }

            _foldingManager.UpdateFoldings(result, -1);
        }

        private CompletionWindow _completionWindow;

        private void TextAreaOnTextEntered(Object sender, TextCompositionEventArgs e)
        {
            if (e.Text == "{")
            {
                // Open code completion after the user has pressed {:
                _completionWindow = new CompletionWindow(TextBox.TextArea);
                IList<ICompletionData> data = _completionWindow.CompletionList.CompletionData;

                String language = TranslateLanguages.Instance.CurrentLanguage;
                TranslationFile file = TranslationFile.Get(language);
                var completionData = TagsHighlighting.Ensure(file.Tags).CompletionData;
                foreach (var item in completionData)
                    data.Add(item);

                _completionWindow.Show();
                _completionWindow.Closed += OnCompletionWindowOnClosed;
            }
        }

        private void OnCompletionWindowOnClosed(Object? sender, EventArgs e)
        {
            _completionWindow = null;
        }

        private void TextAreaOnTextEntering(Object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Length < 1)
                return;

            // Disable new line
            if (e.Text.EndsWith('\n'))
            {
                e.Handled = true;
                return;
            }

            if (_completionWindow == null)
                return;

            if (Char.IsLetterOrDigit(e.Text[0]))
                return;

            // Whenever a non-letter is typed while the completion window is open,
            // insert the currently selected element.
            _completionWindow.CompletionList.RequestInsertion(e);
        }

        private void TranslateControl_Loaded(object sender, RoutedEventArgs e)
        {
            TranslateLanguages.Instance.CurrentLanguagePropertyChanged += OnCurrentLanguagePropertyChanged;

            if (StageController.Instance.SelectedControl == null)
            {
                IInputElement focusedControl = Keyboard.FocusedElement;
                TextBox.Focus();
                focusedControl?.Focus();
            }

            TextBox.Document.UndoStack.ClearAll();
        }

        private void TranslateControl_Unloaded(object sender, RoutedEventArgs e)
        {
            UnsubscribeHistoryUpdated();
            TranslateLanguages.Instance.CurrentLanguagePropertyChanged -= OnCurrentLanguagePropertyChanged;
        }

        private void OnCurrentLanguagePropertyChanged(Object? sender, EventArgs e)
        {
            String language = TranslateLanguages.Instance.CurrentLanguage;
            TranslationFile file = TranslationFile.Get(language);
            TextBox.SyntaxHighlighting = TagsHighlighting.Ensure(file.Tags).Highlighting;
            History = file.GetHistory(Message.Key);
        }

        public SaveCommand SaveClick { get; }

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message", typeof(StageMessage), typeof(TranslateControl), new PropertyMetadata(default(StageMessage), OnMessageChanged));

        private static void OnMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TranslateControl control = (TranslateControl) d;
            StageMessage message = (StageMessage) e.NewValue;

            String language = TranslateLanguages.Instance.CurrentLanguage;
            TranslationFile file = TranslationFile.Get(language);
            control.TextBox.SyntaxHighlighting = TagsHighlighting.Ensure(file.Tags).Highlighting;
            control.History = file.GetHistory(message.Key);

            StageSpeakerInfo? speaker = message.Speaker;
            if (speaker != null)
            {
                ImageSource? background = PortraitSet.Instance.FindIcon(speaker.Name, speaker.Emotion);
                if (background is null && !String.IsNullOrEmpty(speaker.ImagePath))
                    background = ImageSets.FindImageSource(speaker.ImagePath);

                control.SpeakerBackground = background;
                StageActionBalloonType? floating = speaker.Floating;
                if (floating != null)
                {
                    if (floating.IsNormal)
                        control.SpeakerForeground = GeometryRenderer.NormalBalloon;
                    else if (floating.IsShout)
                        control.SpeakerForeground = GeometryRenderer.ShoutBalloon;
                    else if (floating.IsThink)
                        control.SpeakerForeground = GeometryRenderer.ThinkBalloon;

                    if (floating.IsPlayer) control.SpeakerForegroundColor = Color.FromArgb(128, 0, 255, 0);
                    else if (floating.IsCivil) control.SpeakerForegroundColor = Color.FromArgb(128, 0, 255, 255);
                    else if (floating.IsAlly) control.SpeakerForegroundColor = Color.FromArgb(128, 128, 255, 0);
                    else if (floating.IsNeutral) control.SpeakerForegroundColor = Color.FromArgb(128, 90, 120, 255);
                    else if (floating.IsThird) control.SpeakerForegroundColor = Color.FromArgb(128, 255, 0, 255);
                    else if (floating.IsEnemy) control.SpeakerForegroundColor = Color.FromArgb(128, 255, 0, 0);
                    else if (floating.IsBeast) control.SpeakerForegroundColor = Color.FromArgb(128, 255, 255, 0);
                    else if (floating.IsMachine) control.SpeakerForegroundColor = Color.FromArgb(128, 0, 0, 0);
                }
                else
                {
                    control.SpeakerForeground = null;
                }
            }
            else
            {
                control.SpeakerForeground = null;
                control.SpeakerBackground = null;
            }
        }

        public StageMessage Message
        {
            get => (StageMessage) GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        private TranslationHistory _history;

        private TranslationHistory History
        {
            get => _history;
            set
            {
                UnsubscribeHistoryUpdated();
                _history = value;
                SubscribeHistoryUpdated();

                OnHistoryUpdated(value);
            }
        }

        private void SubscribeHistoryUpdated()
        {
            if (_history != null)
                _history.Changed += OnHistoryUpdated;
        }

        private void UnsubscribeHistoryUpdated()
        {
            if (_history != null)
                _history.Changed -= OnHistoryUpdated;
        }

        private void OnHistoryUpdated(TranslationHistory history)
        {
            if (history != null)
                TextBox.Text = history.CurrentText;
            SaveClick.RaiseCanExecuteChanged();
        }

        public static readonly DependencyProperty SpeakerBackgroundProperty = DependencyProperty.Register("SpeakerBackground", typeof(ImageSource), typeof(TranslateControl), new PropertyMetadata(default(ImageSource)));
        public static readonly DependencyProperty SpeakerForegroundProperty = DependencyProperty.Register("SpeakerForeground", typeof(Geometry), typeof(TranslateControl), new PropertyMetadata(GeometryRenderer.NormalBalloon));
        public static readonly DependencyProperty SpeakerForegroundColorProperty = DependencyProperty.Register("SpeakerForegroundColor", typeof(Color), typeof(TranslateControl), new PropertyMetadata(Colors.White));

        public ImageSource? SpeakerBackground
        {
            get => (ImageSource?) GetValue(SpeakerBackgroundProperty);
            set => SetValue(SpeakerBackgroundProperty, value);
        }

        public Geometry? SpeakerForeground
        {
            get => (Geometry?) GetValue(SpeakerForegroundProperty);
            set => SetValue(SpeakerForegroundProperty, value);
        }

        public Color? SpeakerForegroundColor
        {
            get => (Color?) GetValue(SpeakerForegroundColorProperty);
            set => SetValue(SpeakerForegroundColorProperty, value);
        }

        public sealed class SaveCommand : ICommand
        {
            private readonly TranslateControl _control;

            public SaveCommand(TranslateControl control)
            {
                _control = control;
            }

            public Boolean CanExecute(Object parameter)
            {
                var history = _control.History;
                if (history is null)
                    return false;

                return history.HasChanges;
            }

            public void Execute(Object parameter)
            {
                if (!LoginWindow.CheckAuthenticated()) return;

                _control.History.SaveChanges();
                RaiseCanExecuteChanged();
            }

            public void RaiseCanExecuteChanged()
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }

            public event EventHandler CanExecuteChanged;
        }

        private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            StageController.Instance.SelectedMessage = Message;
            StageController.Instance.SelectedControl = TextBox;
        }
    }
}
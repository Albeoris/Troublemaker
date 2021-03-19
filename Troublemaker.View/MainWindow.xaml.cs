using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NLua;
using Troublemaker.Xml;
using Path = System.IO.Path;

namespace Troublemaker.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // using (var interpreter = new LuaInstance())
            //      interpreter.Exec();

            Controller = new Controller(Types, Masteries, MasterySets, Content);
            Controller.Init();
        }

        public Controller Controller { get; }
    }


    public sealed class Controller
    {
        private readonly ListView _types;
        private readonly ListView _masteries;
        private readonly ListView _masterySets;
        private readonly TextBox _content;

        private readonly HashSet<String> _checkedTypes = new HashSet<String>();
        private readonly HashSet<String> _checkedMastery = new HashSet<String>();
        
        private MasteryTypeViewModel[] _listTypes;
        private MasteryViewModel[] _listMastery;
        private Dictionary<String, MasteryViewModel> _dicMastery;
        private MasterySetViewModel[] _listSets;

        public Controller(ListView types, ListView masteries, ListView masterySets, TextBox content)
        {
            _types = types;
            _masteries = masteries;
            _masterySets = masterySets;
            _content = content;
        }

        internal void Init()
        {
            new ResourceLoader(Environment.CurrentDirectory).Init();

            _listMastery = DB.Masteries.Entries.Values
                .Where(Filter)
                .Select(v => new MasteryViewModel(v, this))
                .OrderBy(v => v.DisplayName)
                .ToArray();

            _listTypes = _listMastery
                .Select(m=>m.Type)
                .Where(m=>!String.IsNullOrEmpty(m))
                .Distinct()
                .OrderBy(m=>m)
                .Select(m=> new MasteryTypeViewModel(m, this))
                .ToArray();

            _dicMastery = _listMastery.ToDictionary(d=>d.Name);

            _listSets = DB.MasterySets.Entries.Values
                .Select(v => new MasterySetViewModel(v, this))
                .OrderBy(v => v.DisplayName)
                .ToArray();

            _types.ItemsSource = _listTypes;
            _masteries.ItemsSource = _listMastery;
            _masterySets.ItemsSource = _listSets;
        }

        private bool Filter(XmlMastery arg)
        {
            switch (arg.Category)
            {
                case "Normal":
                case "Sub":
                case "Attack":
                case "Defence":
                case "Ability":
                    return true;
            }
            return false;
        }
        
        internal bool IsTypeChecked(string masteryType)
        {
            return _checkedTypes.Contains(masteryType);
        }

        internal void SetTypeChecked(string masteryType, bool value)
        {
            if (value)
                _checkedTypes.Add(masteryType);
            else
                _checkedTypes.Remove(masteryType);

            Refresh();
        }

        internal bool IsChecked(XmlMastery mastery)
        {
            return _checkedMastery.Contains(mastery.Name);
        }

        internal void SetChecked(XmlMastery mastery, Boolean value)
        {
            if (value)
                _checkedMastery.Add(mastery.Name);
            else
                _checkedMastery.Remove(mastery.Name);

            Refresh();
        }

        private void Refresh()
        {
            var listMasteries = _listMastery;
            var listSets = _listSets;

            if (_checkedTypes.Count > 0)
            {
                listMasteries = _listMastery.Where( m=> _checkedTypes.Contains(m.Type)).ToArray();
                listSets = _listSets.Where(s =>
                {
                    foreach (var value in s.EnumerateMasteries())
                    {
                        if (!DB.Masteries.Entries.TryGetValue(value, out var mastery) || !_checkedTypes.Contains(mastery.Type))
                        {
                            return false;
                        }
                        return true;
                    }

                    return false;
                }).ToArray();
            }

            var dic = listMasteries.ToDictionary(m=>m.Name);


            if (_checkedMastery.Count == 0)
            {
                _masteries.ItemsSource = listMasteries;
                _masterySets.ItemsSource = listSets;
            }
            else
            {
                var masteries = new List<MasteryViewModel>();
                var sets = new List<MasterySetViewModel>();
                var hash = new HashSet<String>();
                foreach (var item in listSets)
                {
                    Boolean added = false;

                    foreach (var mastery in item.EnumerateMasteries())
                    {
                        if (_checkedMastery.Contains(mastery))
                        {
                            if (dic.TryGetValue(mastery, out var mas) && hash.Add(mastery))
                                masteries.Add(mas);
                            added = true;
                        }
                    }

                    if (added)
                        sets.Add(item);
                }

                //_masteries.ItemsSource = masteries;
                _masterySets.ItemsSource = sets;
            }
        }

        internal void SetSelected(XmlMasterySet set, MasterySetViewModel view, Boolean value)
        {
            if (value)
            {
                _content.Text = Format(view);
            }
        }

        private string Format(MasterySetViewModel set)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(set.DisplayName);
            sb.AppendLine();

            foreach (var mastery in set.EnumerateMasteryViewModels())
            {
                sb.AppendLine(mastery.DisplayName + " (" + mastery.Type + ")");
            }
            
            return sb.ToString();
        }
    }

    public sealed class MasterySetViewModel
    {
        private readonly XmlMasterySet _set;
        private readonly Controller _controller;

        public MasterySetViewModel(XmlMasterySet set, Controller controller)
        {
            _set = set;
            _controller = controller;
        }

        public String DisplayName => _set.Name;

        public String Mastery1 => _set.Mastery1;
        public String Mastery2 => _set.Mastery2;
        public String Mastery3 => _set.Mastery3;
        public String Mastery4 => _set.Mastery4;

        public Boolean IsSelected
        {
            set => _controller.SetSelected(_set, this, value);
        }

        public IEnumerable<String> EnumerateMasteries()
        {
            if (Mastery1 != "None") yield return Mastery1;
            if (Mastery2 != "None") yield return Mastery2;
            if (Mastery3 != "None") yield return Mastery3;
            if (Mastery4 != "None") yield return Mastery4;
        }

        public IEnumerable<MasteryViewModel> EnumerateMasteryViewModels()
        {
            foreach (var item in EnumerateMasteries())
            {
                if (DB.Masteries.Entries.TryGetValue(item, out var mastery))
                    yield return new MasteryViewModel(mastery, _controller);
            }
        }
    }

    public sealed class MasteryTypeViewModel
    {
        private readonly String _masteryType;
        private readonly Controller _controller;

        public MasteryTypeViewModel(String masteryType, Controller controller)
        {
            _masteryType = masteryType;
            _controller = controller;
        }

        public String DisplayName => _masteryType;

        public Boolean IsChecked
        {
            get => _controller.IsTypeChecked(_masteryType);
            set => _controller.SetTypeChecked(_masteryType, value);
        }
    }

    public sealed class MasteryViewModel
    {
        private readonly XmlMastery _mastery;
        private readonly Controller _controller;

        public MasteryViewModel(XmlMastery mastery, Controller controller)
        {
            _mastery = mastery;
            _controller = controller;
        }

        public String Name => _mastery.Name;
        public String Category => _mastery.Category;
        public String Type => _mastery.Type;
        
        public String DisplayName
        {
            get
            {
                LocalizationTree tree = LocalizationMap.Instance.Tree["Mastery"];

                if (tree.TryGet(_mastery.Name, out var m))
                {
                    if (m.TryGet("Base_Title", out var v))
                    {
                        return $"[{_mastery.Category}] {Localize.Get("eng", v.Value).Text}";
                    }
                }
                
                return "Unknown Name";
            }
        }

        public Boolean IsChecked
        {
            get => _controller.IsChecked(_mastery);
            set => _controller.SetChecked(_mastery, value);
        }
    }

    public sealed class ResourceLoader
    {
        public String DirectoryPath { get; }

        public ResourceLoader(String directoryPath)
        {
            DirectoryPath = directoryPath;
        }

        public String LocalizationDirectory => Path.Combine(DirectoryPath, "Dictionary");
        public String XmlDirectory => Path.Combine(DirectoryPath, "Data", "xml");

        public void Init()
        {
            LoadLocalization();
        }

        private void LoadLocalization()
        {
            String keymapPath = Path.Combine(LocalizationDirectory, "keymap.dkm");
            String[] directories = Directory.GetDirectories(LocalizationDirectory);

            LocalizationMap.LoadMap(keymapPath);

            foreach (String child in directories)
            {
                String keywordPath = Path.Combine(child, "dic_keyword.dic");
                String textPath = Path.Combine(child, "dic_text.dic");

                String language = Path.GetFileName(child);
                Localize.Read(language, "Keyword", keywordPath);
                Localize.Read(language, "Text", textPath);
            }

            Localize.Remap();
        }
    }
}
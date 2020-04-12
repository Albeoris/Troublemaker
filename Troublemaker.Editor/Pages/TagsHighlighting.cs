using System;
using System.Collections.Generic;
using System.Windows.Media;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using Troublemaker.Framework;
using Troublemaker.Xml;

namespace Troublemaker.Editor.Pages
{
    public sealed class TagsHighlighting
    {
        private static readonly Dictionary<TranslationTags, TagsHighlighting> Cache = new Dictionary<TranslationTags, TagsHighlighting>();
        
        public static TagsHighlighting Ensure(TranslationTags tags)
        {
            return Cache.Ensure(tags, () => new TagsHighlighting(tags));
        }

        public IHighlightingDefinition Highlighting { get; }
        public IReadOnlyList<ICompletionData> CompletionData { get; }

        public TagsHighlighting(TranslationTags tags)
        {
            var syntaxDefinition = MakeSyntaxDefinition(tags);
            Highlighting = HighlightingLoader.Load(syntaxDefinition, HighlightingManager.Instance);

            var completionData = MakeCompletionData(tags);
            CompletionData = completionData;
        }

        private static XshdSyntaxDefinition MakeSyntaxDefinition(TranslationTags tags)
        {
            XshdSyntaxDefinition syntaxDefinition = new XshdSyntaxDefinition();
            XshdRuleSet ruleSet = new XshdRuleSet();

            var span = MakeBrackets(tags);
            ruleSet.Elements.Add(span);
            
            var colors = MakeColors();
            ruleSet.Elements.Add(colors);
            
            var nativeTags = MakeNativeTags();
            ruleSet.Elements.Add(nativeTags);
            
            var nativeComments = MakeNativeComments();
            ruleSet.Elements.Add(nativeComments);

            var keywords = MakeKeywords(tags);
            ruleSet.Elements.Add(keywords);

            syntaxDefinition.Elements.Add(ruleSet);
            return syntaxDefinition;
        }

        private IReadOnlyList<ICompletionData> MakeCompletionData(TranslationTags tags)
        {
            List<ICompletionData> result = new List<ICompletionData>(tags.All.Count);
            foreach ((String tag, String value) in tags.Pairs)
                result.Add(new CompletionItem(tag, value));
            return result;
        }

        private static XshdKeywords MakeKeywords(TranslationTags tags)
        {
            XshdKeywords keywords = new XshdKeywords()
            {
                ColorReference = new XshdReference<XshdColor>(new XshdColor()
                {
                    Foreground = new SimpleHighlightingBrush(Colors.ForestGreen)
                })
            };

            foreach (String tag in tags.All)
                keywords.Words.Add(tag);
            return keywords;
        }
        
        private static XshdSpan MakeNativeTags()
        {
            XshdSpan span = new XshdSpan
            {
                BeginRegex = @"\$", EndRegex = @"\$",
                SpanColorReference = new XshdReference<XshdColor>(new XshdColor
                {
                    Foreground = new SimpleHighlightingBrush(Colors.RoyalBlue)
                }),
            };
            return span;
        }
        
        private static XshdSpan MakeNativeComments()
        {
            XshdSpan span = new XshdSpan
            {
                BeginRegex = @"\[", EndRegex = @"\]",
                SpanColorReference = new XshdReference<XshdColor>(new XshdColor
                {
                    Foreground = new SimpleHighlightingBrush(Colors.RoyalBlue)
                }),
            };
            return span;
        }
        
        private static XshdSpan MakeColors()
        {
            XshdSpan span = new XshdSpan
            {
                BeginRegex = @"\[colour='", EndRegex = @"'\]",
                SpanColorReference = new XshdReference<XshdColor>(new XshdColor
                {
                    Foreground = new SimpleHighlightingBrush(Colors.RoyalBlue)
                }),
                RuleSetReference = MakeColorKeywords()
            };
            return span;
        }
        
        private static XshdReference<XshdRuleSet> MakeColorKeywords()
        {
            XshdRuleSet ruleSet = new XshdRuleSet();
            var background = new SimpleHighlightingBrush(Colors.DimGray);

            foreach ((String name, Color color) in GameColors.KnownColors.Pairs)
            {
                XshdRule unknownRule = new XshdRule
                {
                    Regex = name,
                    ColorReference = new XshdReference<XshdColor>(new XshdColor()
                    {
                        Foreground = new SimpleHighlightingBrush(color),
                        Background = background
                    })
                };
                ruleSet.Elements.Add(unknownRule);
            }

            return new XshdReference<XshdRuleSet>(ruleSet);
        }

        private static XshdSpan MakeBrackets(TranslationTags tags)
        {
            XshdSpan span = new XshdSpan
            {
                BeginRegex = @"\{", EndRegex = @"\}",
                SpanColorReference = new XshdReference<XshdColor>(new XshdColor
                {
                    Foreground = new SimpleHighlightingBrush(Colors.RoyalBlue)
                }),
                RuleSetReference = MakeBracketedKeywords(tags)
            };
            return span;
        }
        
        private static XshdReference<XshdRuleSet> MakeBracketedKeywords(TranslationTags tags)
        {
            XshdRuleSet ruleSet = new XshdRuleSet();
            
            var color = new SimpleHighlightingBrush(Colors.RoyalBlue);
            
            XshdKeywords keywords = new XshdKeywords()
            {
                ColorReference = new XshdReference<XshdColor>(new XshdColor
                {
                    Foreground = color
                })
            };

            foreach (String tag in tags.All)
                keywords.Words.Add(tag);
            
            ruleSet.Elements.Add(keywords);

            XshdRule unknownRule = new XshdRule
            {
                Regex = @"[^}]+\}?",
                ColorReference = new XshdReference<XshdColor>(new XshdColor()
                {
                    Foreground = new SimpleHighlightingBrush(Colors.Red)
                })
            };
            ruleSet.Elements.Add(unknownRule);

            return new XshdReference<XshdRuleSet>(ruleSet);
        }
        
        private  sealed class  CompletionItem : ICompletionData
        {
            public CompletionItem(String tag, String value)
            {
                Text = tag;
                Description = MakeDescription(tag, value);
            }

            private static String MakeDescription(String tag, String value)
            {
                Char last = tag[tag.Length - 1];
                switch (last)
                {
                    case 'Р':  return $"Мне не хватает {value}.";
                    case 'Д':  return $"Это нужно {value}.";
                    case 'В':  return $"{value} трясёт.";
                    case 'Т':  return $"Сделано {value}.";
                    case 'П':  return $"Дело в {value}.";
                    case '1':  return $"Здесь 1 {value}.";
                    case '2':  return $"Здесь 2 {value}.";
                    case '5':  return $"Здесь 5 {value}.";
                }

                return value;
            }

            public ImageSource Image { get; }
            public String Text { get; }
            public Object Content => Text;
            public Object Description { get; }
            public Double Priority { get; }

            public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
            {
                Int32 textLength = textArea.Document.TextLength;
                String tag = Text;
                if (textLength < 1)
                    return;

                if (tag[0] != '{')
                {
                    if (completionSegment.Offset == 0 || textArea.Document.GetText(completionSegment.Offset - 1, 1) != "{")
                        tag = '{' + tag;
                }

                if (tag[tag.Length - 1] != '}')
                {
                    if (completionSegment.EndOffset == textLength || textArea.Document.GetText(completionSegment.EndOffset, 1) != "}")
                        tag = tag + '}';
                }

                textArea.Document.Replace(completionSegment, tag);
            }
        }
    }
}
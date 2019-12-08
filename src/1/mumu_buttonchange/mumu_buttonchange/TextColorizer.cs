using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace mumu_buttonchange
{

	public enum CodeLanguage 
    {
        Plain = 0, 
        XAML, 
        CS
    };
	public enum LexemType 
    { 
        Error = 0,
        Block,
        Symbol, 
        Object,
        Property, 
        Value, 
        Space,
        LineBreak, 
        Complex, 
        Comment, 
        PlainText,
        String, 
        KeyWord 
    }
	public class CodeBlockPresenter 
    {
		public CodeLanguage CodeLanguage { get; set; }
		public CodeBlockPresenter() : this(CodeLanguage.Plain) { }
		public CodeBlockPresenter(CodeLanguage language) 
        {
			CodeLanguage = language;
		}

		public TextBlock ToTextBlock(string text) 
        {
			TextBlock textBlock = new TextBlock();
			FillInlines(text, textBlock.Inlines);
			return textBlock;
		}
		public void FillInlines(string text, InlineCollection collection) 
        {
			text = text.Replace("\r", "");
			CodeLexem lex = new CodeLexem(text);
			List<CodeLexem> res = lex.Parse(CodeLanguage);
			foreach (CodeLexem elem in res)
				collection.Add(elem.ToInline());
		}
	}
	public class CodeLexem 
    {
		public string Text { get; set; }
		public LexemType Type { get; set; }
		public CodeLexem() : this("") { }
		public CodeLexem(string text) : this(LexemType.Complex, text) { }
		public CodeLexem(LexemType type, string text) 
        {
			Text = text;
			Type = type;
		}
		public List<CodeLexem> Parse(CodeLanguage lang) 
        {
			switch(lang) {
				case CodeLanguage.Plain: return (new BaseParser()).Parse(Text);
				case CodeLanguage.XAML: return (new XAMLParser()).Parse(Text);
				case CodeLanguage.CS: return (new CSParser()).Parse(Text);
			}
			return null;
		}
		protected Run CreateRun(string text, Color color)
        {
            return new Run() 
            { 
                Text = text, Foreground = new SolidColorBrush(color)
            };
        }
		public Inline ToInline()
        {
			switch(Type) {
				case LexemType.Complex: return CreateRun(Text, Colors.LightGray);
				case LexemType.LineBreak:	return new LineBreak();
				case LexemType.Object:return CreateRun(Text, Colors.Brown);
				case LexemType.Property: return CreateRun(Text, Colors.Red);
				case LexemType.Space: return CreateRun(Text, Colors.Black);
				case LexemType.Symbol: return CreateRun(Text, Colors.Blue);
				case LexemType.Value: return CreateRun(Text, Colors.Blue);
				case LexemType.PlainText: return CreateRun(Text, Colors.Black);
				case LexemType.Comment: return CreateRun(Text, Colors.Green);
				case LexemType.Error: return CreateRun(Text, Colors.LightGray);
				case LexemType.String: return CreateRun(Text, Colors.Brown);
				case LexemType.KeyWord: return CreateRun(Text, Colors.Blue);
			}
			return null;
		}
	}
	public class BaseParser
    {
		char[] SpaceChars = { ' ', '	' };
		public BaseParser() { }
		protected char previousSimbol;
		protected string StringCut(ref string text, int count) 
        {
			if (count == 0)
				return string.Empty;
			previousSimbol = text[count - 1];
			string result = text.Substring(0, count);
			text = text.Substring(count);
			return result;
		}
		protected void TrySpace(List<CodeLexem> res, ref string text)
        {
			StringBuilder spaces = new StringBuilder();
			while(SpaceChars.Contains(text[0]))
				spaces.Append(StringCut(ref text, 1));
			if(spaces.Length > 0)
				res.Add(new CodeLexem(LexemType.Space, spaces.ToString()));
		}
		protected bool TryExtract(List<CodeLexem> res, ref string text, string lex, LexemType type) 
        {
			if(text.StartsWith(lex)) {
				res.Add(new CodeLexem(type, StringCut(ref text, lex.Length)));
				return true;
			}
			return false;
		}
		protected void TryExtractTo(List<CodeLexem> res, ref string text, string lex, LexemType type, string except) 
        {
			int index = text.IndexOf(lex);
			if(except != null)
				while(index >= 0 && text.Substring(0, index + 1).EndsWith(except))
					index = text.IndexOf(lex, index + 1);
			if(index < 0) return;
			BreackLines(res, ref text, index + lex.Length, type);
		}

		protected void BreackLines(List<CodeLexem> res, ref string text, int to, LexemType type) 
        {
			while(text.Length > 0 && to > 0) 
            {
				int index = text.IndexOf("\n");
				if(index >= to) {
					res.Add(new CodeLexem(type, StringCut(ref text, to)));
					break;
				}
				if(index != 0) res.Add(new CodeLexem(type, StringCut(ref text, index)));
				res.Add(new CodeLexem(LexemType.LineBreak, StringCut(ref text, 1)));
				to -= index + 1;
			}
		}
		public virtual List<CodeLexem> Parse(string text) 
        {
			List<CodeLexem> res = new List<CodeLexem>();
			string extendedText = text + "\n";
			BreackLines(res, ref extendedText, extendedText.Length, LexemType.PlainText);
			return res;
		}
	}
	public class CSParser : BaseParser 
    {
		char[] CSEndOfTerm = { ' ', '	', '\n', '=', '/', '>', '<', '"', '{', '}', ',', '(', ')', ';', '\0' };
		string[] CSKeyWords = { "abstract","event","new","struct","as","explicit","null",
								"switch","base","extern","object","this","bool","false",
								"operator","throw","break","finally","out","true","byte",
								"fixed","override","try","case","float","params","typeof",
								"catch","for","private","uint","char","foreach","protected",
								"ulong","checked","goto","public","unchecked","class",
								"if","readonly","unsafe","const","implicit","ref","ushort",
								"continue","in","return","using","decimal","int","sbyte",
								"virtual","default","interface","sealed","volatile","delegate",
								"internal","short","void","do","is","sizeof","while",
								"double","lock","stackalloc","else","long","static","enum",
								"namespace","string","from","get","group","into","join","let",
								"orderby","partial","select","set","value","var","where","yield",
							    "#region","#endregion","#if","#endif"};
		public CSParser() { }
		public override List<CodeLexem> Parse(string text) {
			string extendedText = text + "\n";
			List<CodeLexem> res = new List<CodeLexem>();
			while(extendedText.Length > 0) {
				if(TryExtract(res, ref extendedText, "/*", LexemType.Comment)) {
					TryExtractTo(res, ref extendedText, "*/", LexemType.Comment, null);
				}
				if(TryExtract(res, ref extendedText, "//", LexemType.Comment)) {
					TryExtractTo(res, ref extendedText, "\n", LexemType.Comment, null);
				}
				if(TryExtract(res, ref extendedText, "\"", LexemType.String)) {
					TryExtractTo(res, ref extendedText, "\"", LexemType.String, "\\\"");
				}
				if(TryExtract(res, ref extendedText, "'", LexemType.String)) {
					TryExtractTo(res, ref extendedText, "'", LexemType.String, null);
				}
				ParseCSKeyWord(res, ref extendedText, LexemType.KeyWord);
				ParseCSSymbol(res, ref extendedText, LexemType.PlainText);
				TrySpace(res, ref extendedText);
				TryExtract(res, ref extendedText, "\n", LexemType.LineBreak);
			}
			return res;
		}
		int lastLength = -1;
		void ParseCSSymbol(List<CodeLexem> res, ref string text, LexemType lt) {
			if(lastLength == -1 || lastLength != text.Length) {
				lastLength = text.Length;
				return;
			}
			CodeLexem cl = res.Count > 0 ? res.Last() : null;
			if(cl != null && cl.Type == LexemType.PlainText)
				cl.Text += StringCut(ref text, 1);
			else
				res.Add(new CodeLexem(LexemType.PlainText, StringCut(ref text, 1)));
		}
		void ParseCSKeyWord(List<CodeLexem> res, ref string text, LexemType type) {
			int index = -1;
			if(!CSEndOfTerm.Contains(previousSimbol)) return;
			foreach(string str in CSKeyWords) {
				index = text.IndexOf(str);
				if(index == 0) {
					index = str.Length;
					if(!CSEndOfTerm.Contains(text[index])) continue;
					break;
				}
			}
			if(index < 0) return;
			res.Add(new CodeLexem(type, StringCut(ref text, index)));
		}
	}

	public class XAMLParser : BaseParser 
    {
		char[] XAMLEndOfTerm = { ' ', '	', '\n', '=', '/', '>', '<', '"', '{', '}', ',' };
		char[] XAMLSymbol = { '=', '/', '>', '"', '{', '}', ',' };
		char[] XAMLNamespaceDelimeter = { ':' };
		public XAMLParser() { }
		protected bool IsInsideBlock = false;
		public override List<CodeLexem> Parse(string text) {
			string extendedText = text + "\n";
			List<CodeLexem> res = new List<CodeLexem>();
			while(extendedText.Length > 0) {
				if(TryExtract(res, ref extendedText, "<!--", LexemType.Comment)) {
					TryExtractTo(res, ref extendedText, "-->", LexemType.Comment, null);
				}
				if(extendedText.StartsWith("<")) IsInsideBlock = false;
				if (TryExtract(res, ref extendedText, "\"{}", LexemType.Value)) 
					TryExtractTo(res, ref extendedText, "\"", LexemType.Value, null);
				if(TryExtract(res, ref extendedText, "</", LexemType.Symbol) ||
				   TryExtract(res, ref extendedText, "<", LexemType.Symbol) ||
				   TryExtract(res, ref extendedText, "{", LexemType.Symbol) ||
				   TryExtract(res, ref extendedText, "\"{", LexemType.Symbol)) {
					ParseXAMLKeyWord(res, ref extendedText, LexemType.Object);
				}
				if(TryExtract(res, ref extendedText, "\"", LexemType.Value)) {
					TryExtractTo(res, ref extendedText, "\"", LexemType.Value, null);
				}
				ParseXAMLKeyWord(res, ref extendedText, IsInsideBlock ? LexemType.Object : LexemType.Property);
				TryExtract(res, ref extendedText, "}\"", LexemType.Symbol);
				if(extendedText.StartsWith(">")) IsInsideBlock = true;
				ParseSymbol(res, ref extendedText, LexemType.Symbol);
				TrySpace(res, ref extendedText);
				TryExtract(res, ref extendedText, "\n", LexemType.LineBreak);
			}
			return res;
		}
		void ParseSymbol(List<CodeLexem> res, ref string text, LexemType lt) 
        {
			int index = text.IndexOfAny(XAMLSymbol);
			if(index != 0) return;
			res.Add(new CodeLexem(LexemType.Symbol, text.Substring(0, 1)));
			text = text.Substring(1);
		}
		void ParseXAMLKeyWord(List<CodeLexem> res, ref string text, LexemType type) 
        {
			int index = text.IndexOfAny(XAMLEndOfTerm);
			if(index <= 0) return;
			int nsIndex = text.IndexOf(':');
			if(nsIndex > 0 && nsIndex < index) 
            {
				res.Add(new CodeLexem(type, StringCut(ref text, nsIndex)));
				res.Add(new CodeLexem(LexemType.Symbol, StringCut(ref text, 1)));
				res.Add(new CodeLexem(type, StringCut(ref text, index - nsIndex - 1)));
			} 
            else 
            {
				res.Add(new CodeLexem(type, StringCut(ref text, index)));
			}
		}
	}
}

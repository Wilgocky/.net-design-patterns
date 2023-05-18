namespace Creational.Builder.HtmlElement;

internal class HtmlElement
{
    private readonly List<HtmlElement> _children;

    public string Name { get; }
    public string Text { get; }
    public IReadOnlyCollection<HtmlElement> Children => _children.AsReadOnly();

    private HtmlElement(string name, string text)
    {
        Name = name;
        Text = text;
        _children = new List<HtmlElement>();
    }

    public static HtmlElementBuilder Builder(HtmlElement root) => new(root);
    
    public override string ToString()
    {
        var inner = _children.Select(p => p.ToString());
        return $"<{Name}>{Text}{string.Join("", inner)}</{Name}>";
    }

    public static implicit operator string(HtmlElement element) => element.ToString();
    public static implicit operator HtmlElement(string tag) => new(tag, "");

    internal class HtmlElementBuilder
    {
        private readonly HtmlElement _root;

        public HtmlElementBuilder(HtmlElement root)
        {
            _root = root;
        }

        public HtmlElementBuilder AddChild(string tag, string value)
        {
            _root._children.Add(new HtmlElement(tag, value));
            return this;
        }
        
        public static implicit operator HtmlElement(HtmlElementBuilder builder) => builder._root;
        public static implicit operator string(HtmlElementBuilder builder) => builder._root.ToString();
    }
}
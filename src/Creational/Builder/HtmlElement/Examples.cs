using System.Text;
using FluentAssertions;
using Xunit;

namespace Creational.Builder.HtmlElement;

public class Examples
{
    [Fact]
    public void Builder_MakesItEasyToNestObjects()
    {
        var texts = new[] { "Hello", "World"};
        var sb = new StringBuilder("<ul>");
        foreach (var text in texts)
        {
            sb.Append($"<li>{text}</li>");
        }
        sb.Append("</ul>");
        var withoutDedicatedBuilder = sb.ToString();

        string withDedicatedBuilder = HtmlElement.Builder("ul")
            .AddChild("li", "Hello")
            .AddChild("li", "World");

        new[]{ withoutDedicatedBuilder, withDedicatedBuilder }
            .Should()
            .AllBeEquivalentTo("<ul><li>Hello</li><li>World</li></ul>");
    }
}
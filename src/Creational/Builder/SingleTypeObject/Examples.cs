using FluentAssertions;
using Xunit;

namespace Creational.Builder.ExternalComplexObject;

public class Examples
{
    private const string Something = nameof(Something);
    private const string Else = nameof(Else);
    private const string Other = nameof(Other);
    private const string Optional = nameof(Optional);

    [Fact]
    public void Builder_MakesItDifficultToMistakenlyPassParametersOfTheSameType()
    {
        var expected = new SingleTypeObject(Something, Else, Other)
        {
            Optional = Optional
        };
        
        var mistakenlyCreated = new SingleTypeObject(Something, Other, Else){ Optional = Optional };

        SingleTypeObject valid = SingleTypeObject.Builder()
            .WithSomething(Something)
            .WithElse(Else)
            .WithOther(Other)
            .WithOptional(Optional);

        valid.Should().BeEquivalentTo(expected);
        mistakenlyCreated.Should().NotBeEquivalentTo(expected);
        mistakenlyCreated.Should().NotBeEquivalentTo(valid);
    }
}
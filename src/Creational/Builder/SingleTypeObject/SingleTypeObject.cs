namespace Creational.Builder.ExternalComplexObject;

internal class SingleTypeObject
{
    public string Something { get; }
    public string Else { get; }
    public string Other { get; }
    public string? Optional { get; set; }

    public SingleTypeObject(string something, string @else, string other)
    {
        Something = something;
        Else = @else;
        Other = other;
    }

    public static SingleTypeObjectBuilder Builder() => new();

    internal class SingleTypeObjectBuilder
    {
        private string? _something;
        private string? _else;
        private string? _other;
        private string? _optional;

        public SingleTypeObjectBuilder WithSomething(string v)
        {
            _something = v;
            return this;
        }
            
        public SingleTypeObjectBuilder WithElse(string v)
        {
            _else = v;
            return this;
        }
            
        public SingleTypeObjectBuilder WithOther(string v)
        {
            _other = v;
            return this;
        }

        public SingleTypeObjectBuilder WithOptional(string v)
        {
            _optional = v;
            return this;
        }

        public SingleTypeObject Build()
        {
            ArgumentException.ThrowIfNullOrEmpty(_something);
            ArgumentException.ThrowIfNullOrEmpty(_else);
            ArgumentException.ThrowIfNullOrEmpty(_other);

            var o = new SingleTypeObject(_something, _else, _other);

            if (!string.IsNullOrWhiteSpace(_optional))
            {
                o.Optional = _optional;
            }

            return o;
        }

        public static implicit operator SingleTypeObject(SingleTypeObjectBuilder builder) => builder.Build();
    }
}
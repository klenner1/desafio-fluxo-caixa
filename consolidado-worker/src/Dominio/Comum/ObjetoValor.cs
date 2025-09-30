using System.Diagnostics.CodeAnalysis;

namespace Dominio.Comum;

[ExcludeFromCodeCoverage]
public abstract class ObjetoValor
{
    protected static bool EqualOperator(ObjetoValor left, ObjetoValor right)
    {
        if (left is null ^ right is null)
        {
            return false;
        }

        return left?.Equals(right!) != false;
    }

    protected static bool NotEqualOperator(ObjetoValor left, ObjetoValor right)
    {
        return !(EqualOperator(left, right));
    }

    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (ObjetoValor)obj;
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();

        foreach (var component in GetEqualityComponents())
        {
            hash.Add(component);
        }

        return hash.ToHashCode();
    }
}

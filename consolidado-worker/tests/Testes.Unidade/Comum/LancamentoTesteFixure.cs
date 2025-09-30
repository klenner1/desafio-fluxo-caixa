using Bogus;
using Dominio.Enum;

namespace Testes.Unidade.Comum;

public abstract class LancamentoTesteFixure
{
    public Faker Faker { get; set; } = new Faker("pt_BR");
    public Guid IdValido
    {
        get
        {
            return Faker.Random.Guid();
        }
    }

    public string DescricaoValida
    {
        get
        {
            var tamanho = Faker.Random.Int(3, 255);
            return Faker.Random.AlphaNumeric(tamanho);
        }
    }

    public decimal ValorValido
    {
        get
        {
            return Faker.Random.Decimal(max: 100_000_000);
        }
    }


    public ETipoLancamento TipoValido
    {
        get
        {
            return Faker.Random.Enum<ETipoLancamento>();
        }
    }

    public DateTimeOffset DataCriacaoValida
    {
        get
        {
            return Faker.Date.RecentOffset();
        }
    }
}

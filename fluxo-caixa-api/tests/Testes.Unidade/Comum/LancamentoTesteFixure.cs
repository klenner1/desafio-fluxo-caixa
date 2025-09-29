using Aplicacao.Repositorios;
using Bogus;
using Dominio.Entidades;
using Dominio.Enum;
using Moq;

namespace Testes.Unidade.Comum;

public abstract class LancamentoTesteFixure
{
    public Faker Faker { get; set; } = new Faker("pt_BR");
    public string DescricaoValida
    {
        get
        {
            var tamanho = Faker.Random.Int(3, 255);
            return Faker.Random.AlphaNumeric(tamanho);
        }
    }

    public string DescricaoInvalidaTamanhoMaiorQue255
    {
        get
        {
            var tamanho = Faker.Random.Int(min: 256, max: 1_000);
            return Faker.Random.AlphaNumeric(tamanho);
        }
    }

    public string DescricaoInvalidaTamanhoMenorQue3
    {
        get
        {
            var tamanho = Faker.Random.Int(0, 2);
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

    public decimal ValorInvalidoMenorQue0
    {
        get
        {
            return Faker.Random.Decimal(min: -100_000_000, max: 0);
        }
    }


    public ETipoLancamento TipoValido
    {
        get
        {
            return Faker.Random.Enum<ETipoLancamento>();
        }
    }

    public ETipoLancamento TipoInvalido
    {
        get
        {
            return (ETipoLancamento)Faker.Random
                    .Int(min: (int)ETipoLancamento.Saida);
        }
    }

    public Lancamento LancamentoValido
    {
        get
        {
            return Lancamento.Criar(DescricaoValida, ValorValido, TipoValido);
        }
    }

}

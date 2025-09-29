using FluentValidation;

namespace Dominio.Entidades.Validadores;

public class LancamentoValidador
    : AbstractValidator<Lancamento>
{
    public LancamentoValidador()
    {
        RuleFor(x => x.Descricao)
            .NotNull()
            .WithMessage($"{nameof(Lancamento)}.{nameof(Lancamento.Descricao)}: Não deve ser nulo");

        RuleFor(x => x.Descricao)
            .MaximumLength(255)
            .WithMessage($"{nameof(Lancamento)}.{nameof(Lancamento.Descricao)}: Não deve ter tamanho maior que 255 caracteres");

        RuleFor(x => x.Descricao)
          .MinimumLength(3)
          .WithMessage($"{nameof(Lancamento)}.{nameof(Lancamento.Descricao)}: Não deve ter tamanho menor que 3 caracteres");

        RuleFor(x => x.Valor)
          .GreaterThanOrEqualTo(0)
          .WithMessage($"{nameof(Lancamento)}.{nameof(Lancamento.Valor)}: Deve ser maior ou igual a 0");

        RuleFor(x => x.Tipo)
          .IsInEnum()
          .WithMessage($"{nameof(Lancamento)}.{nameof(Lancamento.Tipo)}: Possui um intervalo de valores que não inclui o valor informado");
    }
}


namespace Dominio.Exceptions;

public class ValidacaoEntidadeException(string? messagem)
    : Exception(messagem)
{
}

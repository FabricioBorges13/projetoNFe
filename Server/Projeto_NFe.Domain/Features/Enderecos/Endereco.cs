using Projeto_NFe.Base.Domain;
using System;

namespace Projeto_NFe.Domain.Features.Enderecos
{
    public class Endereco
    {
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }

        public virtual void Validar()
        {
            ValidarLogradouro();
            ValidarBairro();
            ValidarMunicipio();
            ValidarEstado();
            ValidarPais();
            ValidarNumero();
        }
        private void ValidarLogradouro()
        {
            if (string.IsNullOrEmpty(Logradouro))
                throw new ExceptionLogradouroInvalidoVazioOuNulo();
        }
        private void ValidarBairro()
        {
            if (string.IsNullOrEmpty(Bairro))
                throw new ExceptionBairroInvalidoVazioOuNulo();
        }
        private void ValidarMunicipio()
        {
            if (string.IsNullOrEmpty(Municipio))
                throw new ExceptionMunicipioInvalidoVazioOuNulo();
        }
        private void ValidarEstado()
        {
            if (string.IsNullOrEmpty(Estado))
                throw new ExceptionEstadoInvalidoVazioOuNulo();
        }
        private void ValidarPais()
        {
            if (string.IsNullOrEmpty(Pais))
                throw new ExceptionPaisInvalidoVazioOuNulo();
        }
        private void ValidarNumero()
        {
            if (Numero < 1)
                throw new ExceptionNumeroInvalidoMenorDoQueUm();
        }
    }
}

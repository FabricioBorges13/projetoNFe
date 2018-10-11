using System;
using System.Linq;

namespace Projeto_NFe.Domain.Base.NumeroDeDocumentos
{
    public class NumeroDeDocumento
    {
        public string NumeroDoDocumento { get; set; }
        public NumeroDeDocumento() { }

        public NumeroDeDocumento(string numeroDoDocumento)
        {
            ValidarDocumento(numeroDoDocumento);
        }

        private void ValidarDocumento(string numeroDoDocumento)
        {
            if (string.IsNullOrEmpty(numeroDoDocumento))
                throw new NumeroDocumentoVazioOuNuloException();


            var numeroValidar = RemoverCaracteresEspeciais(numeroDoDocumento);
            VerificaNumerosRepetidos(numeroValidar);

            if (numeroDoDocumento.Count() == 18)
            {
                ValidarFormatoCNPJ(numeroDoDocumento, numeroValidar);
            }
            else
            {
                if (numeroDoDocumento.Count() == 14)
                {
                    ValidarFormatoCPF(numeroDoDocumento, numeroValidar);
                }
                else
                {
                    throw new NumeroDoDocumentoInvalido();
                }
            }
        }

        private void VerificaNumerosRepetidos(string numeroDoDocumento)
        {
            for (int i = 0; i < 9; i++)
            {
                var stringAux = numeroDoDocumento.Replace(char.Parse(i + ""), ' ');
                if (string.IsNullOrWhiteSpace(stringAux))
                    throw new NumeroDoDocumentoInvalido();
            }
        }

        private void ValidarFormatoCPF(string numeroDoDocumento, string numeroValidar)
        {
            if (numeroValidar.All(Char.IsDigit))
            {
                if (ValidaCPF(numeroValidar))
                {
                    this.NumeroDoDocumento = numeroDoDocumento;
                }
                else
                {
                    throw new CPFInvalidoValorInvalidoException();
                }
            }
            else
            {
                throw new CPFInvalidoPossuiLetrasOuForaDePadraoException();
            }
        }

        private void ValidarFormatoCNPJ(string numeroDoDocumento, string numeroValidar)
        {
            if (numeroValidar.All(Char.IsDigit))
            {
                if (ValidaCNPJ(numeroValidar))
                {
                    this.NumeroDoDocumento = numeroDoDocumento;
                }
                else
                {
                    throw new CNPJInvalidoValorInvalidoException();
                }
            }
            else
            {
                throw new CNPJInvalidoPossuiLetrasOuForaDePadraoException();
            }
        }

        public static implicit operator NumeroDeDocumento(string v)
        {
            return new NumeroDeDocumento(v);
        }

        public static explicit operator string(NumeroDeDocumento v)
        {
            return v.NumeroDoDocumento;
        }

     


        private Boolean ValidaCNPJ(String cnpj)
        {
            Int32[] digitos, soma, resultado;
            Int32 nrDig;
            String ftmt;
            Boolean[] cnpjOk;

            ftmt = "6543298765432";
            soma = new Int32[2];
            resultado = new Int32[2];
            cnpjOk = new Boolean[2];
            for (int i = 0; i < soma.Length; i++)
            {
                soma[i] = 0;
                resultado[i] = 0;
                cnpjOk[i] = false;
            }
            digitos = new Int32[14];

            for (nrDig = 0; nrDig < 14; nrDig++)
            {
                digitos[nrDig] = int.Parse(cnpj.Substring(nrDig, 1));

                if (nrDig <= 11)
                    soma[0] += (digitos[nrDig] * int.Parse(ftmt.Substring(nrDig + 1, 1)));

                if (nrDig <= 12)
                    soma[1] += (digitos[nrDig] * int.Parse(ftmt.Substring(nrDig, 1)));
            }
            for (nrDig = 0; nrDig < 2; nrDig++)
            {
                resultado[nrDig] = (soma[nrDig] % 11);
                if ((resultado[nrDig] == 0) || (resultado[nrDig] == 1))
                    cnpjOk[nrDig] = (digitos[12 + nrDig] == 0);
                else
                    cnpjOk[nrDig] = (digitos[12 + nrDig] == (
                    11 - resultado[nrDig]));
            }
            return (cnpjOk[0] && cnpjOk[1]);
        }

        private string RemoverCaracteresEspeciais(string cnpj)
        {
            if (cnpj.Count() > 14)
                cnpj = cnpj.Replace("/", "");

            cnpj = cnpj.Replace(".", "");
            cnpj = cnpj.Replace("-", "");
            return cnpj;
        }

        private bool ValidaCPF(String cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf = cpf.Substring(0, 9);
            string digito;
            int soma = 0;
            int resto;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }
        public string DescobrirTipoDeDocumento()
        {
            return NumeroDoDocumento.Length == 14 ? "CPF" : "CNPJ";
        }
    }
}

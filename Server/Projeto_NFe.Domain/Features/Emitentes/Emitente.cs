using Projeto_NFe.Base.Domain;
using Projeto_NFe.Domain.Base.Identificacao;
using System;

namespace Projeto_NFe.Domain.Features.Emitentes
{
    public class Emitente : Identificacao
    {
        public string inscricaoMunicipal { get; set; }

        //public override void Validar()
        //{
        //    base.Validar();
        //    ValidarMunicipal();
        //}
        //public void ValidarMunicipal() {
        //    if (string.IsNullOrEmpty(inscricaoMunicipal))
        //        throw new InscricaoMunicipalEstaVaziaException();
        //}
    }
}

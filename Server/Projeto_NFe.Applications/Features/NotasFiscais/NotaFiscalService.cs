using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Projeto_NFe.Applications.Features.NotasFiscais.Commands;
using Projeto_NFe.Domain.Features.Destinatarios;
using Projeto_NFe.Domain.Features.Emitentes;
using Projeto_NFe.Domain.Features.NotasFiscais;
using Projeto_NFe.Domain.Features.Produtos;
using Projeto_NFe.Domain.Features.Transportadores;

namespace Projeto_NFe.Applications.Features.NotasFiscais
{
    public class NotaFiscalService : INotaFiscalService
    {
        INotaFiscalRepository _notaFiscalRepository;
        IProdutoRepository _produtoRepository;
        ITransportadorRepository _transportadorRepository;
        IEmitenteRepository _emitenteRepository;
        IDestinatarioRepository _destinatarioRepository;



        public NotaFiscalService(
            INotaFiscalRepository notaFiscalRepository, 
            IProdutoRepository produtoRepository, 
            ITransportadorRepository transportadorRepository,
            IEmitenteRepository emitenteRepository, 
            IDestinatarioRepository destinatarioRepository)
        {
            _notaFiscalRepository = notaFiscalRepository;
            _produtoRepository = produtoRepository;
            _transportadorRepository = transportadorRepository;
            _emitenteRepository = emitenteRepository;
            _destinatarioRepository = destinatarioRepository;
        }

        public long Add(NotaFiscalAddCommand notaFiscal)
        {
            var notaFiscalAdd = Mapper.Map<NotaFiscalAddCommand, NotaFiscal>(notaFiscal);

            notaFiscalAdd.Destinatario = _destinatarioRepository.GetById(notaFiscal.DestinatarioId);
            notaFiscalAdd.Transportador = _transportadorRepository.GetById(notaFiscal.TransportadorId);
            notaFiscalAdd.Emitente = _emitenteRepository.GetById(notaFiscal.EmitenteId);
            notaFiscalAdd.DataEmissao = notaFiscalAdd.DataEntrada;

            return _notaFiscalRepository.Add(notaFiscalAdd).Id;
        }

        public bool Delete(NotaFiscalDeleteCommand notaFiscais)
        {
            notaFiscais.Validar();
            var isRemovedAll = true;
            foreach (var notaFiscalId in notaFiscais.NotaFiscalIds)
            {
                var isRemoved = _notaFiscalRepository.Delete(notaFiscalId);
                // Aqui poderia dar o tramento adequado, armazenado quais ids foram removidos
                // e quais não forma removidos (e buscar o porquê). 
                // Como é somente um exemplo, vamos somente retornar false (que não foi totalmente concluído)
                isRemovedAll = isRemoved ? isRemovedAll : false;
            }
            return isRemovedAll;
        }

        public IQueryable<NotaFiscal> GetAll()
        {
            var listNotaFiscais = _notaFiscalRepository.GetAll();
            foreach (var notaFiscalGet in listNotaFiscais)
            {
                notaFiscalGet.Destinatario = _destinatarioRepository.GetById((long)notaFiscalGet.DestinatarioId);
                notaFiscalGet.Transportador = _transportadorRepository.GetById((long)notaFiscalGet.TransportadorId);
                notaFiscalGet.Emitente = _emitenteRepository.GetById((long)notaFiscalGet.EmitenteId);
            }

            return listNotaFiscais;
        }

        public NotaFiscal GetById(long id)
        {

            var notaFiscalGet = _notaFiscalRepository.GetById(id);
            notaFiscalGet.Destinatario = _destinatarioRepository.GetById((long)notaFiscalGet.DestinatarioId);
            notaFiscalGet.Transportador= _transportadorRepository.GetById((long)notaFiscalGet.TransportadorId);
            notaFiscalGet.Emitente = _emitenteRepository.GetById((long)notaFiscalGet.EmitenteId);
            return notaFiscalGet;
        }

        public IQueryable<Produto> GetListaDeProdutoDaNotaFiscal(long id)
        {
            var notaFiscalGet = _notaFiscalRepository.GetById(id);
            if (notaFiscalGet == null)
            {
                throw new Exception();
            }
            return notaFiscalGet.Produtos.AsQueryable();
        }

        public bool UpdateProdutos(NotaFiscalUpdateProdutosCommand notaFiscalComProdutos)
        {
            List<Produto> produtosList = new List<Produto>();

            foreach (var idProduto in notaFiscalComProdutos.ProdutosId)
            {
                produtosList.Add(_produtoRepository.GetById(idProduto));
            }

            NotaFiscal notaFiscal = this.GetById(notaFiscalComProdutos.NotaFiscalId);
            notaFiscal.Produtos = produtosList;

            return _notaFiscalRepository.Update(notaFiscal);
        }

        public bool Update(NotaFiscalUpdateCommand notaFiscal)
        {
            var _transportador = _notaFiscalRepository.GetById(notaFiscal.Id);
            notaFiscal.Validar();
            
            var notaFiscalUpdate = Mapper.Map(notaFiscal, _transportador);

            notaFiscalUpdate.Destinatario = _destinatarioRepository.GetById(notaFiscal.DestinatarioId);
            notaFiscalUpdate.Transportador = _transportadorRepository.GetById(notaFiscal.TransportadorId);
            notaFiscalUpdate.Emitente = _emitenteRepository.GetById(notaFiscal.EmitenteId);
            notaFiscalUpdate.DataEmissao = notaFiscalUpdate.DataEntrada;


            return _notaFiscalRepository.Update(notaFiscalUpdate);
        }
    }
}

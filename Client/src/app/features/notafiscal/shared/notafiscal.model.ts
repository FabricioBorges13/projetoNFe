import { Produto } from '../../produto/shared/produto.model';
import { Imposto } from '../../imposto/imposto.model';
import { Emitente } from '../../emitentes/shared/emitente.model';
import { Transportador } from '../../transportador/shared/transportador.model';
import { Destinatario } from '../../destinatario/shared/destinatario.model';

export class NotaFiscal {
    public id: number;
    public naturezaOperacao: string;

    public dataEntrada: Date;

    public produtos: Produto;

    public impostoDaNota: Imposto;

    public emitente: number;

    public transportador: number;

    public destinatario: number;

    public notaEmitida: boolean;

    public dataEmissao: Date;

    public chaveAcesso: string;

    public valorDoFrete: number;

    public valorTotalDosProdutos: number;

    public valorTotalDaNota: number;
}
export class NotaFiscalDataCommand {
    public id?: number;
    public naturezaOperacao: string;
    public dataEntrada: Date;
    public valorIpi: number;
    public valorIcms: number;
    public emitente: number;
    public emitenteNome: string;
    public transportador: number;
    public transportadorNome: string;
    public destinatario: number;
    public destinatarioNome: string;
    public valorDoFrete: number;

    constructor(notafiscal: any) {
        this.id = notafiscal.id;
        this.naturezaOperacao = notafiscal.naturezaOperacao;
        this.dataEntrada = notafiscal.dataEntrada;
        this.valorIpi = notafiscal.valorIpi;
        this.valorIcms = notafiscal.valorIcms;
        this.emitente = notafiscal.emitente;
        this.emitenteNome = notafiscal.emitenteNome;
        this.transportador = notafiscal.transportador;
        this.transportadorNome = notafiscal.transportadorNome;
        this.destinatario = notafiscal.destinatario;
        this.destinatarioNome = notafiscal.destinatarioNome;
        this.valorDoFrete = notafiscal.valorDoFrete;
    }
}

export class NotaFiscalDeleteCommand {
    public notafiscalIds: number[] = [];

    constructor(notafiscal: NotaFiscal[]) {
        this.notafiscalIds = notafiscal.map((n: NotaFiscal) => n.id);
    }
}

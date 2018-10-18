import { Produto } from '../../produto/shared/produto.model';
import { Imposto } from '../../imposto/imposto.model';
import { Emitente } from '../../emitentes/shared/emitente.model';
import { Transportador } from '../../transportador/shared/transportador.model';
import { Destinatario } from '../../destinatario/shared/destinatario.model';

export class NotaFiscal {
    public id?: number;
    public naturezaOperacao: string;

    public dataEntrada: Date;

    public emitente: number;

    public transportador: number;

    public destinatario: number;
    public produto: Produto[];

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
    public emitenteId: number;
    public emitenteNome: string;
    public transportadorId: number;
    public transportadorNome: string;
    public destinatarioId: number;
    public destinatarioNome: string;
    public produtoId: number;
    public produtoDescricao: string;
    public valorDoFrete: number;

    constructor(notafiscal: any) {
        this.id = notafiscal.id;
        this.naturezaOperacao = notafiscal.naturezaOperacao;
        this.dataEntrada = notafiscal.dataEntrada;
        this.valorIpi = notafiscal.valorIpi;
        this.valorIcms = notafiscal.valorIcms;

        this.emitenteId = notafiscal.emitenteId;
        this.emitenteNome = notafiscal.emitenteNome;

        this.transportadorId = notafiscal.transportadorId;
        this.transportadorNome = notafiscal.transportadorNome;

        this.destinatarioId = notafiscal.destinatarioId;
        this.destinatarioNome = notafiscal.destinatarioNome;

        this.produtoId = notafiscal.produtoId;
        this.produtoDescricao = notafiscal.produtoDescricao;

        this.valorDoFrete = notafiscal.valorDoFrete;
    }
}

export class NotaFiscalPatchCommand {
    public notafiscalId: number;
    public produtoId: number[];
    public produtoDescricao: string;
    public valorDoFrete: number;

    constructor(notafiscal: any) {
        this.notafiscalId = notafiscal.notafiscalId;
        this.produtoId = notafiscal.produtoId;
        this.produtoDescricao = notafiscal.produtoDescricao;
    }
}

export class NotaFiscalDeleteCommand {
    public notafiscalIds: number[] = [];

    constructor(notafiscal: NotaFiscal[]) {
        this.notafiscalIds = notafiscal.map((n: NotaFiscal) => n.id);
    }
}
